using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Pechka.DLL.Abstract;
using Pechka.DLL.ModelsForWEBUI;


namespace Pechka.WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository userService;
        private readonly IEmailService emailService;
        public AccountController(IUserRepository repo,IEmailService email)
        {
            userService = repo;
            emailService = email;

        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LogViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (userService.ValidateUser(model.Email, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Request");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный пароль, логин, или аакаунт неактивен");
                }
            }
            return View(model);
        }
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Registration()
        {
            return View();
        }

        public ActionResult ConfirmEmail(string Token, string Email)
        {
            var confirmed = userService.ConfirmEmail(Token,Email);
            return RedirectToAction("Login");


        }

        [HttpPost]
        public ActionResult Registration(RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
               bool result= userService.SaveNewUser(model);
                if (result)
                {
                    
                    var user = userService.GetUser(model.Email);
                    if (emailService.ConfirmEmailSend(user,
                        string.Format(
                            "Для завершения регистрации перейдите по ссылке:" +
                            "<a href=\"{0}\" title=\"Подтвердить регистрацию\">{0}</a>",
                            Url.Action("ConfirmEmail", "Account", new {Token = user.Id, Email = user.Email},
                                Request.Url.Scheme))))
                    {
                        ModelState.AddModelError("",
                            "На указанный email выслано письмо, действуйте согласно инструкциям для активации аккаунта.");
                        return View(model);
                    }
                    else
                    {
                        ModelState.AddModelError("","Ошибка отправки email, свяжитесь с админом");
                        return View(model);
                    }

                }
                ModelState.AddModelError("","Данный email уже занят!");
               
            }
            return View(model);
        }
    }

}