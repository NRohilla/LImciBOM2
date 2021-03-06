using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Model.Quote
{
    public class DL_BOMListModel
    {
        public int BOMID { get; set; }
        public int OpportunityID { get; set; }
        public int OpportunityBOMListID { get; set; }
        public string Name { get; set; }
        public Decimal TotalPrice { get; set; }
        public Decimal Discount{ get; set; }
        public Decimal PriceAfterDiscount { get; set; }
        public Decimal FinalAgreedPrice { get; set; }
        public string ClosedDate { get; set; }

    }
}
