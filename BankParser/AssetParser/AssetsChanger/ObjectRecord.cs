using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.AssetsChanger
{
    public class ObjectRecord
    {
        public Int64 ObjectID { get; set; } = -1;
        public Int32 DataOffset { get; set; } = -1;
        public Int32 DataSize { get; set; } = -1;
        public Int32 TypeIndex { get; private set; } = -1;


        private ObjectRecord()
        { }

        public ObjectRecord(AssetsReader reader)
        {
            Parse(reader);
        }

        private void Parse(AssetsReader reader)
        {
            ObjectID = reader.ReadInt64();
            DataOffset = reader.ReadInt32();
            DataSize = reader.ReadInt32();
            TypeIndex = reader.ReadInt32();
        }

        public void Write(AssetsWriter writer)
        {
            writer.Write(ObjectID);
            writer.Write(DataOffset);
            writer.Write(DataSize);
            writer.Write(TypeIndex);
        }

    }
}
