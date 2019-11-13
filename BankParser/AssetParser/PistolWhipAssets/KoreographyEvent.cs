using AssetParser.AssetsChanger;
using AssetParser.AssetsChanger.Assets;
using AssetParser.AssetsChanger.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.PistolWhipAssets
{
    public class KoreographyEvent : MonoBehaviourObject, INeedAssetsMetadata
    {
        public int StartSample { get; set; }
        public int EndSample { get; set; }

        public KoreographyEvent(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool readLiteral = false) : base(objectInfo, reader, readLiteral)
        {
        }

        public KoreographyEvent(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("KoreographyEvent"))
        { }

        public override void ParseObject(AssetsReader reader)
        {
            StartSample = reader.ReadInt32();
            EndSample = reader.ReadInt32();
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            writer.Write(StartSample);
            writer.Write(EndSample);
        }
    }
}
