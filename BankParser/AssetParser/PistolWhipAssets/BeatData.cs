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
    public sealed class BeatData : MonoBehaviourObject, INeedAssetsMetadata
    {
        public Single time { get; set; }
        List<TargetData> targets { get; set; }
        List<ObstacleData> obstacles { get; set; }

        private AssetsFile file;
        private AssetsObject owner;
        public BeatData(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public BeatData(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("BeatData"))
        { }

        public override void ParseObject(AssetsReader reader)
        {
            time = reader.ReadSingle();
            targets = reader.ReadArrayOf((r) => new TargetData(ObjectInfo, r, true));
            obstacles = reader.ReadArrayOf((r) => new ObstacleData(ObjectInfo, r, true));
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            writer.Write(time);
            writer.WriteArrayOf(targets, (item, w) => item.Write(w));
            writer.WriteArrayOf(obstacles, (item, w) => item.Write(w));
        }
    }
}
