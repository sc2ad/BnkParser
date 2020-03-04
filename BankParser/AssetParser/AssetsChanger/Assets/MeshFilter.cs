using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.AssetsChanger.Assets
{
    public class MeshFilter : Component
    {
        public MeshFilter(AssetsFile assetsFile) : base(assetsFile, AssetsConstants.ClassID.MeshFilterClassID)
        {
        }

        public MeshFilter(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader) : base(objectInfo, reader)
        {
        }
        public SmartPtr<MeshObject> Mesh { get; set; }

        public override void ParseObject(AssetsReader reader)
        {
            Mesh = SmartPtr<MeshObject>.Read(ObjectInfo.ParentFile, this, reader);
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            Mesh.WritePtr(writer);
        }
    }
}
