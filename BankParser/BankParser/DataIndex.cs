using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankParser
{
    public class DataIndex : Parsable
    {
        UInt32 id;
        UInt32 offset;
        UInt32 filesize;

        public long Size => 12;

        public void Read(CustomBinaryReader reader)
        {
            id = reader.ReadUInt32();
            offset = reader.ReadUInt32();
            filesize = reader.ReadUInt32();
        }
    }
}
