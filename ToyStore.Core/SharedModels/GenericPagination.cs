using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ToyStore.Core.SharedModels
{
    public class GenericPagination
    {
        public  List<keyPairValueString>? Sortings { get; set; } = new List<keyPairValueString>();
        public List<keyPairValueString>? FilterStrings { get; set; } = new List<keyPairValueString>();
        public List<keyPairValueNums>? FilterNums { get; set; } = new List<keyPairValueNums>();
        public bool IsAndOperator { get; set; }
        public int PageIdx { get; set; }
        public int PageSize { get; set; }
    }


    public class keyPairValueString
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
    public class keyPairValueNums
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string Operator { get; set; }
    }
}
