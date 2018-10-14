﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Product
    {
        public string idProduct { get; set; }
        public string idTransport { get; set; }
        public string idEntertainment { get; set; }
        public string idHotel { get; set; }
        public string name { get; set; }
        public string urlImage { get; set; }
        public string price { get; set; }
        public string discountRate { get; set; }
        public string code { get; set; }
        public string source_city { get; set; }
        public string target_city { get; set; }
        public string spectacle_date { get; set; }
        public string arrival_date { get; set; }
        public string departure_date { get; set; }
        public string description { get; set; }
    }
}