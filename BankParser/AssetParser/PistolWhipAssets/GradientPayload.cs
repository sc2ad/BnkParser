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
    public class GradientPayload : MonoBehaviourObject
    {
        public Gradient val { get; set; }
        [JsonConstructor]
        public GradientPayload() { }
        public GradientPayload(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("GradientPayload"))
        {
        }

        public GradientPayload(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public override void ParseObject(AssetsReader reader)
        {
            val = new Gradient(ObjectInfo, reader, true);
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            val.Write(writer);
        }
    }
}
