using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Microsoft.AspNetCore.Components.Authorization;
using StudentApplication;
using StudentApplication.Services;
using Blazored.LocalStorage;



var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["WebApiAddress"]!) });

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

builder.Services.AddBlazoredLocalStorage();



//builder.Services.AddDbContext<StudentApplicationDbContext>(options => options.UseInMemoryDatabase("StudentApplicationDb"));

// Register ViewModel
// builder.Services.AddScoped<CourseViewModel>();



//builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ServerAPI"));

//builder.Services.AddApiAuthorization();

await builder.Build().RunAsync();
