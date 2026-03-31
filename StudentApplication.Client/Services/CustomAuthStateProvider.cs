using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json.Nodes;
using Blazored.LocalStorage;
using StudentDomain.Dto;


namespace StudentApplication.Client.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient httpClient;
        private readonly ISyncLocalStorageService localStorage;

        public CustomAuthStateProvider(HttpClient httpClient, ISyncLocalStorageService localstorage)
        {
            this.httpClient = httpClient;
            this.localStorage = localstorage;

            var accessToken = localstorage.GetItem<string>("accessToken");

            if (accessToken != null)
            {
                this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }
        }



        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            /*var claims = new List<Claim> { new Claim(ClaimTypes.Name, "John") };
            var identity = new ClaimsIdentity(claims, "Any");
            var user = new ClaimsPrincipal(identity);
            
             return Task.FromResult(new AuthenticationState(user));
             */

            var user = new ClaimsPrincipal(new ClaimsIdentity());



            try
            {
                var response = await httpClient.GetAsync("manage/info");

                if (response.IsSuccessStatusCode)
                {
                    var strResponse = await response.Content.ReadAsStringAsync();

                    var jsonResponse = JsonNode.Parse(strResponse);
                    var email = jsonResponse!["email"]?.ToString();

                    var claims = new List<Claim>
                    {
                        new (ClaimTypes.Name, email ?? "Unknown")
                    };

                    var identity = new ClaimsIdentity(claims, "Token");

                    user = new ClaimsPrincipal(identity);
                    return new AuthenticationState(user);

                }

            }
            catch (Exception ex)
            {

            }

            return new AuthenticationState(user);
        }

        public async Task<FormResult> LoginAsync(string email, string password)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/auth/login", new { email, password });

                if (response.IsSuccessStatusCode)
                {
                    var strResponse = await response.Content.ReadAsStringAsync();

                    var jsonResponse = JsonNode.Parse(strResponse);
                    var accessToken = jsonResponse?["accessToken"]?.ToString();
                    var refreshToken = jsonResponse?["refreshToken"]?.ToString();

                    localStorage.SetItem("accessToken", accessToken);
                    localStorage.SetItem("refreshToken", refreshToken);

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());


                    return new FormResult { Succeeded = true };
                }
                else
                {
                    return new FormResult { Succeeded = false, Errors = ["Invalid email or password"] };
                }
            }
            catch { }
            {
                return new FormResult { Succeeded = false, Errors = ["Connection Error"] };
            }
        }

        public async Task<FormResult> Login(string email, string password)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/auth/login", new { email, password });
            }
            catch { }
            {
                return new FormResult { Succeeded = false, Errors = new[] { "An error occurred while trying to log in" } };
            }


        }

        public class FormResult
        {
            public bool Succeeded { get; set; }
            public string[] Errors { get; set; } = [];
        }

        public void Logout()
        {
            //deletes tokens from local storage
            localStorage.RemoveItem("authToken");
            localStorage.RemoveItem("refreshToken");
            httpClient.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

        }

        public async Task<FormResult> RegisterAsync(RegisterDto registerDto)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("register", registerDto);

                if (response.IsSuccessStatusCode)
                {
                    var loginResponse = await LoginAsync(registerDto.Email, registerDto.Password);
                    return loginResponse;
                }
                else
                {
                    var strResponse = await response.Content.ReadAsStringAsync();
                    var jsonResponse = JsonNode.Parse(strResponse);

                    //var errors = jsonResponse?["errors"]?.AsArray().Select(e => e.ToString()).ToArray() ?? new string[] { "Registration failed" };
                    var errorsObject = jsonResponse?["errors"]!.AsObject();
                    var errorsList = new List<string>();

                    foreach (var error in errorsObject)
                    {
                        errorsList.AddRange(error.Value![0]!.ToString());
                    }

                    var formResult = new FormResult
                    {
                        Succeeded = false,
                        Errors = errorsList.ToArray()
                    };
                    return formResult;
                }
            }
            catch
            {
                return new FormResult { Succeeded = false, Errors = new[] { "An error occurred while trying to register" } };
            }
        }
    }
}
