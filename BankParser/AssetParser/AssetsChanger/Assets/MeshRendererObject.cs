using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.AssetsChanger.Assets
{
    public sealed class MeshRendererObject : Renderer
    {
        public MeshRendererObject(AssetsFile assetsFile) : base(assetsFile, AssetsConstants.ClassID.MeshFilterClassID)
        {
        }

        public MeshRendererObject(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader) : base(objectInfo, reader)
        {
        }
        public SmartPtr<MeshObject> AdditionalVertexStreams { get; set; }

        public override void ParseObject(AssetsReader reader)
        {
            base.ParseObject(reader);
            AdditionalVertexStreams = SmartPtr<MeshObject>.Read(ObjectInfo.ParentFile, this, reader);
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            base.WriteObject(writer);
            AdditionalVertexStreams.WritePtr(writer);
        }
    }
}
