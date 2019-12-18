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
    public class Event : BaseType
    {
        public SmartPtr<WwiseObjectReference> wwiseObjectReference { get; set; }
        [JsonConstructor]
        public Event() { }
        public Event(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public Event(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("Event"))
        { }

        public override void ParseObject(AssetsReader reader)
        {
            base.ParseObject(reader);
            wwiseObjectReference = SmartPtr<WwiseObjectReference>.Read(ObjectInfo.ParentFile, this, reader);
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            base.WriteObject(writer);
            wwiseObjectReference.WritePtr(writer);
        }
    }
}
