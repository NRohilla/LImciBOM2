using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBOM.Models
{
    public class BOMListModel
    {
        public int BOMID { get; set; }
        public int OpportunityID { get; set; }
        public int OpportunityBOMListID { get; set; }
        public string Name { get; set; }
        public Decimal TotalPrice { get; set; }
        public Decimal Discount { get; set; }
        public Decimal PriceAfterDiscount { get; set; }
        public Decimal FinalAgreedPrice { get; set; }
        public string ClosedDate { get; set; }

        public bool IsDeleted { get; set; }
        public int VersionNum { get; set; }
    }
}