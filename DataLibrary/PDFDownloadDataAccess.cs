using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DataLibrary.Model.QuoteBOM;

namespace DataLibrary
{
    public class PDFDownloadDataAccess
    {

        public static string GetConnectionString(string ConnectionName = "OnlineBOMEntities")
        {
            return Utility.UtilityFunctions.ReturnFormattedConnectionString(ConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString);
        }

        #region GetEditBOMByQuoteItemMasterID
        public DL_OpportunityBOMItemsViewModel GetOpportunityBOMList_BOMDownload(int OpportunityID,int BOMID,int State)
        {
           DL_OpportunityBOMItemsViewModel QuoteBOMView = new DL_OpportunityBOMItemsViewModel();
         
           try
            {
    

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                //return cnn.Query<T>(sql).ToList();
                conn.Open();
                DataSet ds = new DataSet();
                string SQLSP;
    
                SQLSP = "Get_OpportunityBOMList_BOMDownload"; 
                SqlCommand dCmd = new SqlCommand(SQLSP, conn);
                dCmd.CommandType = CommandType.StoredProcedure;
                dCmd.Parameters.Add(new SqlParameter("@OpportunityID", OpportunityID));
                dCmd.Parameters.Add(new SqlParameter("@BOMID", BOMID));
                dCmd.Parameters.Add(new SqlParameter("@State", State));
                
                SqlDataAdapter da = new SqlDataAdapter(dCmd);
                DataTable dt = new DataTable();
                ds.Clear();

                da.Fill(dt);
                conn.Close();

                List<DL_OpportunityBOMItem> BOMlst = new List<DL_OpportunityBOMItem>();
                if (dt != null && dt.Rows.Count > 0)
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        DL_OpportunityBOMItem BOM = new DL_OpportunityBOMItem();
                        BOM.Description = dr["Description"].ToString();
                        BOM.ItemPrice = Convert.ToDecimal(dr["ItemPrice"].ToString());
                        BOM.Price = Convert.ToDecimal(dr["Price"].ToString());
                        BOM.MatthewsCode = dr["MatthewsCode"].ToString();
                        BOM.Qty = Convert.ToDecimal(dr["Qty"].ToString());
                        BOM.Category = dr["Category"].ToString();
                        BOM.CompanyName=dr["CompanyName"].ToString();
                        BOM.AccountContactEmail= dr["AccountContactEmail"].ToString();
                        BOM.DispatchAddress= dr["DispatchAddress"].ToString();
                        BOM.Discount = Convert.ToDecimal(dr["Discount"].ToString());
                        BOM.AfterDiscount = Convert.ToDecimal(dr["PriceAfterDiscount"].ToString());
                        BOM.FinalAgreedPrice= Convert.ToDecimal(dr["FinalAgreedPrice"].ToString());
                        BOM.QuoteNo = dr["QuoteNo"].ToString();

                       BOMlst.Add(BOM);

                    }
                    QuoteBOMView.BOMListViewModel = BOMlst;

                }
                return QuoteBOMView;
            }
            }
            catch (Exception ex)
            {

                return QuoteBOMView;
            }

            
        }
        #endregion

        public DL_OpportunityBOMItemsViewModel GetOpportunityPickingList_BOMDownload(int OpportunityID, int BOMID, int State)
        {
            DL_OpportunityBOMItemsViewModel QuoteBOMView = new DL_OpportunityBOMItemsViewModel();

            try
            {


                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    //return cnn.Query<T>(sql).ToList();
                    conn.Open();
                    DataSet ds = new DataSet();
                    string SQLSP;

                    SQLSP = "Get_OpportunityPickingList_BOMDownload";
                    SqlCommand dCmd = new SqlCommand(SQLSP, conn);
                    dCmd.CommandType = CommandType.StoredProcedure;
                    dCmd.Parameters.Add(new SqlParameter("@OpportunityID", OpportunityID));
                    dCmd.Parameters.Add(new SqlParameter("@BOMID", BOMID));
                    dCmd.Parameters.Add(new SqlParameter("@State", State));

                    SqlDataAdapter da = new SqlDataAdapter(dCmd);
                    DataTable dt = new DataTable();
                    ds.Clear();

                    da.Fill(dt);
                    conn.Close();

                    List<DL_OpportunityBOMItem> BOMlst = new List<DL_OpportunityBOMItem>();
                    if (dt != null && dt.Rows.Count > 0)
                    {

                        foreach (DataRow dr in dt.Rows)
                        {
                            DL_OpportunityBOMItem BOM = new DL_OpportunityBOMItem();
                            BOM.Description = dr["Description"].ToString();                   
                            BOM.MatthewsCode = dr["MatthewsCode"].ToString();
                            BOM.Qty = Convert.ToDecimal(dr["Qty"].ToString());
                            BOM.Category = dr["Category"].ToString();
                            BOM.CompanyName = dr["CompanyName"].ToString();
                            BOM.AccountContactEmail = dr["AccountContactEmail"].ToString();
                            BOM.DispatchAddress = dr["DispatchAddress"].ToString();
                            BOM.QuoteNo = dr["QuoteNo"].ToString();
                            BOM.AssemblyCode= dr["AssemblyCode"].ToString();
                            BOM.AssemblyDesc = dr["AssemblyDesc"].ToString();
                            BOMlst.Add(BOM);

                        }
                        QuoteBOMView.BOMListViewModel = BOMlst;

                    }
                    return QuoteBOMView;
                }
            }
            catch (Exception ex)
            {

                return QuoteBOMView;
            }


        }

        
    }

}
