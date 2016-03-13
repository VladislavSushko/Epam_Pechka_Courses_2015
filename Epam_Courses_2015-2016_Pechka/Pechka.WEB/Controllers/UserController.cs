using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pechka.DLL.Abstract;
using Pechka.DLL.ModelsForWEBUI.DTO.User;

namespace Pechka.WEB.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class UserController : Controller
    {
        private readonly IMenuService menuService;
        private readonly IUserService userService;
        private readonly IReviewService reviewService;
        private readonly IOrderService orderService;
        private readonly IScoreService scoreService;

        public UserController(IMenuService menu,IUserService user,IReviewService review,IOrderService order,IScoreService score)
        {
            menuService = menu;
            userService = user;
            reviewService = review;
            orderService = order;
            scoreService = score;
        }
        public ActionResult Index()
        {
            var menu=menuService.GetMenuForUser(User.Identity.Name);
            return View(menu);
        }

        public ActionResult OrderNewOrEditing(MenuForUserDTO model)
        {
            var user = userService.GetUser(User.Identity.Name);
            var menu = menuService.GetMenu(model.MenuId);
            if (model.NewReview != null)
            {
                reviewService.AddNewReview(user.Id, model.MenuId, model.NewReview);
            }
            decimal money = user.LastScore.Money;
            var order = orderService.GetUserOrderByDateAndUserId(menu.Date, user.Id);
            if (order == null && (model.OrderWithFirst != 0 && user.LastScore.Money-model.WithFirst>0 || model.OrderWithoutFirst != 0 && user.LastScore.Money-model.WithoutFirst>0))
            {
                var change = model.OrderWithFirst != 0 ? menu.PriceWithFirst : menu.PriceWithoutFirst;
                if (orderService.AddNewOrder(model, user.Id))
                {
                    var id = scoreService.ChangeScoreByUserId(user.Id, user.LastScore.Money - change);
                    userService.SetUserLastScore(user.Id, id.Value);
                }

            }
            
            else if (order != null && order.Menu.Date > DateTime.Now.AddHours(9) && (order.WithoutFirst!=(model.OrderWithoutFirst!=0) || order.WithFirst!=(model.OrderWithFirst!=0)))
            {
                if (order.WithoutFirst)
                    money += menu.PriceWithoutFirst;
                if (order.WithFirst)
                    money += menu.PriceWithFirst;
                    if (model.OrderWithFirst != 0)
                    {
                        money -= menu.PriceWithFirst;
                    }
                    if (model.OrderWithoutFirst != 0)
                    {
                        money -= menu.PriceWithoutFirst;
                    }
                if (money > 0)
                {
                    if (orderService.UpdateUserOrder(model, user.Id))
                    {
                       var newScoreId= scoreService.ChangeScoreByUserId(user.Id, money);
                        userService.SetUserLastScore(user.Id, newScoreId.Value);
                    }
                }

            }
            return RedirectToAction("Index");
        }
    }
}