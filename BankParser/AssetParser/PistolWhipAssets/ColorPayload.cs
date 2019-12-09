using AssetParser.AssetsChanger;
using AssetParser.AssetsChanger.Assets;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.PistolWhipAssets
{
    public class ColorPayload : MonoBehaviourObject
    {
        public Color val { get; set; }
        [JsonConstructor]
        public ColorPayload() { }
        public ColorPayload(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("ColorPayload"))
        {
        }

        public ColorPayload(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public override void ParseObject(AssetsReader reader)
        {
            val = new Color(reader);
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            val.Write(writer);
        }
    }
}
