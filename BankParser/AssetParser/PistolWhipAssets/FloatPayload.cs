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
    public class FloatPayload : MonoBehaviourObject
    {
        public Single val { get; set; }
        [JsonConstructor]
        public FloatPayload() { }
        public FloatPayload(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("FloatPayload"))
        {
        }

        public FloatPayload(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public override void ParseObject(AssetsReader reader)
        {
            val = reader.ReadSingle();
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            writer.Write(val);
        }
    }
}
