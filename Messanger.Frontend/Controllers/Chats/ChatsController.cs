using Messanger.Frontend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Messanger.Frontend.Controllers.Chats
{
    public class ChatsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int Id)
        {

            return View(Id);
        }
        public IActionResult CreatePrivate(SearchUserRequest SearchRequest)
        {
            SearchRequest.ChatType = Enums.EChatType.Private;
            return View(SearchRequest);
        }
        public IActionResult CreateGroup(SearchUserRequest SearchRequest)
        {
            SearchRequest.ChatType = Enums.EChatType.Group;
            return View(SearchRequest);
        }
    }
}
