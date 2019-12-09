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
    public class LevelAssetDatabase : MonoBehaviourObject, INeedAssetsMetadata
    {
        public SmartPtr<MeshObject> baseMesh { get; set; }
        public List<SmartPtr<MaterialObject>> worldMats { get; set; }
        public SmartPtr<GameObject> obstacle { get; set; }
        public SmartPtr<GameObject> sidestepObstacle { get; set; }
        public SmartPtr<GameObject> limboObstacleTall { get; set; }
        public SmartPtr<GameObject> limboObstacleShort { get; set; }
        public List<SmartPtr<LevelDecoratorBase>> levelDecorators { get; set; }
        [JsonConstructor]
        public LevelAssetDatabase() { }
        public LevelAssetDatabase(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public LevelAssetDatabase(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("LevelAssetDatabase"))
        { }

        public override void ParseObject(AssetsReader reader)
        {
            baseMesh = SmartPtr<MeshObject>.Read(ObjectInfo.ParentFile, this, reader);
            worldMats = reader.ReadArrayOf((r) => SmartPtr<MaterialObject>.Read(ObjectInfo.ParentFile, this, r));
            obstacle = SmartPtr<GameObject>.Read(ObjectInfo.ParentFile, this, reader);
            sidestepObstacle = SmartPtr<GameObject>.Read(ObjectInfo.ParentFile, this, reader);
            limboObstacleTall = SmartPtr<GameObject>.Read(ObjectInfo.ParentFile, this, reader);
            limboObstacleShort = SmartPtr<GameObject>.Read(ObjectInfo.ParentFile, this, reader);
            levelDecorators = reader.ReadArrayOf((r) => SmartPtr<LevelDecoratorBase>.Read(ObjectInfo.ParentFile, this, r));
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            baseMesh.WritePtr(writer);
            writer.WriteArrayOf(worldMats, (item, w) => item.WritePtr(w));
            obstacle.WritePtr(writer);
            sidestepObstacle.WritePtr(writer);
            limboObstacleTall.WritePtr(writer);
            limboObstacleShort.WritePtr(writer);
            writer.WriteArrayOf(levelDecorators, (item, w) => item.WritePtr(w));
        }
    }
}
