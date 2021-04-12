using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineBOM.Models
{
    public class OpportunityBOMItemsViewModel
    {
        public int OpportunityID { get; set; }
        public int BOMID { get; set; }
        public string ItemMasterName { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,#.00}")]
        public decimal GrandTotal { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,#.00}")]
        public decimal GrandTotalAfterDiscount { get; set; }
        public bool IsCustomParts { get; set; }
        public decimal Discount { get; set; }
        public decimal FinalAgreedPrice { get; set; }
        public string QuoteNo { get; set; }
        public bool ViewBOM { get; set; }
        public String BOM { get; set; }
        public String ClosedDate { get; set; }
        [Display(Name = "Ink Usage")]
        public String InkUsage { get; set; }

        [AllowHtml]
        public List<OpportunityBOMItem> BOMListViewModel { get; set; }

        public List<PrintHead_Consummable_Relations> _LstTbl_PrntHd_Cons_Solv_Clnr;
    }

    public class PrintHead_Consummable_Relations
    {
        public DataLibrary.DBEntity.tblConsmbl_Solv_Clnr_Relations _ConS_SolV_ClnR { get; set; }
        public string _CompatiblePrintHeads { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal CleanerPrice { get; set; }
        public decimal CleanerQuantity { get; set; }
        public decimal SolventPrice { get; set; }
        public decimal SolventQuantity { get; set; }
        public string ConsmblRecID { get; set; }
        public string CleanerRecID { get; set; }
        public string SolventRecID { get; set; }

        public int OppID { get; set; }
        public OpportunityBOMItem _LstConsumable_BOMListViewModel { get; set; }
        public OpportunityBOMItem _LstCleaner_BOMListViewModel { get; set; }
        public OpportunityBOMItem _LstSolvent_BOMListViewModel { get; set; }
    }
}