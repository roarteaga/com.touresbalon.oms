using BusinessObjects;
using log4net;
using Newtonsoft.Json;
using RestServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace webapp.Controllers
{
    public class ProductController : Controller
    {
        
        public async Task<ActionResult> Index()
        {
            if (Session["token"] != null)
            {
                ProductService productService = new ProductService();
                return View(await productService.GetAllProducts());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
    }
}