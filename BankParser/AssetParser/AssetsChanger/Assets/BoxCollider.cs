using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.AssetsChanger.Assets
{
    public class BoxCollider : Collider
    {
        public BoxCollider(AssetsFile assetsFile) : base(assetsFile, AssetsConstants.ClassID.BoxColliderClassID)
        {
        }

        public BoxCollider(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader) : base(objectInfo)
        {
            Parse(reader);
        }

        public override void ParseObject(AssetsReader reader)
        {
            base.ParseObject(reader);
            Size = new Vector3F(reader);
            Center = new Vector3F(reader);
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            base.WriteObject(writer);
            Size.Write(writer);
            Center.Write(writer);
        }

        public Vector3F Size { get; set; }
        public Vector3F Center { get; set; }
    }
}
