using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Model.Quote
{
    public class QuoteDLLineDetails
    {
        public int QuoteID { get; set; }
        public int ItemDetailLineID { get; set; }
        public string Description { get; set; }
        public string SupplierCode { get; set; }
        public string MatthewsCode { get; set; }
        public double ItemPrice { get; set; }
        public int Qty { get; set; }

    }
}
