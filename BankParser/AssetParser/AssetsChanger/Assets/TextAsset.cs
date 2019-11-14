using AssetParser.AssetsChanger.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.AssetsChanger.Assets
{
    public sealed class TextAsset : AssetsObject, IHaveName
    {
        public string Name { get; set; }
        public string Script { get; set; }

        public TextAsset(AssetsFile assetsFile) : base(assetsFile, AssetsConstants.ClassID.TextAssetClassID)
        {
        }

        public TextAsset(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader) : base(objectInfo)
        {
            Parse(reader);
        }

        public override void ParseObject(AssetsReader reader)
        {
            Name = reader.ReadString();
            Script = reader.ReadString();
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            writer.Write(Name);
            writer.Write(Script);
        }

        [System.ComponentModel.Browsable(false)]
        [Newtonsoft.Json.JsonIgnore]
        public override byte[] Data { get => throw new InvalidOperationException("Data cannot be accessed from this class!"); set => throw new InvalidOperationException("Data cannot be accessed from this class!"); }
    }
}
