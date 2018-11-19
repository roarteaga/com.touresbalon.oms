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
        public async Task<string> GetJson(List<JsonHeaders> parameters, string serviceRoute,object objectReq,string Baseurl, HttpMethod method)
        {
            if (objectReq == null)
                objectReq = "";
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
                    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //client.DefaultRequestHeaders.Add("charset", "utf-8");
                    foreach (JsonHeaders lv in parameters)
                    {
                        if (lv.Llave.Equals("Authorization"))
                            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization",lv.Valor);
                    }
                    //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
                    
                    HttpResponseMessage Res;
                    HttpContent httpContent = null;
                    if (objectReq != null)
                    {
                        var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(objectReq));
                        httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                    }
                    if (method == HttpMethod.POST)
                        Res = await client.PostAsync(serviceRoute, httpContent);
                    else
                    {
                        if (objectReq != "")
                            objectReq = @"/" + objectReq;
                        Res = await client.GetAsync(serviceRoute + objectReq);
                    }
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
