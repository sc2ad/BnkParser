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
    public sealed class LevelData : MonoBehaviourObject, INeedAssetsMetadata
    {
        
        public byte[] _inital_16 { get; set; }
        public ISmartPtr<WwiseStateReference> songSwitch { get; set; }
        public List<SmartPtr<GameMap>> maps { get; set; }
        public List<SmartPtr<>>
        public LevelData(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool readLiteral = false) : base(objectInfo, reader, readLiteral)
        {
        }

        public LevelData(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("LevelData"))
        { }

        public override void ParseObject(AssetsReader reader)
        {
            SourceClip = SmartPtr<AudioClipObject>.Read(ObjectInfo.ParentFile, this, reader);
            SourceClipPath = reader.ReadString();
            SampleRate = reader.ReadInt32();
            IgnoreLatencyOffset = reader.ReadBoolean();
            reader.AlignTo(4);
            TempoSections = reader.ReadArrayOf((r) => new TempoSectionDef(r));
            Tracks = reader.ReadArrayOf((r) => (ISmartPtr<KoreographyTrackBase>)SmartPtr<KoreographyTrackBase>.Read(ObjectInfo.ParentFile, this, r));
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
