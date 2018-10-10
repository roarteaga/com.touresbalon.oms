using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class Campaigns
    {
        
        public List<DAL.Campaign> GetCampaigns()
        {
            DAL.touresBalonBDEntities db =new DAL.touresBalonBDEntities();
            return db.Campaign.ToList();
        }
    }
}
