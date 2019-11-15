using AssetParser.AssetsChanger;
using AssetParser.AssetsChanger.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.PistolWhipAssets
{
    public class WwiseGroupValueObjectReference : WwiseObjectReference
    {
        public WwiseGroupValueObjectReference(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("WwiseGroupValueObjectReference"))
        {
        }

        public WwiseGroupValueObjectReference(AssetsFile assetsFile, MonoScriptObject scriptObject) : base(assetsFile, scriptObject)
        {
        }

        public WwiseGroupValueObjectReference(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
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
