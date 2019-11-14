using AssetParser.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser
{
    public class AssetsConfig
    {
        public IFileProvider RootFileProvider { get; set; }

        public string AssetsPath { get; set; }
    }
}
