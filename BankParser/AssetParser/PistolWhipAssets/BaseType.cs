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
    public class BaseType : MonoBehaviourObject
    {
        // Enemy: 0x6C
        public int idInternal { get; set; }
        // Enemy: 0x70
        public byte[] valueGuidInternal { get; set; }
        [JsonConstructor]
        public BaseType() { }
        public BaseType(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public BaseType(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("BaseType"))
        { }

        public BaseType(AssetsFile assetsFile, MonoScriptObject scriptObject) : base(assetsFile, scriptObject)
        {
        }

        public override void ParseObject(AssetsReader reader)
        {
            idInternal = reader.ReadInt32();
            valueGuidInternal = reader.ReadArray();
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            writer.Write(idInternal);
            writer.Write(valueGuidInternal);
        }
    }
}
