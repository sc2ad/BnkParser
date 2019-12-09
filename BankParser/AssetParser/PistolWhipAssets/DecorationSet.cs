using AssetParser.AssetsChanger;
using AssetParser.AssetsChanger.Assets;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.PistolWhipAssets
{
    public class DecorationSet : MonoBehaviourObject
    {
        public List<DecorationEntry> set { get; set; }
        public Single totalWeight { get; set; }
        [JsonConstructor]
        public DecorationSet() { }
        public DecorationSet(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("DecorationSet"))
        {
        }

        public DecorationSet(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public override void ParseObject(AssetsReader reader)
        {
            set = reader.ReadArrayOf((r) => new DecorationEntry(ObjectInfo.ParentFile, this, r));
            totalWeight = reader.ReadSingle();
        }
        protected override void WriteObject(AssetsWriter writer)
        {
            writer.WriteArrayOf(set, (item, w) => item.Write(w));
            writer.Write(totalWeight);
        }
    }
    public class DecorationEntry
    {
        public SmartPtr<Decoration> decoration { get; set; }
        public Single weight { get; set; }

        private AssetsFile file;
        private AssetsObject owner;
        public DecorationEntry(AssetsFile f, AssetsObject o, AssetsReader reader)
        {
            file = f;
            owner = o;
            Parse(reader);
        }
        private void Parse(AssetsReader reader)
        {
            decoration = SmartPtr<Decoration>.Read(file, owner, reader);
            weight = reader.ReadSingle();
        }
        public void Write(AssetsWriter writer)
        {
            decoration.WritePtr(writer);
            writer.Write(weight);
        }
    }
}
