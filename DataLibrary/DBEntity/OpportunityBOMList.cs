//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLibrary.DBEntity
{
    using System;
    using System.Collections.Generic;
    
    public partial class OpportunityBOMList
    {
        public int ID { get; set; }
        public int OpportunityID { get; set; }
        public int BOMID { get; set; }
        public long BOMItemsID { get; set; }
        public decimal Qty { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal Price { get; set; }
        public string CustomDescription { get; set; }
        public string CustomCode { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> FinalAgreedPrice { get; set; }
        public Nullable<bool> IsDiscountApply { get; set; }
        public Nullable<decimal> PriceAfterDiscount { get; set; }
        public int State { get; set; }
        public bool IsActive { get; set; }
        public Nullable<decimal> MaximumQty { get; set; }
        public Nullable<System.DateTime> UpdatedDatetime { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public bool IsInTotal { get; set; }
        public bool IsDecimalAllowed { get; set; }
    }
}
