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
        public decimal  GrandTotal { get; set; }
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
        public List<DataLibrary.DBEntity.tblConsmbl_Solv_Clnr_Relations> _LstTblCons_Solv_Clnr;
    }
}