using BusinessObjects;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UtilitiesLibrary;
using UtilitiesProject;

namespace RestServices
{
    public class CampaignService
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(CampaignService));
        static string Baseurl = ConfigurationManager.AppSettings["CampaignGetServiceRoute"];
        public async Task<CampaignResponse> GetAllCampaigns(string token,int? page)
        {
            List<JsonHeaders> parametros = new List<JsonHeaders>();
            parametros.Add(new JsonHeaders("Authorization", token));
            JsonAdapters.JsonAdapters jadapters = new JsonAdapters.JsonAdapters();
            int pageSend = page.HasValue ? page.Value : 1;
            string response = await jadapters.GetJson(parametros, ConfigurationManager.AppSettings["CampaignGetServiceRoute"].ToString(), pageSend, Baseurl, UtilitiesProject.HttpMethod.GET);
            CampaignResponse retObj = JsonConvert.DeserializeObject<CampaignResponse>(response);
            return retObj;
        }
        public async Task<Response> CreateCampaign(Campaign campaign, string token)
        {
            List<JsonHeaders> parametros = new List<JsonHeaders>();
            parametros.Add(new JsonHeaders("Authorization", token));
            JsonAdapters.JsonAdapters jadapters = new JsonAdapters.JsonAdapters();
            string response = await jadapters.GetJson(parametros, ConfigurationManager.AppSettings["CampaignCreateServiceRoute"].ToString(), campaign, Baseurl, UtilitiesProject.HttpMethod.POST);
            Response retObj = JsonConvert.DeserializeObject<Response>(response);
            return retObj;
        }
        public async Task<Response> EditCampaign(Campaign campaign, string token)
        {
            List<JsonHeaders> parametros = new List<JsonHeaders>();
            parametros.Add(new JsonHeaders("Authorization", token));
            JsonAdapters.JsonAdapters jadapters = new JsonAdapters.JsonAdapters();
            string response = await jadapters.GetJson(parametros, ConfigurationManager.AppSettings["CampaignEditServiceRoute"].ToString(), campaign, Baseurl, UtilitiesProject.HttpMethod.POST);
            Response retObj = JsonConvert.DeserializeObject<Response>(response);
            return retObj;
        }
        public async Task<Campaign> GetCampaign(string token, string id)
        {
            List<JsonHeaders> parametros = new List<JsonHeaders>();
            JsonAdapters.JsonAdapters jadapters = new JsonAdapters.JsonAdapters();
            parametros.Add(new JsonHeaders("Authorization", token));
            UriBuilder builder = new UriBuilder(ConfigurationManager.AppSettings["CampaignGetIdServiceRoute"].ToString());
            builder.Query = "Id=" + id;
            string response = await jadapters.GetJson(parametros, builder.Uri.ToString(), null, Baseurl, UtilitiesProject.HttpMethod.GET);
            Campaign retObj = JsonConvert.DeserializeObject<Campaign>(response);
            return retObj;
        }
    }
}
