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
    public class WwiseEventReference : WwiseObjectReference
    {
        [JsonConstructor]
        public WwiseEventReference() { }
        public WwiseEventReference(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("WwiseEventReference"))
        {
        }

        public WwiseEventReference(AssetsFile assetsFile, MonoScriptObject scriptObject) : base(assetsFile, scriptObject)
        {
        }

        public WwiseEventReference(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public override void ParseObject(AssetsReader reader)
        {
            base.ParseObject(reader);
        }
        protected override void WriteObject(AssetsWriter writer)
        {
            base.WriteObject(writer);
        }
    }
}
