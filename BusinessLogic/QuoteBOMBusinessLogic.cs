using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary;
using DataLibrary.Model.QuoteBOM;

namespace BusinessLogic
{
    public class QuoteBOMBusinessLogic
    {
        protected QuoteBOMDataAccess CustomerDL;

        public QuoteBOMBusinessLogic()
        {
            this.CustomerDL = new QuoteBOMDataAccess();
        }

        public DL_OpportunityBOMItemsViewModel GetOpportunityBOMItemsByOpportunityID(int OpportunityID,int BOMID,bool NewBOM,int State,  int versionnum)
        {
            DL_OpportunityBOMItemsViewModel ret = new DL_OpportunityBOMItemsViewModel();
            ret = this.CustomerDL.Get_OpportunityBOMItemsByOpportunityID(OpportunityID, BOMID, NewBOM,State,versionnum);
            return ret;
        }

              
        public DL_OpportunityBOMItemsViewModel GetOpportunityBOMChildItemsByBOMItemID(int OpportunityID, string BOMItemID, int BOMID, int State)
        {
            DL_OpportunityBOMItemsViewModel ret = new DL_OpportunityBOMItemsViewModel();
            ret = this.CustomerDL.Get_OpportunityBOMChildItemsByBOMItemID(OpportunityID, BOMItemID, BOMID, State);
            return ret;
        }

        public string SaveQuoteBOM(List<DL_OpportunityBOMItem> Bom)
        {
            try
            {
                string SaveBom = this.CustomerDL.SaveBom(Bom);
               return SaveBom;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }

          
        }




    }
}
