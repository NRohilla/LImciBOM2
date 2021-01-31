using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnlineBOM.Models
{
    public class BOMFinancialReviewModel
    {

        [Display(Name = "BOM Price $")]
        public decimal BOMTotal { get; set; }

        [Display(Name = "Final Agreed Price $")]
        public decimal FinalAgreedPrice { get; set; }

        [Display(Name = "CTO Cerials")]
        public decimal CTOCerials { get; set; }

       public List<FinancialBOMAssemly> BOMAssemly { get; set; }
       public string QuoteNo { get; set; }

        [Display(Name = " Deposit$")]
        public decimal Deposit { get; set; }

        [Display(Name = "Pre-Delivery $")]
        public decimal PreDelivery { get; set; }

        [Display(Name = "Final Handover $")]
        public decimal Final { get; set; }

        [Display(Name = "PO Number")]
        public string PONumber { get; set; }

        [Display(Name = " %")]
        public decimal DepositPerc { get; set; }

        [Display(Name = " %")]
        public decimal PreDeliveryPerc { get; set; }
        [Display(Name = " %")]
        public decimal FinalPerc { get; set; }

        public int BOMID { get; set; }
        public int OpportunityID { get; set; }
    }


    public class FinancialBOMAssemly
    {
    public string AssemblyCode { get; set; }
    public string Category { get; set; }
    public string Area { get; set; }

    public string Device { get; set; }
    public Decimal Qty { get; set; }
        public Decimal Price { get; set; }
        public Decimal Revenue { get; set; }
}
}