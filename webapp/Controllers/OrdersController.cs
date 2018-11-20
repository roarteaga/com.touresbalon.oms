﻿using BusinessObjects;
using log4net;
using Newtonsoft.Json;
using RestServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UtilitiesProject;

namespace webapp.Controllers
{
    public class OrdersController : Controller
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(OrdersController));
        
        public async Task<ActionResult> Index(string IdOrden,string IdProducto)
        {
            if (Session["token"] != null)
            {
                OrdersService ordersService = new OrdersService();
                var ordersResponse = new OrdersResponse();
                ordersResponse.orders = new List<Order>();
                ordersResponse.totalPaginas = 1;
                var pager = new Pager(1, 1, 10, 1);
                var viewModel = new IndexViewModelOrders
                {
                    Items = ordersResponse.orders,
                    Pager = pager
                };
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        
        [HttpPost]
        public async Task<ActionResult> SearchOrder(string IdOrden, string IdProducto, int? page)
        {

            if (page == null)
                page = 1;
            OrdersService ordersService = new OrdersService();
            Order order=await ordersService.GetOrder(Session["token"].ToString(), IdOrden);
            var ordersResponse = new OrdersResponse();
            ordersResponse.orders = new List<Order>();
            ordersResponse.orders.Add(order);
            ordersResponse.totalPaginas = 1;
            var pager = new Object();
            pager=new Pager(ordersResponse.orders.Count(), page, 10, ordersResponse.totalPaginas.Value + 1);
            var viewModel = new IndexViewModelOrders
            {
                Items = ordersResponse.orders,
                Pager = (Pager)pager
            };
            return View(viewModel);
        }
    }
}