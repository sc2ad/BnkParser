using AssetParser.PistolWhipAssets;
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
            scriptHashToTypes.Add("AlbumArtDatabase", typeof(AlbumArtDatabase));
            scriptHashToTypes.Add("AssetPayload", typeof(AssetPayload));
            scriptHashToTypes.Add("BaseType", typeof(BaseType));
            scriptHashToTypes.Add("BeatData", typeof(BeatData));
            scriptHashToTypes.Add("BodyPart", typeof(BodyPart));
            scriptHashToTypes.Add("ColorPayload", typeof(ColorPayload));
            scriptHashToTypes.Add("CullingRange", typeof(CullingRange));
            scriptHashToTypes.Add("CurvePayload", typeof(CurvePayload));
            scriptHashToTypes.Add("Decoration", typeof(Decoration));
            scriptHashToTypes.Add("DecorationSet", typeof(DecorationSet));
            scriptHashToTypes.Add("Enemy", typeof(Enemy));
            scriptHashToTypes.Add("EnemyAction", typeof(EnemyAction));
            scriptHashToTypes.Add("EnemyActionAimAndFire", typeof(EnemyActionAndFire));
            scriptHashToTypes.Add("EnemyActionAimEnd", typeof(EnemyActionAimEnd));
            scriptHashToTypes.Add("EnemyActionAimStart", typeof(EnemyActionAimStart));
            scriptHashToTypes.Add("EnemyActionFire", typeof(EnemyActionFire));
            scriptHashToTypes.Add("EnemyActionInstant", typeof(EnemyActionInstant));
            scriptHashToTypes.Add("EnemyActionMove", typeof(EnemyActionMove));
            scriptHashToTypes.Add("EnemyActionRootMotion", typeof(EnemyActionRootMotion));
            scriptHashToTypes.Add("EnemyActionStopFiring", typeof(EnemyActionStopFiring));
            scriptHashToTypes.Add("EnemyActionWait", typeof(EnemyActionWait));
            scriptHashToTypes.Add("EnemyDatabase", typeof(EnemyDatabase));
            scriptHashToTypes.Add("EnemySequence", typeof(EnemySequence));
            scriptHashToTypes.Add("Event", typeof(Event));
            scriptHashToTypes.Add("FloatPayload", typeof(FloatPayload));
            scriptHashToTypes.Add("GameMap", typeof(GameMap));
            scriptHashToTypes.Add("GeoSet", typeof(GeoSet));
            scriptHashToTypes.Add("Gradient", typeof(Gradient));
            scriptHashToTypes.Add("GradientPayload", typeof(GradientPayload));
            scriptHashToTypes.Add("GroundDecorator", typeof(GroundDecorator));
            scriptHashToTypes.Add("IntPayload", typeof(IntPayload));
            scriptHashToTypes.Add("Koreography", typeof(Koreography));
            scriptHashToTypes.Add("KoreographyEvent", typeof(KoreographyEvent));
            scriptHashToTypes.Add("KoreographyTrack", typeof(KoreographyTrackBase));
            scriptHashToTypes.Add("KoreographyTrackBase", typeof(KoreographyTrackBase));
            scriptHashToTypes.Add("LevelAssetDatabase", typeof(LevelAssetDatabase));
            scriptHashToTypes.Add("LevelData", typeof(LevelData));
            scriptHashToTypes.Add("LevelDatabase", typeof(LevelDatabase));
            scriptHashToTypes.Add("LevelDecoratorBase", typeof(LevelDecoratorBase));
            scriptHashToTypes.Add("MeleeBodyPart", typeof(MeleeBodyPart));
            scriptHashToTypes.Add("ObstacleData", typeof(ObstacleData));
            scriptHashToTypes.Add("PlayerKiller", typeof(PlayerKiller));
            scriptHashToTypes.Add("SongPanelUIController", typeof(SongPanelUIController));
            scriptHashToTypes.Add("SpectrumPayload", typeof(SpectrumPayload));
            scriptHashToTypes.Add("TargetableObject", typeof(TargetableObject));
            scriptHashToTypes.Add("TargetData", typeof(TargetData));
            scriptHashToTypes.Add("TempoSectionDef", typeof(TempoSectionDef));
            scriptHashToTypes.Add("TextPayload", typeof(TextPayload));
            scriptHashToTypes.Add("TrackData", typeof(TrackData));
            scriptHashToTypes.Add("TrackSection", typeof(TrackSection));
            scriptHashToTypes.Add("WallDecorator", typeof(WallDecorator));
            scriptHashToTypes.Add("WorldObject", typeof(WorldObject));
            scriptHashToTypes.Add("WorldRegion", typeof(WorldRegion));
            scriptHashToTypes.Add("WwiseEventReference", typeof(WwiseEventReference));
            scriptHashToTypes.Add("WwiseGroupValueObjectReference", typeof(WwiseGroupValueObjectReference));
            scriptHashToTypes.Add("WwiseKoreographySet", typeof(WwiseKoreographySet));
            scriptHashToTypes.Add("WwiseKoreoMediaIDEntry", typeof(WwiseKoreoMediaIDEntry));
            scriptHashToTypes.Add("WwiseObjectReference", typeof(WwiseObjectReference));
            scriptHashToTypes.Add("WwiseStateGroupReference", typeof(WwiseStateGroupReference));
            scriptHashToTypes.Add("WwiseStateReference", typeof(WwiseStateReference));
            return scriptHashToTypes;
        }

        public static readonly string DebugCertificatePEM = @"";
    }
}
