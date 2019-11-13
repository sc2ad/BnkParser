using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankParser
{
    public class DataIndex : Parsable
    {
        public UInt32 id;
        public UInt32 offset;
        public UInt32 filesize;
        [JsonIgnore]
        public long Size => 12;

        public void Read(CustomBinaryReader reader)
        {
            id = reader.ReadUInt32();
            offset = reader.ReadUInt32();
            filesize = reader.ReadUInt32();
        }
    }
}
