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
    public sealed class EnemyAction : MonoBehaviourObject, INeedAssetsMetadata
    {
        public Single duration { get; set; }
        public WorldPoint localStartingPoint { get; set; }
        public WorldPoint localEndingPoint { get; set; }
        public ISmartPtr<EnemySequence> sequence { get; set; }
        public Single sequenceStartTime { get; set; }

        public EnemyAction(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public EnemyAction(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("EnemyAction"))
        { }

        public override void ParseObject(AssetsReader reader)
        {
            duration = reader.ReadSingle();
            localStartingPoint = new WorldPoint(reader);
            localEndingPoint = new WorldPoint(reader);
            sequence = SmartPtr<EnemySequence>.Read(ObjectInfo.ParentFile, this, reader);
            sequenceStartTime = reader.ReadSingle();
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            writer.Write(duration);
            localStartingPoint.Write(writer);
            localEndingPoint.Write(writer);
            sequence.WritePtr(writer);
            writer.Write(sequenceStartTime);
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
