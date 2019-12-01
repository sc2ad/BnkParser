using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankParser.HIRCData
{

    public class MusicSegmentHIRCObject : HIRCObject
    {
        public MusicSegmentHIRCObject(CustomBinaryReader reader, byte obj) : base(obj)
        {
            Read(reader, true);
        }

        public override void Read(CustomBinaryReader reader, bool readData = true)
        {
            base.Read(reader, false);
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
