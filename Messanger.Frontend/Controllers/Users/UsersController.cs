using Messanger.Frontend.Enums;
using Messanger.Frontend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Messanger.Frontend.Controllers.Users
{
    public class UsersController : Controller
    {
        public IActionResult Index(SearchUserRequest SearchUserRequest)
        {
            if (SearchUserRequest.ChatType == EChatType.None)
            {
                //SearchUserRequest = new();//
                
                //re
            }
            SearchUserRequest.ChatType = EChatType.Private;

                //return RedirectToAction("Index", "Chats");
            return View(SearchUserRequest);
        }
        /*public IActionResult Index(SearchUserRequest SearchUserRequest)
        {
            return View( );
        }*/
    }
}
