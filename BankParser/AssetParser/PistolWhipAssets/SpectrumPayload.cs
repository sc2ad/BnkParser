using AssetParser.AssetsChanger;
using AssetParser.AssetsChanger.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.PistolWhipAssets
{
    public class SpectrumPayload : MonoBehaviourObject
    {
        public SpectrumInfo spectrumInfo { get; set; }
        public List<Spectrum> spectrumData { get; set; }
        public SpectrumPayload(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("SpectrumPayload"))
        {
        }

        public SpectrumPayload(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public override void ParseObject(AssetsReader reader)
        {
            spectrumInfo = new SpectrumInfo(reader);
            spectrumData = reader.ReadArrayOf((r) => new Spectrum(r));
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            spectrumInfo.Write(writer);
            writer.WriteArrayOf(spectrumData, (item, w) => item.Write(w));
        }
        public class SpectrumInfo
        {
            public float binFrequencyWidth { get; set; }
            public float minBinFrequency { get; set; }
            public int startSample { get; set; }
            public int endSample { get; set; }
            public SpectrumInfo(AssetsReader reader)
            {
                Parse(reader);
            }
            private void Parse(AssetsReader reader)
            {
                binFrequencyWidth = reader.ReadSingle();
                minBinFrequency = reader.ReadSingle();
                startSample = reader.ReadInt32();
                endSample = reader.ReadInt32();
            }
            public void Write(AssetsWriter writer)
            {
                writer.Write(binFrequencyWidth);
                writer.Write(minBinFrequency);
                writer.Write(startSample);
                writer.Write(endSample);
            }
        }

        public class Spectrum
        {
            public List<float> data { get; set; }
            public Spectrum(AssetsReader reader)
            {
                Parse(reader);
            }
            private void Parse(AssetsReader reader)
            {
                data = reader.ReadArrayOf((r) => r.ReadSingle());
            }
            public void Write(AssetsWriter writer)
            {
                writer.WriteArrayOf(data, (item, w) => w.Write(item));
            }
        }
    }
}
