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
    public class LevelDecoratorBase : MonoBehaviourObject, INeedAssetsMetadata
    {
        public List<GridDecorationSet> gridDecorations { get; set; }
        public int minimumAttempts { get; set; }
        public bool showSuccessIndicators { get; set; }
        public bool showFailureIndicators { get; set; }
        public bool showRegionBoundsIndicators { get; set; }
        public bool showPropBoundsIndicators { get; set; }

        public LevelDecoratorBase(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public LevelDecoratorBase(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("LevelDecoratorBase"))
        { }

        public LevelDecoratorBase(AssetsFile assetsFile, MonoScriptObject monoScriptObject) : base(assetsFile, monoScriptObject)
        {
        }

        public override void ParseObject(AssetsReader reader)
        {
            gridDecorations = reader.ReadArrayOf((r) => new GridDecorationSet(ObjectInfo, r));
            minimumAttempts = reader.ReadInt32();
            showSuccessIndicators = reader.ReadBoolean();
            reader.AlignTo(4);
            showFailureIndicators = reader.ReadBoolean();
            reader.AlignTo(4);
            showRegionBoundsIndicators = reader.ReadBoolean();
            reader.AlignTo(4);
            showPropBoundsIndicators = reader.ReadBoolean();
            reader.AlignTo(4);
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            writer.WriteArrayOf(gridDecorations, (item, w) => item.Write(w));
            writer.Write(minimumAttempts);
            writer.Write(showSuccessIndicators);
            writer.AlignTo(4);
            writer.Write(showFailureIndicators);
            writer.AlignTo(4);
            writer.Write(showRegionBoundsIndicators);
            writer.AlignTo(4);
            writer.Write(showPropBoundsIndicators);
            writer.AlignTo(4);
        }

        public class GridDecorationSet
        {
            public DecorationSet decorations { get; set; }
            public Vector2I grid { get; set; }
            public Single slop { get; set; }
            public Vector2I offset { get; set; }
            public Vector2I limits { get; set; }
            public bool flipY { get; set; }
            private IObjectInfo<AssetsObject> objectinfo;
            public GridDecorationSet(IObjectInfo<AssetsObject> oi, AssetsReader reader)
            {
                objectinfo = oi;
                Parse(reader);
            }
            private void Parse(AssetsReader reader)
            {
                decorations = new DecorationSet(objectinfo, reader, true);
                grid = new Vector2I(reader);
                slop = reader.ReadSingle();
                offset = new Vector2I(reader);
                limits = new Vector2I(reader);
                flipY = reader.ReadBoolean();
                reader.AlignTo(4);
            }
            public void Write(AssetsWriter writer)
            {
                decorations.Write(writer);
                grid.Write(writer);
                writer.Write(slop);
                offset.Write(writer);
                limits.Write(writer);
                writer.Write(flipY);
                writer.AlignTo(4);
            }
        }
    }
}
