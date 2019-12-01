using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankParser.HIRCData
{
    public class Point : Parsable
    {
        public float x;
        public float y;
        public uint curveShape;
        public Point(CustomBinaryReader reader)
        {
            Read(reader);
        }

        public long Size => 12;

        public void Read(CustomBinaryReader reader)
        {
            x = reader.ReadSingle();
            y = reader.ReadSingle();
            curveShape = reader.ReadUInt32();
        }
        public void Write(CustomBinaryWriter writer, bool writeData = false)
        {
            writer.Write(x);
            writer.Write(y);
            writer.Write(curveShape);
        }
    }
}
