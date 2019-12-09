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
    public class WwiseKoreoMediaIDEntry : MonoBehaviourObject, INeedAssetsMetadata
    {
        public SmartPtr<Koreography> koreo { get; set; }
        public uint mediaID { get; set; }
        [JsonConstructor]
        public WwiseKoreoMediaIDEntry() { }
        public WwiseKoreoMediaIDEntry(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("WwiseKoreoMediaIDEntry"))
        {
        }

        public WwiseKoreoMediaIDEntry(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public override void ParseObject(AssetsReader reader)
        {
            koreo = SmartPtr<Koreography>.Read(ObjectInfo.ParentFile, this, reader);
            mediaID = reader.ReadUInt32();
        }
        protected override void WriteObject(AssetsWriter writer)
        {
            koreo.WritePtr(writer);
            writer.Write(mediaID);
        }
    }
}
