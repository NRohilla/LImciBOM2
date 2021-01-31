using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting;
using BusinessLogic;
using DataLibrary.Model.QuoteBOM;
using DataLibrary.Model.Quote;
using System.Globalization;

namespace OnlineBOM.ReportDownload
{
    public class PDFBOMList
    {
      
        public PDFBOMList()
        {
           
        }

        int _maxColumns = 4;
        Document _documet;
        Font _fontStyle;
        PdfPTable _pdfTable = new PdfPTable(4);
        
        MemoryStream _memoryStream = new MemoryStream();
        PdfPCell _pdfCell;
        DL_OpportunityBOMItemsViewModel DLVM = new DL_OpportunityBOMItemsViewModel();
        DL_QuoteViewModel QVM = new DL_QuoteViewModel();
        decimal subtotal;
        decimal grandtotal;
        decimal afterDiscount;
        string discount;
        decimal agreedPrice;

        public byte[] DownloadBOM(int OpportunityID, int BOMID,int State)
        {
            _documet = new Document();    
            _documet.SetPageSize(PageSize.A4);
            _documet.SetMargins(5f, 5f, 20f, 5f);
            _pdfTable.WidthPercentage = 100;
            _pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);
            PdfWriter docWrite = PdfWriter.GetInstance(_documet, _memoryStream);
            _documet.Open();

            float[] sizes = new float[_maxColumns];
            sizes[0] = 50;
            sizes[1] = 100;
            sizes[2] = 20;
            sizes[3] = 30;

            //for (var i = 0; i < _maxColumns; i++)
            //{
            //    if (i == 2) sizes[i] = 20;
            //    else sizes[i] = 100;
            //}

            //--Get the BOM details'
                        
            PDFDownloadBusinessLogic BL = new PDFDownloadBusinessLogic();
            QuoteBusinessLogic QBL = new QuoteBusinessLogic();

            DLVM = BL.GetOpportunityBOMList_BOMDownload(OpportunityID, BOMID, State);
       
            this.ReportHeader();
            this.EmptyRow(2,_maxColumns);
            this.DownloadBOM_ReportBody();

            _pdfTable.SetWidths(sizes);
            _pdfTable.HeaderRows = 2;
            _documet.Add(_pdfTable);
            _documet.Close();
            return _memoryStream.ToArray();

        }

        
        private void ReportHeader()
        {
            //_pdfCell = new PdfPCell(this.AddLogo());
            //_pdfCell.Colspan = 1;
            //_pdfCell.Border = 0;
            //_pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(this.SetPageTitle());
            //_pdfCell.Colspan = _maxColumns-1;
            _pdfCell.Colspan = _maxColumns ;
            _pdfCell.Border = 0;
            _pdfTable.AddCell(_pdfCell);

            _pdfTable.CompleteRow();

        }

        private PdfPTable AddLogo()
        {
            int maxColumn = 1;
            PdfPTable pdfTable = new PdfPTable(maxColumn);
            //string path = MapPath + "/Images";

            //string imgCombine = Path.Combine(path, "iDSnet-logo-RGB.png");
            //Image img = Image.GetInstance(imgCombine);

            _pdfCell = new PdfPCell();
            _pdfCell.Colspan = maxColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.ExtraParagraphSpace = 0;
            pdfTable.AddCell(_pdfCell);
            pdfTable.CompleteRow();
            return (pdfTable);


        }

