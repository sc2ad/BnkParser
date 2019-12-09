using AssetParser.AssetsChanger;
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
    public class KoreographyTrack : KoreographyTrackBase
    {
        public List<AssetPayload> AssetPayloads { get; set; }
        public List<int> _AssetPayloadIdxs { get; set; }
        public List<ColorPayload> _ColorPayloads { get; set; }
        public List<int> _ColorPayloadIdxs { get; set; }
        public List<CurvePayload> CurvePayloads { get; set; }
        public List<int> _CurvePayloadIdxs { get; set; }
        public List<FloatPayload> FloatPayloads { get; set; }
        public List<int> _FloatPayloadIdxs { get; set; }
        public List<GradientPayload> GradientPayloads { get; set; }
        public List<int> _GradientPayloadIdxs { get; set; }
        public List<IntPayload> IntPayloads { get; set; }
        public List<int> _IntPayloadIdxs { get; set; }
        public List<SpectrumPayload> SpectrumPayloads { get; set; }
        public List<int> _SpectrumPayloadIdxs { get; set; }
        public List<TextPayload> TextPayloads { get; set; }
        public List<int> _TextPayloadIdxs { get; set; }
        [JsonConstructor]
        public KoreographyTrack() { }
        public KoreographyTrack(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool readLiteral = false) : base(objectInfo, reader, readLiteral)
        {
        }

        public KoreographyTrack(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("KoreographyTrack"))
        { }

        public override void ParseObject(AssetsReader reader)
        {
            base.ParseObject(reader);

        }

        protected override void WriteObject(AssetsWriter writer)
        {
            base.WriteObject(writer);
        }

        [System.ComponentModel.Browsable(false)]
        [Newtonsoft.Json.JsonIgnore]
        public override byte[] ScriptParametersData
        {
            get
            {
                throw new InvalidOperationException("Cannot access parameters data from this object.");
            }
            set
            {
                throw new InvalidOperationException("Cannot access parameters data from this object.");
            }
        }
    }
}
