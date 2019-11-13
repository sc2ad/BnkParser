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
    public class WwiseStateReference : MonoBehaviourObject, INeedAssetsMetadata
    {
        byte[] unknown;

        public WwiseStateReference(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public WwiseStateReference(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("WwiseStateReference"))
        { }

        public override void ParseObject(AssetsReader reader)
        {
            unknown = reader.ReadBytes(ObjectInfo.DataSize - (reader.Position - ObjectInfo.DataOffset));
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            writer.Write(unknown);
        }
    }
}
