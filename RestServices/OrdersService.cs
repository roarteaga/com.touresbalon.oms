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
            builder.Query = "Id=" + idOrden;
            string response = await jadapters.GetJson(parametros, builder.Uri.ToString(), null, Baseurl, UtilitiesProject.HttpMethod.GET);
            Response retObj = JsonConvert.DeserializeObject<Response>(response);
            return retObj;
        }

        public async Task<List<Order>> GetOrderOpenTop(string token)
        {
            List<JsonHeaders> parametros = new List<JsonHeaders>();
            JsonAdapters.JsonAdapters jadapters = new JsonAdapters.JsonAdapters();
            parametros.Add(new JsonHeaders("Authorization", token));
            UriBuilder builder = new UriBuilder(ConfigurationManager.AppSettings["OrdersOpenServiceRoute"].ToString());
            RequestOpenOrder requestOpenOrder = new RequestOpenOrder();
            requestOpenOrder.IdOrder = 3;
            requestOpenOrder.Page = 1;
            string response = await jadapters.GetJson(parametros, builder.Uri.ToString(), requestOpenOrder, Baseurl, UtilitiesProject.HttpMethod.POST);
            List<Order> retObj = JsonConvert.DeserializeObject<List<Order>>(response);
            return retObj;
        }

        public async Task<List<OrderClosed>> GetOrderClosed(string token)
        {
            List<JsonHeaders> parametros = new List<JsonHeaders>();
            JsonAdapters.JsonAdapters jadapters = new JsonAdapters.JsonAdapters();
            //parametros.Add(new JsonHeaders("Authorization", token));
            UriBuilder builder = new UriBuilder(ConfigurationManager.AppSettings["OrdersClosedServiceRoute"].ToString());
            string response = await jadapters.GetJson(parametros, builder.Uri.ToString(), "", Baseurl, UtilitiesProject.HttpMethod.GET);
            List<OrderClosed> retObj = JsonConvert.DeserializeObject<List<OrderClosed>>(response);
            return retObj;
        }
    }
}
