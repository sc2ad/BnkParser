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
    public class PlayerKiller : MonoBehaviourObject
    {
        public bool autoRegister { get; set; }
        // Align4
        public bool hasHitPlayer { get; set; }
        // Align4
        public List<SmartPtr<Collider>> colliders { get; set; }

        [JsonConstructor]
        public PlayerKiller() { }
        public PlayerKiller(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public PlayerKiller(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("PlayerKiller"))
        { }

        public override void ParseObject(AssetsReader reader)
        {
            autoRegister = reader.ReadBoolean();
            reader.AlignTo(4);
            hasHitPlayer = reader.ReadBoolean();
            reader.AlignTo(4);
            colliders = reader.ReadArrayOf(r => SmartPtr<Collider>.Read(ObjectInfo.ParentFile, this, r));
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            writer.Write(autoRegister);
            writer.AlignTo(4);
            writer.Write(hasHitPlayer);
            writer.AlignTo(4);
            writer.WriteArrayOf(colliders, (c, w) => c.WritePtr(w));
        }
    }
}
