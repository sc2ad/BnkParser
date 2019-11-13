using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.Utils.Logging
{
    public interface ILog
    {
        void LogMsg(string message, params object[] args);

        void LogErr(string message, Exception ex);

        void LogErr(string message, params object[] args);
    }
}
