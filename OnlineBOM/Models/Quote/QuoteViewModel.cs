using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBOM.Models
{
    public class QuoteViewModel:OpportunityCoverModel
    {
        public List<OpportunityCoverModel> QuoteCustomerListModel { get; set; }
        public List<BOMListModel> BOMListModel { get; set; }
        public List<TerritoryModel> TerritoryListModel { get; set; }

    }
}