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
    public sealed class EnemyActionRootMotion : EnemyAction, INeedAssetsMetadata
    {
        public ActorAction action { get; set; }
        public int triggerHash { get; set; }
        // RootMotionReference
        public SmartPtr<AssetsObject> motionData { get; set; }
        [JsonConstructor]
        public EnemyActionRootMotion() { }
        public EnemyActionRootMotion(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public EnemyActionRootMotion(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("EnemyActionRootMotion"))
        { }

        public override void ParseObject(AssetsReader reader)
        {
            base.ParseObject(reader);
            action = reader.ReadEnum<ActorAction>();
            triggerHash = reader.ReadInt32();
            motionData = SmartPtr<AssetsObject>.Read(ObjectInfo.ParentFile, this, reader);
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            base.WriteObject(writer);
            writer.Write((int)action);
            writer.Write(triggerHash);
            motionData.WritePtr(writer);
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
