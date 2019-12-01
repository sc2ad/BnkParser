using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankParser.HIRCData
{
    public class RTPC : Parsable
    {
        // 0x31 0x57
        public uint gameParameterID;
        public uint yType;
        public uint unknownID;
        //private byte _unknown;
        public byte numPoints;
        private byte _unknown2;
        // 0x3D 0x63
        public List<Point> points;

        public long Size => 15 + points.Count * points[0].Size;

        public RTPC(CustomBinaryReader reader)
        {
            Read(reader);
        }
        public void Read(CustomBinaryReader reader)
        {
            gameParameterID = reader.ReadUInt32();
            yType = reader.ReadUInt32();
            unknownID = reader.ReadUInt32();
            //_unknown = reader.ReadByte();
            numPoints = reader.ReadByte();
            _unknown2 = reader.ReadByte();
            points = reader.ReadMany((r) => new Point(r), (ulong)numPoints);
        }
        public void Write(CustomBinaryWriter writer, bool writeData = false)
        {
            writer.Write(gameParameterID);
            writer.Write(yType);
            writer.Write(unknownID);
            //writer.Write(_unknown);
            writer.Write(points.Count);
            writer.Write(_unknown2);
            writer.WriteMany(points);
        }
    }
}
