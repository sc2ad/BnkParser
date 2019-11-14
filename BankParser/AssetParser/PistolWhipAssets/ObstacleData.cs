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
    public class ObstacleData : MonoBehaviourObject
    {
        public Placement Placement { get; set; }
        public ObstacleType ObstacleType { get; set; }

        public ObstacleData(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public ObstacleData(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("ObstacleData"))
        { }

        public override void ParseObject(AssetsReader reader)
        {
            Placement = reader.ReadEnum<Placement>();
            ObstacleType = reader.ReadEnum<ObstacleType>();
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            writer.Write((int)Placement);
            writer.Write((int)ObstacleType);
        }
    }

    public enum Placement
    {
        EvenMoreLeft = -3,
        FarLeft,
        Left,
        Center,
        Right,
        FarRight,
        EvenMoreRight
    }
    public enum ObstacleType
    {
        Sidestep,
        LimboTall,
        LimboShort
    }
}
