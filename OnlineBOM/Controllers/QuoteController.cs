using DataLibrary.Model.Quote;
using OnlineBOM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OnlineBOM.ReportDownload;
using Microsoft.AspNetCore.Hosting;
using DataLibrary.Model.QuoteBOM;

namespace OnlineBOM.Controllers
{
    public class QuoteController : Controller
    {


        public enum BOMState
        {
            Sales,
            PM
        }


        public ActionResult BOMDownloadPDF(int OpportunityID, int BOMID, bool PMView)
        {

            int State;
            if (PMView == false)
            { State = (int)BOMState.Sales; }
            else
            { State = (int)BOMState.PM; }

            PDFBOMList rep = new PDFBOMList();

            return File(rep.DownloadBOM(OpportunityID, BOMID, State), "application/pdf");

            //MemoryStream workStream = new MemoryStream();
            //Document document = new Document();
            //PdfWriter.GetInstance(document, workStream).CloseStream = false;

            //document.Open();
            //document.Add(new Paragraph("Here we go"));
            //document.Add(new Paragraph(DateTime.Now.ToString()));
            //document.Close();

            //byte[] byteInfo = workStream.ToArray();
            //workStream.Write(byteInfo, 0, byteInfo.Length);
            //workStream.Position = 0;
            //Response.Buffer = true;
            //Response.AddHeader("Content-Disposition", "attachment; filename= " + Server.HtmlEncode("abc.pdf"));
            //Response.ContentType = "APPLICATION/pdf";
            //Response.BinaryWrite(byteInfo);
            //return new FileStreamResult(workStream, "application/pdf");
        }


        public ActionResult PickingListDownload(int OpportunityID, int BOMID, bool PMView)
        {

            int State;
            if (PMView == false)
            { State = (int)BOMState.Sales; }
            else
            { State = (int)BOMState.PM; }

            PDFPickingSlip rep = new PDFPickingSlip();

            return File(rep.DownloadPickingSlip(OpportunityID, BOMID, State), "application/pdf");


        }

        public ActionResult Index(string QuoteNo = "")
        {
            if (QuoteNo == "")
            {
                DL_QuoteViewModel QuoteLst = new DL_QuoteViewModel();
                QuoteViewModel QuoteVM = new QuoteViewModel();
                QuoteBusinessLogic BL = new QuoteBusinessLogic();
                QuoteLst = BL.GetCustomersList();
                QuoteViewModel CustLst = PopulateQuoteViewModel(QuoteLst);
                return View(CustLst);
            }
            else
            {
                DL_QuoteViewModel view = new DL_QuoteViewModel();
                QuoteBusinessLogic CBL = new QuoteBusinessLogic();
                QuoteViewModel CustomerView = new QuoteViewModel();
                view = CBL.BL_GetOpportunityByQuoteNo(QuoteNo);

                QuoteViewModel ViewModel = PopulateBOMViewModel(view);

                return View("Edit", ViewModel);
            }
        }


        // GET The BOM List
        public ActionResult GetBOMList(int OpportunityID, int BOMID, string QuoteNo, string Name, bool NewBOM, bool ViewBOM, bool PMView, int VersionNum)
        {
            int State = (int)BOMState.PM;

            if (PMView == false)
                State = (int)BOMState.Sales;

            DL_OpportunityBOMItemsViewModel DLVM = new DL_OpportunityBOMItemsViewModel();
            QuoteBOMBusinessLogic BL = new QuoteBOMBusinessLogic();
            DLVM = BL.GetOpportunityBOMItemsByOpportunityID(OpportunityID, BOMID, NewBOM, State, VersionNum);
            OpportunityBOMItemsViewModel view = PopulateBOMList(DLVM, QuoteNo);
            view.ItemMasterName = Name;
            view.ViewBOM = ViewBOM;

            List<OpportunityBOMItem> _ObjTempList = new List<OpportunityBOMItem>();
            //Only enter records for this BOM
            foreach (var item in view.BOMListViewModel)
            {
                if (_ObjTempList.Where(p => p.BOMItemID == item.BOMItemID).FirstOrDefault() == null)
                {
                    _ObjTempList.Add(item);
                }
            }
            view.BOMListViewModel = _ObjTempList;

            List<string> Categories = new List<string>();
            Categories = view.BOMListViewModel.OrderBy(p => p.CategoryOrder).Select(p => p.Category).ToList();
            Categories = Categories.GroupBy(p => p).Select(g => g.First()).ToList();
            TempData["Categories"] = Categories;
            TempData["CountOfConsumable"] = view.BOMListViewModel.Where(p => p.Category.Equals("Consumables")).Count();

            return View("BOMList", view);
        }

