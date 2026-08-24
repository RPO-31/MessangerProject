using Microsoft.AspNetCore.Mvc;

namespace Messanger.Frontend.Controllers.Users
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
