using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Model.Quote
{
    public class DL_OpportunityModel
    {
    public int ID { get; set; }
    public string Opportunity { get; set; }
    public string ClosedDate { get; set; }
    public string Representative { get; set; }
    public string CompanyName { get; set; }
    public string CustomerType { get; set; }
    public DateTime DeliveryDate { get; set; }
    public DateTime CalcDeliveryDate { get; set; }
    public string QuoteNo { get; set; }
    public string PONumber { get; set; }
    public string Authorisation { get; set; }
    public string Campaign { get; set; }
    public string CampaignCode { get; set; }
    public int Territory1Split { get; set; }
    public int Territory2Split { get; set; }
    public string Territory1ID { get; set; }
    public string Territory2ID { get; set; }
    public string DispatchAddress { get; set; }
    public string AccountContactName { get; set; }
    public string AccountContactTitle { get; set; }
    public string AccountContactPhoneNo { get; set; }
    public string AccountContactEmail { get; set; }
    public string FinanceDeal { get; set; }
    public string FinanceType { get; set; }
    public string FinanceApproved { get; set; }
    public decimal FinanceTotalAmount { get; set; }
    public int FinancePeriod { get; set; }
    public string InkUsage { get; set; }
    public string SolventUsage { get; set; }
    public string Comments { get; set; }
    public string SalesPerson { get; set; }
    public string CHOPComments { get; set; }
    public string PMComments { get; set; }
    public string CustomerCode { get; set; }



    }
}
