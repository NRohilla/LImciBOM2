using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DataLibrary.Model.Quote;
using DataLibrary.Model.QuoteBOM;



namespace DataLibrary
{
    public class QuoteDataAccess
    {
        public static string GetConnectionString(string ConnectionName = "OnlineBOMEntities")
        {
            return Utility.UtilityFunctions.ReturnFormattedConnectionString(ConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString);
        }
        #region 

        public bool CreateLead(string CustomerName, DateTime QuoteDate, string QuoteBy, string QuoteNumber, int NoOfWeeks, DateTime DeliveryDate, string PONumber, string DispatchAddress, string DispatchName, string Title, string PhoneNumber, string Email, string ReferenceNo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {


                    DataSet ds = new DataSet();

                    SqlCommand dCmd = new SqlCommand("CreateLead", conn);
                    dCmd.CommandType = CommandType.StoredProcedure;
                    dCmd.Parameters.Add(new SqlParameter("@CustomerName", CustomerName));
                    dCmd.Parameters.Add(new SqlParameter("@LeadDate", QuoteDate));
                    dCmd.Parameters.Add(new SqlParameter("@LeadBy", QuoteBy));
                    dCmd.Parameters.Add(new SqlParameter("@LeadNumber", QuoteNumber));
                    dCmd.Parameters.Add(new SqlParameter("@NoOfWeeks", NoOfWeeks));
                    dCmd.Parameters.Add(new SqlParameter("@DeliveryDate", DeliveryDate));
                    dCmd.Parameters.Add(new SqlParameter("@PONumber", PONumber));
                    dCmd.Parameters.Add(new SqlParameter("@DispatchAddress", DispatchAddress));
                    dCmd.Parameters.Add(new SqlParameter("@DispatchName", DispatchName));
                    dCmd.Parameters.Add(new SqlParameter("@Title", Title));
                    dCmd.Parameters.Add(new SqlParameter("@PhoneNumber", PhoneNumber));
                    dCmd.Parameters.Add(new SqlParameter("@Email", Email));
                    dCmd.Parameters.Add(new SqlParameter("@CRMNumber", ReferenceNo));
                    conn.Open();
                    dCmd.ExecuteNonQuery();
                    conn.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {

                return false;
            }

        }

        public string Update_OpportunityCustomerDetails(DL_OpportunityModel customer)
        {
            try
            {


                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {

                    DataSet ds = new DataSet();

                    SqlCommand dCmd = new SqlCommand("Update_OpportunityCustomerDetails", conn);
                    dCmd.CommandType = CommandType.StoredProcedure;

                    dCmd.Parameters.Add(new SqlParameter("@QuoteNo", customer.QuoteNo));
                    dCmd.Parameters.Add(new SqlParameter("@DispatchAddress", customer.DispatchAddress));
                    dCmd.Parameters.Add(new SqlParameter("@AccountContactName", customer.AccountContactName));
                    dCmd.Parameters.Add(new SqlParameter("@AccountContactTitle", customer.AccountContactTitle));
                    dCmd.Parameters.Add(new SqlParameter("@AccountContactPhoneNo", customer.AccountContactPhoneNo));
                    dCmd.Parameters.Add(new SqlParameter("@AccountContactEmail", customer.AccountContactEmail));
                    dCmd.Parameters.Add(new SqlParameter("@FinanceDeal", customer.FinanceDeal));
                    dCmd.Parameters.Add(new SqlParameter("@FinanceType", customer.FinanceType));
                    dCmd.Parameters.Add(new SqlParameter("@FinanceApproved", customer.FinanceApproved));
                    dCmd.Parameters.Add(new SqlParameter("@FinanceTotalAmount", customer.FinanceTotalAmount));
                    dCmd.Parameters.Add(new SqlParameter("@FinancePeriod", customer.FinancePeriod));

                    conn.Open();
                    dCmd.ExecuteNonQuery();
                    conn.Close();
                    return "";
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }

        }

        public string Update_OpportunityAssemblyDetails(DL_OpportunityModel Assembly)
        {
            try
            {


                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {

                    DataSet ds = new DataSet();

                    SqlCommand dCmd = new SqlCommand("Update_OpportunityAssemblyDetails", conn);
                    dCmd.CommandType = CommandType.StoredProcedure;

                    dCmd.Parameters.Add(new SqlParameter("@QuoteNo", Assembly.QuoteNo));
                    dCmd.Parameters.Add(new SqlParameter("@CompanyName", Assembly.CompanyName));
                    dCmd.Parameters.Add(new SqlParameter("@Representative", Assembly.Representative));
                    dCmd.Parameters.Add(new SqlParameter("@CustomerType", Assembly.CustomerType));
                    if (Assembly.DeliveryDate.Year != 1)
                    { dCmd.Parameters.Add(new SqlParameter("@DeliveryDate", Assembly.DeliveryDate)); }
                    else { dCmd.Parameters.Add(new SqlParameter("@DeliveryDate", null)); }

                    dCmd.Parameters.Add(new SqlParameter("@PONumber", Assembly.PONumber));
                    dCmd.Parameters.Add(new SqlParameter("@Authorisation", Assembly.Authorisation));
                    dCmd.Parameters.Add(new SqlParameter("@Campaign", Assembly.Campaign));
                    dCmd.Parameters.Add(new SqlParameter("@CampaignCode", Assembly.CampaignCode));
                    dCmd.Parameters.Add(new SqlParameter("@SalesPerson", Assembly.SalesPerson));
                    dCmd.Parameters.Add(new SqlParameter("@SaleTypeID", Assembly.SaleTypeID));

                    conn.Open();
                    dCmd.ExecuteNonQuery();
                    conn.Close();
                    return "";
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }

        }

        public string Update_OpportunityConsumableDetails(DL_OpportunityModel consu)
        {
            try
            {


                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {

                    DataSet ds = new DataSet();

                    SqlCommand dCmd = new SqlCommand("Update_OpportunityConsumableDetails", conn);
                    dCmd.CommandType = CommandType.StoredProcedure;

                    dCmd.Parameters.Add(new SqlParameter("@QuoteNo", consu.QuoteNo));
                    dCmd.Parameters.Add(new SqlParameter("@InkUsage", consu.InkUsage));
                    dCmd.Parameters.Add(new SqlParameter("@SolventUsage", consu.SolventUsage));
                    dCmd.Parameters.Add(new SqlParameter("@Comments", consu.Comments));

                    conn.Open();
                    dCmd.ExecuteNonQuery();
                    conn.Close();
                    return "";
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }

        }

        public string Update_OpportunityCHOPComments(DL_OpportunityModel chop)
        {
            try
            {


                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {

                    DataSet ds = new DataSet();

                    SqlCommand dCmd = new SqlCommand("Update_OpportunityCHOPComments", conn);
                    dCmd.CommandType = CommandType.StoredProcedure;

                    dCmd.Parameters.Add(new SqlParameter("@QuoteNo", chop.QuoteNo));
                    dCmd.Parameters.Add(new SqlParameter("@CHOPComments", chop.CHOPComments));


                    conn.Open();
                    dCmd.ExecuteNonQuery();
                    conn.Close();
                    return "";
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }

        }

        public string Update_OpportunityPMComments(DL_OpportunityModel pm)
        {
            try
            {


                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {

                    DataSet ds = new DataSet();

                    SqlCommand dCmd = new SqlCommand("Update_OpportunityPMComments", conn);
                    dCmd.CommandType = CommandType.StoredProcedure;

                    dCmd.Parameters.Add(new SqlParameter("@QuoteNo", pm.QuoteNo));
                    dCmd.Parameters.Add(new SqlParameter("@PMComments", pm.PMComments));


                    conn.Open();
                    dCmd.ExecuteNonQuery();
                    conn.Close();
                    return "";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public string Update_OpportunityTerritorySplit(DL_OpportunityModel TS)
        {
            try
            {


                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {

                    DataSet ds = new DataSet();

                    SqlCommand dCmd = new SqlCommand("Update_OpportunityTerritorySplit", conn);
                    dCmd.CommandType = CommandType.StoredProcedure;

                    dCmd.Parameters.Add(new SqlParameter("@QuoteNo", TS.QuoteNo));
                    dCmd.Parameters.Add(new SqlParameter("@Territory1ID", TS.Territory1ID));
                    dCmd.Parameters.Add(new SqlParameter("@Territory2ID", TS.Territory2ID));
                    dCmd.Parameters.Add(new SqlParameter("@Territory1Split", TS.Territory1Split));
                    dCmd.Parameters.Add(new SqlParameter("@Territory2Split", TS.Territory2Split));

                    conn.Open();
                    dCmd.ExecuteNonQuery();
                    conn.Close();
                    return "";
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }

        }


        public string Save_SaveProjectFinancials(DL_BOMFinancialReiewModel FS)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    DataSet ds = new DataSet();
                    SqlCommand dCmd = new SqlCommand("Insert_ProjectMilestones", conn);
                    dCmd.CommandType = CommandType.StoredProcedure;

                    dCmd.Parameters.Add(new SqlParameter("@OpportunityID", FS.OpportunityID));
                    dCmd.Parameters.Add(new SqlParameter("@BOMID", FS.BOMID));
                    dCmd.Parameters.Add(new SqlParameter("@DepositPerc", FS.DepositPerc));
                    dCmd.Parameters.Add(new SqlParameter("@Deposit", FS.Deposit));
                    dCmd.Parameters.Add(new SqlParameter("@PreDeliveryPerc", FS.PreDeliveryPerc));
                    dCmd.Parameters.Add(new SqlParameter("@PreDelivery", FS.PreDelivery));
                    dCmd.Parameters.Add(new SqlParameter("@FinalPerc", FS.FinalPerc));
                    dCmd.Parameters.Add(new SqlParameter("@Final", FS.Final));

                    conn.Open();
                    dCmd.ExecuteNonQuery();
                    conn.Close();
                    return "";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public DL_QuoteViewModel LoadQuoteList()
        {
            DL_QuoteViewModel QuoteList = new DL_QuoteViewModel();

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                //return cnn.Query<T>(sql).ToList();
                conn.Open();
                DataSet ds = new DataSet();

                SqlCommand dCmd = new SqlCommand("Get_OpportunityList", conn);
                dCmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(dCmd);
                DataTable dt = new DataTable();
                ds.Clear();

                da.Fill(dt);
                conn.Close();

                if (dt != null && dt.Rows.Count > 0)
                {
                    QuoteList.QuoteCustomers = new List<DL_OpportunityModel>();

                    foreach (DataRow dr in dt.Rows)
                    {
                        DL_OpportunityModel x = new DL_OpportunityModel();
                        x.Opportunity = dr["opportunity"].ToString();
                        x.CompanyName = dr["Name"].ToString();
                        x.QuoteNo = dr["map_QuoteNumber"].ToString();
                        x.SalesPerson = dr["cvn_SalespersonName"].ToString();
                        QuoteList.QuoteCustomers.Add(x);
                    }
                }
                //var das = ds.Tables[0].AsEnumerable();

                return QuoteList;

            }
        }


        //filling tabs on Quote Details page
        public DL_QuoteViewModel GetOpportunityByQuoteNo(string QuoteNo)
        {
            DL_QuoteViewModel QuoteList = new DL_QuoteViewModel();

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                //return cnn.Query<T>(sql).ToList();
                conn.Open();
                DataSet ds = new DataSet();

                SqlCommand dCmd = new SqlCommand("Get_OpportunityByQuoteNo", conn);
                dCmd.CommandType = CommandType.StoredProcedure;
                dCmd.Parameters.Add(new SqlParameter("@QuoteNo", QuoteNo));
                //dCmd.Parameters.Add(new SqlParameter("@parameter1", parameter1));
                SqlDataAdapter da = new SqlDataAdapter(dCmd);
                DataTable dt = new DataTable();
                ds.Clear();
                da.Fill(dt);
                conn.Close();
                DL_QuoteViewModel x = new DL_QuoteViewModel();

                if (dt != null && dt.Rows.Count > 0)
                {

                    DataRow dr = dt.Rows[0];
                    x.ID = Convert.ToInt32(dr["ID"].ToString());
                    x.Opportunity = dr["Opportunity"].ToString();
                    x.ClosedDate = dr["ClosedDate"].ToString();
                    x.Representative = dr["Representative"].ToString();
                    x.CompanyName = dr["CompanyName"].ToString();
                    x.CustomerType = dr["CustomerType"].ToString();
                    //if (dr["LeadTime"].ToString() != "0")
                    //{ x.DeliveryDate = Convert.ToDateTime(dr["DeliveryDate"].ToString()); }
                    //else
                    //{
                    //    x.DeliveryDate = DateTime.Now;
                    //}
                    if (dr["DeliveryDate"].ToString() == "")
                    {
                        x.DeliveryDate = DateTime.Now;
                    }
                    else
                    {
                        x.DeliveryDate = Convert.ToDateTime(dr["DeliveryDate"].ToString());
                    }
                    x.CalcDeliveryDate = Convert.ToDateTime(dr["CalcDeliveryDate"].ToString());
                    x.QuoteNo = dr["QuoteNo"].ToString();
                    x.PONumber = dr["PONumber"].ToString();
                    x.Authorisation = dr["Authorisation"].ToString();
                    x.Campaign = dr["Campaign"].ToString();
                    x.CampaignCode = dr["CampaignCode"].ToString();
                    x.Territory1ID = dr["Territory1ID"].ToString();
                    x.Territory2ID = dr["Territory2ID"].ToString();
                    if (dr["Territory1Split"].ToString() != "")
                    { x.Territory1Split = Convert.ToInt32(dr["Territory1Split"].ToString()); }
                    else
                    { x.Territory1Split = 0; }
                    if (dr["Territory2Split"].ToString() != "")
                    { x.Territory2Split = Convert.ToInt32(dr["Territory2Split"].ToString()); }
                    else
                    { x.Territory2Split = 0; }
                    x.DispatchAddress = dr["DispatchAddress"].ToString();
                    x.AccountContactName = dr["AccountContactName"].ToString();
                    x.AccountContactTitle = dr["AccountContactTitle"].ToString();
                    x.AccountContactPhoneNo = dr["AccountContactPhoneNo"].ToString();
                    x.AccountContactEmail = dr["AccountContactEmail"].ToString();
                    x.FinanceDeal = dr["FinanceDeal"].ToString();
                    x.FinanceType = dr["FinanceType"].ToString();
                    x.FinanceApproved = dr["FinanceApproved"].ToString();
                    if (dr["FinanceTotalAmount"].ToString() != "")
                    { x.FinanceTotalAmount = Convert.ToDecimal(dr["FinanceTotalAmount"].ToString()); }
                    else { x.FinanceTotalAmount = 0; }
                    if (dr["FinancePeriod"].ToString() == "" || dr["FinancePeriod"].ToString() == null)
                    { x.FinancePeriod = 0; }
                    else { x.FinancePeriod = Convert.ToInt32(dr["FinancePeriod"].ToString()); }
                    x.InkUsage = dr["InkUsage"].ToString();
                    x.SolventUsage = dr["SolventUsage"].ToString();
                    x.Comments = dr["Comments"].ToString();
                    x.SalesPerson = dr["SalesPerson"].ToString();
                    x.CHOPComments = dr["CHOPComments"].ToString();
                    x.CustomerCode = dr["CustomerCode"].ToString();
                    if (dr["SaleTypeID"].ToString() != "")
                    { x.SaleTypeID = Convert.ToInt32(dr["SaleTypeID"].ToString()); }
                    else
                    { x.SaleTypeID = 0; }
                }

                /*conn.Open();
                dCmd = new SqlCommand("Get_OpportunityBOMListBYOpportunityID", conn);
                dCmd.CommandType = CommandType.StoredProcedure;
                dCmd.Parameters.Add(new SqlParameter("@OpportunityID", x.ID));
                //dCmd.Parameters.Add(new SqlParameter("@parameter1", parameter1));
                da = new SqlDataAdapter(dCmd);
                dt = new DataTable();

                ds.Clear();
                da.Fill(dt);
                conn.Close();

                List<DL_BOMListModel> QL = new List<DL_BOMListModel>();
                if (dt != null && dt.Rows.Count > 0)
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        DL_BOMListModel l = new DL_BOMListModel();
                        l.BOMID = Convert.ToInt32(dr["BOMID"].ToString());
                        l.Name = dr["BOM"].ToString();

                        //if (dr["OpportunityBOMListID"].ToString() != "") { l.OpportunityBOMListID = Convert.ToInt32(dr["OpportunityBOMListID"].ToString()); }
                        if (dr["OpportunityID"].ToString() != "") { l.OpportunityID = Convert.ToInt32(dr["OpportunityID"].ToString()); }
                        if (dr["TotalPrice"] != DBNull.Value) { l.TotalPrice = Convert.ToDecimal(dr["TotalPrice"].ToString()); } else { l.TotalPrice = 0; }

                        l.Discount = Convert.ToDecimal(dr["Discount"].ToString());
                        l.PriceAfterDiscount = Convert.ToDecimal(dr["PriceAfterDiscount"].ToString());
                        l.FinalAgreedPrice = Convert.ToDecimal(dr["FinalAgreedPrice"].ToString());
                        l.ClosedDate = dr["ClosedDate"].ToString();
                        QL.Add(l);
                    }
                    x.BOMListModel = QL;
                }
                else
                {
                    x.BOMListModel = QL;
                }
                 */

                conn.Open();
                dCmd = new SqlCommand("Get_Territory", conn);
                dCmd.CommandType = CommandType.StoredProcedure;
                da = new SqlDataAdapter(dCmd);
                dt = new DataTable();

                ds.Clear();
                da.Fill(dt);
                conn.Close();

                List<DL_Territory> TL = new List<DL_Territory>();

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        DL_Territory l = new DL_Territory();
                        l.ID = dr["SALESDISTRICTID"].ToString();
                        l.Description = dr["DESCRIPTION"].ToString();

                        TL.Add(l);
                    }
                    x.TerritoryList = TL;
                }
                else
                {
                    x.TerritoryList = TL;
                }

                //Get Sale Types
                conn.Open();
                dCmd = new SqlCommand("Get_SaleTypes", conn);
                dCmd.CommandType = CommandType.StoredProcedure;
                da = new SqlDataAdapter(dCmd);
                dt = new DataTable();

                ds.Clear();
                da.Fill(dt);
                conn.Close();

                List<DL_SaleType> SL = new List<DL_SaleType>();

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        DL_SaleType l = new DL_SaleType();
                        l.ID = dr["ID"].ToString();
                        l.SaleType = dr["SaleType"].ToString();

                        SL.Add(l);
                    }
                    x.SaleTypeList = SL;
                }
                else
                {
                    x.SaleTypeList = SL;
                }

                return x;

            }
        }

        public DL_BOMFinancialReiewModel GetAssemblyForBOMByOpportunityID(int OpportunityID, int BOMID)
        {
            DL_BOMFinancialReiewModel bfw = new DL_BOMFinancialReiewModel();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                //return cnn.Query<T>(sql).ToList();
                conn.Open();

                //------------Get the BOM Assemblies
                SqlCommand dCmd = new SqlCommand("Get_AssemblyForBOMByOpportunityID", conn);
                dCmd.CommandType = CommandType.StoredProcedure;
                dCmd.Parameters.Add(new SqlParameter("@OpportunityID", OpportunityID));
                dCmd.Parameters.Add(new SqlParameter("@BOMID", BOMID));
                SqlDataAdapter da = new SqlDataAdapter(dCmd);
                DataTable dt = new DataTable();

                da.Fill(dt);
                List<DL_BOMAssembly> lst = new List<DL_BOMAssembly>();

                if (dt != null && dt.Rows.Count > 0)
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        DL_BOMAssembly a = new DL_BOMAssembly();
                        a.AssemblyCode = dr["AssemblyCode"].ToString();
                        a.Area = dr["Area"].ToString();
                        a.Category = dr["Category"].ToString();
                        a.Device = dr["Device"].ToString();
                        if (dr["Price"].ToString() != null && dr["Price"].ToString() != "")
                        { a.Revenue = Convert.ToDecimal(dr["Price"].ToString()); }
                        else { a.Revenue = 0; }
                        if ((dr["Qty"].ToString() != null) && (dr["Qty"].ToString() != ""))
                        { a.Qty = Convert.ToDecimal(dr["Qty"].ToString()); }
                        else { a.Qty = 1; }
                        a.PMPercentage = Convert.ToDecimal(dr["PMPercentage"].ToString());
                        bfw.QuoteNo = dr["QuoteNo"].ToString();
                        lst.Add(a);
                    }
                }
                bfw.BOMAssembly = lst;


                //------------Get the BOM Totals 
                SqlCommand Cmd = new SqlCommand("Get_BOMPriceTotalsByOpportunityID", conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.Add(new SqlParameter("@OpportunityID", OpportunityID));
                Cmd.Parameters.Add(new SqlParameter("@BOMID", BOMID));
                SqlDataAdapter ad = new SqlDataAdapter(Cmd);
                DataTable t = new DataTable();
                ad.Fill(t);

                if (t != null && t.Rows.Count > 0)
                {
                    foreach (DataRow r in t.Rows)
                    {
                        bfw.BOMTotal = Convert.ToDecimal(r["TotalPrice"].ToString());
                        bfw.FinalAgreedPrice = Convert.ToDecimal(r["FinalAgreedPrice"].ToString());
                        bfw.PONumber = r["PONumber"].ToString();


                    }
                }

                //------------Get the BOM Totals 

                SqlCommand Cm = new SqlCommand("Get_PMFeeNotChargableByBOMID", conn);
                Cm.CommandType = CommandType.StoredProcedure;
                Cm.Parameters.Add(new SqlParameter("@BOMID", BOMID));
                SqlDataAdapter adp = new SqlDataAdapter(Cm);
                DataTable tbl = new DataTable();
                adp.Fill(tbl);

                conn.Close();
                List<DL_PMChargableAssemly> pm = new List<DL_PMChargableAssemly>();
                if (tbl != null && tbl.Rows.Count > 0)
                {

                    foreach (DataRow dr in tbl.Rows)
                    {
                        DL_PMChargableAssemly p = new DL_PMChargableAssemly();
                        p.AssemblyCode = dr["ITEMID"].ToString();
                        pm.Add(p);

                    }
                }
                bfw.PMChargableAssemly = pm;

                //------------Get Project Milestones 

                SqlCommand CmPM = new SqlCommand("Get_ProjectMilestones", conn);
                CmPM.CommandType = CommandType.StoredProcedure;
                CmPM.Parameters.Add(new SqlParameter("@OpportunityID", OpportunityID));
                CmPM.Parameters.Add(new SqlParameter("@BOMID", BOMID));

                SqlDataAdapter adM = new SqlDataAdapter(CmPM);
                DataTable tblM = new DataTable();
                adM.Fill(tblM);

                conn.Close();

                if (tblM != null && tblM.Rows.Count > 0)
                {
                    foreach (DataRow dr in tblM.Rows)
                    {
                        bfw.Deposit = Convert.ToDecimal(dr["Deposit"].ToString());
                        bfw.PreDelivery = Convert.ToDecimal(dr["PreDelivery"].ToString());
                        bfw.Final = Convert.ToDecimal(dr["Final"].ToString());
                        bfw.DepositPerc = Convert.ToDecimal(dr["DepositPerc"].ToString());
                        bfw.PreDeliveryPerc = Convert.ToDecimal(dr["PreDeliveryPerc"].ToString());
                        bfw.FinalPerc = Convert.ToDecimal(dr["FinalPerc"].ToString());

                    }

                }
                bfw.BOMID = BOMID;
                bfw.OpportunityID = OpportunityID;
                return bfw;

            }
        }

        //to add a new BOM
        public Boolean AddNewBOM(int BOMID, int oppurtunityID, string ActivateNew)
        {
            int ResultCount = 0;
            using (var context = new DataLibrary.DBEntity.OnlineBOMEntities())
            {
                //Get all the records of this BOM+OppID Family
                var GetAllOppBomList = context.OpportunityBOMLists.Where(p => p.BOMID == BOMID && p.OpportunityID == oppurtunityID).ToList();

                //Deactivate Old records
                if (ActivateNew.Trim().ToLower().Equals("yes"))
                    GetAllOppBomList.ForEach(a => a.IsActive = false);

                var GetMaxVersion = GetAllOppBomList.Max(p => p.VersionNum);// Find Maximum Current Version of this combi

                //Get BOM Template Items for insertion
                var GetBomItems = context.BOMTemplates.Where(p => p.BOM_ID == BOMID);

                foreach (var item in GetBomItems)
                {
                    //code to add New BOM
                    context.OpportunityBOMLists.Add(
                        new DataLibrary.DBEntity.OpportunityBOMList
                        {
                            BOMID = item.BOM_ID,
                            BOMItemsID = item.BOMItem_ID,
                            CreatedDateTime = System.DateTime.Now,
                            CustomCode = string.Empty,
                            CustomDescription = string.Empty,
                            Discount = 0,
                            FinalAgreedPrice = 0,

                            IsDecimalAllowed = item.IsDecimalAllowed,
                            IsDiscountApply = item.IsDiscountApply,
                            IsInTotal = item.IsInTotal,
                            ItemPrice = (item.IsInTotal == true ? item.Quantity * item.Price : 0),
                            MaximumQty = item.MaximumQty,
                            Price = (item.IsInTotal == true ? item.Quantity * item.Price : 0),
                            Qty = item.Quantity,

                            PriceAfterDiscount = 0,
                            OpportunityID = oppurtunityID,
                            State = 1,
                            UpdatedDatetime = System.DateTime.Now,
                            IsActive = (ActivateNew.Trim().ToLower().Equals("No".ToLower()) ? false : true),
                            IsDeleted = false,
                            VersionNum = GetMaxVersion + 1,
                        });
                }

                ResultCount = context.SaveChanges();
            }

            if (ResultCount > 0)
                return true;

            return false;

        }



        //private List<DL_OpportunityBOMItem> PopulateBOMDL(List<OpportunityBOMItem> BOMV)
        //{
        //    List<DL_OpportunityBOMItem> lstBOMDL = new List<DL_OpportunityBOMItem>();
        //    if (BOMV.Count > 0)
        //    {
        //        char[] charsToTrim = { ' ' };
        //        foreach (var item in BOMV)
        //        {

        //            if (item.Description != null)
        //            {
        //                DL_OpportunityBOMItem dl = new DL_OpportunityBOMItem();
        //                dl.OpportunityID = item.OpportunityID;
        //                dl.OpportunityBOMListID = item.OpportunityBOMListID;
        //                dl.BOMID = item;
        //                dl.LineItemDetailID = item.LineItemDetailID;
        //                dl.MatthewsCode = item.MatthewsCode.Trim(charsToTrim);
        //                dl.Qty = item.Qty;
        //                dl.ItemPrice = item.ItemPrice;
        //                dl.Price = item.Price;
        //                if (item.LineItemDetailID == 0)
        //                {

        //                    dl.CustomDescription = item.Description.Trim(charsToTrim);
        //                }
        //                lstBOMDL.Add(dl);

        //            }
        //        }

        //    }

        //    return lstBOMDL;
        //}
        #endregion
    }
}
