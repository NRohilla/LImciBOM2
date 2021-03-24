using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Model.QuoteBOM
{
    public class DL_OpportunityBOMItem
    {
        public int BOMID { get; set; }
        public long BOMItemID { get; set; }
        public string Description { get; set; }
        public int QuoteItemMasterID { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal Price { get; set; }
        public decimal Qty { get; set; }
        public string Category { get; set; }
        public int CategoryOrder { get; set; }
        public string SubCategory { get; set; }
        public int SubCategoryOrder { get; set; }
        public int OpportunityID { get; set; }
        public string MatthewsCode { get; set; }
        public int OpportunityBOMListID { get; set; }
        public string CustomDescription { get; set; }
        public string CustomCode { get; set; }
        public bool IsCustomParts { get; set; }
        public String CompanyName { get; set; }
        public string AccountContactTitle { get; set; }
        public string AccountContactName { get; set; }
        public string AccountContactEmail { get; set; }
        public string AccountContactPhoneNo { get; set; }
        public string DispatchAddress { get; set; }
        public string QuoteNo { get; set; }
        public decimal Discount { get; set; }
        public string FinanceDeal { get; set; }
        public decimal FinalAgreedPrice { get; set; }
        public string FinanceType { get; set; }
        public string FinanceApproved { get; set; }
        public bool IsDiscountApply { get; set; }
        public decimal AfterDiscount { get; set; }
        public bool IsQtyFixed { get; set; }
        public decimal PriceAfterDiscount { get; set; }
        public decimal FinanceTotalAmount { get; set; }
        public string BOM { get; set; }
        public decimal MaximumQty { get; set; }
        public string ClosedDate { get; set; }
        public int Stock { get; set; }
        public int State { get; set; }
        public string AssemblyCode { get; set; }
        public string AssemblyDesc { get; set; }
        public bool IsInTotal { get; set; }
        public bool IsDecimalAllowed { get; set; }
        public String InkUsage { get; set; }
        public int VersionNum { get; set; }

    }

}
