using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.AssetsChanger.Assets
{
    public class Collider : Component
    {
        [JsonConstructor]
        public Collider()
        {
        }
        protected Collider(AssetsFile assetsFile, int classID) : base(assetsFile, classID)
        {
        }

        protected Collider(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        protected Collider(AssetsFile assetsFile, Guid typeHash)
        {
            ObjectInfo = ObjectInfo<AssetsObject>.FromTypeHash(assetsFile, typeHash, this);
        }

        protected Collider(IObjectInfo<AssetsObject> objectInfo) : base(objectInfo)
        {
        }

        public Collider(AssetsFile assetsFile) : base(assetsFile, AssetsConstants.ClassID.ColliderClassID)
        {
        }

        public Collider(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader) : base(objectInfo)
        {
            Parse(reader);
        }

        public override void ParseObject(AssetsReader reader)
        {
            Material = SmartPtr<PhysicMaterial>.Read(ObjectInfo.ParentFile, this, reader);
            IsTrigger = reader.ReadBoolean();
            Enabled = reader.ReadBoolean();
            reader.AlignTo(4);
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            Material.WritePtr(writer);
            writer.Write(IsTrigger);
            writer.Write(Enabled);
            writer.AlignTo(4);
        }

        public SmartPtr<PhysicMaterial> Material { get; set; }
        public bool IsTrigger { get; set; }
        public bool Enabled { get; set; }
        // Align4

        [System.ComponentModel.Browsable(false)]
        [Newtonsoft.Json.JsonIgnore]
        public override byte[] Data { get => throw new InvalidOperationException("Data cannot be accessed from this class!"); set => throw new InvalidOperationException("Data cannot be accessed from this class!"); }
    }
}
