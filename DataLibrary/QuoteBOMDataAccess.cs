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

    
    public class QuoteBOMDataAccess
    {
        public static string GetConnectionString(string ConnectionName = "OnlineBOMEntities")
        {
            return ConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString;
        }


        #region GetEditBOMByQuoteItemMasterID
        public DL_OpportunityBOMItemsViewModel Get_OpportunityBOMItemsByOpportunityID(int OpportunityID, int BOMID,bool NewBOM,int State)
        {
            
        DL_OpportunityBOMItemsViewModel QuoteBOMView = new DL_OpportunityBOMItemsViewModel();

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                //return cnn.Query<T>(sql).ToList();
                conn.Open();
                DataSet ds = new DataSet();
                string SQLSP;
  
                { SQLSP= "Get_OpportunityBOMItemsByOpportunityID"; }
                SqlCommand dCmd = new SqlCommand(SQLSP, conn);
                dCmd.CommandType = CommandType.StoredProcedure;
                dCmd.Parameters.Add(new SqlParameter("@OpportunityID", OpportunityID));
                dCmd.Parameters.Add(new SqlParameter("@BOMID", BOMID));
                dCmd.Parameters.Add(new SqlParameter("@NewBOM", NewBOM));
                dCmd.Parameters.Add(new SqlParameter("@State", State)); 
                SqlDataAdapter da = new SqlDataAdapter(dCmd);
                DataTable dt = new DataTable();
                ds.Clear();

                da.Fill(dt);
                conn.Close();

                List< DL_OpportunityBOMItem> BOMlst= new List<DL_OpportunityBOMItem>();
                if (dt != null && dt.Rows.Count > 0)
                {
             
                    foreach (DataRow dr in dt.Rows)
                    {
                        DL_OpportunityBOMItem BOM = new DL_OpportunityBOMItem();
                        BOM.OpportunityID = Convert.ToInt32(dr["OpportunityID"].ToString());
                        BOM.BOMID = Convert.ToInt32(dr["BOMID"].ToString());
                        BOM.BOMItemID= Convert.ToInt64(dr["BOMItemsID"].ToString());
                        BOM.Description = dr["Description"].ToString();
                        BOM.ItemPrice = Convert.ToDecimal(dr["ItemPrice"].ToString());
                        BOM.Price = Convert.ToDecimal(dr["Price"].ToString());
                        BOM.Qty = Convert.ToDecimal(dr["Qty"].ToString());
                        BOM.Category= dr["Category"].ToString();
                        BOM.SubCategory =dr["SubCategory"].ToString();
                        BOM.MatthewsCode= dr["ITEMID"].ToString();
                        BOM.OpportunityBOMListID = Convert.ToInt32(dr["OpportunityBOMListID"].ToString());
                        BOM.IsCustomParts= Convert.ToBoolean(dr["IsCustomParts"].ToString()); 
                        BOM.Discount= Convert.ToDecimal(dr["Discount"].ToString());
                        BOM.FinalAgreedPrice = Convert.ToDecimal(dr["Finalagreedprice"].ToString());
                        BOM.IsQtyFixed= Convert.ToBoolean(dr["IsQtyFixed"].ToString());
                        BOM.PriceAfterDiscount= Convert.ToDecimal(dr["PriceAfterDiscount"].ToString());
                        BOM.BOM= dr["BOM"].ToString();
                        BOM.MaximumQty = Convert.ToDecimal(dr["MaximumQty"].ToString());
                        BOM.ClosedDate = dr["ClosedDate"].ToString();
                        BOM.Stock = Convert.ToInt32(dr["Stock"].ToString());
                        BOM.State = State;
                        BOM.IsInTotal= Convert.ToBoolean(dr["IsInTotal"].ToString());
                        BOM.IsDecimalAllowed= Convert.ToBoolean(dr["IsDecimalAllowed"].ToString()); 

                        if (dr["IsDiscountApply"].ToString() != null && dr["IsDiscountApply"].ToString() != "" )
                        { BOM.IsDiscountApply = Convert.ToBoolean(dr["IsDiscountApply"].ToString());  }
                        else { BOM.IsDiscountApply = false; }

                        if (BOM.IsDiscountApply == true)
                        {
                            BOM.AfterDiscount =( (BOM.ItemPrice) - (BOM.ItemPrice / 100) * BOM.Discount) * BOM.Qty;
                        }
                        else
                        {
                            BOM.AfterDiscount = (BOM.ItemPrice * BOM.Qty);
                        }
                        BOMlst.Add(BOM); 

                    }
                    QuoteBOMView.BOMListViewModel = BOMlst;

                }
                return QuoteBOMView;
            }
        }

        #endregion

        #region GetEditBOMByQuoteItemMasterID
        public DL_OpportunityBOMItemsViewModel Get_OpportunityBOMChildItemsByBOMItemID(int OpportunityID, string BOMItemID, int BOMID, int State)
        {

            DL_OpportunityBOMItemsViewModel QuoteBOMView = new DL_OpportunityBOMItemsViewModel();

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                //return cnn.Query<T>(sql).ToList();
                conn.Open();
                DataSet ds = new DataSet();
                string SQLSP;

                { SQLSP = "Get_OpportunityBOMChildItemsByBOMItemID"; }
                SqlCommand dCmd = new SqlCommand(SQLSP, conn);
                dCmd.CommandType = CommandType.StoredProcedure;
                dCmd.Parameters.Add(new SqlParameter("@OpportunityID", OpportunityID));
                dCmd.Parameters.Add(new SqlParameter("@BOMID", BOMID));
                dCmd.Parameters.Add(new SqlParameter("@BOMItemID", BOMItemID));

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
                        BOM.OpportunityID = Convert.ToInt32(dr["OpportunityID"].ToString());
                        BOM.BOMID = Convert.ToInt32(dr["BOMID"].ToString());
                        BOM.BOMItemID = Convert.ToInt64(dr["BOMItemsID"].ToString());
                        BOM.Description = dr["Description"].ToString();
                        BOM.ItemPrice = Convert.ToDecimal(dr["ItemPrice"].ToString());
                        BOM.Price = Convert.ToDecimal(dr["Price"].ToString());
                        BOM.Qty = Convert.ToDecimal(dr["Qty"].ToString());
                        BOM.Category = dr["Category"].ToString();
                        BOM.SubCategory = dr["SubCategory"].ToString();
                        BOM.MatthewsCode = dr["ITEMID"].ToString();
                        BOM.OpportunityBOMListID = Convert.ToInt32(dr["OpportunityBOMListID"].ToString());
                        BOM.IsCustomParts = Convert.ToBoolean(dr["IsCustomParts"].ToString());
                        BOM.Discount = Convert.ToDecimal(dr["Discount"].ToString());
                        BOM.FinalAgreedPrice = Convert.ToDecimal(dr["Finalagreedprice"].ToString());
                        BOM.IsQtyFixed = Convert.ToBoolean(dr["IsQtyFixed"].ToString());
                        BOM.PriceAfterDiscount = Convert.ToDecimal(dr["PriceAfterDiscount"].ToString());
                        BOM.BOM = dr["BOM"].ToString();
                        BOM.MaximumQty = Convert.ToDecimal(dr["MaximumQty"].ToString());
                        BOM.ClosedDate = dr["ClosedDate"].ToString();
                        BOM.Stock = Convert.ToInt32(dr["Stock"].ToString());
                        BOM.State = State;
                        BOM.IsInTotal = Convert.ToBoolean(dr["IsInTotal"].ToString());
                        BOM.IsDecimalAllowed = Convert.ToBoolean(dr["IsDecimalAllowed"].ToString());

                        if (dr["IsDiscountApply"].ToString() != null && dr["IsDiscountApply"].ToString() != "")
                        { BOM.IsDiscountApply = Convert.ToBoolean(dr["IsDiscountApply"].ToString()); }
                        else { BOM.IsDiscountApply = false; }

                        if (BOM.IsDiscountApply == true)
                        {
                            BOM.AfterDiscount = ((BOM.ItemPrice) - (BOM.ItemPrice / 100) * BOM.Discount) * BOM.Qty;
                        }
                        else
                        {
                            BOM.AfterDiscount = (BOM.ItemPrice * BOM.Qty);
                        }
                        BOMlst.Add(BOM);

                    }
                    QuoteBOMView.BOMListViewModel = BOMlst;

                }
                return QuoteBOMView;
            }
        }

        #endregion



        #region SaveBOM
        public string SaveBom(List<DL_OpportunityBOMItem> BOMList)
        {
            try
            {
                if (BOMList.Count > 0)

                    using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                        if (BOMList.Count > 0)
                        {  
                           //Deactivate all the items
                            SqlCommand Cmd = new SqlCommand("Update_OpportunityBOMListIsActiveFlag", conn);
                            Cmd.CommandType = CommandType.StoredProcedure;
                            Cmd.Parameters.Add(new SqlParameter("@OpportunityID", BOMList[0].OpportunityID));
                            Cmd.Parameters.Add(new SqlParameter("@BOMID", BOMList[0].BOMID));
                            Cmd.Parameters.Add(new SqlParameter("@State", BOMList[0].State));
                            conn.Open();
                            Cmd.ExecuteNonQuery();

                            foreach (var item in BOMList)
                                {
                                    {

                                        SqlCommand dCmd = new SqlCommand("Insert_OpportunityBOMList", conn);
                                        dCmd.CommandType = CommandType.StoredProcedure;
                                        dCmd.Parameters.Add(new SqlParameter("@OpportunityID", item.OpportunityID));
                                        dCmd.Parameters.Add(new SqlParameter("@BOMID", item.BOMID));
                                        dCmd.Parameters.Add(new SqlParameter("@BOMItemsID", item.BOMItemID));
                                        dCmd.Parameters.Add(new SqlParameter("@Qty", item.Qty));
                                        dCmd.Parameters.Add(new SqlParameter("@ItemPrice", item.ItemPrice));
                                        dCmd.Parameters.Add(new SqlParameter("@Price", item.Price));
                                        dCmd.Parameters.Add(new SqlParameter("@CustomDescription", item.CustomDescription));
                                        dCmd.Parameters.Add(new SqlParameter("@CustomCode", item.MatthewsCode));
                                        dCmd.Parameters.Add(new SqlParameter("@Discount", item.Discount));
                                        dCmd.Parameters.Add(new SqlParameter("@FinalAgreedPrice", item.FinalAgreedPrice));
                                        dCmd.Parameters.Add(new SqlParameter("@IsDiscountApply", item.IsDiscountApply));
                                        dCmd.Parameters.Add(new SqlParameter("@PriceAfterDiscount", item.AfterDiscount));
                                        dCmd.Parameters.Add(new SqlParameter("@MaximumQty", item.MaximumQty));
                                        dCmd.Parameters.Add(new SqlParameter("@State", item.State));
                                        dCmd.Parameters.Add(new SqlParameter("@IsInTotal", item.IsInTotal));
                                        dCmd.Parameters.Add(new SqlParameter("@IsDecimalAllowed", item.IsDecimalAllowed));
                                    

                                    dCmd.ExecuteNonQuery();
                                    }

                                                                    

                                }
                            
                            conn.Close();
                            }
                   
                      

                

                return "";
            }
            catch (Exception ex)
            {

                return ex.Message ;
            }
                }
           
   #endregion SaveBOM
     }
}








