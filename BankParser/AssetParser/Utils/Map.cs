using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.Utils
{
    public class Map<T, Y>
    {
        public Map() { }

        public Map(T first, Y second)
        {
            First = first;
            Second = second;
        }


        public T First { get; set; }
        public Y Second { get; set; }
    }
}
