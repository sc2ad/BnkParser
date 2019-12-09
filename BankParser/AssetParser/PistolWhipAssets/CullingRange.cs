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
    public class CullingRange : MonoBehaviourObject, INeedAssetsMetadata
    {
        public int start { get; set; }
        public int end { get; set; }
        public List<WorldObject> members { get; set; }
        [JsonConstructor]
        public CullingRange() { }

        public CullingRange(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public CullingRange(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("CullingRange"))
        { }

        public override void ParseObject(AssetsReader reader)
        {
            start = reader.ReadInt32();
            end = reader.ReadInt32();
            members = reader.ReadArrayOf((r) => new WorldObject(ObjectInfo, r, true));
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            writer.Write(start);
            writer.Write(end);
            writer.WriteArrayOf(members, (item, w) => item.Write(w));
        }
    }
}
