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
    public class GeoSet : MonoBehaviourObject, INeedAssetsMetadata
    {
        public SmartPtr<TrackData> track { get; set; }
        public Vector3I chunkSize { get; set; }
        public Single scale { get; set; }
        // 0x54
        public List<ChunkMeshData> chunkData { get; set; }
        public List<ChunkMeshSlice> chunkSlices { get; set; }
        public List<WorldObject> staticProps { get; set; }
        public List<WorldObject> dynamicProps { get; set; }
        // 0x195D4C
        public List<OscillatingObjectData> decoratorCubes { get; set; }

        public GeoSet(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool readLiteral = false) : base(objectInfo, reader, readLiteral)
        {
        }

        public GeoSet(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("GeoSet"))
        { }

        public override void ParseObject(AssetsReader reader)
        {
            track = SmartPtr<TrackData>.Read(ObjectInfo.ParentFile, this, reader);
            chunkSize = new Vector3I(reader);
            scale = reader.ReadSingle();
            // 0x58
            chunkData = reader.ReadArrayOf((r) => new ChunkMeshData(r));
            // 0xA4758 WRONG
            chunkSlices = reader.ReadArrayOf((r) => new ChunkMeshSlice(r));
            staticProps = reader.ReadArrayOf((r) => new WorldObject(ObjectInfo, r, true));
            dynamicProps = reader.ReadArrayOf((r) => new WorldObject(ObjectInfo, r, true));
            decoratorCubes = reader.ReadArrayOf((r) => new OscillatingObjectData(r));
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            track.WritePtr(writer);
            chunkSize.Write(writer);
            writer.Write(scale);
            writer.WriteArrayOf(chunkData, (item, w) => item.Write(w));
            writer.WriteArrayOf(chunkSlices, (item, w) => item.Write(w));
            writer.WriteArrayOf(staticProps, (item, w) => item.Write(w));
            writer.WriteArrayOf(dynamicProps, (item, w) => item.Write(w));
            writer.WriteArrayOf(decoratorCubes, (item, w) => item.Write(w));
        }
    }
}
