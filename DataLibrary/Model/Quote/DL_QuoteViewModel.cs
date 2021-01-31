using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Model.Quote
{
    public class DL_QuoteViewModel : DL_OpportunityModel
    {   
        public List<DL_OpportunityModel> QuoteCustomers { get; set; }
        public List<DL_BOMListModel> BOMListModel { get; set; } 
        public List<QuoteDLLineDetails> QuoteLineDetails{ get; set; }
        public List<DL_Territory> TerritoryList { get; set; }
    }
   
}
