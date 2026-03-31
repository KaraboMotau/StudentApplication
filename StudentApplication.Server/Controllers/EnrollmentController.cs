using Microsoft.AspNetCore.Mvc;

namespace StudentApplication.Server.Controllers
{
    public class EnrollmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
