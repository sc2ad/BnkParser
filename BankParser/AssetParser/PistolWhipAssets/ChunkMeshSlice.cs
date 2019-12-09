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
    public class ChunkMeshSlice
    {
        public int z { get; set; }
        public List<Vector3F> verts { get; set; }
        public List<int> meshSizes { get; set; }
        public List<int> tris { get; set; }
        [JsonConstructor]
        public ChunkMeshSlice()
        { }

        public ChunkMeshSlice(AssetsReader reader)
        {
            Parse(reader);
        }

        private void Parse(AssetsReader reader)
        {
            z = reader.ReadInt32();
            verts = reader.ReadArrayOf((r) => new Vector3F(r));
            meshSizes = reader.ReadArrayOf((r) => r.ReadInt32());
            tris = reader.ReadArrayOf((r) => r.ReadInt32());
        }

        public void Write(AssetsWriter writer)
        {
            writer.Write(z);
            writer.WriteArrayOf(verts, (item, w) => item.Write(w));
            writer.WriteArrayOf(meshSizes, (item, w) => w.Write(item));
            writer.WriteArrayOf(tris, (item, w) => w.Write(item));
        }
    }
}
