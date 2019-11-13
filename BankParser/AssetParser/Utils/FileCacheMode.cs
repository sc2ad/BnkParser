using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.Utils
{
    public enum FileCacheMode
    {
        //None won't work for some stuff since it can't seek.
        None,
        Memory,
        TempFiles
    };
}
