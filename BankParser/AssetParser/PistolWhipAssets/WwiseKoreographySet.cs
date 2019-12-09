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
    public class WwiseKoreographySet : MonoBehaviourObject
    {
        public List<WwiseKoreoMediaIDEntry> koreographies { get; set; }
        [JsonConstructor]
        public WwiseKoreographySet() { }
        public WwiseKoreographySet(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("WwiseKoreographySet"))
        {
        }

        public WwiseKoreographySet(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public override void ParseObject(AssetsReader reader)
        {
            koreographies = reader.ReadArrayOf((r) => new WwiseKoreoMediaIDEntry(ObjectInfo, r, true));
        }
        protected override void WriteObject(AssetsWriter writer)
        {
            writer.WriteArrayOf(koreographies, (item, w) => item.Write(w));
        }
    }
}
