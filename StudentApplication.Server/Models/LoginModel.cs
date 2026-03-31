namespace StudentApplication.Server.Models
{
    public class LoginModel
    {
   
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        // Optional: if you want to support "Remember Me" functionality
        public bool RememberMe { get; set; } = false;
    }
}
