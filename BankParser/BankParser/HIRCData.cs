using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankParser
{
    class HIRCData : Parsable
    {
        [JsonIgnore]
        public byte[] header;
        public string headerName;
        public UInt32 size;
        public byte[] data;
        [JsonIgnore]
        public long Size => size + 8;

        public void Read(CustomBinaryReader reader)
        {
            header = reader.ReadBytes(4);
            headerName = Encoding.UTF8.GetString(header);
            size = reader.ReadUInt32();
            data = reader.ReadBytes((int)size);
        }
    }
}
