using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Pechka.DLL.Abstract;
using Pechka.DLL.ModelsForWEBUI;
using Pechka.DLL.ModelsForWEBUI.DTO;


namespace Pechka.WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        private readonly IEmailService emailService;
        private readonly IScoreService scoreService;
        public AccountController(IUserService repo,IEmailService email,IScoreService score)
        {
            userService = repo;
            emailService = email;
            scoreService = score;

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
            var user = userService.GetUser(Email);
            scoreService.AddNewScore(user.Id);
            return RedirectToAction("Login");


        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Registration(RegistrationModel model, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    model.ImgType = image.ContentType;
                    model.ImgData = new byte[image.ContentLength];
                    image.InputStream.Read(model.ImgData, 0, image.ContentLength);
                }
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
        [Authorize]
        public FileContentResult GetImg(int id =0)
        {
            var castedUser=new User();
            if (id != 0)
            {
                castedUser = userService.GetUserById(id);
            }
            else
            {
                castedUser = userService.GetUser(User.Identity.Name);
            }
            if (castedUser != null && castedUser.ImgData != null)
            {
                return File(castedUser.ImgData, castedUser.ImgType);
            }
            else
            {
                return null;
            }
        }
        [Authorize]
        [HttpGet]
        public ActionResult Setting()
        {
            var user = userService.GetUserForsetting(User.Identity.Name);
            return View(user);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Setting(UserForSettingDTO model, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    model.ImgType = image.ContentType;
                    model.ImgData = new byte[image.ContentLength];
                    image.InputStream.Read(model.ImgData, 0, image.ContentLength);
                }
                if (userService.SaveNewSettings(model, User.Identity.Name))
                {
                    ModelState.AddModelError("","Новые настройки были сохранены успешно.");
                    return View(userService.GetUserForsetting(User.Identity.Name));
                }
                
            }
            ModelState.AddModelError("", "Новые настройки не были сохранены по одной из следующих причин:");
            ModelState.AddModelError("", "Данный Email уже занят");
            ModelState.AddModelError("", "Введен неправильный пароль");
            return View(userService.GetUserForsetting(User.Identity.Name));
        }
        [Authorize]
        [HttpGet]
        public ActionResult HistoryOfBalanse()
        {
           var user= userService.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
            var model = scoreService.HistoryOfScoreByUserId(user.Id);
            return View(model);
        }
    }

}