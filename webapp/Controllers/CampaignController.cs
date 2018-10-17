using log4net;
using Newtonsoft.Json;
using RestServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace webapp.Controllers
{
    public class CampaignController : Controller
    {
        // GET: Campaign
        public async Task<ActionResult> Index()
        {
            if (Session["token"] != null)
            {
                CampaignService campaignService = new CampaignService();
                return View(await campaignService.GetAllCampaigns());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}