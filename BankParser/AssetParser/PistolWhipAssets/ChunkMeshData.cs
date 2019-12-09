using AssetParser.AssetsChanger;
using AssetParser.AssetsChanger.Assets;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.PistolWhipAssets
{
    public class ChunkMeshData
    {
        // 0x58
        public Vector3I id { get; set; }
        // 0x64
        // 44 * 3 * 4 + 0x68
        public List<Vector3F> verts { get; set; }
        // 0x278
        public List<int> meshSizes { get; set; }
        // 0x284
        public List<int> tris { get; set; }
        [JsonConstructor]
        public ChunkMeshData()
        { }

        public ChunkMeshData(AssetsReader reader)
        {
            Parse(reader);
        }

        private void Parse(AssetsReader reader)
        {
            id = new Vector3I(reader);
            verts = reader.ReadArrayOf((r) => new Vector3F(r));
            meshSizes = reader.ReadArrayOf((r) => r.ReadInt32());
            tris = reader.ReadArrayOf((r) => r.ReadInt32());
        }

        public void Write(AssetsWriter writer)
        {
            id.Write(writer);
            writer.WriteArrayOf(verts, (item, w) => item.Write(w));
            writer.WriteArrayOf(meshSizes, (item, w) => w.Write(item));
            writer.WriteArrayOf(tris, (item, w) => w.Write(item));
        }
    }
}
