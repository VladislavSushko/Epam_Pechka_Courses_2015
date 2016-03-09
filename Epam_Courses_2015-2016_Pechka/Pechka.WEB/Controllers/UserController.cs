using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pechka.DLL.Abstract;

namespace Pechka.WEB.Controllers
{
    public class UserController : Controller
    {
        private readonly IMenuService menuService;

        public UserController(IMenuService menu)
        {
            menuService = menu;
        }
        // GET: User
        public ActionResult Index()
        {
            var menu=menuService.GetMenuForUser(User.Identity.Name);
            return View();
        }
    }
}