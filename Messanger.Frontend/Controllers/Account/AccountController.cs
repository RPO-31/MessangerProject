using Messanger.Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Messanger.Frontend.Controllers.Account
{
    public class AccountController : Controller
    {

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegRequest regrequest)
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LogRequest logrequest)
        {
            return View();
        } 
        public IActionResult LogOut()
        {
            return View();
        }
    }
}
// редирект или возрат на страницу 