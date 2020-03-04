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
    public sealed class EnemyActionAndFire : EnemyAction, INeedAssetsMetadata
    {
        public float actionDuration { get; set; }
        public bool stopFacingOnExit { get; set; }
        // Align4
        public bool stopLookingOnExit { get; set; }
        // Align4
        public float fireTime { get; set; }
        [JsonConstructor]
        public EnemyActionAndFire() { }
        public EnemyActionAndFire(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public EnemyActionAndFire(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("EnemyActionAndFire"))
        { }

        public override void ParseObject(AssetsReader reader)
        {
            base.ParseObject(reader);
            actionDuration = reader.ReadSingle();
            stopFacingOnExit = reader.ReadBoolean();
            reader.AlignTo(4);
            stopLookingOnExit = reader.ReadBoolean();
            reader.AlignTo(4);
            fireTime = reader.ReadSingle();
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            base.WriteObject(writer);
            writer.Write(actionDuration);
            writer.Write(stopFacingOnExit);
            writer.AlignTo(4);
            writer.Write(stopLookingOnExit);
            writer.AlignTo(4);
            writer.Write(fireTime);
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
