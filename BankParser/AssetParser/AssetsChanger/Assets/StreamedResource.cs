﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.AssetsChanger.Assets
{
    public class StreamedResource
    {
        public StreamedResource(string source, UInt64 offset, UInt64 size)
        {
            Source = source;
            Offset = offset;
            Size = size;
        }

        public StreamedResource(AssetsReader reader)
        {
            Parse(reader);
        }

        private void Parse(AssetsReader reader)
        {
            Source = reader.ReadString();
            Offset = reader.ReadUInt64();
            Size = reader.ReadUInt64();
        }

        public void Write(AssetsWriter writer)
        {
            writer.Write(Source);
            writer.Write(Offset);
            writer.Write(Size);
        }

        public string Source { get; set; }
        public UInt64 Offset { get; private set; }
        public UInt64 Size { get; private set; }
    }
}
