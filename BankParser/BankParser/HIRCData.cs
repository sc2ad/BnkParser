using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankParser
{
    class HIRCData : Parsable
    {
        byte[] header;
        UInt32 size;
        byte[] data;

        public long Size => size + 8;

        public void Read(CustomBinaryReader reader)
        {
            header = reader.ReadBytes(4);
            size = reader.ReadUInt32();
            data = reader.ReadBytes((int)size);
        }
    }
}
