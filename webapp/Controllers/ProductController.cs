using BusinessObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace webapp.Controllers
{
    public class ProductController : Controller
    {

        string Baseurl = ConfigurationManager.AppSettings["ProductServiceBaseURL"];
        public async Task<ActionResult> Index()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter> { new BadDateFixingConverter() },
                DateParseHandling = DateParseHandling.None
            };
            List<Product> retObj = new List<Product>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.PostAsync(ConfigurationManager.AppSettings["ProductServiceRoute"],null);
                if (Res.IsSuccessStatusCode)
                {
                    var response = Res.Content.ReadAsStringAsync().Result;
                    retObj = JsonConvert.DeserializeObject<List<Product>>(response,settings);
                }
                return View(retObj);
            }
        }
    }
    class BadDateFixingConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(DateTime) || objectType == typeof(DateTime?));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string rawDate = (string)reader.Value;
            DateTime date;

            // First try to parse the date string as is (in case it is correctly formatted)
            if (DateTime.TryParse(rawDate, out date))
            {
                return date;
            }

            // If not, see if the string matches the known bad format. 
            // If so, replace the ':' with '.' and reparse.
            if (rawDate.Length > 19 && rawDate[19] == ':')
            {
                rawDate = rawDate.Substring(0, 19) + '.' + rawDate.Substring(20);
                if (DateTime.TryParse(rawDate, out date))
                {
                    return date;
                }
            }

            // It's not a date after all, so just return the default value
            if (objectType == typeof(DateTime?))
                return null;

            return DateTime.MinValue;
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}