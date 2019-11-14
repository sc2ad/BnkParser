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
    public sealed class LevelData : MonoBehaviourObject, INeedAssetsMetadata
    {

        public Single playerPathLength { get; set; }
        public int worldBegin { get; set; }
        public int worldEnd { get; set; }
        public bool isPlayable { get; set; }
        // Align4
        public ISmartPtr<WwiseStateReference> songSwitch { get; set; }
        public List<GameMap> maps { get; set; }
        // Starts at 0x1B4
        public List<TrackSection> sections { get; set; }
        // Starts at 0xB20
        public List<WorldRegion> regions { get; set; }
        // Starts at 0x1394
        public List<WorldObject> worldObjects { get; set; }
        // Starts at 0x1400
        public List<WorldObject> simpleStaticWorldObjects { get; set; }
        public List<WorldObject> simpleDynamicWorldObjects { get; set; }
        public List<CullingRange> staticCullingRanges { get; set; }
        // Starts at 0x8858
        public List<CullingRange> dynamicCullingRanges { get; set; }
        // Starts at 0x885C
        public float playerSpeed { get; set; }
        public string songName { get; set; }
        public Single songLength { get; set; }
        [JsonIgnore]
        public byte[] sectionData { get; set; }

        public LevelData(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool readLiteral = false) : base(objectInfo, reader, readLiteral)
        {
        }

        public LevelData(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("LevelData"))
        { }

        public override void ParseObject(AssetsReader reader)
        {
            playerPathLength = reader.ReadSingle();
            worldBegin = reader.ReadInt32();
            worldEnd = reader.ReadInt32();
            isPlayable = reader.ReadBoolean();
            reader.AlignTo(4);
            songSwitch = SmartPtr<WwiseStateReference>.Read(ObjectInfo.ParentFile, this, reader);
            maps = reader.ReadArrayOf((r) => new GameMap(ObjectInfo, r, true));
            sections = reader.ReadArrayOf((r) => new TrackSection(ObjectInfo, r, true));
            regions = reader.ReadArrayOf((r) => new WorldRegion(ObjectInfo, r, true));
            worldObjects = reader.ReadArrayOf((r) => new WorldObject(ObjectInfo, r, true));
            simpleStaticWorldObjects = reader.ReadArrayOf((r) => new WorldObject(ObjectInfo, r, true));
            simpleDynamicWorldObjects = reader.ReadArrayOf((r) => new WorldObject(ObjectInfo, r, true));
            staticCullingRanges = reader.ReadArrayOf((r) => new CullingRange(ObjectInfo, r, true));
            dynamicCullingRanges = reader.ReadArrayOf((r) => new CullingRange(ObjectInfo, r, true));
            playerSpeed = reader.ReadSingle();
            songName = reader.ReadString();
            songLength = reader.ReadSingle();
            sectionData = reader.ReadArray();
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            writer.Write(playerPathLength);
            writer.Write(worldBegin);
            writer.Write(worldEnd);
            writer.Write(isPlayable);
            writer.AlignTo(4);
            songSwitch.WritePtr(writer);
            writer.WriteArrayOf(maps, (item, w) => item.Write(w));
            writer.WriteArrayOf(sections, (item, w) => item.Write(w));
            writer.WriteArrayOf(regions, (item, w) => item.Write(w));
            writer.WriteArrayOf(worldObjects, (item, w) => item.Write(w));
            writer.WriteArrayOf(simpleStaticWorldObjects, (item, w) => item.Write(w));
            writer.WriteArrayOf(simpleDynamicWorldObjects, (item, w) => item.Write(w));
            writer.WriteArrayOf(staticCullingRanges, (item, w) => item.Write(w));
            writer.WriteArrayOf(dynamicCullingRanges, (item, w) => item.Write(w));
            writer.Write(playerSpeed);
            writer.Write(songName);
            writer.Write(songLength);
            writer.Write(sectionData);
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
