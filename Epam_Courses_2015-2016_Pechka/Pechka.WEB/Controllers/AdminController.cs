using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pechka.DLL.Abstract;
using Pechka.DLL.ModelsForWEBUI;
using Pechka.DLL.ModelsForWEBUI.DTO.Admin;

namespace Pechka.WEB.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService adminService;

        public AdminController(IAdminService service)
        {
            adminService = service;
        }
        public ActionResult Index()
        {
            var users=new UsersForAdmin();
            users.Users=adminService.GetUsersToManageByAdmin(User.Identity.Name);
            return View(users);
        }

        public ActionResult UpdateUser(User model)
        {
            return RedirectToAction("Index");
        }
    }
}