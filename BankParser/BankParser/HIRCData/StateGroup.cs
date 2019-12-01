using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankParser.HIRCData
{
    public class StateGroup : Parsable
    {
        public uint id;
        public byte changeOccursWhen;
        // 0x2B
        public ushort numberOfStates;
        public List<CustomSettingsState> states;
        public StateGroup(CustomBinaryReader reader)
        {
            Read(reader);
        }

        public long Size => 7 + states.Count * states[0].Size;

        public void Read(CustomBinaryReader reader)
        {
            id = reader.ReadUInt32();
            changeOccursWhen = reader.ReadByte();
            numberOfStates = reader.ReadUInt16();
            states = reader.ReadMany((r) => new CustomSettingsState(r), numberOfStates);
        }
        public void Write(CustomBinaryWriter writer, bool writeData = false)
        {
            writer.Write(id);
            writer.Write(changeOccursWhen);
            writer.Write(numberOfStates);
            writer.WriteMany(states);
        }
    }
}
