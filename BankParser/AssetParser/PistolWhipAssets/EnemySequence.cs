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
    public sealed class EnemySequence : MonoBehaviourObject, INeedAssetsMetadata
    {
        public EnemyToughness enemyType { get; set; }
        public SmartPtr<GameObject> actionHolder { get; set; }
        // Actually of type EnemyAction
        public List<SmartPtr<MonoBehaviourObject>> actions { get; set; }
        public Single playerActionLerp { get; set; }
        public WorldPoint localActionPoint { get; set; }
        public bool autospawn { get; set; }
        // Align4
        public bool isThreat { get; set; }
        // Align4
        public Single duration { get; set; }
        public SmartPtr<Enemy> actor { get; set; }
        [JsonConstructor]
        public EnemySequence() { }
        public EnemySequence(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool readLiteral = false) : base(objectInfo, reader, readLiteral)
        {
        }

        public EnemySequence(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("EnemySequence"))
        { }

        public override void ParseObject(AssetsReader reader)
        {
            enemyType = reader.ReadEnum<EnemyToughness>();
            actionHolder = SmartPtr<GameObject>.Read(ObjectInfo.ParentFile, this, reader);
            actions = reader.ReadArrayOf((r) => SmartPtr<MonoBehaviourObject>.Read(ObjectInfo.ParentFile, this, r));
            playerActionLerp = reader.ReadSingle();
            localActionPoint = new WorldPoint(reader);
            autospawn = reader.ReadBoolean();
            reader.AlignTo(4);
            isThreat = reader.ReadBoolean();
            reader.AlignTo(4);
            duration = reader.ReadSingle();
            actor = SmartPtr<Enemy>.Read(ObjectInfo.ParentFile, this, reader);
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            writer.Write((int)enemyType);
            actionHolder.WritePtr(writer);
            writer.WriteArrayOf(actions, (item, w) => item.WritePtr(w));
            writer.Write(playerActionLerp);
            localActionPoint.Write(writer);
            writer.Write(autospawn);
            writer.AlignTo(4);
            writer.Write(isThreat);
            writer.AlignTo(4);
            writer.Write(duration);
            actor.WritePtr(writer);
        }

        [System.ComponentModel.Browsable(false)]
        [Newtonsoft.Json.JsonIgnore]
        public override byte[] ScriptParametersData
        {
            get
            {
                throw new InvalidOperationException("Cannot access parameters data from this object.");
            }
            set
            {
                throw new InvalidOperationException("Cannot access parameters data from this object.");
            }
        }
    }
}
