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
    public class WwiseObjectReference : MonoBehaviourObject
    {
        public string objectName { get; set; }
        public uint id { get; set; }
        public string guid { get; set; }
        [JsonConstructor]
        public WwiseObjectReference() { }
        public WwiseObjectReference(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("WwiseObjectReference"))
        {
        }

        public WwiseObjectReference(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public WwiseObjectReference(AssetsFile assetsFile, MonoScriptObject scriptObject) : base(assetsFile, scriptObject)
        {
        }

        public override void ParseObject(AssetsReader reader)
        {
            objectName = reader.ReadString();
            id = reader.ReadUInt32();
            guid = reader.ReadString();
        }
        protected override void WriteObject(AssetsWriter writer)
        {
            writer.Write(objectName);
            writer.Write(id);
            writer.Write(guid);
        }
    }
}
