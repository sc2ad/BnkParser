using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankParser
{
    public interface Parsable
    {
        long Size { get; }
        void Read(CustomBinaryReader reader);
        void Write(CustomBinaryWriter writer, bool writeData = true);
    }
}
