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
    public class WwiseStateReference : WwiseGroupValueObjectReference, INeedAssetsMetadata
    {
        public SmartPtr<WwiseStateGroupReference> WwiseStateGroupReference { get; set; }

        public WwiseStateReference(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public WwiseStateReference(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("WwiseStateReference"))
        { }

        public override void ParseObject(AssetsReader reader)
        {
            base.ParseObject(reader);
            WwiseStateGroupReference = SmartPtr<WwiseStateGroupReference>.Read(ObjectInfo.ParentFile, this, reader);
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            base.WriteObject(writer);
            WwiseStateGroupReference.WritePtr(writer);
        }
    }
}
