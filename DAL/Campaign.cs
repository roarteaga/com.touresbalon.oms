//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Campaign
    {
        public long idCampaign { get; set; }
        public string name { get; set; }
        public long idStateCampaign { get; set; }
        public string urlImage { get; set; }
        public string description { get; set; }
        public Nullable<long> idProduct { get; set; }
        public Nullable<System.DateTime> startDate { get; set; }
        public Nullable<System.DateTime> endDate { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual StateCampaign StateCampaign { get; set; }
    }
}