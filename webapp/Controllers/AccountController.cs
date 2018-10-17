using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webapp.App_Start;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using RestServices;
using LoginObjects;
using System.Threading.Tasks;
using System.Net;

namespace webapp.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager;
                    //?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager;
                    //?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        public ActionResult LogOff()
        {
            Session["user"] = null;
            Session["token"] = null;
            return RedirectToAction("Login", "Account");
        }
        //
        // GET: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            //string token = UtilitiesLibrary.TokenGenerator.GenerateTokenJwt("rodolfo.arteaga");
            AuthService auth = new AuthService();
            string token = await auth.authentication(model.UserName, model.Password);
            
            if (token!=null && token != "")
            {
                Session["user"] = model.UserName;
                Session["token"] = token;
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}