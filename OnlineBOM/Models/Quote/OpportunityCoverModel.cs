using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;


namespace OnlineBOM.Models
{


    public class OpportunityCoverModel
    {
        public int QuoteID { get; set; }
        [Required(ErrorMessage = "Required Field")]

        [MaxLength(128)]
        [Display(Name = "Opportunity")]
        public string Opportunity { get; set; }

        [Display(Name = "Closed Date")]
        public string ClosedDate { get; set; }

        [Display(Name = "Representative")]
        public string Representative { get; set; }

        [Display(Name = "CompanyName")]
        public string CompanyName { get; set; }

        [Display(Name = "Customer Type")]
        public string CustomerType { get; set; }

        [Display(Name = "Ideal Delivery Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DeliveryDate { get; set; }

        [Display(Name = "Estimated Delivery Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CalcDeliveryDate { get; set; }

        [Display(Name = "Quote No")]
        public string QuoteNo { get; set; }
        [
        Display(Name = "PO Number")]
        public string PONumber { get; set; }
        
        [Display(Name = "Authorisation")]
        public string Authorisation { get; set; }
        
        [Display(Name = "Campaign")]
        public string Campaign { get; set; }
        
        [Display(Name = "Campaign Code")]
        public string CampaignCode { get; set; }

        [Required(ErrorMessage ="Please enter the Dispatched Address")]
        [Display(Name = "Dispatch Address")]
        public string DispatchAddress { get; set; }
       
        [Display(Name = "AccountContact Name")]
         public string AccountContactName { get; set; }
        
         [Display(Name = "AccountContact Title")]
        public string AccountContactTitle { get; set; }
        
        [Display(Name = "AccountContact PhoneNo")]
        public string AccountContactPhoneNo { get; set; }
        
        [Display(Name = "AccountContact Email")]
        public string AccountContactEmail { get; set; }
       
        [Display(Name = "Finance Deal")]
        public string FinanceDeal { get; set; }
        
        [Display(Name = "Finance Type")]
        public string FinanceType { get; set; }
        
        [Display(Name = "Finance Approved")]
        public string FinanceApproved { get; set; }
        
        [Display(Name = "Finance Total Amount")]
        public decimal FinanceTotalAmount { get; set; }
        
        [Display(Name = "Finance Period")]
        public int FinancePeriod { get; set; }
        
        [Display(Name = "Ink Usage")]
        public string InkUsage { get; set; }
        [Display(Name = "Solvent Usage")]
        public string SolventUsage { get; set; }
        
        [Display(Name = "Comments")]
        public string Comments { get; set; }

        [Display(Name = "Sales Person")]
        public string SalesPerson { get; set; }

        [Display(Name = "BOM Price $")]
        public string BOMPrice { get; set; }
      
        [Display(Name = "Final Agreed Price $")]
        public string FinalAgreedPrice { get; set; }

        [Display(Name = "CTO Cerials")]
        public string CTOCerials { get; set; }
        
        [Display(Name = "CHOP Comments")]
        [DataType(DataType.MultilineText)]
        public string CHOPComments { get; set; }
        
        [Display(Name = "PM Comments")]
        public string PMComments { get; set; }

        [Display(Name = "Territory 1 ")]
        public string Territory1ID { get; set; }

        [Display(Name = "Territory 2")]
        public string Territory2ID { get; set; }

        [Display(Name = "Territory 1 Split %")]
        public int Territory1Split { get; set; }
        [Display(Name = "Territory 2 Split %")]
        public int Territory2Split { get; set; }

        [Display(Name = "Customer Code")]
        public string  CustomerCode { get; set; }
        public int BOMID { get; set; }
        public int OpportunityID { get; set; }
    }

    public class Title
    {
        public string Titles { get; set; }
    }

}