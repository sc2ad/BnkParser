using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankParser.HIRCData
{
    public class EffectStructure : Parsable
    {
        public byte index;
        public uint id;
        private byte[] _unknown;

        public EffectStructure(CustomBinaryReader reader)
        {
            Read(reader);
        }

        public long Size => 7;

        public void Read(CustomBinaryReader reader)
        {
            index = reader.ReadByte();
            id = reader.ReadUInt32();
            _unknown = reader.ReadBytes(2);
        }
        public void Write(CustomBinaryWriter writer, bool writeData = false)
        {
            writer.Write(index);
            writer.Write(id);
            writer.Write(_unknown);
        }
    }
}
