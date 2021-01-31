using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Model.Items
{
    public class LineTypeDLModellList : ReturnData
    {
        public List<LineTypeDLModel>LineTypeList { get; set; }
    }
    public class LineTypeDLModel
    {
        public int ID { get; set; }
        public int LineType { get; set; }
    }
}
