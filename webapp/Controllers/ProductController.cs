using BusinessObjects;
using log4net;
using Newtonsoft.Json;
using RestServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UtilitiesProject;

namespace webapp.Controllers
{
    public class ProductController : Controller
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(ProductController));
        public async Task<ActionResult> Index()
        {
            if (Session["token"] != null)
            {
                ProductService productService = new ProductService();
                ProductResponse response = await productService.GetAllProducts(Session["token"].ToString());
                return View(response);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public async Task<ActionResult> EditProduct(long Id)
        {
            try
            {
                if (Session["token"] != null)
                {
                    ProductService productService = new ProductService();
                    Product product = await productService.GetProduct(Session["token"].ToString(), Id.ToString());
                    #region Items de transporte
                    SelectListItem transportItem1 = new SelectListItem();
                    transportItem1.Text = "Avión - Avianca";
                    transportItem1.Value = "1";

                    SelectListItem transportItem2 = new SelectListItem();
                    transportItem2.Text = "Avión - American Airlines";
                    transportItem2.Value = "2";

                    if (product.idTransport.Equals("1"))
                        transportItem1.Selected = true;
                    else if (product.idTransport.Equals("2"))
                        transportItem2.Selected = true;

                    List<SelectListItem> transportItems = new List<SelectListItem>();
                    transportItems.Add(transportItem1);
                    transportItems.Add(transportItem2);
                    ViewData["listTransport"] = transportItems;
                    #endregion
                    #region Items de entretenimiento
                    SelectListItem entretainmentItem = new SelectListItem();
                    entretainmentItem.Text = "Partido de fútbol";
                    entretainmentItem.Value = "1";
                    if (product.idEntertainment.Equals("1"))
                        entretainmentItem.Selected = true;

                    List<SelectListItem> entretimentItems = new List<SelectListItem>();
                    entretimentItems.Add(entretainmentItem);

                    ViewData["listEntertainment"] = entretimentItems;


                    #endregion
                    #region Items de hoteles
                    SelectListItem hotelItem1 = new SelectListItem();
                    hotelItem1.Text = "Hilton";
                    hotelItem1.Value = "1";
                    SelectListItem hotelItem2 = new SelectListItem();
                    hotelItem2.Text = "Hotel Dann Carlton";
                    hotelItem2.Value = "2";

                    if (product.idHotel.Equals("1"))
                        hotelItem1.Selected = true;
                    else if (product.idHotel.Equals("2"))
                        hotelItem2.Selected = true;

                    List<SelectListItem> hotelItems = new List<SelectListItem>();
                    hotelItems.Add(hotelItem1);
                    hotelItems.Add(hotelItem2);


                    ViewData["listHotel"] = hotelItems;
                    #endregion
                    #region Mock product
                    //product = new Product();
                    //product.idProduct = Id;
                    //product.idTransport = 1;
                    //product.idEntertainment = 1;
                    //product.idHotel = "1";
                    //product.name = "Producto prueba";
                    //product.urlImage = "img/product.jpg";
                    //product.price = "123";
                    //product.discountRate = "12";
                    //product.code = "asd";
                    //product.source_city = "Bogotá";
                    //product.target_city = "Medellín";
                    //product.spectacle_date = "2018-11-11";
                    //product.arrival_date = "2018-11-09";
                    //product.departure_date = "2018-11-12";
                    //product.description = "Descripción xxx";
                    //product.IdUser = Session["user"].ToString();
                    //product.EventDate = DateTime.Now.ToLongDateString();
                    #endregion

                    return PartialView("_EditProduct", product);
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception exp)
            {
                log.Error("ProductController/EditProduct", exp);
                return RedirectToAction("Login", "Account");
            }
        }
        public async Task<ActionResult> CreateProduct()
        {
            try
            {
                if (Session["token"] != null)
                {
                    //Product product = new Product();
                    #region Items de transporte
                    SelectListItem transportItem1 = new SelectListItem();
                    transportItem1.Text = "Avión - Avianca";
                    transportItem1.Value = "1";

                    SelectListItem transportItem2 = new SelectListItem();
                    transportItem2.Text = "Avión - American Airlines";
                    transportItem2.Value = "2";

                    List<SelectListItem> transportItems = new List<SelectListItem>();
                    transportItems.Add(transportItem1);
                    transportItems.Add(transportItem2);
                    ViewData["listTransport"] = transportItems;
                    #endregion
                    #region Items de entretenimiento
                    SelectListItem entretainmentItem = new SelectListItem();
                    entretainmentItem.Text = "Partido de fútbol";
                    entretainmentItem.Value = "1";
                    List<SelectListItem> entretimentItems = new List<SelectListItem>();
                    entretimentItems.Add(entretainmentItem);
                    ViewData["listEntertainment"] = entretimentItems;
                    #endregion
                    #region Items de hoteles
                    SelectListItem hotelItem1 = new SelectListItem();
                    hotelItem1.Text = "Hilton";
                    hotelItem1.Value = "1";
                    SelectListItem hotelItem2 = new SelectListItem();
                    hotelItem2.Text = "Hotel Dann Carlton";
                    hotelItem2.Value = "2";
                    List<SelectListItem> hotelItems = new List<SelectListItem>();
                    hotelItems.Add(hotelItem1);
                    hotelItems.Add(hotelItem2);
                    ViewData["listHotel"] = hotelItems;
                    #endregion
                    #region Mock product
                    //product = new Product();
                    //product.idProduct = Id;
                    //product.idTransport = 1;
                    //product.idEntertainment = 1;
                    //product.idHotel = "1";
                    //product.name = "Producto prueba";
                    //product.urlImage = "img/product.jpg";
                    //product.price = "123";
                    //product.discountRate = "12";
                    //product.code = "asd";
                    //product.source_city = "Bogotá";
                    //product.target_city = "Medellín";
                    //product.spectacle_date = "2018-11-11";
                    //product.arrival_date = "2018-11-09";
                    //product.departure_date = "2018-11-12";
                    //product.description = "Descripción xxx";
                    //product.IdUser = Session["user"].ToString();
                    //product.EventDate = DateTime.Now.ToLongDateString();
                    #endregion

                    return PartialView("_CreateProduct");
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception exp)
            {
                log.Error("ProductController/EditProduct", exp);
                return RedirectToAction("Login", "Account");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> CreateProduct([Bind(Include = "idTransport,idEntertainment,idHotel,name,urlImage,price,discountRate,code,source_city,target_city,spectacle_date,arrival_date,departure_date,description,IdUser,EventDate")]Product product, string listTransport, string listEntertainment, string listHotel)
        {
            long idTransport = 0;
            long idEntretiment = 0;
            long idHotel = 0;
            Int64.TryParse(listTransport, out idTransport);
            Int64.TryParse(listEntertainment, out idEntretiment);
            //Int32.TryParse(listHotel, out idHotel);
            product.idTransport = idTransport;
            product.idEntertainment = idEntretiment;
            Int64.TryParse(listHotel, out idHotel);
            product.idHotel = idHotel;
            product.arrival_date = DateTime.Now;
            product.departure_date = DateTime.Now;
            product.idUser = 2;
            product.modificationDate = DateTime.Now;
            product.spectacle_date = DateTime.Now;
            Response message = new UtilitiesProject.Response();
            ProductService productService = new ProductService();
            if (ModelState.IsValid)
            {
                try
                {
                    message = await productService.CreateProduct(product, Session["token"].ToString());
                }
                catch (Exception exp)
                {
                    log.Error("ProductController/EditProduct", exp);
                }
            }
            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = message.Description
            };
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EditProduct([Bind(Include= "idProduct,idTransport,idEntertainment,idHotel,name,urlImage,price,discountRate,code,source_city,target_city,spectacle_date,arrival_date,departure_date,description,IdUser,EventDate")]Product product,string listTransport,string listEntertainment,string listHotel)
        {
            Response message=new UtilitiesProject.Response();

            if (ModelState.IsValid)
            {
                try
                {
                    long idTransport = 0;
                    long idEntretiment=0;
                    long idHotel= 0;
                    Int64.TryParse(listTransport, out idTransport);
                    Int64.TryParse(listEntertainment, out idEntretiment);
                    //Int32.TryParse(listHotel, out idHotel);
                    product.idTransport = idTransport;
                    product.idEntertainment = idEntretiment;

                    Int64.TryParse(listHotel, out idHotel);
                    product.idHotel = idHotel;
                    ProductService productService = new ProductService();
                    message = await productService.EditProduct(product,Session["token"].ToString());
                }catch(Exception exp)
                {
                    log.Error("ProductController/EditProduct", exp);
                }
            }
            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = message.Description
            };
        }
    }
}
