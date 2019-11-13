using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.Utils
{
    public class FileUtils
    {
        public static Func<string> GetTempDirectoryOverride;
        public static string GetTempDirectory()
        {
            if (GetTempDirectoryOverride != null)
                return GetTempDirectoryOverride();

            return System.IO.Path.GetTempPath();
        }
    }
}
