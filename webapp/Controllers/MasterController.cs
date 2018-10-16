using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webapp.Controllers
{
    public class MasterController : Controller
    {
        protected String Username { get { return !User.Identity.IsAuthenticated ? "Anonimo" : User.Identity.Name; } }
        protected static readonly ILog log = LogManager.GetLogger(typeof(MasterController));
        // GET: Master
        public ActionResult Index()
        {
            return View();
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            log.Error(filterContext.Exception);
            filterContext.ExceptionHandled = true;
            base.OnException(filterContext);
            try
            {
                HandleErrorInfo error = new HandleErrorInfo(new Exception("No se pudo procesar la solicitud"), filterContext.RouteData.Values["controller"].ToString(), filterContext.RouteData.Values["action"].ToString());
                if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    filterContext.Result = PartialView("Error", error);
                }
                else
                {
                    filterContext.Result = View("Error", error);
                }
            }
            catch (Exception e)
            {
                log.Error(e);
                HandleErrorInfo error = new HandleErrorInfo(new Exception("No se pudo procesar la solicitud"), "Home", "Index");
                filterContext.Result = View("Error", error);
            }

        }
    }
}