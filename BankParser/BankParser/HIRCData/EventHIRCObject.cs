using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankParser.HIRCData
{
    public class EventHIRCObject : HIRCObject
    {
        public EventHIRCObject(CustomBinaryReader reader, byte obj) : base(obj)
        {
            Read(reader, true);
        }

        public override void Read(CustomBinaryReader reader, bool readData = true)
        {
            base.Read(reader, false);
            //EventCount = reader.ReadInt32();
            //EventIDs = new List<int>();
            //for (int i = 0; i < EventCount; i++)
            //{
            //    EventIDs.Add(reader.ReadInt32());
            //}
            if (readData)
                ReadData(reader);
        }

        public override void Write(CustomBinaryWriter writer, bool writeData = true)
        {
            base.Write(writer, false);
            if (writeData)
                WriteData(writer, 0);
        }
    }

}
