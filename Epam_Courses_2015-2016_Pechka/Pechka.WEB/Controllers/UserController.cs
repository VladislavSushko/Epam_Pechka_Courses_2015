using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pechka.DLL.Abstract;
using Pechka.DLL.ModelsForWEBUI.DTO.User;

namespace Pechka.WEB.Controllers
{
    public class UserController : Controller
    {
        private readonly IMenuService menuService;
        private readonly IUserService userService;
        private readonly IReviewService reviewService;

        public UserController(IMenuService menu,IUserService user,IReviewService review)
        {
            menuService = menu;
            userService = user;
            reviewService = review;
        }
        // GET: User
        public ActionResult Index()
        {
            var menu=menuService.GetMenuForUser(User.Identity.Name);
            return View(menu);
        }

        public ActionResult OrderNewOrEditing(MenuForUserDTO model)
        {
            var user = userService.GetUser(User.Identity.Name);
            if (model.NewReview != null)
            {
                reviewService.AddNewReview(user.Id, model.MenuId, model.NewReview);
            }

            return RedirectToAction("Index");
        }
    }
}