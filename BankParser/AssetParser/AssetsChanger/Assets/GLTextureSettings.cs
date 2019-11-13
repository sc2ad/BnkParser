﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.AssetsChanger.Assets
{
    public class GLTextureSettings
    {
        public GLTextureSettings()
        { }

        public GLTextureSettings(AssetsReader reader)
        {
            Parse(reader);
        }

        private void Parse(AssetsReader reader)
        {
            FilterMode = reader.ReadInt32();
            Aniso = reader.ReadInt32();
            MipBias = reader.ReadSingle();
            WrapU = reader.ReadInt32();
            WrapV = reader.ReadInt32();
            WrapW = reader.ReadInt32();
        }

        public void Write(AssetsWriter writer)
        {
            writer.Write(FilterMode);
            writer.Write(Aniso);
            writer.Write(MipBias);
            writer.Write(WrapU);
            writer.Write(WrapV);
            writer.Write(WrapW);
        }
        public Int32 FilterMode { get; set; }
        public Int32 Aniso { get; set; }
        public Single MipBias { get; set; }
        public Int32 WrapU { get; set; }
        public Int32 WrapV { get; set; }
        public Int32 WrapW { get; set; }


    }
}
