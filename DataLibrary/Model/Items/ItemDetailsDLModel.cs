using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Model.Items
{
    public class ItemDetailsDLModelList : ReturnData
    {
       public List<ItemDetailsDLModel> ItemList { get; set; }
    }

    public class ItemDetailsDLModel
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
