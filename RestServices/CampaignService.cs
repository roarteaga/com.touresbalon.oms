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

namespace RestServices
{
    public class CampaignService
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(CampaignService));
        static string Baseurl = ConfigurationManager.AppSettings["CampaignGetServiceRoute"];
        public async Task<List<Campaign>> GetAllCampaigns()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter> { new JsonFix() },
                DateParseHandling = DateParseHandling.None
            };
            List<Campaign> retObj = new List<Campaign>();
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await client.PostAsync(ConfigurationManager.AppSettings["CampaignGetServiceRoute"], null);
                    if (Res.IsSuccessStatusCode)
                    {
                        var response = Res.Content.ReadAsStringAsync().Result;
                        retObj = JsonConvert.DeserializeObject<List<Campaign>>(response, settings);
                    }
                }
                catch (Exception exp)
                {
                    log.Error("CampaignGetServiceRoute", exp);
                }
            }
            return retObj;
        }
    }
}
