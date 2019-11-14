using AssetParser.AssetsChanger.Interfaces;
using AssetParser.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.AssetsChanger.Assets
{
    public sealed class GameObject : AssetsObject, IHaveName
    {
        public GameObject(AssetsFile assetsFile) : base(assetsFile, AssetsConstants.ClassID.GameObjectClassID)
        {
            IsActive = true;
        }

        public GameObject(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader) : base(objectInfo)
        {
            Parse(reader);
        }

        protected GameObject(IObjectInfo<AssetsObject> objectInfo) : base(objectInfo)
        { IsActive = true; }


        public override void ParseObject(AssetsReader reader)
        {
            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
                Components.Add(SmartPtr<AssetsObject>.Read(ObjectInfo.ParentFile, this, reader));
            Layer = reader.ReadUInt32();
            Name = reader.ReadString();
            Tag = reader.ReadUInt16();
            IsActive = reader.ReadBoolean();
        }

        protected override void WriteBase(AssetsWriter writer)
        {
            base.WriteBase(writer);
            writer.Write(Components.Count);
            foreach (var c in Components)
                c.Write(writer);
            writer.Write(Layer);
            writer.Write(Name);
            writer.Write(Tag);
            writer.Write(IsActive);
        }

        public List<ISmartPtr<AssetsObject>> Components { get; set; } = new List<ISmartPtr<AssetsObject>>();

        public UInt32 Layer { get; set; }

        public string Name { get; set; }

        public UInt16 Tag { get; set; }

        public bool IsActive { get; set; }

        [System.ComponentModel.Browsable(false)]
        [Newtonsoft.Json.JsonIgnore]
        public override byte[] Data { get => throw new InvalidOperationException("Data cannot be accessed from this class!"); set => throw new InvalidOperationException("Data cannot be accessed from this class!"); }
    }
}