        // GET The BOM List
        public ActionResult GetChildBOMList(int OpportunityID, string BOMitemID, int BOMID, int State)
        {

            DL_OpportunityBOMItemsViewModel DLVM = new DL_OpportunityBOMItemsViewModel();
            QuoteBOMBusinessLogic BL = new QuoteBOMBusinessLogic();
            DLVM = BL.GetOpportunityBOMChildItemsByBOMItemID(OpportunityID, BOMitemID, BOMID, State);
            OpportunityBOMItemsViewModel view = PopulateBOMList(DLVM, "");

            return Json(view.BOMListViewModel, JsonRequestBehavior.AllowGet);
        }

        // POST: QuoteBOM/Create
        [HttpPost]
        public ActionResult CreateBOM(List<OpportunityBOMItem> BOMList, int VersionNum)
        {
            try
            {
                if (BOMList.Count > 0)
                {
                    if (BOMList[0].FinalAgreedPrice == 0)
                        return Json("Final Agreed Price is Not Avialable", JsonRequestBehavior.AllowGet);

                    if (BOMList[0].Discount > 100)
                        return Json("Invalid Discount Percentage", JsonRequestBehavior.AllowGet);

                    if (BOMList[0].InkUsage == null)
                        return Json("Consumables, Ink Usage required", JsonRequestBehavior.AllowGet);

                    if (BOMList[0].InkUsage.Length < 10)
                        return Json("Consumables, Ink Usage required minimum of 10 Characters", JsonRequestBehavior.AllowGet);

                    QuoteBOMBusinessLogic bl = new QuoteBOMBusinessLogic();
                    List<DL_OpportunityBOMItem> BomDL = new List<DL_OpportunityBOMItem>();
                    BomDL = PopulateBOMDL(BOMList);
                    string Saved = bl.SaveQuoteBOM(BomDL,VersionNum);
                    return Json(Saved, JsonRequestBehavior.AllowGet);


                }
                else { return Json("Error Saving the Records", JsonRequestBehavior.AllowGet); }


            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }



        public JsonResult CreateLead(QuoteViewModel c)
        {
            var result = false;
            try
            {
                QuoteBusinessLogic BL = new QuoteBusinessLogic();
                //result = BL.BL_CreateLead(c.CustomerName, c.QuotedDate, c.QuotedBy, c.ReferenceNo, c.NoOfWeeks, c.DeliveryDate, c.PONumber, c.DispatchAddress, c.DispatchAddress, c.Title, c.PhoneNo, c.Email, c.ReferenceNo);

            }
            catch (Exception ex)
            {

                throw ex;

            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        // GET: Customer/Edit/5
        public ActionResult Edit(string QuoteNo)
        {
            DL_QuoteViewModel view = new DL_QuoteViewModel();
            QuoteBusinessLogic CBL = new QuoteBusinessLogic();
            QuoteViewModel CustomerView = new QuoteViewModel();
            view = CBL.BL_GetOpportunityByQuoteNo(QuoteNo);

            QuoteViewModel ViewModel = PopulateBOMViewModel(view);

            var context = new DataLibrary.DBEntity.OnlineBOMEntities();
            List<DataLibrary.Model.BOM.BOM> _objBOM = new List<DataLibrary.Model.BOM.BOM>();

            var GetAllBOMs = context.BOMs.ToList();//Get BOMs
            var ObjOp = context.Opportunities.Where(p => p.QuoteNo == QuoteNo).FirstOrDefault(); //Get Oppurtunity
            List<OnlineBOM.Models.BOMListModel> _objOppBom = new List<OnlineBOM.Models.BOMListModel>();

            if (ObjOp != null)
            {
                foreach (var item in GetAllBOMs)
                {
                    _objBOM.Add(new DataLibrary.Model.BOM.BOM
                    {
                        _ID = item.ID,
                        _BOM = item.BOM1,
                        _IsCustomParts = item.IsCustomParts
                    });

                    //Get All OPP+BOM records based on OppID
                    var GetOppBOMList = context.OpportunityBOMLists.Where(p => p.OpportunityID == ObjOp.ID && p.BOMID == item.ID && p.IsDeleted == false && p.IsInTotal == true).ToList();
                    if (GetOppBOMList.Count() <= 0)
                    {
                        _objOppBom.Add(new OnlineBOM.Models.BOMListModel
                        {
                            BOMID = item.ID,
                            Name = item.BOM1,
                            TotalPrice = 0,
                            OpportunityID = ObjOp.ID,
                            PriceAfterDiscount = 0,
                            FinalAgreedPrice = 0,
                            ClosedDate = Convert.ToString(ObjOp.ClosedDate),
                        });
                    }
                    else
                    {
                        var GetMaxState = GetOppBOMList.Max(p => p.State);//Get Max of State for the BOM Opp List
                        var GetTotalVersion = GetOppBOMList.Select(p => p.VersionNum).Distinct();

                        foreach (var itemBOMVersion in GetTotalVersion)
                        {
                            var GetBOMForVersion = GetOppBOMList.Where(p => p.VersionNum == itemBOMVersion).ToList();//Filter BOM + Opp for the vserion Number////&& p.State == GetMaxState

                            decimal GetPriceSum = GetBOMForVersion.Sum(p => p.Price);//total Price for this BOM
                            decimal GetDiscountSum = Convert.ToDecimal(GetBOMForVersion.Sum(p => p.Discount));//total Price for this BOM
                            decimal GetAfterDiscountSum = Convert.ToDecimal(GetBOMForVersion.Sum(p => p.PriceAfterDiscount));
                            decimal GetFinalAgreedSum = Convert.ToDecimal(GetBOMForVersion.Max(p => p.FinalAgreedPrice));

                            _objOppBom.Add(new OnlineBOM.Models.BOMListModel
                            {
                                BOMID = item.ID,
                                Name = item.BOM1,
                                TotalPrice = GetPriceSum,
                                Discount = GetDiscountSum,
                                OpportunityID = ObjOp.ID,
                                PriceAfterDiscount = GetAfterDiscountSum,
                                FinalAgreedPrice = GetFinalAgreedSum,
                                ClosedDate = Convert.ToString(ObjOp.ClosedDate),
                                VersionNum = Convert.ToInt32(itemBOMVersion)
                            });
                        }
                    }
                }
            }

            ViewModel.BOMListModel = _objOppBom;
            ViewBag._ObjBOM = _objBOM;
            ViewBag.Territorylist = ViewModel.TerritoryListModel;
            return View("Edit", ViewModel);
        }

        /*
         * select sum(price) as price, sum(discount) as discount,sum(PriceAfterDiscount) as PriceAfterDiscount,max(FinalAgreedPrice) as FinalAgreedPrice from OpportunityBOMList  
    where OpportunityID in (61)
    and versionnum=2
    and [State]= (select max([state]) from OpportunityBOMList where  OpportunityID in (61)  and versionnum=2)
    and IsInTotal=1
         */

        //Used for AJAX
        [HttpPost]
        public ActionResult DeleteBOM(int BOMID, int oppurtunityID, int VersionNum)
        {
            int ResultCount = 0;
            //Update the BOM for deletion
            using (var context = new DataLibrary.DBEntity.OnlineBOMEntities())
            {
                var GetAllOppBomList = context.OpportunityBOMLists.Where(p => p.BOMID == BOMID && p.OpportunityID == oppurtunityID && p.VersionNum == VersionNum).ToList();
                GetAllOppBomList.ForEach(p => p.IsActive = false);
                GetAllOppBomList.ForEach(p => p.IsDeleted = true);
                ResultCount = context.SaveChanges();
            }

            if (ResultCount > 0)
                return Json("BOM deleted Successfully", JsonRequestBehavior.AllowGet);

            return Json("Operation failed!", JsonRequestBehavior.AllowGet);
        }


        //Used for AJAX
        [HttpPost]
        public ActionResult AddNewBOM(int BOMID, int oppurtunityID, string ActivateNew)
        {
            bool IsActionCompleted = new QuoteBusinessLogic().BL_AddNewBOM(BOMID, oppurtunityID, ActivateNew);

            if (IsActionCompleted)
                return Json("BOM deleted Successfully", JsonRequestBehavior.AllowGet);

            return Json("Operation failed!", JsonRequestBehavior.AllowGet);
        }

        public ActionResult FinancialReviewModal(int OpportunityID, int BOMID)
        {
            BOMFinancialReviewModel FM = new BOMFinancialReviewModel();
            DL_BOMFinancialReiewModel DFM = new DL_BOMFinancialReiewModel();
            QuoteBusinessLogic BL = new QuoteBusinessLogic();
            DFM = BL.GetAssemblyForBOMByOpportunityID(OpportunityID, BOMID);
            FM = PopulateFinanceReviewViewModel(DFM);

            return PartialView(FM);
        }

        public ActionResult ProjectFinancials(int OpportunityID, int BOMID)
        {
            BOMFinancialReviewModel FM = new BOMFinancialReviewModel();
            DL_BOMFinancialReiewModel DFM = new DL_BOMFinancialReiewModel();
            QuoteBusinessLogic BL = new QuoteBusinessLogic();
            DFM = BL.GetAssemblyForBOMByOpportunityID(OpportunityID, BOMID);
            FM = PopulateFinanceReviewViewModel(DFM);

            return PartialView(FM);
        }

        public ActionResult Customer()
        {
            OpportunityCoverModel rec = new OpportunityCoverModel();
            return PartialView(rec);
        }

        [HttpPost]
        public ActionResult SaveCustomer(List<OpportunityCoverModel> Customer)
        {
            DL_OpportunityModel Cust = new DL_OpportunityModel();
            QuoteBusinessLogic BL = new QuoteBusinessLogic();
            Cust = PopulateCustomerDL(Customer);
            string Saved = BL.BL_UpdateOpportunityCustomerDetails(Cust);
            return Json(Saved, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Assembly()
        {
            OpportunityCoverModel rec = new OpportunityCoverModel();
            return PartialView(rec);
        }

        [HttpPost]
        public ActionResult SaveAssembly(List<OpportunityCoverModel> Assembly)
        {
            DL_OpportunityModel Cust = new DL_OpportunityModel();
            QuoteBusinessLogic BL = new QuoteBusinessLogic();
            Cust = PopulateAssemblyDL(Assembly);
            string Saved = BL.BL_UpdateOpportunityAssemblyDetails(Cust);
            return Json(Saved, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Consumables()
        {
            OpportunityCoverModel rec = new OpportunityCoverModel();
            return PartialView(rec);
        }

        [HttpPost]
        public ActionResult SaveConsumables(List<OpportunityCoverModel> Consumables)
        {
            DL_OpportunityModel cls = new DL_OpportunityModel();
            QuoteBusinessLogic BL = new QuoteBusinessLogic();
            cls = PopulateConsumableDL(Consumables);
            string Saved = BL.BL_UpdateOpportunityConsumableDetails(cls);
            return Json(Saved, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FinancialReview()
        {
            OpportunityCoverModel rec = new OpportunityCoverModel();
            return PartialView(rec);
        }

        public ActionResult CHOPComments()
        {
            OpportunityCoverModel rec = new OpportunityCoverModel();
            return PartialView(rec);
        }

        [HttpPost]
        public ActionResult SaveCHOPComments(List<OpportunityCoverModel> Chop)
        {
            DL_OpportunityModel cls = new DL_OpportunityModel();
            QuoteBusinessLogic BL = new QuoteBusinessLogic();
            cls = PopulateCHOPDL(Chop);
            string Saved = BL.BL_UpdateOpportunityCHOPComments(cls);
            return Json(Saved, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PMComments()
        {
            OpportunityCoverModel rec = new OpportunityCoverModel();
            return PartialView(rec);
        }

        [HttpPost]
        public ActionResult SavePMComments(List<OpportunityCoverModel> pm)
        {
            DL_OpportunityModel cls = new DL_OpportunityModel();
            QuoteBusinessLogic BL = new QuoteBusinessLogic();
            cls = PopulatePMDL(pm);
            string Saved = BL.BL_UpdateOpportunityPMComments(cls);
            return Json(Saved, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TerritorySplit()
        {
            OpportunityCoverModel rec = new OpportunityCoverModel();
            return PartialView(rec);
        }

        [HttpPost]
        public ActionResult SaveTerritorySplit(List<OpportunityCoverModel> Territory)
        {
            DL_OpportunityModel cls = new DL_OpportunityModel();
            QuoteBusinessLogic BL = new QuoteBusinessLogic();
            cls = PopulateTerritorySplitDL(Territory);
            string Saved = BL.BL_UpdateOpportunityTerritorySplit(cls);
            return Json(Saved, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult SaveProjectFinancials(List<BOMFinancialReviewModel> Financials)
        {

            decimal DepositPerc = Convert.ToDecimal(Financials[0].DepositPerc);
            decimal PreDPerc = Convert.ToDecimal(Financials[0].PreDeliveryPerc);
            decimal TotalPerc = DepositPerc + PreDPerc;
            string Saved = "";

            if (TotalPerc > 100)
            {
                Saved = "Project Milestones Exceeding 100%";
            }
            else
            {
                DL_BOMFinancialReiewModel cls = new DL_BOMFinancialReiewModel();
                QuoteBusinessLogic BL = new QuoteBusinessLogic();
                cls = PopulateFinancialReviewDL(Financials);
                Saved = BL.BL_SaveProjectFinancials(cls);
            }
            return Json(Saved, JsonRequestBehavior.AllowGet);
        }

        #region PopulateViews

        private BOMFinancialReviewModel PopulateFinanceReviewViewModel(DL_BOMFinancialReiewModel Assembly)
        {
            BOMFinancialReviewModel bl = new BOMFinancialReviewModel();
            List<FinancialBOMAssemly> fa = new List<FinancialBOMAssemly>();


            if (Assembly.BOMAssembly.Count > 0)
            {

                bl.FinalAgreedPrice = Assembly.FinalAgreedPrice;
                bl.BOMTotal = Assembly.BOMTotal;
                bl.CTOCerials = Assembly.CTOCerials;
                bl.QuoteNo = Assembly.QuoteNo;
                bl.Deposit = Assembly.Deposit;
                bl.PreDelivery = Assembly.PreDelivery;
                bl.Final = Assembly.Final;
                bl.PONumber = Assembly.PONumber;
                bl.DepositPerc = Assembly.DepositPerc;
                bl.PreDeliveryPerc = Assembly.PreDeliveryPerc;
                bl.FinalPerc = Assembly.FinalPerc;
                bl.BOMID = Assembly.BOMID;
                bl.OpportunityID = Assembly.OpportunityID;

                foreach (var item in Assembly.BOMAssembly)
                {
                    FinancialBOMAssemly b = new FinancialBOMAssemly();
                    b.AssemblyCode = item.AssemblyCode;
                    b.Area = item.Area;
                    b.Category = item.Category;
                    b.Device = item.Device;
                    b.Revenue = item.Revenue;
                    b.Qty = item.Qty;

                    fa.Add(b);
                }
                bl.BOMAssemly = fa;
            }
            return bl;
        }

        private List<DL_OpportunityBOMItem> PopulateBOMDL(List<OpportunityBOMItem> BOMV)
        {
            List<DL_OpportunityBOMItem> lstBOMDL = new List<DL_OpportunityBOMItem>();
            if (BOMV.Count > 0)
            {
                char[] charsToTrim = { ' ' };
                foreach (var item in BOMV)
                {


                    DL_OpportunityBOMItem dl = new DL_OpportunityBOMItem();
                    dl.OpportunityID = item.OpportunityID;
                    dl.OpportunityBOMListID = item.OpportunityBOMListID;
                    dl.BOMID = item.BOMID;
                    dl.BOMItemID = item.BOMItemID;
                    dl.Qty = item.Qty;
                    dl.ItemPrice = item.ItemPrice;
                    dl.Price = item.Price;
                    dl.Discount = item.Discount;
                    dl.FinalAgreedPrice = item.FinalAgreedPrice;
                    if (item.CustomDescription != null)
                    {
                        dl.CustomDescription = item.CustomDescription.Trim(charsToTrim);
                        dl.CustomCode = item.CustomCode;
                    }

                    dl.IsDiscountApply = item.IsDiscountApply;
                    dl.AfterDiscount = item.AfterDiscount;
                    dl.MaximumQty = item.MaximumQty;
                    dl.State = item.State;
                    if (item.IsInTotal == 1)
                    { dl.IsInTotal = true; }
                    else
                    { dl.IsInTotal = false; }

                    if (item.IsDecimalAllowed == 1)
                    { dl.IsDecimalAllowed = true; }
                    else
                    { dl.IsDecimalAllowed = false; }
                    dl.InkUsage = item.InkUsage;
                    lstBOMDL.Add(dl);

                }

            }
            return lstBOMDL;
        }


        private QuoteViewModel PopulateQuoteViewModel(DL_QuoteViewModel QuoteList)
        {
            QuoteViewModel ret = new QuoteViewModel();
            List<OpportunityCoverModel> Custlst = new List<OpportunityCoverModel>();

            if (QuoteList == null)
                return ret;

            foreach (var item in QuoteList.QuoteCustomers)
            {
                OpportunityCoverModel Model = new OpportunityCoverModel();
                Model.Opportunity = item.Opportunity;
                Model.CompanyName = item.CompanyName;
                Model.DeliveryDate = item.DeliveryDate;
                Model.QuoteNo = item.QuoteNo;
                Model.SalesPerson = item.SalesPerson;
                Custlst.Add(Model);
            }
            ret.QuoteCustomerListModel = Custlst;
            return ret;
        }

        private QuoteViewModel PopulateBOMViewModel(DL_QuoteViewModel view)
        {
            QuoteViewModel ret = new QuoteViewModel();
            if (view == null)
            { return ret; }
            else
            {
                ret.QuoteID = view.ID;
                ret.Opportunity = view.Opportunity;
                ret.ClosedDate = view.ClosedDate;

                ret.Representative = view.Representative;
                ret.CompanyName = view.CompanyName;
                ret.CustomerType = view.CustomerType;
                ret.DeliveryDate = view.DeliveryDate;
                ret.QuoteNo = view.QuoteNo;
                ret.PONumber = view.PONumber;
                ret.Authorisation = view.Authorisation;
                ret.Campaign = view.Campaign;
                ret.CampaignCode = view.CampaignCode;
                ret.Territory1ID = view.Territory1ID;
                ret.Territory2ID = view.Territory2ID;
                ret.Territory1Split = view.Territory1Split;
                ret.Territory2Split = view.Territory2Split;
                ret.DispatchAddress = view.DispatchAddress;
                ret.AccountContactName = view.AccountContactName;
                ret.AccountContactTitle = view.AccountContactTitle;
                ret.AccountContactPhoneNo = view.AccountContactPhoneNo;
                ret.AccountContactEmail = view.AccountContactEmail;
                ret.FinanceDeal = view.FinanceDeal;
                ret.FinanceType = view.FinanceType;
                ret.FinanceApproved = view.FinanceApproved;
                ret.FinanceTotalAmount = view.FinanceTotalAmount;
                ret.FinancePeriod = view.FinancePeriod;
                ret.InkUsage = view.InkUsage;
                ret.SolventUsage = view.SolventUsage;
                ret.Comments = view.Comments;
                ret.SalesPerson = view.SalesPerson;
                ret.CHOPComments = view.CHOPComments;
                ret.ClosedDate = view.ClosedDate;
                ret.CalcDeliveryDate = view.CalcDeliveryDate;
                ret.DeliveryDate = view.DeliveryDate;
                ret.CustomerCode = view.CustomerCode;
                ret.SaleTypeID = view.SaleTypeID;
            }
            List<BOMListModel> Linelst = new List<BOMListModel>();
            if (view.BOMListModel != null &&
                view.BOMListModel.Count > 0)
            {

                foreach (var item in view.BOMListModel)
                {
                    BOMListModel l = new BOMListModel();
                    l.BOMID = item.BOMID;
                    l.OpportunityBOMListID = item.OpportunityBOMListID;
                    l.OpportunityID = Convert.ToInt32(item.OpportunityID);
                    l.Name = item.Name;
                    l.TotalPrice = Convert.ToDecimal(item.TotalPrice);
                    l.Discount = item.Discount;
                    l.PriceAfterDiscount = item.PriceAfterDiscount;
                    l.FinalAgreedPrice = item.FinalAgreedPrice;
                    l.ClosedDate = item.ClosedDate;

                    Linelst.Add(l);
                }
                ret.BOMListModel = Linelst;
            }
            else
            {
                ret.BOMListModel = Linelst;
            }

            List<TerritoryModel> Territorylst = new List<TerritoryModel>();
            if (view.TerritoryList.Count > 0)
            {

                foreach (var item in view.TerritoryList)
                {
                    TerritoryModel l = new TerritoryModel();
                    l.ID = item.ID;
                    l.Description = item.Description;

                    Territorylst.Add(l);
                }
                ret.TerritoryListModel = Territorylst;
            }
            else
            {
                ret.TerritoryListModel = Territorylst;
            }


            List<SaleModel> Salelst = new List<SaleModel>();
            if (view.TerritoryList.Count > 0)
            {

                foreach (var item in view.SaleTypeList)
                {
                    SaleModel l = new SaleModel();
                    l.ID = item.ID;
                    l.SaleType = item.SaleType;

                    Salelst.Add(l);
                }
                ret.SaleListModel = Salelst;
            }
            else
            {
                ret.TerritoryListModel = Territorylst;
            }

            return ret;
        }


        //---------------------------------Populate Data to View Modal and from BL
        private OpportunityBOMItemsViewModel PopulateBOMList(DL_OpportunityBOMItemsViewModel BOMDL, string QuoteNo)
        {
            OpportunityBOMItemsViewModel ret = new OpportunityBOMItemsViewModel();
            List<OpportunityBOMItem> bomlst = new List<OpportunityBOMItem>();

            if (BOMDL == null)
            { return ret; }

            if (BOMDL.BOMListViewModel == null)
            {
                OpportunityBOMItem Model = new OpportunityBOMItem();
                bomlst.Add(Model);
                ret.BOMListViewModel = bomlst;
                ret.QuoteNo = QuoteNo;
                return ret;
            }

            if (BOMDL.BOMListViewModel.Count > 0)
            {
                decimal GrandTotal = 0;
                decimal GrandTotalAfterDiscount = 0;
                foreach (var item in BOMDL.BOMListViewModel)
                {
                    OpportunityBOMItem Model = new OpportunityBOMItem();
                    Model.OpportunityID = item.OpportunityID;
                    Model.BOMItemID = item.BOMItemID;
                    Model.BOMID = item.BOMID;
                    Model.Description = item.Description;
                    Model.ItemPrice = item.ItemPrice;
                    Model.Price = item.Price;
                    Model.Category = item.Category;
                    Model.SubCategory = item.SubCategory;
                    Model.Qty = item.Qty;
                    Model.MatthewsCode = item.MatthewsCode;
                    Model.OpportunityBOMListID = item.OpportunityBOMListID;
                    Model.Category = item.Category;
                    Model.IsDiscountApply = item.IsDiscountApply;
                    Model.Discount = item.Discount;
                    Model.AfterDiscount = item.AfterDiscount;
                    Model.IsQtyFixed = item.IsQtyFixed;
                    Model.MaximumQty = item.MaximumQty;
                    Model.Stock = item.Stock;
                    Model.State = item.State;
                    Model.CategoryOrder = item.CategoryOrder;
                    Model.SubCategoryOrder = item.SubCategoryOrder;
                    if (item.IsInTotal == true)
                    {
                        Model.IsInTotal = 1;
                        GrandTotal += item.Price;
                        GrandTotalAfterDiscount += item.PriceAfterDiscount;
                    }
                    else
                    {
                        Model.IsInTotal = 0;
                    }

                    Model.IsDecimalAllowed = 0;
                    if (item.IsDecimalAllowed == true)
                        Model.IsDecimalAllowed = 1;

                    ret.FinalAgreedPrice = item.FinalAgreedPrice;
                    ret.Discount = item.Discount;
                    ret.IsCustomParts = item.IsCustomParts;
                    ret.OpportunityID = item.OpportunityID;
                    ret.BOMID = item.BOMID;
                    ret.BOM = item.BOM;
                    ret.ClosedDate = item.ClosedDate;
                    ret.InkUsage = item.InkUsage;
                    bomlst.Add(Model);
                }

                ret.GrandTotalAfterDiscount = GrandTotalAfterDiscount;
                ret.GrandTotal = GrandTotal;
                ret.BOMListViewModel = bomlst;
                ret.QuoteNo = QuoteNo;

            }
            return ret;
        }


        private DL_OpportunityModel PopulateCustomerDL(List<OpportunityCoverModel> CUSDL)
        {
            DL_OpportunityModel ret = new DL_OpportunityModel();


            if (CUSDL == null)
                return ret;

            foreach (var item in CUSDL)
            {
                ret.QuoteNo = item.QuoteNo;
                ret.DispatchAddress = item.DispatchAddress;
                ret.AccountContactName = item.AccountContactName;
                ret.AccountContactTitle = item.AccountContactTitle;
                ret.AccountContactPhoneNo = item.AccountContactPhoneNo;
                ret.AccountContactEmail = item.AccountContactEmail;
                ret.FinanceDeal = item.FinanceDeal;
                ret.FinanceType = item.FinanceType;
                ret.FinanceApproved = item.FinanceApproved;
                ret.FinanceTotalAmount = item.FinanceTotalAmount;
                ret.FinancePeriod = item.FinancePeriod;

            }

            return ret;
        }

        private DL_OpportunityModel PopulateAssemblyDL(List<OpportunityCoverModel> Asmbly)
        {
            DL_OpportunityModel ret = new DL_OpportunityModel();

            if (Asmbly == null)
                return ret;
            foreach (var item in Asmbly)
            {
                ret.QuoteNo = item.QuoteNo;
                ret.CompanyName = item.CompanyName;
                ret.Representative = item.Representative;
                ret.CustomerType = item.CustomerType;
                ret.DeliveryDate = item.DeliveryDate;
                ret.PONumber = item.PONumber;
                ret.Authorisation = item.Authorisation;
                ret.Campaign = item.Campaign;
                ret.CampaignCode = item.CampaignCode;
                ret.SalesPerson = item.SalesPerson;
                ret.SaleTypeID = item.SaleTypeID;

            }

            return ret;
        }


        private DL_OpportunityModel PopulateConsumableDL(List<OpportunityCoverModel> Consumable)
        {
            DL_OpportunityModel ret = new DL_OpportunityModel();

            if (Consumable == null)
                return ret;
            foreach (var item in Consumable)
            {
                ret.QuoteNo = item.QuoteNo;
                ret.InkUsage = item.InkUsage;
                ret.SolventUsage = item.SolventUsage;
                ret.Comments = item.Comments;
            }

            return ret;
        }

        private DL_OpportunityModel PopulateCHOPDL(List<OpportunityCoverModel> CHOP)
        {
            DL_OpportunityModel ret = new DL_OpportunityModel();

            if (CHOP == null)
                return ret;
            foreach (var item in CHOP)
            {
                ret.QuoteNo = item.QuoteNo;
                ret.CHOPComments = item.CHOPComments;
            }

            return ret;
        }

        private DL_OpportunityModel PopulatePMDL(List<OpportunityCoverModel> pm)
        {
            DL_OpportunityModel ret = new DL_OpportunityModel();

            if (pm == null)
                return ret;
            foreach (var item in pm)
            {
                ret.QuoteNo = item.QuoteNo;
                ret.PMComments = item.PMComments;
            }

            return ret;
        }

        private DL_OpportunityModel PopulateTerritorySplitDL(List<OpportunityCoverModel> TS)
        {
            DL_OpportunityModel ret = new DL_OpportunityModel();

            if (TS == null)
                return ret;
            foreach (var item in TS)
            {
                ret.QuoteNo = item.QuoteNo;
                ret.Territory1Split = item.Territory1Split;
                ret.Territory2Split = item.Territory2Split;
                ret.Territory1ID = item.Territory1ID;
                ret.Territory2ID = item.Territory2ID;
            }

            return ret;
        }


        private DL_BOMFinancialReiewModel PopulateFinancialReviewDL(List<BOMFinancialReviewModel> FR)
        {
            DL_BOMFinancialReiewModel ret = new DL_BOMFinancialReiewModel();

            if (FR == null)
                return ret;
            foreach (var item in FR)
            {
                ret.BOMID = item.BOMID;
                ret.OpportunityID = item.OpportunityID;
                ret.Deposit = item.Deposit;
                ret.DepositPerc = item.DepositPerc;
                ret.PreDelivery = item.PreDelivery;
                ret.PreDeliveryPerc = item.PreDeliveryPerc;
                ret.Final = item.Final;
                ret.FinalPerc = item.FinalPerc;
            }

            return ret;
        }
        #endregion  PopulateViews 

    }
}
