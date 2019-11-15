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
    public class WallDecorator : LevelDecoratorBase, INeedAssetsMetadata
    {
        public int upperFloorAttempts { get; set; }
        public SmartPtr<DecorationSet> groundFloor { get; set; }
        public SmartPtr<DecorationSet> upperFloor { get; set; }
        public int lowestFloor { get; set; }
        public int highestFloor { get; set; }
        public Single groundFloorChance { get; set; }
        public Single groundFloorDensity { get; set; }
        public Single upperFloorChance { get; set; }
        public Single upperFloorDensity { get; set; }
        public WallDecorator(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public WallDecorator(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("WallDecorator"))
        { }

        public override void ParseObject(AssetsReader reader)
        {
            base.ParseObject(reader);
            upperFloorAttempts = reader.ReadInt32();
            groundFloor = SmartPtr<DecorationSet>.Read(ObjectInfo.ParentFile, this, reader);
            upperFloor = SmartPtr<DecorationSet>.Read(ObjectInfo.ParentFile, this, reader);
            lowestFloor = reader.ReadInt32();
            highestFloor = reader.ReadInt32();
            groundFloorChance = reader.ReadSingle();
            groundFloorDensity = reader.ReadSingle();
            upperFloorChance = reader.ReadSingle();
            upperFloorDensity = reader.ReadSingle();
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            base.WriteObject(writer);
            writer.Write(upperFloorAttempts);
            groundFloor.WritePtr(writer);
            upperFloor.WritePtr(writer);
            writer.Write(lowestFloor);
            writer.Write(highestFloor);
            writer.Write(groundFloorChance);
            writer.Write(groundFloorDensity);
            writer.Write(upperFloorChance);
            writer.Write(upperFloorDensity);
        }
    }
}
