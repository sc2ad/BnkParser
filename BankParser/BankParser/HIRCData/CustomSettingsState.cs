using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankParser.HIRCData
{

    public class CustomSettingsState : Parsable
    {
        public uint id;
        public uint ownerID;

        public CustomSettingsState(CustomBinaryReader reader)
        {
            Read(reader);
        }

        public long Size => 4;

        public void Read(CustomBinaryReader reader)
        {
            id = reader.ReadUInt32();
            ownerID = reader.ReadUInt32();
        }
        public void Write(CustomBinaryWriter writer, bool writeData = false)
        {
            writer.Write(id);
            writer.Write(ownerID);
        }
    }
}
