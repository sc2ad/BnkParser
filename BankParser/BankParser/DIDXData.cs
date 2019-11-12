using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankParser
{
    public class DIDXData : Parsable
    {
        byte[] header;
        UInt32 size;
        DataIndex[] dataIndexes;

        public long Size => size + 8;

        public void Read(CustomBinaryReader reader)
        {
            header = reader.ReadBytes(4);
            size = reader.ReadUInt32();
            long _doubleCheck = reader.Position;
            if (size % 12 != 0)
            {
                throw new ParseException($"Failed to parse DIDXData.dataIndexes! {size} / {12} is not an integer!");
            }
            dataIndexes = new DataIndex[size / 12];
            for (int i = 0; i < size / 12; i++)
            {
                dataIndexes[i].Read(reader);
            }
            if (reader.Position - _doubleCheck != 0)
            {
                throw new ParseException($"Failed to parse DIDXData! Attempted to read: {size} bytes, but actually read: {reader.Position - _doubleCheck} bytes!");
            }
        }
    }
}
