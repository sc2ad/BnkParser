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
    public sealed class EnemyActionInstant : EnemyAction, INeedAssetsMetadata
    {
        public ActorAction action { get; set; }
        public int triggerHash { get; set; }
        [JsonConstructor]
        public EnemyActionInstant() { }
        public EnemyActionInstant(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public EnemyActionInstant(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("EnemyActionInstant"))
        { }

        public override void ParseObject(AssetsReader reader)
        {
            base.ParseObject(reader);
            action = reader.ReadEnum<ActorAction>();
            triggerHash = reader.ReadInt32();
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            base.WriteObject(writer);
            writer.Write((int)action);
            writer.Write(triggerHash);
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
    public enum ActorAction
    {
        Wait,
        Move,
        AimStart,
        AimStop,
        Fire,
        AimAndFire,
        Crouch,
        Stand,
        Slide,
        Roll,
        Vault,
        StandingCoverIdle,
        MeleeKick,
        PopOut,
        HideStanding,
        StopFiring,
        Guard,
        Dance_01,
        Dance_02
    }
}
