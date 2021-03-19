using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary;
using DataLibrary.Model.Quote;
using DataLibrary.Model.QuoteBOM;


namespace BusinessLogic
{
   public class QuoteBusinessLogic
    {
        protected QuoteDataAccess CustomerDL;

        public QuoteBusinessLogic()
        {
            this.CustomerDL = new QuoteDataAccess();
        }

        public DL_QuoteViewModel GetCustomersList()
        {
            DL_QuoteViewModel ret = new DL_QuoteViewModel();
            //ret = this.ItemDetailsDL.LaodData();
            ret = this.CustomerDL.LoadQuoteList () ;
            return ret;
        }

        public DL_QuoteViewModel BL_GetOpportunityByQuoteNo(string QuoteNo)
        {
            DL_QuoteViewModel ret = new DL_QuoteViewModel();
            ret = this.CustomerDL.GetOpportunityByQuoteNo(QuoteNo);
            return ret;
        }

        public bool BL_CreateLead(string CustomerName,DateTime QuoteDate,string QuoteBy, string QuoteNumber,int NoOfWeeks,DateTime DeliveryDate,string PONumber, string DispatchAddress, string DispatchName, string title, string PhoneNumber,string Email,string ReferenceNo)
        {
            bool ret;
            ret = this.CustomerDL.CreateLead(CustomerName, QuoteDate, QuoteBy, QuoteNumber, NoOfWeeks, DeliveryDate, PONumber, DispatchAddress, DispatchName, title, PhoneNumber, Email, ReferenceNo);
            return ret;
        }

        public string  BL_UpdateOpportunityCustomerDetails(DL_OpportunityModel customer)
        {
           
           string  ret= this.CustomerDL.Update_OpportunityCustomerDetails(customer);
            return ret;
        }
       
        public string BL_UpdateOpportunityAssemblyDetails(DL_OpportunityModel Assembly)
        {

            string ret = this.CustomerDL.Update_OpportunityAssemblyDetails(Assembly);
            return ret;
        }

        public string BL_UpdateOpportunityConsumableDetails(DL_OpportunityModel Consumable)
        {

            string ret = this.CustomerDL.Update_OpportunityConsumableDetails(Consumable);
            return ret;
        }
        
        public string BL_UpdateOpportunityCHOPComments(DL_OpportunityModel CHOP)
        {

            string ret = this.CustomerDL.Update_OpportunityCHOPComments(CHOP);
            return ret;
        }

        public string BL_UpdateOpportunityPMComments(DL_OpportunityModel pm)
        {

            string ret = this.CustomerDL.Update_OpportunityPMComments(pm);
            return ret;
        }

        public string BL_UpdateOpportunityTerritorySplit(DL_OpportunityModel TS)
        {

            string ret = this.CustomerDL.Update_OpportunityTerritorySplit(TS);
            return ret;
        }

        
       public string BL_SaveProjectFinancials(DL_BOMFinancialReiewModel FR)
        {

            string ret = this.CustomerDL.Save_SaveProjectFinancials(FR);
            return ret;
        }

        public DL_BOMFinancialReiewModel GetAssemblyForBOMByOpportunityID(int OpportunityID, int BOMID)
        {
            DL_BOMAssembly ba = new DL_BOMAssembly();
            DL_BOMFinancialReiewModel fr = new DL_BOMFinancialReiewModel();                                          
            fr = this.CustomerDL.GetAssemblyForBOMByOpportunityID(OpportunityID,BOMID);


            decimal PMTotalDeductions = 0;
            decimal TotalDeductions = 0;
            decimal PMRevenue = 0;
            decimal NumberOfUnits =1;
            decimal PMPerc = 0;
            decimal dmlDeposit = 0;
            decimal dmlPreDelivery = 0;
            decimal dmlFinal = 0;

            foreach (var item in fr.BOMAssembly)
            {
                if (item.Category == "Capital")
                {
                  NumberOfUnits = item.Qty;
                  
                }
            }

             //Calculate the Total Deductions to calculate the PM Revenue
             //Calculate the Total Deductions to calculate the Capital Revenue
                if (fr.PMChargableAssemly.Count > 0)
                 {      
                    foreach (var ch in fr.PMChargableAssemly)
                  {
                    foreach (var item in fr.BOMAssembly)
                    {
                        if (ch.AssemblyCode == item.AssemblyCode)
                        {
                            PMTotalDeductions += (item.Revenue* NumberOfUnits);
                            item.Revenue= (item.Revenue * NumberOfUnits);

                            }
                        if (item.Category  != "Capital") 
                        {
                        TotalDeductions+= item.Revenue;
                    
                        }
                        
                    }
                    
                }

              //Calculate the PM Revenue
               foreach (var item in fr.BOMAssembly)
                {
                    if (item.PMPercentage>0)
                    {
                        PMPerc = item.PMPercentage ;
                                PMRevenue = (fr.FinalAgreedPrice - PMTotalDeductions) * PMPerc;
                            item.Revenue = PMRevenue;
                            TotalDeductions += PMRevenue;
                  
                    }
                }

                //Calculate Cpaital Revenue
                foreach (var item in fr.BOMAssembly)
                {
                    if (item.Category == "Capital")
                    {
                        item.Revenue = fr.FinalAgreedPrice - TotalDeductions;
                    }
                }

             }

             
            dmlDeposit = Math.Round((fr.FinalAgreedPrice / 100) * fr.DepositPerc,2);
            dmlPreDelivery= Math.Round((fr.FinalAgreedPrice / 100) * fr.PreDeliveryPerc, 2);
            dmlFinal = fr.FinalAgreedPrice - (dmlDeposit + dmlPreDelivery);

            fr.Deposit = dmlDeposit;
            fr.PreDelivery = dmlPreDelivery;
            fr.Final = dmlFinal;

            return fr;
        }

        public Boolean BL_AddNewBOM(int BOMID, int oppurtunityID, string ActivateNew) {
            return this.CustomerDL.AddNewBOM(BOMID, oppurtunityID, ActivateNew);
        }
    }
}
