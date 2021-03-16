using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Model.BOM
{
   public class BOM
    {
        public int _ID { get; set; }
        public string _BOM { get; set; }
        public bool? _IsCustomParts { get; set; }
    }
}
