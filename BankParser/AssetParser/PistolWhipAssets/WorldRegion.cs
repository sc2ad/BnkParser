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
    public class WorldRegion : MonoBehaviourObject
    {
        public Vector3I position { get; set; }
        public Vector3I min { get; set; }
        public Vector3I max { get; set; }
        public Region region { get; set; }
        [JsonConstructor]
        public WorldRegion() { }
        public WorldRegion(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public WorldRegion(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("WorldRegion"))
        { }

        public override void ParseObject(AssetsReader reader)
        {
            position = new Vector3I(reader);
            min = new Vector3I(reader);
            max = new Vector3I(reader);
            region = reader.ReadEnum<Region>();
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            position.Write(writer);
            min.Write(writer);
            max.Write(writer);
            writer.Write((int)region);
        }
    }
    public enum Region
    {
        Open,
        Closed
    }
}
