using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using UtilitiesProject;
using HttpMethod = UtilitiesProject.HttpMethod;
using UtilitiesLibrary;

namespace JsonAdapters
{
    public class JsonAdapters
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(JsonAdapters));
        public async Task<string> GetJson(List<JsonPostParameters> parameters, string serviceRoute,string Baseurl, HttpMethod method)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter> { new JsonFix() },
                DateParseHandling = DateParseHandling.None
            };
            string ret = "";
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Add("charset", "utf-8");
                    foreach (JsonPostParameters lv in parameters)
                    {
                        client.DefaultRequestHeaders.Add(lv.Llave, lv.Valor);
                    }
                    //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
                    HttpResponseMessage Res = await client.PostAsync(serviceRoute, null);
                    if (Res.IsSuccessStatusCode)
                    {
                        var response = Res.Content.ReadAsStringAsync().Result;
                        ret = response;
                    }
                }
                catch (Exception exp)
                {
                    log.Error("JsonAdapter", exp);
                }
            }
            return ret;
        }
    }
}
