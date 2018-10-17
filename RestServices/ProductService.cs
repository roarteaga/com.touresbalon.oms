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

namespace RestServices
{
    public class ProductService
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(ProductService));
        static string Baseurl = ConfigurationManager.AppSettings["ProductGetServiceRoute"];
        public async Task<List<Product>> GetAllProducts()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter> { new JsonFix() },
                DateParseHandling = DateParseHandling.None
            };
            List<Product> retObj = new List<Product>();
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await client.PostAsync(ConfigurationManager.AppSettings["ProductGetServiceRoute"], null);
                    if (Res.IsSuccessStatusCode)
                    {
                        var response = Res.Content.ReadAsStringAsync().Result;
                        retObj = JsonConvert.DeserializeObject<List<Product>>(response, settings);
                    }
                }
                catch (Exception exp)
                {
                    log.Error("ProductService", exp);
                }
            }
            return retObj;
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
