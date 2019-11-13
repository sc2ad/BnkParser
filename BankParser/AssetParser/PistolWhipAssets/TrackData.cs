using AssetParser.AssetsChanger;
using AssetParser.AssetsChanger.Assets;
using AssetParser.AssetsChanger.Interfaces;
using AssetParser.PistolWhipAssets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.PistolWhipAssets
{
    public sealed class TrackData : MonoBehaviourObject, INeedAssetsMetadata
    {
        public ISmartPtr<LevelData> level { get; set; }
        public Difficulty difficulty { get; set; }
        public ISmartPtr<Koreography> koreography { get; set; }
        public Single playerSpeed { get; set; }
        List<Single> beatTimes { get; set; }
        List<BeatData> beats { get; set; }

        public TrackData(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool readLiteral = false) : base(objectInfo, reader, readLiteral)
        {
        }

        public TrackData(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("TrackData"))
        { }

        public override void ParseObject(AssetsReader reader)
        {
            level = SmartPtr<LevelData>.Read(ObjectInfo.ParentFile, this, reader);
            difficulty = reader.ReadEnum<Difficulty>();
            koreography = SmartPtr<Koreography>.Read(ObjectInfo.ParentFile, this, reader);
            playerSpeed = reader.ReadSingle();
            beatTimes = reader.ReadArrayOf((r) => r.ReadSingle());
            beats = reader.ReadArrayOf((r) => new BeatData(ObjectInfo, r, true));
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            level.WritePtr(writer);
            writer.Write((int)difficulty);
            koreography.WritePtr(writer);
            writer.Write(playerSpeed);
            writer.WriteArrayOf(beatTimes, (item, w) => w.Write(item));
            writer.WriteArrayOf(beats, (item, w) => item.Write(w));
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

    public enum Difficulty
    {
        Easy,
        Normal,
        Hard,
        Expert
    }
}
