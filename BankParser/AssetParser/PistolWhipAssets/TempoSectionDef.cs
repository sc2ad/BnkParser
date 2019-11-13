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
    public class TempoSectionDef : MonoBehaviourObject
    {
        public string name { get; set; }
        public int startSample { get; set; }
        public double samplesPerBeat { get; set; }
        public int beatsPerMeasure { get; set; }
        public bool startNewMeasure { get; set; }
        // Align4

        public TempoSectionDef(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool readLiteral = false) : base(objectInfo, reader, readLiteral)
        {
        }

        public TempoSectionDef(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("TempoSectionDef"))
        { }

        public override void ParseObject(AssetsReader reader)
        {
            name = reader.ReadString();
            startSample = reader.ReadInt32();
            samplesPerBeat = reader.ReadDouble();
            beatsPerMeasure = reader.ReadInt32();
            startNewMeasure = reader.ReadBoolean();
            reader.AlignTo(4);
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            writer.Write(name);
            writer.Write(startSample);
            writer.Write(samplesPerBeat);
            writer.Write(beatsPerMeasure);
            writer.Write(startNewMeasure);
            writer.AlignTo(4);
        }
    }
}
