using AssetParser.AssetsChanger;
using AssetParser.AssetsChanger.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.PistolWhipAssets
{
    public class IntPayload : MonoBehaviourObject
    {
        public int val { get; set; }
        public IntPayload(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("IntPayload"))
        {
        }

        public IntPayload(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public override void ParseObject(AssetsReader reader)
        {
            val = reader.ReadInt32();
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            writer.Write(val);
        }
    }
}
