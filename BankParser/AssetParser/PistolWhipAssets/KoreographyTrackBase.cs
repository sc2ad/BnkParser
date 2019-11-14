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
    public class KoreographyTrackBase : MonoBehaviourObject, INeedAssetsMetadata
    {
        public string EventID { get; set; }
        public List<KoreographyEvent> EventList { get; set; }
        public List<string> SerializedPayloadTypes { get; set; }

        public KoreographyTrackBase(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool readLiteral = false) : base(objectInfo, reader, readLiteral)
        {
        }

        public KoreographyTrackBase(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("KoreographyTrackBase"))
        { }

        public KoreographyTrackBase(AssetsFile assetsFile, MonoScriptObject scriptObject) : base(assetsFile, scriptObject)
        {
        }

        public override void ParseObject(AssetsReader reader)
        {
            EventID = reader.ReadString();
            EventList = reader.ReadArrayOf((r) => new KoreographyEvent(ObjectInfo, r, true));
            SerializedPayloadTypes = reader.ReadArrayOf((r) => r.ReadString());
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            writer.Write(EventID);
            writer.WriteArrayOf(EventList, (item, w) => item.Write(w));
            writer.WriteArrayOf(SerializedPayloadTypes, (item, w) => w.Write(item));
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
