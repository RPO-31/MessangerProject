using Microsoft.AspNetCore.Mvc;

namespace Messanger.Frontend.Controllers.Chats
{
    public class ChatsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
