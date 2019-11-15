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
    public class GroundDecorator : LevelDecoratorBase, INeedAssetsMetadata
    {
        public SmartPtr<DecorationSet> decorations { get; set; }
        public Single decorationChance { get; set; }
        public Single decorationDensity { get; set; }
        public GroundDecorator(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public GroundDecorator(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("GroundDecorator"))
        { }

        public override void ParseObject(AssetsReader reader)
        {
            base.ParseObject(reader);
            decorations = SmartPtr<DecorationSet>.Read(ObjectInfo.ParentFile, this, reader);
            decorationChance = reader.ReadSingle();
            decorationDensity = reader.ReadSingle();
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            base.WriteObject(writer);
            decorations.WritePtr(writer);
            writer.Write(decorationChance);
            writer.Write(decorationDensity);
        }
    }
}