        private PdfPTable SetPageTitle()
        {

            int maxColumn = 5;
            PdfPTable pdfpTable = new PdfPTable(maxColumn);

            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);
            _pdfCell = new PdfPCell(new Phrase("Matthews Quote", _fontStyle));
            _pdfCell.Colspan = maxColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.Border = 0;
            _pdfCell.ExtraParagraphSpace = 0;
            pdfpTable.AddCell(_pdfCell);
            pdfpTable.CompleteRow();
            if (DLVM.BOMListViewModel!=null)
            { 
            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);
            _pdfCell = new PdfPCell(new Phrase(DLVM.BOMListViewModel[0].CompanyName, _fontStyle));
            _pdfCell.Colspan = maxColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.Border = 0;
            _pdfCell.ExtraParagraphSpace = 0;
            pdfpTable.AddCell(_pdfCell);
            pdfpTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);
            _pdfCell = new PdfPCell(new Phrase(DLVM.BOMListViewModel[0].AccountContactEmail, _fontStyle));
            _pdfCell.Colspan = maxColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.Border = 0;
            _pdfCell.ExtraParagraphSpace = 0;
            pdfpTable.AddCell(_pdfCell);
            pdfpTable.CompleteRow();


            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);
            _pdfCell = new PdfPCell(new Phrase(DLVM.BOMListViewModel[0].DispatchAddress, _fontStyle));
            _pdfCell.Colspan = maxColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.Border = 0;
            _pdfCell.ExtraParagraphSpace = 0;
            pdfpTable.AddCell(_pdfCell);
            pdfpTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);
            _pdfCell = new PdfPCell(new Phrase("REF No-" + DLVM.BOMListViewModel[0].QuoteNo, _fontStyle));
            _pdfCell.Colspan = maxColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.Border = 0;
            _pdfCell.ExtraParagraphSpace = 0;
            pdfpTable.AddCell(_pdfCell);
            pdfpTable.CompleteRow();

            }

            return pdfpTable;
                                 
        }

        private void EmptyRow(int nCount,int maxColumns)
        {

            for (int i = 0; i < nCount; i++)
            {
                _pdfCell = new PdfPCell(new Phrase("", _fontStyle));
                _pdfCell.Colspan = maxColumns;
                _pdfCell.Border = 0;
                _pdfCell.ExtraParagraphSpace = 10;
                _pdfTable.AddCell(_pdfCell);
                _pdfTable.CompleteRow();
           }         
           
        }

        private void DownloadBOM_ReportBody()
        {
            var fontStyleBold = FontFactory.GetFont("Tahoma", 9f, 1);
            _fontStyle = FontFactory.GetFont("Tahoma", 9f,0);

            _pdfCell = new PdfPCell(new Phrase("Matthews Code", fontStyleBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("Item", fontStyleBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("Qty", fontStyleBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("Price", fontStyleBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable.AddCell(_pdfCell);

            _pdfTable.CompleteRow();
            if (DLVM.BOMListViewModel!=null)
            {
                this.EmptyRow(1, _maxColumns);

                var itemCategory= DLVM.BOMListViewModel[0].Category;
                string Category="";
                if (itemCategory == "Device") { Category = DLVM.BOMListViewModel[0].Category; } else { Category = DLVM.BOMListViewModel[0].Category; }
                _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);
                _pdfCell = new PdfPCell(new Phrase(Category, _fontStyle));
                _pdfCell.Colspan = _maxColumns;
                _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pdfCell.Border = 0;
                _pdfCell.ExtraParagraphSpace = 0;
                _pdfTable.AddCell(_pdfCell);
                _pdfTable.CompleteRow();

                subtotal = 0;
                grandtotal = 0;

                foreach (var item in DLVM.BOMListViewModel)
                {
                    if (itemCategory != item.Category)
                    {
                        
                        itemCategory = item.Category;
                        if (itemCategory == "") { Category = "Custom"; } else {Category= itemCategory; }
               
                        PrintSubtotlal(subtotal);
                        _pdfTable.CompleteRow();
                        subtotal = 0;

                        this.EmptyRow(1, _maxColumns);
                        _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);
                        _pdfCell = new PdfPCell(new Phrase(Category, _fontStyle));
                        _pdfCell.Colspan = _maxColumns;
                        _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        _pdfCell.Border = 0;
                        _pdfCell.ExtraParagraphSpace = 0;
                        _pdfTable.AddCell(_pdfCell);
                        _pdfTable.CompleteRow();
                       
                    }
                    _pdfCell = new PdfPCell(new Phrase(item.MatthewsCode, _fontStyle));
                    _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    _pdfCell.BackgroundColor = BaseColor.White;
                    _pdfTable.AddCell(_pdfCell);

                    _pdfCell = new PdfPCell(new Phrase(item.Description, _fontStyle));
                    _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    _pdfCell.BackgroundColor = BaseColor.White;
                    _pdfTable.AddCell(_pdfCell);

                    _pdfCell = new PdfPCell(new Phrase(item.Qty.ToString(), _fontStyle));
                    _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    _pdfCell.BackgroundColor = BaseColor.White;
                    _pdfTable.AddCell(_pdfCell);

                    _pdfCell = new PdfPCell(new Phrase(item.Price.ToString("C", CultureInfo.CurrentCulture), _fontStyle));
                    _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    _pdfCell.BackgroundColor = BaseColor.White;
                    _pdfTable.AddCell(_pdfCell);

                    subtotal +=  item.Price;
                    grandtotal = grandtotal + item.Price;
                    discount = item.Discount.ToString();
                    afterDiscount += item.AfterDiscount;
                    agreedPrice = item.FinalAgreedPrice;
                    _pdfTable.CompleteRow();
                }

                PrintSubtotlal(subtotal);
                this.EmptyRow(1, _maxColumns);
                PrintGrandtotlal(grandtotal);
                this.EmptyRow(1, _maxColumns);
                PrintDiscount(discount);
                this.EmptyRow(1, _maxColumns);
                PrintAfterDiscount(afterDiscount);
                this.EmptyRow(1, _maxColumns);
                PrintAgreedPrice(agreedPrice);

            }
        }


        private void PrintSubtotlal(decimal Subtotal)
        {
            _pdfCell = new PdfPCell(new Phrase("", _fontStyle));
            _pdfCell.Colspan = 2;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfTable.AddCell(_pdfCell);


            _pdfCell = new PdfPCell(new Phrase("Sub total", _fontStyle));
             _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(Subtotal.ToString("C", CultureInfo.CurrentCulture), _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfTable.AddCell(_pdfCell);
        }

        private void PrintGrandtotlal(decimal GrandTotal)
        {
            _pdfCell = new PdfPCell(new Phrase("", _fontStyle));
            _pdfCell.Colspan = 2;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfTable.AddCell(_pdfCell);


            _pdfCell = new PdfPCell(new Phrase("Grand total", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(GrandTotal.ToString("C", CultureInfo.CurrentCulture), _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfTable.AddCell(_pdfCell);
        }

        private void PrintDiscount(string Discount)
        {
            _pdfCell = new PdfPCell(new Phrase("", _fontStyle));
            _pdfCell.Colspan = 2;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfTable.AddCell(_pdfCell);


            _pdfCell = new PdfPCell(new Phrase("Discount (%)", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(Discount, _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfTable.AddCell(_pdfCell);
        }

        private void PrintAfterDiscount(decimal AfterDiscount)
        {
            _pdfCell = new PdfPCell(new Phrase("", _fontStyle));
            _pdfCell.Colspan = 2;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfTable.AddCell(_pdfCell);


            _pdfCell = new PdfPCell(new Phrase("After Discount", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(AfterDiscount.ToString("C", CultureInfo.CurrentCulture), _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfTable.AddCell(_pdfCell);
        }

        private void PrintAgreedPrice(decimal agreedprice)
        {
            _pdfCell = new PdfPCell(new Phrase("", _fontStyle));
            _pdfCell.Colspan = 2;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfTable.AddCell(_pdfCell);


            _pdfCell = new PdfPCell(new Phrase("Agreed Price", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(agreedprice.ToString("C", CultureInfo.CurrentCulture), _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfTable.AddCell(_pdfCell);
        }

    }
}
