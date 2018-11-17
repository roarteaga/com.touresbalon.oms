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
            string response =  await jadapters.GetJson(parametros,builder.Uri.ToString(),null,Baseurl,HttpMethod.GET);
            Product retObj = JsonConvert.DeserializeObject<Product>(response);
            return retObj;
        }
        public async Task<ProductResponse> GetAllProducts(string token)
        {
            List<JsonHeaders> parametros = new List<JsonHeaders>();
            parametros.Add(new JsonHeaders("Authorization", token));
            JsonAdapters.JsonAdapters jadapters = new JsonAdapters.JsonAdapters();
            string response = await jadapters.GetJson(parametros, ConfigurationManager.AppSettings["ProductGetServiceRoute"].ToString(),1,Baseurl, HttpMethod.GET);
            ProductResponse retObj = JsonConvert.DeserializeObject<ProductResponse>(response);
            return retObj;
        }

        public async Task<Response> EditProduct(Product product,string token)
        {
            List<JsonHeaders> parametros = new List<JsonHeaders>();
            parametros.Add(new JsonHeaders("Authorization", token));
            JsonAdapters.JsonAdapters jadapters = new JsonAdapters.JsonAdapters();
            string response = await jadapters.GetJson(parametros, ConfigurationManager.AppSettings["ProductEditServiceRoute"].ToString(),product, Baseurl, HttpMethod.POST);
            Response retObj = JsonConvert.DeserializeObject<Response>(response);
            return retObj;
        }

        public async Task<Response> CreateProduct(Product product, string token)
        {
            List<JsonHeaders> parametros = new List<JsonHeaders>();
            parametros.Add(new JsonHeaders("Authorization", token));
            JsonAdapters.JsonAdapters jadapters = new JsonAdapters.JsonAdapters();
            string response = await jadapters.GetJson(parametros, ConfigurationManager.AppSettings["ProductCreateServiceRoute"].ToString(),product, Baseurl, HttpMethod.POST);
            Response retObj = JsonConvert.DeserializeObject<Response>(response);
            return retObj;
        }
    }
}
