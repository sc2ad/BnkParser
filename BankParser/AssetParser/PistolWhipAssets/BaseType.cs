using AssetParser.AssetsChanger;
using AssetParser.AssetsChanger.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.PistolWhipAssets
{
    public class BaseType : MonoBehaviourObject
    {
        public int idInternal { get; set; }
        public byte[] valueGuidInternal { get; set; }
        public BaseType(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public BaseType(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("BaseType"))
        { }

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
