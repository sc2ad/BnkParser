using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankParser.HIRCData
{
    public class MusicTrackHIRCObject : HIRCObject
    {
        private byte _startByte;
        public byte type_state;
        private byte[] _unknown1;
        public uint ID1;
        private byte[] _unknown2;
        public uint ID2;
        public MusicTrackHIRCObject(CustomBinaryReader reader, byte obj) : base(obj)
        {
            Read(reader, true);
        }

        public override void Read(CustomBinaryReader reader, bool readData = true)
        {
            base.Read(reader, false);
            // I'm hoping this is ALWAYS 10
            _startByte = reader.ReadByte();
            type_state = reader.ReadByte();
            _unknown1 = reader.ReadBytes(8);
            // Followed by ID
            ID1 = reader.ReadUInt32();
            if (type_state == 1)
                _unknown2 = reader.ReadBytes(13);
            else
                _unknown2 = reader.ReadBytes(10);
            ID2 = reader.ReadUInt32();
            if (readData)
                ReadData(reader);
        }

        public override void Write(CustomBinaryWriter writer, bool writeData = true)
        {
            base.Write(writer, false);
            writer.Write(_startByte);
            writer.Write(type_state);
            writer.Write(_unknown1);
            writer.Write(ID1);
            writer.Write(_unknown2);
            writer.Write(ID2);
            if (writeData)
                WriteData(writer, 0);
        }
    }

}
