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
    public sealed class EnemyActionAimEnd : EnemyAction, INeedAssetsMetadata
    {
        public bool stopFacing { get; set; }
        // Align4
        public bool stopLooking { get; set; }
        // Align4
        [JsonConstructor]
        public EnemyActionAimEnd() { }
        public EnemyActionAimEnd(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public EnemyActionAimEnd(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("EnemyActionAimEnd"))
        { }

        public override void ParseObject(AssetsReader reader)
        {
            base.ParseObject(reader);
            stopFacing = reader.ReadBoolean();
            reader.AlignTo(4);
            stopLooking = reader.ReadBoolean();
            reader.AlignTo(4);
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            base.WriteObject(writer);
            writer.Write(stopFacing);
            writer.AlignTo(4);
            writer.Write(stopLooking);
            writer.AlignTo(4);
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
