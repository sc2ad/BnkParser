using AssetParser.AssetsChanger;
using AssetParser.AssetsChanger.Assets;
using AssetParser.AssetsChanger.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.PistolWhipAssets
{
    public class TargetableObject : MonoBehaviourObject, INeedAssetsMetadata
    {
        public SmartPtr<Collider> attachedCollider { get; set; }
        public int priority { get; set; }

        [JsonConstructor]
        public TargetableObject() { }
        public TargetableObject(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public TargetableObject(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("TargetableObject"))
        { }

        public override void ParseObject(AssetsReader reader)
        {
            attachedCollider = SmartPtr<Collider>.Read(ObjectInfo.ParentFile, this, reader);
            priority = reader.ReadInt32();
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            attachedCollider.WritePtr(writer);
            writer.Write(priority);
        }
    }
}
