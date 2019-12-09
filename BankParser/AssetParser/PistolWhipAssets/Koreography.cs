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
    public sealed class Koreography : MonoBehaviourObject, INeedAssetsMetadata
    {
        public SmartPtr<AudioClipObject> SourceClip { get; set; }
        public string SourceClipPath { get; set; }
        public int SampleRate { get; set; }
        public bool IgnoreLatencyOffset { get; set; }
        // Align4
        List<TempoSectionDef> TempoSections { get; set; }
        List<SmartPtr<KoreographyTrackBase>> Tracks { get; set; }
        [JsonConstructor]
        public Koreography() { }
        public Koreography(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool readLiteral = false) : base(objectInfo, reader, readLiteral)
        {
        }

        public Koreography(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("Koreography"))
        { }

        public override void ParseObject(AssetsReader reader)
        {
            SourceClip = SmartPtr<AudioClipObject>.Read(ObjectInfo.ParentFile, this, reader);
            SourceClipPath = reader.ReadString();
            SampleRate = reader.ReadInt32();
            IgnoreLatencyOffset = reader.ReadBoolean();
            reader.AlignTo(4);
            TempoSections = reader.ReadArrayOf((r) => new TempoSectionDef(ObjectInfo, r, true));
            Tracks = reader.ReadArrayOf((r) => SmartPtr<KoreographyTrackBase>.Read(ObjectInfo.ParentFile, this, r));
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            SourceClip.WritePtr(writer);
            writer.Write(SourceClipPath);
            writer.Write(SampleRate);
            writer.Write(IgnoreLatencyOffset);
            writer.AlignTo(4);
            writer.WriteArrayOf(TempoSections, (item, w) => item.Write(w));
            writer.WriteArrayOf(Tracks, (item, w) => item.WritePtr(w));
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
