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
    public class OrdersService
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(OrdersService));
        static string Baseurl = ConfigurationManager.AppSettings["OrdersGetServiceRoute"];

        public async Task<Order> GetOrder(string token, string id)
        {
            List<JsonHeaders> parametros = new List<JsonHeaders>();
            JsonAdapters.JsonAdapters jadapters = new JsonAdapters.JsonAdapters();
            parametros.Add(new JsonHeaders("Authorization", token));
            UriBuilder builder = new UriBuilder(ConfigurationManager.AppSettings["OrdersGetServiceRoute"].ToString());
            builder.Query = "Id=" + id;
            string response = await jadapters.GetJson(parametros, builder.Uri.ToString(), null, Baseurl, UtilitiesProject.HttpMethod.GET);
            Order retObj = JsonConvert.DeserializeObject<Order>(response);
            return retObj;
        }

        public async Task<Response> CancelOrderAsync(string token, long idOrden)
        {
            List<JsonHeaders> parametros = new List<JsonHeaders>();
            JsonAdapters.JsonAdapters jadapters = new JsonAdapters.JsonAdapters();
            parametros.Add(new JsonHeaders("Authorization", token));
            UriBuilder builder = new UriBuilder(ConfigurationManager.AppSettings["OrdersCancelServiceRoute"].ToString());
            string response = await jadapters.GetJson(parametros, builder.Uri.ToString(), null, Baseurl, UtilitiesProject.HttpMethod.GET);
            Response retObj = JsonConvert.DeserializeObject<Response>(response);
            return retObj;
        }
    }
}
