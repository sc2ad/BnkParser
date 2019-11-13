﻿using AssetParser.AssetsChanger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.Utils
{
    public class ExternalFile
    {
        public string AssetName { get; set; }
        public Guid ID { get; set; }
        public Int32 Type { get; set; }
        public string FileName { get; set; }

        public ExternalFile()
        { }

        public ExternalFile(AssetsReader reader)
        {
            Parse(reader);
        }

        public void Write(AssetsWriter writer)
        {
            writer.WriteCString(AssetName);
            writer.Write(ID);
            writer.Write(Type);
            writer.WriteCString(FileName);
        }

        private void Parse(AssetsReader reader)
        {
            AssetName = reader.ReadCStr();
            ID = reader.ReadGuid();
            Type = reader.ReadInt32();
            FileName = reader.ReadCStr();
        }
    }
}
