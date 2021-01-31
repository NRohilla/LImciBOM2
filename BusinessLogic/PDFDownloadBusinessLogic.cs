using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary;
using DataLibrary.Model.QuoteBOM;

namespace BusinessLogic
{
    public class PDFDownloadBusinessLogic
    {

        protected PDFDownloadDataAccess CustomerDL;

        public PDFDownloadBusinessLogic()
        {
            this.CustomerDL = new PDFDownloadDataAccess();
        }

        public DL_OpportunityBOMItemsViewModel GetOpportunityBOMList_BOMDownload(int OpportunityID, int BOMID,int State)
        {
            DL_OpportunityBOMItemsViewModel ret = new DL_OpportunityBOMItemsViewModel();
            ret = this.CustomerDL.GetOpportunityBOMList_BOMDownload(OpportunityID, BOMID,State);
            return ret;
        }

        
        public DL_OpportunityBOMItemsViewModel GetOpportunityPickingList_BOMDownload(int OpportunityID, int BOMID, int State)
        {
            DL_OpportunityBOMItemsViewModel ret = new DL_OpportunityBOMItemsViewModel();
            ret = this.CustomerDL.GetOpportunityPickingList_BOMDownload(OpportunityID, BOMID, State);
            return ret;
        }

    }
}
