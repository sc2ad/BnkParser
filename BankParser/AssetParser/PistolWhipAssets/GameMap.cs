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
    public class GameMap : MonoBehaviourObject, INeedAssetsMetadata
    {
        public ISmartPtr<LevelData> data { get; set; }
        public ISmartPtr<TrackData> trackData { get; set; }
        public ISmartPtr<GeoSet> geoSet { get; set; }
        public bool isPlayable { get; set; }
        // Align4
        public int enemyIgnoreCount { get; set; }
        public int enemyHitIgnoreCount { get; set; }
        public int enemyCount { get; set; }
        public int trueMaxScore { get; set; }
        public int rankMaxScore { get; set; }
        public List<int> rankQuotas { get; set; }

        public GameMap(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool readLiteral = false) : base(objectInfo, reader, readLiteral)
        {
        }

        public GameMap(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("GameMap"))
        { }

        public override void ParseObject(AssetsReader reader)
        {
            data = SmartPtr<LevelData>.Read(ObjectInfo.ParentFile, this, reader);
            trackData = SmartPtr<TrackData>.Read(ObjectInfo.ParentFile, this, reader);
            geoSet = SmartPtr<GeoSet>.Read(ObjectInfo.ParentFile, this, reader);
            isPlayable = reader.ReadBoolean();
            reader.AlignTo(4);
            enemyIgnoreCount = reader.ReadInt32();
            enemyHitIgnoreCount = reader.ReadInt32();
            enemyCount = reader.ReadInt32();
            trueMaxScore = reader.ReadInt32();
            rankMaxScore = reader.ReadInt32();
            rankQuotas = reader.ReadArrayOf((r) => r.ReadInt32());
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            data.WritePtr(writer);
            trackData.WritePtr(writer);
            geoSet.WritePtr(writer);
            writer.Write(isPlayable);
            writer.AlignTo(4);
            writer.Write(enemyIgnoreCount);
            writer.Write(enemyHitIgnoreCount);
            writer.Write(enemyCount);
            writer.Write(trueMaxScore);
            writer.Write(rankMaxScore);
            writer.WriteArrayOf(rankQuotas, (item, w) => w.Write(item));
        }
    }
}
