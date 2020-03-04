using AssetParser.AssetsChanger.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.AssetsChanger.Assets
{
    public class PhysicMaterial : Component, IHaveName
    {
        public PhysicMaterial(AssetsFile assetsFile) : base(assetsFile, AssetsConstants.ClassID.PhysicMaterialClassID)
        {
        }

        public PhysicMaterial(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader) : base(objectInfo)
        {
            Parse(reader);
        }

        public override void ParseObject(AssetsReader reader)
        {
            Name = reader.ReadString();
            dynamicFriction = reader.ReadSingle();
            staticFriction = reader.ReadSingle();
            bounciness = reader.ReadSingle();
            frictionCombine = reader.ReadInt32();
            bounceCombine = reader.ReadInt32();
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            writer.Write(Name);
            writer.Write(dynamicFriction);
            writer.Write(staticFriction);
            writer.Write(bounciness);
            writer.Write(frictionCombine);
            writer.Write(bounceCombine);
        }

        public string Name { get; set; }
        public Single dynamicFriction { get; set; }
        public Single staticFriction { get; set; }
        public Single bounciness { get; set; }
        public int frictionCombine { get; set; }
        public int bounceCombine { get; set; }

        [System.ComponentModel.Browsable(false)]
        [Newtonsoft.Json.JsonIgnore]
        public override byte[] Data { get => throw new InvalidOperationException("Data cannot be accessed from this class!"); set => throw new InvalidOperationException("Data cannot be accessed from this class!"); }
    }
}
