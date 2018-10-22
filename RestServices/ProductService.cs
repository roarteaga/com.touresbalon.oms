using log4net;
using Newtonsoft.Json;
using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using UtilitiesLibrary;
using System.Net.Http.Headers;
using System.Web;
using UtilitiesProject;
using HttpMethod = UtilitiesProject.HttpMethod;
using JsonAdapters;

namespace RestServices
{
    public class ProductService
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(ProductService));
        static string Baseurl = ConfigurationManager.AppSettings["ProductGetServiceRoute"];
        public async Task<Product> GetProduct(string token,string id)
        {
            List<JsonHeaders> parametros = new List<JsonHeaders>();
            JsonAdapters.JsonAdapters jadapters = new JsonAdapters.JsonAdapters();
            parametros.Add(new JsonHeaders("Authorization", token));
            UriBuilder builder = new UriBuilder(ConfigurationManager.AppSettings["ProductGetIdServiceRoute"].ToString());
            builder.Query = "Id=" + id;
            string response =  await jadapters.GetJson(parametros,builder.Uri.ToString(),Baseurl,HttpMethod.GET);
            Product retObj = JsonConvert.DeserializeObject<Product>(response);
            return retObj;
        }
        public async Task<List<Product>> GetAllProducts(string token)
        {
            List<JsonHeaders> parametros = new List<JsonHeaders>();
            parametros.Add(new JsonHeaders("Authorization", token));
            JsonAdapters.JsonAdapters jadapters = new JsonAdapters.JsonAdapters();
            string response = await jadapters.GetJson(parametros, ConfigurationManager.AppSettings["ProductGetServiceRoute"].ToString(),Baseurl, HttpMethod.GET);
            List<Product> retObj = JsonConvert.DeserializeObject<List<Product>>(response);
            return retObj;
        }
        public async Task<Response> EditProduct(Product product,string token)
        {
            List<JsonHeaders> parametros = new List<JsonHeaders>();
            parametros.Add(new JsonHeaders("Authorization", token));
            JsonAdapters.JsonAdapters jadapters = new JsonAdapters.JsonAdapters();
            string response = await jadapters.GetJson(parametros, ConfigurationManager.AppSettings["ProductEditServiceRoute"].ToString(), Baseurl, HttpMethod.GET);
            Response retObj = JsonConvert.DeserializeObject<Response>(response);
            return retObj;
        }
    }
}
