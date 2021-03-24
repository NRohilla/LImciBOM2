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
using System.Data.Linq;
using System.Data.Linq.Mapping;
namespace DataLibrary
{
    public class QuoteBOMDataAccess
    {
        public static string GetConnectionString(string ConnectionName = "OnlineBOMEntities")
        {
            return Utility.UtilityFunctions.ReturnFormattedConnectionString(ConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString);
        }

        #region GetEditBOMByQuoteItemMasterID
        public DL_OpportunityBOMItemsViewModel Get_OpportunityBOMItemsByOpportunityID(int OpportunityID, int BOMID, bool NewBOM, int State, int versionnum)
        {
            DL_OpportunityBOMItemsViewModel QuoteBOMView = new DL_OpportunityBOMItemsViewModel();
            DL_OpportunityBOMItem BOM = new DL_OpportunityBOMItem();
            List<DL_OpportunityBOMItem> BOMlst = new List<DL_OpportunityBOMItem>();

            var context = new DataLibrary.DBEntity.OnlineBOMEntities();

            if (!NewBOM)
            {
                //EDIT scope for the saved BOMs
                //Get the OppBomList based on major params
                var _objGetOppBomList = context.OpportunityBOMLists.Where(p => p.OpportunityID == OpportunityID && p.BOMID == BOMID).ToList();

                if (versionnum > 0)
                    _objGetOppBomList.Where(p => p.VersionNum == versionnum).ToList();

                foreach (var item in _objGetOppBomList)
                {
                    var _ObjBOM = context.BOMs.Where(p => p.ID == item.BOMID).FirstOrDefault();
                    var _ObjBOMItem = context.BOMItems.Where(p => p.RECID == item.BOMItemsID).FirstOrDefault();
                    var _ObjPartInfo = context.PartToCategories.Where(p => p.PartID == item.BOMItemsID).FirstOrDefault();
                    var _ObjBOMTemplate = context.BOMTemplates.Where(p => p.BOMItem_ID == _ObjBOMItem.RECID).FirstOrDefault();
                    var _ObjOppurtunity = context.Opportunities.Where(p => p.ID == item.OpportunityID).FirstOrDefault();
                    Decimal _StockInHand = context.Database.SqlQuery<Decimal>("Select AVAILPHYSICAL from InventoryOnHand where ITEMID='" + _ObjBOMItem.ITEMID + "'")
                                .FirstOrDefault();

                    BOMlst.Add(new DL_OpportunityBOMItem
                    {
                        OpportunityBOMListID = item.ID,
                        OpportunityID = item.OpportunityID,
                        BOMID = item.BOMID,
                        VersionNum = Convert.ToInt32(item.VersionNum),
                        BOMItemID = item.BOMItemsID,
                        ItemPrice = item.Price,
                        Price = item.IsInTotal ? (item.Price * item.Qty) : 0,
                        Qty = item.Qty,
                        Description = (_ObjBOMItem != null ? _ObjBOMItem.DESCRIPTION : string.Empty),
                        Category = _ObjPartInfo != null ? (_ObjPartInfo.ItemCategory != null ? _ObjPartInfo.ItemCategory.Category : string.Empty) : string.Empty,
                        CategoryOrder = _ObjPartInfo != null ? (_ObjPartInfo.ItemCategory != null ? Convert.ToInt32(_ObjPartInfo.ItemCategory.ItemOrder) : 0) : 0,
                        SubCategory = _ObjPartInfo != null ? (_ObjPartInfo.ItemSubCategory != null ? _ObjPartInfo.ItemSubCategory.Name : string.Empty) : string.Empty,
                        SubCategoryOrder = _ObjPartInfo != null ? (_ObjPartInfo.ItemSubCategory != null ? Convert.ToInt32(_ObjPartInfo.ItemSubCategory.ItemOrder) : 0) : 0,
                        MatthewsCode = _ObjBOMItem != null ? _ObjBOMItem.ITEMID : string.Empty,
                        IsCustomParts = _ObjBOM != null ? Convert.ToBoolean(_ObjBOM.IsCustomParts) : false,
                        FinalAgreedPrice = Convert.ToDecimal(item.FinalAgreedPrice),
                        Discount = Convert.ToDecimal(item.Discount),
                        IsDiscountApply = Convert.ToBoolean(item.IsDiscountApply),
                        IsQtyFixed = _ObjBOMTemplate != null ? _ObjBOMTemplate.IsQtyFixed : false,
                        PriceAfterDiscount = Convert.ToDecimal(item.PriceAfterDiscount),
                        AfterDiscount = Convert.ToDecimal(((item.Price) - (item.Price / 100) * item.Discount) * item.Qty),
                        BOM = _ObjBOM != null ? _ObjBOM.BOM1 : string.Empty,
                        MaximumQty = Convert.ToDecimal(item.MaximumQty),
                        ClosedDate = _ObjOppurtunity != null ? Convert.ToString(_ObjOppurtunity.ClosedDate) : string.Empty,
                        IsInTotal = item.IsInTotal,
                        IsDecimalAllowed = item.IsDecimalAllowed,
                        InkUsage = _ObjOppurtunity != null ? _ObjOppurtunity.InkUsage : string.Empty,
                        Stock = Convert.ToInt32(_StockInHand)
                    });
                }
            }
            else
            {
                //Get items from the BOM Template
                var _ObjGetBOMTemplateItems = context.BOMTemplates.Where(p => p.BOM_ID == BOMID).ToList();
                foreach (var item in _ObjGetBOMTemplateItems)
                {
                    var _ObjBOM = context.BOMs.Where(p => p.ID == item.BOM_ID).FirstOrDefault();
                    var _ObjBOMItem = context.BOMItems.Where(p => p.RECID == item.BOMItem_ID).FirstOrDefault();
                    var _ObjPartInfo = context.PartToCategories.Where(p => p.PartID == item.BOMItem_ID).FirstOrDefault();
                    Decimal _StockInHand = 0;

                    if (_ObjBOMItem != null)
                        context.Database.SqlQuery<Decimal>("Select AVAILPHYSICAL from InventoryOnHand where ITEMID='" + _ObjBOMItem.ITEMID + "'")
                                    .FirstOrDefault();

                    BOMlst.Add(new DL_OpportunityBOMItem
                    {
                        OpportunityBOMListID = item.ID,
                        OpportunityID = 0,
                        BOMID = item.BOM_ID,
                        VersionNum = 0,
                        BOMItemID = item.BOMItem_ID,
                        ItemPrice = item.Price,
                        Price = item.IsInTotal ? (item.Price * item.Quantity) : 0,
                        Qty = item.Quantity,
                        Description = (_ObjBOMItem != null ? _ObjBOMItem.DESCRIPTION : string.Empty),
                        Category = _ObjPartInfo != null ? (_ObjPartInfo.ItemCategory != null ? _ObjPartInfo.ItemCategory.Category : string.Empty) : string.Empty,
                        CategoryOrder = _ObjPartInfo != null ? (_ObjPartInfo.ItemCategory != null ? Convert.ToInt32(_ObjPartInfo.ItemCategory.ItemOrder) : 0) : 0,
                        SubCategory = _ObjPartInfo != null ? (_ObjPartInfo.ItemSubCategory != null ? _ObjPartInfo.ItemSubCategory.Name : string.Empty) : string.Empty,
                        SubCategoryOrder = _ObjPartInfo != null ? (_ObjPartInfo.ItemSubCategory != null ? Convert.ToInt32(_ObjPartInfo.ItemSubCategory.ItemOrder) : 0) : 0,
                        MatthewsCode = _ObjBOMItem != null ? _ObjBOMItem.ITEMID : string.Empty,
                        IsCustomParts = _ObjBOM != null ? Convert.ToBoolean(_ObjBOM.IsCustomParts) : false,
                        FinalAgreedPrice = 0,
                        Discount = 0,
                        IsDiscountApply = Convert.ToBoolean(item.IsDiscountApply),
                        IsQtyFixed = item.IsQtyFixed,
                        PriceAfterDiscount = 0,
                        AfterDiscount = 0,
                        BOM = _ObjBOM != null ? _ObjBOM.BOM1 : string.Empty,
                        MaximumQty = Convert.ToDecimal(item.MaximumQty),
                        ClosedDate = string.Empty,
                        IsInTotal = item.IsInTotal,
                        IsDecimalAllowed = item.IsDecimalAllowed,
                        InkUsage = string.Empty,
                        Stock = Convert.ToInt32(_StockInHand)
                    });
                }
            }
            QuoteBOMView.BOMListViewModel = BOMlst;
            return QuoteBOMView;
            /*
            ////using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            ////{
            ////    //return cnn.Query<T>(sql).ToList();
            ////    conn.Open();
            ////    DataSet ds = new DataSet();
            ////    string SQLSP;

            ////    { SQLSP = "Get_OpportunityBOMItemsByOpportunityID"; }
            ////    SqlCommand dCmd = new SqlCommand(SQLSP, conn);
            ////    dCmd.CommandType = CommandType.StoredProcedure;
            ////    dCmd.Parameters.Add(new SqlParameter("@OpportunityID", OpportunityID));
            ////    dCmd.Parameters.Add(new SqlParameter("@BOMID", BOMID));
            ////    dCmd.Parameters.Add(new SqlParameter("@NewBOM", NewBOM));
            ////    dCmd.Parameters.Add(new SqlParameter("@State", State));
            ////    dCmd.Parameters.Add(new SqlParameter("@versionnum", versionnum));
            ////    SqlDataAdapter da = new SqlDataAdapter(dCmd);
            ////    DataTable dt = new DataTable();
            ////    ds.Clear();

            ////    da.Fill(dt);
            ////    conn.Close();


            ////    if (dt != null && dt.Rows.Count > 0)
            ////    {

            ////        foreach (DataRow dr in dt.Rows)
            ////        {
            ////            //DL_OpportunityBOMItem BOM = new DL_OpportunityBOMItem();
            ////            //BOM.OpportunityID = Convert.ToInt32(dr["OpportunityID"].ToString());
            ////            //BOM.BOMID = Convert.ToInt32(dr["BOMID"].ToString());
            ////            //BOM.BOMItemID = Convert.ToInt64(dr["BOMItemsID"].ToString());
            ////            //BOM.Description = dr["Description"].ToString();
            ////            //BOM.ItemPrice = Convert.ToDecimal(dr["ItemPrice"].ToString());
            ////            //BOM.Price = Convert.ToDecimal(dr["Price"].ToString());
            ////            //BOM.Qty = Convert.ToDecimal(dr["Qty"].ToString());
            ////            //BOM.Category = dr["Category"].ToString();
            ////            //BOM.CategoryOrder = Convert.ToString(dr["CategoryOrder"]).Trim().Length > 0 ? Convert.ToInt32(dr["CategoryOrder"]) : 0;
            ////            //BOM.SubCategory = dr["SubCategory"].ToString();
            ////            //BOM.SubCategoryOrder = Convert.ToString(dr["SubCategoryOrder"]).Trim().Length > 0 ? Convert.ToInt32(dr["SubCategoryOrder"]) : 0;
            ////            //BOM.MatthewsCode = dr["ITEMID"].ToString();
            ////            //BOM.OpportunityBOMListID = Convert.ToInt32(dr["OpportunityBOMListID"].ToString());
            ////            //BOM.IsCustomParts = Convert.ToBoolean(dr["IsCustomParts"].ToString());
            ////            //BOM.Discount = Convert.ToDecimal(dr["Discount"].ToString());
            ////            //BOM.FinalAgreedPrice = Convert.ToDecimal(dr["Finalagreedprice"].ToString());
            ////            //BOM.IsQtyFixed = Convert.ToBoolean(dr["IsQtyFixed"].ToString());
            ////            //BOM.PriceAfterDiscount = Convert.ToDecimal(dr["PriceAfterDiscount"].ToString());
            ////            //BOM.BOM = dr["BOM"].ToString();
            ////            //BOM.MaximumQty = Convert.ToDecimal(dr["MaximumQty"].ToString());
            ////            //BOM.ClosedDate = dr["ClosedDate"].ToString();
            ////            //BOM.Stock = Convert.ToInt32(dr["Stock"].ToString());
            ////            //BOM.State = State;
            ////            //BOM.IsInTotal = Convert.ToBoolean(dr["IsInTotal"].ToString());
            ////            //BOM.IsDecimalAllowed = Convert.ToBoolean(dr["IsDecimalAllowed"].ToString());

            ////            //if (dr["IsDiscountApply"].ToString() != null && dr["IsDiscountApply"].ToString() != "")
            ////            //{ BOM.IsDiscountApply = Convert.ToBoolean(dr["IsDiscountApply"].ToString()); }
            ////            //else { BOM.IsDiscountApply = false; }

            ////            //if (BOM.IsDiscountApply == true)
            ////            //{
            ////            //    BOM.AfterDiscount = ((BOM.ItemPrice) - (BOM.ItemPrice / 100) * BOM.Discount) * BOM.Qty;
            ////            //}
            ////            //else
            ////            //{
            ////            //    BOM.AfterDiscount = (BOM.ItemPrice * BOM.Qty);
            ////            //}

            ////            //BOM.InkUsage = dr["InkUsage"].ToString();

            ////            //BOMlst.Add(BOM);

            ////        }
            ////        QuoteBOMView.BOMListViewModel = BOMlst;

            ////    }
            ////    return QuoteBOMView;
            ////}*/
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
        public string SaveBom(List<DL_OpportunityBOMItem> BOMList, int VersionNum)
        {
            try
            {
                var context = new DataLibrary.DBEntity.OnlineBOMEntities();
                int result = 0;

                //Update the records; EDIT BOM CASE
                foreach (var item in BOMList)
                {
                    if (VersionNum > 0)
                    {
                        var GetOppBomList = context.OpportunityBOMLists.Where(p => p.ID == item.OpportunityBOMListID).FirstOrDefault();
                        if (GetOppBomList != null)
                        {
                            GetOppBomList.Discount = item.Discount;
                            GetOppBomList.FinalAgreedPrice = item.FinalAgreedPrice;
                            GetOppBomList.ItemPrice = item.ItemPrice;
                            GetOppBomList.Price = item.Price;
                            GetOppBomList.PriceAfterDiscount = item.PriceAfterDiscount;
                            GetOppBomList.MaximumQty = item.MaximumQty;

                            var GetOppRecord = context.Opportunities.Where(p => p.ID == item.OpportunityID).FirstOrDefault();
                            if (GetOppRecord != null)
                                GetOppRecord.InkUsage = item.InkUsage;
                        }
                    }
                    else
                    {
                        //insert the records;; NEW BOM CASE
                        context.OpportunityBOMLists.Add(new DBEntity.OpportunityBOMList
                        {
                            OpportunityID = item.OpportunityID,
                            BOMID = item.BOMID,
                            Price = item.Price,
                            BOMItemsID = item.BOMItemID,
                            CreatedDateTime = DateTime.Now,
                            CustomCode = item.CustomCode,
                            CustomDescription = item.CustomDescription,
                            Discount = item.Discount,
                            FinalAgreedPrice = item.FinalAgreedPrice,
                            IsActive = true,
                            IsDecimalAllowed = item.IsDecimalAllowed,
                            IsDeleted = false,
                            IsDiscountApply = item.IsDiscountApply,
                            IsInTotal = item.IsInTotal,
                            ItemPrice = item.ItemPrice,
                            MaximumQty = item.MaximumQty,
                            PriceAfterDiscount = 0,
                            Qty = item.Qty,
                            State = item.State,
                            UpdatedDatetime = DateTime.Now,
                            VersionNum = 1
                        });

                    }
                    result += context.SaveChanges();
                }
                return "";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        #endregion SaveBOM
    }
}








