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
    public class TrackSection : MonoBehaviourObject, INeedAssetsMetadata
    {
        public Single colorHue { get; set; }
        public Single colorSaturation { get; set; }
        public Single colorValue { get; set; }
        public Single fogHue { get; set; }
        public Single fogSaturation { get; set; }
        public Single fogValue { get; set; }
        public Single glowHue { get; set; }
        public Single glowSaturation { get; set; }
        public Single glowValue { get; set; }
        public bool customColors { get; set; } // 0x1DC
        // Align4
        public Single exposure { get; set; }
        public bool startingWall { get; set; }
        public bool endingWall { get; set; }
        // Align4
        public int height { get; set; }
        public int groundHeight { get; set; }
        public int seed { get; set; }
        public int start { get; set; }
        public int end { get; set; }
        public Vector3I min { get; set; }
        public Vector3I max { get; set; }
        public bool generateDebris { get; set; }
        // Align4
        public Debris debris { get; set; } // 0x21C
        public int propSeed { get; set; } 


        public TrackSection(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public TrackSection(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("TrackSection"))
        { }

        public override void ParseObject(AssetsReader reader)
        {
            colorHue = reader.ReadSingle();
            colorSaturation = reader.ReadSingle();
            colorValue = reader.ReadSingle();
            fogHue = reader.ReadSingle();
            fogSaturation = reader.ReadSingle();
            fogValue = reader.ReadSingle();
            glowHue = reader.ReadSingle();
            glowSaturation = reader.ReadSingle();
            glowValue = reader.ReadSingle();
            customColors = reader.ReadBoolean();
            reader.AlignTo(4);
            exposure = reader.ReadSingle();
            startingWall = reader.ReadBoolean();
            reader.AlignTo(4);
            endingWall = reader.ReadBoolean();
            reader.AlignTo(4);
            height = reader.ReadInt32();
            groundHeight = reader.ReadInt32();
            seed = reader.ReadInt32();
            start = reader.ReadInt32();
            end = reader.ReadInt32();
            min = new Vector3I(reader);
            max = new Vector3I(reader);
            generateDebris = reader.ReadBoolean();
            reader.AlignTo(4);
            debris = new Debris(reader);
            propSeed = reader.ReadInt32();
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            writer.Write(colorHue);
            writer.Write(colorSaturation);
            writer.Write(colorValue);
            writer.Write(fogHue);
            writer.Write(fogSaturation);
            writer.Write(fogValue);
            writer.Write(glowHue);
            writer.Write(glowSaturation);
            writer.Write(glowValue);
            writer.Write(customColors);
            writer.AlignTo(4);
            writer.Write(exposure);
            writer.Write(startingWall);
            writer.Write(endingWall);
            writer.AlignTo(4);
            writer.Write(height);
            writer.Write(groundHeight);
            writer.Write(seed);
            writer.Write(start);
            writer.Write(end);
            min.Write(writer);
            max.Write(writer);
            writer.Write(generateDebris);
            debris.Write(writer);
            writer.Write(propSeed);
        }
    }
}
