using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankParser.HIRCData
{
    public class SFXHIRCObject : HIRCObject
    {
        public uint _unknown;
        public uint State;
        public uint IDAudio;
        public uint IDSource;
        public byte SoundType;

        public SFXHIRCObject(CustomBinaryReader reader, byte obj) : base(obj)
        {
            Read(reader, true);
        }

        public override void Read(CustomBinaryReader reader, bool readData = true)
        {
            base.Read(reader, false);
            _unknown = reader.ReadUInt32();
            State = reader.ReadUInt32();
            IDAudio = reader.ReadUInt32();
            IDSource = reader.ReadUInt32();
            SoundType = reader.ReadByte();
            if (readData)
                ReadData(reader);
        }

        public override void Write(CustomBinaryWriter writer, bool writeData = true)
        {
            base.Write(writer, false);
            writer.Write(_unknown);
            writer.Write(State);
            writer.Write(IDAudio);
            writer.Write(IDSource);
            writer.Write(SoundType);
            if (writeData)
                WriteData(writer, 17);
        }
    }
}
