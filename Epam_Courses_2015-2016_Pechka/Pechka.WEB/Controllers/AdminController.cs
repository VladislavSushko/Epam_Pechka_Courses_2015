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
        private readonly IScoreService scoreService;
        private readonly IUserService userService;
        private readonly IMenuService menuService;
        private readonly IOrderService orderService;

        public AdminController(IAdminService service,IScoreService score,IUserService user,IMenuService menu,IOrderService order)
        {
            adminService = service;
            scoreService = score;
            userService = user;
            menuService = menu;
            orderService = order;
        }
        public ActionResult Index()
        {
            var users=new UsersForAdmin();
            users.Users=adminService.GetUsersToManageByAdmin(User.Identity.Name);
            return View(users);
        }

        public ActionResult UpdateUser(UserDto model)
        {
            var user = adminService.GetUsersToManageByAdmin(User.Identity.Name);
            var item = user.FirstOrDefault(u => u.Id == model.Id);
            if (item.Money != model.Money)
            {
                var result = scoreService.ChangeScoreByUserId(model.Id, model.Money);
                if (result != null)
                {
                    userService.SetUserLastScore(model.Id, result.Value);
                }
            }
            if (item.InBlackList != model.InBlackList||item.RoleId!=model.RoleId)
            {
                adminService.AddUserToBlackList(model.Id,model.InBlackList,model.RoleId>0&&model.RoleId<3?model.RoleId:1);
            }
            
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult AddNewItemIntoMenu()
        {
            var temp = menuService.GetAllMenuForAdmin();
            return View(menuService.GetAllMenuForAdmin());
        }

        public ActionResult AddOrUpdateMenu(MenuDTO model)
        {
            var menu = menuService.GetMenu(model.Id);
            if (menu != null && menu.IsCompleted != 1)
            {
                var result = menuService.UpdateMenu(model);
                ModelState.AddModelError("","МОдель была обновлена успешно");
            }
            else if (menu == null)
            {
                if (menuService.AddNewMenu(model))
                    ModelState.AddModelError("", "Добавление прошло успешно");
            }
            else
            {
                ModelState.AddModelError("","При выполнении действия возникли ошибки");
            }
            return RedirectToAction("AddNewItemIntoMenu");
        }
        [HttpGet]
        public ActionResult OrderByAllUsers()
        {
            var model=new OrdersDTO();
            return View(model);
        }
        [HttpPost]
        public ActionResult GetOrders(OrdersDTO model)
        {
            model.Date=DateTime.Parse(model.StartDate);
            var result = orderService.GetOrdersForAdmin(model.Date);
            return View("OrderByAllUsers", result);

        }
    }
}