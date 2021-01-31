using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLibrary.Model.Quote
{
    public class DL_BOMFinancialReiewModel
    {

       public Decimal FinalAgreedPrice { get; set; }
            
        public Decimal CTOCerials { get; set; }

        public Decimal BOMTotal { get; set; }
        public Decimal BOMDiscount { get; set; }
        public List<DL_BOMAssembly> BOMAssembly  { get; set; }
        public List<DL_PMChargableAssemly> PMChargableAssemly { get; set; }
        public string QuoteNo { get; set; }
        public decimal Deposit { get; set; }
        public decimal PreDelivery { get; set; }
        public decimal Final { get; set; }
        public decimal DepositPerc { get; set; }
        public decimal PreDeliveryPerc { get; set; }
        public decimal FinalPerc { get; set; }
        public string PONumber { get; set; }
        public int BOMID { get; set; }
        public int OpportunityID { get; set; }

    }

    public class DL_BOMAssembly
    {
        public string AssemblyCode { get; set; }
        public string Area { get; set; }
        public string  Category { get; set; }
        public string Device { get; set; }
        public Decimal Revenue { get; set; }
        public Decimal Qty { get; set; }
        public Decimal PMPercentage { get; set; }
    }

    public class DL_PMChargableAssemly
    {
        public string AssemblyCode { get; set; }

    }


}
