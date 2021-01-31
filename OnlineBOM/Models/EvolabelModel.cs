using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace OnlineBOM.Models
{
    public class EvolabelViewModel
    {
        [Display(Name = "Hardware Items")]
        public List<ItemDetailsModel> ItemDetailsList { get; set; }
        [Display(Name ="LineType")]
        public List<LinetypeModel> LineTypeList { get; set; }
              
        [Display(Name = "Quantity")]
        public int ItemQty { get; set; }
    }

    public class LinetypeModel
    {
        public int ID { get; set; }
        public string LineType { get; set; }
    }

    //public class ItemMasterModel
    //{
    //    public int ID { get; set; }
    //    public string Name { get; set; }
    //    public int BOMItemID { get; set; }
        
    //}
    public class ItemDetailsModel
    {
        public int ID { get; set; }
        public int ItemMasterID { get; set; }
        public string Description { get; set; }
        public string SupplierCode { get; set; }
        public string MatthewsCode { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public int LineTypeID { get; set; }
     }





}