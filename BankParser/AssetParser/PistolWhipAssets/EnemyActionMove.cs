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
    public sealed class EnemyActionMove : EnemyAction, INeedAssetsMetadata
    {
        public WorldPoint destination { get; set; }
        public MoveSpeed speed { get; set; }
        public Facing facing { get; set; }
        [JsonConstructor]
        public EnemyActionMove() { }
        public EnemyActionMove(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public EnemyActionMove(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("EnemyActionMove"))
        { }

        public override void ParseObject(AssetsReader reader)
        {
            base.ParseObject(reader);
            destination = new WorldPoint(reader);
            speed = reader.ReadEnum<MoveSpeed>();
            facing = reader.ReadEnum<Facing>();
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            base.WriteObject(writer);
            destination.Write(writer);
            writer.Write((int)speed);
            writer.Write((int)facing);
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
        public enum Facing
        {
            Foward,
            Player,
            Custom
        }
    }
    public enum MoveSpeed
    {
        Walk,
        Run
    }
}
