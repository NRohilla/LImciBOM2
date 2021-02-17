using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineBOM.Models
{
    public class OpportunityBOMItem
    {
        public int BOMID { get; set; }
        public long BOMItemID { get; set; }
        public string Description { get; set; }
        public int QuoteItemMasterID { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal Price { get; set; }
        public decimal Qty { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public int OpportunityID { get; set; }
        public string MatthewsCode { get; set; }
        public int OpportunityBOMListID { get; set; }
        public string CustomDescription { get; set; }
        public string CustomCode { get; set; }
        public decimal Discount { get; set; }
        public decimal FinalAgreedPrice { get; set; }
        public bool IsDiscountApply { get; set; }
        public decimal AfterDiscount { get; set; }
        public  bool IsQtyFixed { get; set; }
        public decimal PriceAfterDiscount { get; set; }
        public decimal MaximumQty { get; set; }
        public int Stock { get; set; }
        public int State { get; set; }
        public int IsInTotal { get; set; }
        public int IsDecimalAllowed { get; set; }
        [Display(Name = "Ink Usage")]
        public string InkUsage { get; set; }

    }
}