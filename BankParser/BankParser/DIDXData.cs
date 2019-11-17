using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankParser
{
    public class DIDXData : Parsable
    {
        [JsonIgnore]
        public byte[] header;
        public string headerName;
        public UInt32 size;
        public DataIndex[] dataIndexes;
        [JsonIgnore]
        public long Size => size + 8;

        public DIDXData(CustomBinaryReader reader)
        {
            Read(reader);
        }

        public void Read(CustomBinaryReader reader)
        {
            header = reader.ReadBytes(4);
            headerName = Encoding.UTF8.GetString(header);
            size = reader.ReadUInt32();
            long _doubleCheck = reader.Position;
            if (size % 12 != 0)
            {
                throw new ParseException($"Failed to parse DIDXData.dataIndexes! {size} / {12} is not an integer!");
            }
            dataIndexes = new DataIndex[size / 12];
            for (int i = 0; i < size / 12; i++)
            {
                dataIndexes[i] = new DataIndex(reader);
            }
            if (reader.Position - _doubleCheck - size != 0)
            {
                throw new ParseException($"Failed to parse DIDXData! Attempted to read: {size} bytes, but actually read: {reader.Position - _doubleCheck} bytes!");
            }
        }

        public void Write(CustomBinaryWriter writer, bool writeData = false)
        {
            writer.Write(header);
            writer.Write(size);
            writer.WriteMany(dataIndexes);
        }
    }
}
