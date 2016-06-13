using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using ToDoList.Entity;
using ToDoList.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Host.SystemWeb;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ToDoList.Web.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(User model)
        {
            model.Id = 0;
            UserRepository userRepo = new UserRepository();
            string Error = string.Empty;
            int UserId = userRepo.Save(model, out Error);

            if (UserId > 0)
            {
                    User ActiveUser = userRepo.Get(UserId);

                    if (ActiveUser != null)
                    {
                        return AuthUser(ActiveUser);
                    }
            }

            return View();
        }

        public ActionResult Login()
        {
            ViewBag.CustomValidationSummary = string.Empty;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User model)
        {
            UserRepository userRepo = new UserRepository();
            User ActiveUser = userRepo.Login(model.Email, model.Password);

            if (ActiveUser != null)
            {
                return AuthUser(ActiveUser);
            }else
            {
                ViewBag.CustomValidationSummary = "Email or password wrong!";
            }

            return View();
        }

        private ActionResult AuthUser(User ActiveUser)
        {
            IAuthenticationManager authManager = HttpContext.GetOwinContext().Authentication;
       
            ClaimsIdentity identity = new ClaimsIdentity(
                new[] {
                          new Claim(ClaimTypes.NameIdentifier, ActiveUser.Id.ToString()),
                          new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),
                          new Claim(ClaimTypes.Name,ActiveUser.Name),
                          new Claim(ClaimTypes.Email,ActiveUser.Email),
                }, 
                DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationProperties authProps = new AuthenticationProperties();
            authProps.IsPersistent = true;
            authManager.SignIn(authProps, identity);


            return RedirectToAction("Index", "Todo");
        }

        [Authorize]
        public ActionResult Logoff()
        {
            IAuthenticationManager authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("Login");
        }
    }
}