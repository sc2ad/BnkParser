using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankParser
{
    class BnkData : Parsable
    {
        [JsonIgnore]
        public byte[] header;
        public string headerName;
        public UInt32 headerSize;
        public UInt32 version;
        [JsonIgnore]
        public byte[] headerData;
        // pad
        public DIDXData didx;
        public DATAData data;
        public HIRCData hirc;
        [JsonIgnore]
        public long Size => headerSize + 8 + didx.Size + data.Size + hirc.Size;

        public BnkData(CustomBinaryReader reader)
        {
            Read(reader);
        }

        public void Read(CustomBinaryReader reader)
        {
            header = reader.ReadBytes(4);
            headerName = Encoding.UTF8.GetString(header);
            headerSize = reader.ReadUInt32();
            long _headerStartPos = reader.Position;
            version = reader.ReadUInt32();
            headerData = reader.ReadBytes((int)(_headerStartPos - reader.Position + headerSize));
            didx = new DIDXData(reader);
            data = new DATAData(reader);
            hirc = new HIRCData(reader);
        }

        public void Write(CustomBinaryWriter writer, bool writeData = false)
        {
            writer.Write(header);
            writer.Write(headerSize);
            writer.Write(version);
            writer.Write(headerData);
            didx.Write(writer);
            data.Write(writer);
            hirc.Write(writer);
        }

        public byte[] DumpID(UInt32 id, int extra = 0)
        {
            foreach (var item in didx.dataIndexes)
            {
                if (item.id == id)
                {
                    using (var ms = new MemoryStream(data.data))
                    {
                        ms.Seek(item.offset, SeekOrigin.Begin);
                        byte[] oup = new byte[item.filesize + extra];
                        ms.Read(oup, 0, (int)item.filesize + extra);
                        return oup;
                    }
                }
            }
            return null;
        }
    }
}
