﻿using AssetParser.AssetsChanger;
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
    public class ObstacleData : MonoBehaviourObject
    {
        public Placement placement { get; set; }
        public ObstacleType obstacleType { get; set; }
        [JsonConstructor]
        public ObstacleData() { }
        public ObstacleData(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public ObstacleData(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("ObstacleData"))
        { }

        public override void ParseObject(AssetsReader reader)
        {
            placement = reader.ReadEnum<Placement>();
            obstacleType = reader.ReadEnum<ObstacleType>();
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            writer.Write((int)placement);
            writer.Write((int)obstacleType);
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
