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
    public class TargetData : MonoBehaviourObject, INeedAssetsMetadata
    {
        public Distance distance { get; set; }
        public Vector2F placement { get; set; }
        public EnemyToughness toughness { get; set; }
        public WorldPoint enemyOffset { get; set; }
        public SmartPtr<EnemySequence> enemySequence { get; set; }
        public bool ignoreForLevelRank { get; set; }
        // Align4
        [JsonConstructor]
        public TargetData() { }
        public TargetData(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public TargetData(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("TargetData"))
        { }

        public override void ParseObject(AssetsReader reader)
        {
            distance = reader.ReadEnum<Distance>();
            placement = new Vector2F(reader);
            toughness = reader.ReadEnum<EnemyToughness>();
            enemyOffset = new WorldPoint(reader);
            enemySequence = SmartPtr<EnemySequence>.Read(ObjectInfo.ParentFile, this, reader);
            ignoreForLevelRank = reader.ReadBoolean();
            reader.AlignTo(4);
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            writer.Write((int)distance);
            placement.Write(writer);
            writer.Write((int)toughness);
            enemyOffset.Write(writer);
            enemySequence.WritePtr(writer);
            writer.Write(ignoreForLevelRank);
            writer.AlignTo(4);
        }
    }

    public enum Distance
    {
        Near,
        Middle,
        Far
    }
    public enum EnemyToughness
    {
        Normal,
        Tough,
        ChuckNorris
    }
}
