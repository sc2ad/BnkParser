using AssetParser.AssetsChanger;
using AssetParser.AssetsChanger.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.PistolWhipAssets
{
    public class AssetPayload : MonoBehaviourObject
    {
        // Not sure what these are, but they are always the same
        public int FirstFour { get; set; }
        public int SecondFour { get; set; }
        public AssetPayload(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("AssetPayload"))
        {
        }

        public AssetPayload(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public override void ParseObject(AssetsReader reader)
        {
            FirstFour = reader.ReadInt32();
            SecondFour = reader.ReadInt32();
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            writer.Write(FirstFour);
            writer.Write(SecondFour);
        }
    }
}
