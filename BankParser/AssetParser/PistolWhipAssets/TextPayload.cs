using AssetParser.AssetsChanger;
using AssetParser.AssetsChanger.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.PistolWhipAssets
{
    public class TextPayload : MonoBehaviourObject
    {
        public string val { get; set; }
        public TextPayload(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("TextPayload"))
        {
        }

        public TextPayload(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public override void ParseObject(AssetsReader reader)
        {
            val = reader.ReadString();
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            writer.Write(val);
        }
    }
}
