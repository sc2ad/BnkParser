using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankParser
{
    class BnkData : Parsable
    {
        byte[] header;
        UInt32 headerSize;
        UInt32 version;
        byte[] headerData;
        // pad
        DIDXData didx;
        DATAData data;
        HIRCData hirc;

        public long Size => headerSize + 8 + didx.Size + data.Size + hirc.Size;

        public void Read(CustomBinaryReader reader)
        {
            header = reader.ReadBytes(4);
            headerSize = reader.ReadUInt32();
            long _headerStartPos = reader.Position;
            version = reader.ReadUInt32();
            headerData = reader.ReadBytes((int)(reader.Position - _headerStartPos + headerSize));
            didx = new DIDXData();
            didx.Read(reader);
            data = new DATAData();
            data.Read(reader);
            hirc = new HIRCData();
            hirc.Read(reader);
        }
    }
}
