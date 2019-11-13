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
    public class WorldObject : MonoBehaviourObject, INeedAssetsMetadata
    {
        public WorldPoint point { get; set; }
        public ISmartPtr<GameObject> prefab { get; set; }
        public Vector3F scale { get; set; }

        public WorldObject(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public WorldObject(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("WorldObject"))
        { }

        public override void ParseObject(AssetsReader reader)
        {
            point = new WorldPoint(reader);
            prefab = SmartPtr<GameObject>.Read(ObjectInfo.ParentFile, this, reader);
            scale = new Vector3F(reader);
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            point.Write(writer);
            prefab.WritePtr(writer);
            scale.Write(writer);
        }
    }
}
