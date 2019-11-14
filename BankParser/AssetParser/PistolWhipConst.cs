﻿using AssetParser.PistolWhipAssets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser
{
    public class PistolWhipConst
    {
        public static Dictionary<string, Type> GetAssetTypeMap()
        {
            Dictionary<string, Type> scriptHashToTypes = new Dictionary<string, Type>();
            scriptHashToTypes.Add("AssetPayload", typeof(AssetPayload));
            scriptHashToTypes.Add("BaseType", typeof(BaseType));
            scriptHashToTypes.Add("BeatData", typeof(BeatData));
            scriptHashToTypes.Add("ColorPayload", typeof(ColorPayload));
            scriptHashToTypes.Add("CullingRange", typeof(CullingRange));
            scriptHashToTypes.Add("CurvePayload", typeof(CurvePayload));
            scriptHashToTypes.Add("Enemy", typeof(Enemy));
            scriptHashToTypes.Add("EnemyAction", typeof(EnemyAction));
            scriptHashToTypes.Add("EnemySequence", typeof(EnemySequence));
            scriptHashToTypes.Add("FloatPayload", typeof(FloatPayload));
            scriptHashToTypes.Add("GameMap", typeof(GameMap));
            scriptHashToTypes.Add("GeoSet", typeof(GeoSet));
            scriptHashToTypes.Add("Gradient", typeof(Gradient));
            scriptHashToTypes.Add("GradientPayload", typeof(GradientPayload));
            scriptHashToTypes.Add("IntPayload", typeof(IntPayload));
            scriptHashToTypes.Add("Koreography", typeof(Koreography));
            scriptHashToTypes.Add("KoreographyEvent", typeof(KoreographyEvent));
            scriptHashToTypes.Add("KoreographyTrack", typeof(KoreographyTrackBase));
            scriptHashToTypes.Add("KoreographyTrackBase", typeof(KoreographyTrackBase));
            scriptHashToTypes.Add("LevelData", typeof(LevelData));
            scriptHashToTypes.Add("ObstacleData", typeof(ObstacleData));
            scriptHashToTypes.Add("SpectrumPayload", typeof(SpectrumPayload));
            scriptHashToTypes.Add("TargetData", typeof(TargetData));
            scriptHashToTypes.Add("TempoSectionDef", typeof(TempoSectionDef));
            scriptHashToTypes.Add("TextPayload", typeof(TextPayload));
            scriptHashToTypes.Add("TrackData", typeof(TrackData));
            scriptHashToTypes.Add("TrackSection", typeof(TrackSection));
            scriptHashToTypes.Add("WorldObject", typeof(WorldObject));
            scriptHashToTypes.Add("WorldRegion", typeof(WorldRegion));
            scriptHashToTypes.Add("WwiseStateReference", typeof(WwiseStateReference));
            return scriptHashToTypes;
        }
    }
}