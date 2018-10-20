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
            List<JsonPostParameters> parametros = new List<JsonPostParameters>();
            JsonAdapters.JsonAdapters jadapters = new JsonAdapters.JsonAdapters();
            parametros.Add(new JsonPostParameters("Authorization", token));
            string response =  await jadapters.GetJson(parametros, ConfigurationManager.AppSettings["ProductGetServiceRoute"].ToString(),Baseurl,HttpMethod.POST);
            Product retObj = JsonConvert.DeserializeObject<Product>(response);
            return retObj;
        }
        public async Task<List<Product>> GetAllProducts(string token)
        {
            List<JsonPostParameters> parametros = new List<JsonPostParameters>();
            parametros.Add(new JsonPostParameters("Authorization", token));
            JsonAdapters.JsonAdapters jadapters = new JsonAdapters.JsonAdapters();
            string response = await jadapters.GetJson(parametros, ConfigurationManager.AppSettings["ProductGetServiceRoute"].ToString(),Baseurl, HttpMethod.POST);
            List<Product> retObj = JsonConvert.DeserializeObject<List<Product>>(response);
            return retObj;
        }
        
        public string DecodeFrom( string myString)
        {
            // read the string as UTF-8 bytes.
            //byte[] bytes = Encoding.Default.GetBytes(myString);
            //string newString = Encoding.UTF8.GetString(bytes);
            //string newString =HttpUtility.UrlDecode(bytes, Encoding.GetEncoding("utf-8"));


            //return ;
            return "";
        }
        public async void AddProduct(Product product)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter> { new JsonFix() },
                DateParseHandling = DateParseHandling.None
            };

        }
    }
}
