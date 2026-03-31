using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentApplication.Server.Models;


namespace StudentApplication.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        public AccountController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if(User.Identity == null || !User.Identity.IsAuthenticated)
            {
                return Ok("You are not authenticated");
            }


            return Ok("You are Authenticated!");
        }

        [Authorize]
        [HttpGet("Profile")]
        public async Task<IActionResult> Profile()
        { 
            var user = await userManager.GetUserAsync(User);


            if (user == null)
            {
                return BadRequest("User not found");
                
            }

            var userProfile = new UserProfile
            {
                Id = int.Parse(user.Id),
                Username = user.UserName ?? "",
                Email = user.Email ?? "",
                Phone = user.PhoneNumber ?? ""
            };
            return Ok();
        }
    }
}
