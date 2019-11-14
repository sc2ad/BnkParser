using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.AssetsChanger.Assets
{
    public class SkinnedMeshRenderer : Renderer
    {
        public SkinQuality quality { get; set; }
        public bool updateWhenOffScreen { get; set; }
        // Align4
        public SmartPtr<Transform> rootBone { get; set; }
        public List<SmartPtr<Transform>> bones { get; set; }
        public SmartPtr<MeshObject> sharedMesh { get; set; }
        //public SkinnedMeshRenderer(AssetsFile assetsFile) : base(assetsFile, AssetsConstants.ClassID.MeshFilterClassID)
        //{
        //}

        public SkinnedMeshRenderer(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader) : base(objectInfo, reader)
        {
        }

        public override void ParseObject(AssetsReader reader)
        {
            base.ParseObject(reader);
            quality = reader.ReadEnum<SkinQuality>();
            updateWhenOffScreen = reader.ReadBoolean();
            reader.AlignTo(4);
            rootBone = SmartPtr<Transform>.Read(ObjectInfo.ParentFile, this, reader);
            bones = reader.ReadArrayOf((r) => SmartPtr<Transform>.Read(ObjectInfo.ParentFile, this, r));
            sharedMesh = SmartPtr<MeshObject>.Read(ObjectInfo.ParentFile, this, reader);
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            base.WriteObject(writer);
            writer.Write((int)quality);
            writer.Write(updateWhenOffScreen);
            writer.AlignTo(4);
            rootBone.WritePtr(writer);
            writer.WriteArrayOf(bones, (item, w) => item.WritePtr(w));
            sharedMesh.WritePtr(writer);
        }
    }
    public enum SkinQuality
    {
        Auto,
        Bone1,
        Bone2,
        Bone4 = 4
    }
}
