using BusinessObjects;
using log4net;
using Newtonsoft.Json;
using RestServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UtilitiesProject;
namespace webapp.Controllers
{
    public class CampaignController : Controller
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(CampaignController));
        // GET: Campaign
        public async Task<ActionResult> Index(int? page)
        {
            if (Session["token"] != null)
            {
                if (page == null)
                    page = 1;
                CampaignService campaignService = new CampaignService();
                var campaignResponse = await campaignService.GetAllCampaigns(Session["token"].ToString(),page);
                var pager = new Pager(campaignResponse.campaigns.Count(), page,10,campaignResponse.totalPaginas.Value+1);
                var viewModel = new IndexViewModelCampaigns
                {
                    Items = campaignResponse.campaigns,
                    Pager = pager
                };
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public async Task<ActionResult> CreateCampaign()
        {
            try
            {
                if (Session["token"] != null)
                {
                    //Product product = new Product();
                    #region Estados de campaña
                    SelectListItem estado1 = new SelectListItem();
                    estado1.Text = "Activa";
                    estado1.Value = "1";

                    SelectListItem estado2 = new SelectListItem();
                    estado2.Text = "Inactiva";
                    estado2.Value = "2";

                    SelectListItem estado3 = new SelectListItem();
                    estado3.Text = "Suspendida";
                    estado3.Value = "3";

                    List<SelectListItem> estadoCampanaItems = new List<SelectListItem>();
                    estadoCampanaItems.Add(estado1);
                    estadoCampanaItems.Add(estado2);
                    estadoCampanaItems.Add(estado3);
                    ViewData["listStateCampaign"] = estadoCampanaItems;
                    #endregion
                    return PartialView("_CreateCampaign");
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception exp)
            {
                log.Error("CampaignController/CreateCampaign" +
                    "", exp);
                return RedirectToAction("Login", "Account");
            }
        }

        public async Task<ActionResult> EditCampaign(long Id)
        {
            try
            {
                if (Session["token"] != null)
                {
                    CampaignService campaignService = new CampaignService();
                    Campaign campaign = await campaignService.GetCampaign(Session["token"].ToString(), Id.ToString());
                    //Product product = new Product();
                    #region Estados de campaña
                    SelectListItem estado1 = new SelectListItem();
                    estado1.Text = "Activa";
                    estado1.Value = "1";

                    SelectListItem estado2 = new SelectListItem();
                    estado2.Text = "Inactiva";
                    estado2.Value = "2";

                    SelectListItem estado3 = new SelectListItem();
                    estado3.Text = "Suspendida";
                    estado3.Value = "3";

                    List<SelectListItem> estadoCampanaItems = new List<SelectListItem>();
                    estadoCampanaItems.Add(estado1);
                    estadoCampanaItems.Add(estado2);
                    estadoCampanaItems.Add(estado3);
                    ViewData["listStateCampaign"] = estadoCampanaItems;
                    #endregion
                    return PartialView("_EditCampaign", campaign);
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception exp)
            {
                log.Error("CampaignController/EditCampaign" +
                    "", exp);
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCampaign([Bind(Include = "idCampaign,name,idStateCampaign,urlImage,description,idProduct,startDate,endDate")]Campaign campaign, string listStateCampaign)
        {
            Response message = new UtilitiesProject.Response();

            if (ModelState.IsValid)
            {
                try
                {
                    long idStateCampaign = 0;
                    Int64.TryParse(listStateCampaign, out idStateCampaign);
                    campaign.idStateCampaign = idStateCampaign;
                    
                    CampaignService campaignService = new CampaignService();
                    message = await campaignService.EditCampaign(campaign, Session["token"].ToString());
                }
                catch (Exception exp)
                {
                    log.Error("CampaignController/EditCampaign", exp);
                }
            }
            //return new JsonResult()
            //{
            //    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            //    Data = message.Description
            //};
            return RedirectToAction("Index", "Campaign");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCampaign([Bind(Include = "idCampaign,name,idStateCampaign,urlImage,description,idProduct,startDate,endDate")]Campaign campaignz, string listStateCampaign)
        {
            long idStateCampaign= 0;
            
            Int64.TryParse(listStateCampaign, out idStateCampaign);
            campaignz.idStateCampaign = idStateCampaign;
            campaignz.idUser = 2;
            campaignz.modificationDate = DateTime.Now;
            Response message = new UtilitiesProject.Response();
            CampaignService campaignService = new CampaignService();
            if (ModelState.IsValid)
            {
                try
                {
                    message = await campaignService.CreateCampaign(campaignz, Session["token"].ToString());
                }
                catch (Exception exp)
                {
                    log.Error("CampaignController/EditCampaign", exp);
                }
            }
            //return new JsonResult()
            //{
            //    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            //    Data = new { success = true}
            //};
            return RedirectToAction("Index", "Campaign");
        }
    }
}