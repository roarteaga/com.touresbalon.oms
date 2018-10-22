using log4net;
using LoginObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using UtilitiesLibrary;

namespace RestServices
{
    public class AuthService
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(AuthService));
        static string Baseurl = ConfigurationManager.AppSettings["AuthenticationServiceRoute"];
        public async Task<String> authentication(string username,string password)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter> { new JsonFix() },
                DateParseHandling = DateParseHandling.None
            };
            String token = "";
            using (var client = new HttpClient())
            {
                try
                {
                    Login login = new Login();
                    login.Username = username;
                    login.Password = password;
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    string jsonString = new JavaScriptSerializer().Serialize(login);
                    var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    HttpResponseMessage Res = await client.PostAsync(ConfigurationManager.AppSettings["AuthenticationServiceRoute"], content);
                    if (Res.IsSuccessStatusCode)
                    {
                        var response = Res.Content.ReadAsStringAsync().Result;
                        token = response.Replace("\"","");
                    }
                }
                catch (Exception exp)
                {
                    log.Error("AuthService", exp);
                }
            }
            return token;

        }
    }
}
