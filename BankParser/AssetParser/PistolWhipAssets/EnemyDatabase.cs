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
    public sealed class EnemyDatabase : MonoBehaviourObject, INeedAssetsMetadata
    {
        public bool useAsDefault { get; set; }
        // Align4
        public EnemyHitEffect hitEffect { get; set; }
        public List<EnemyEntry> enemies { get; set; }
        [JsonConstructor]
        public EnemyDatabase() { }
        public EnemyDatabase(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public EnemyDatabase(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("EnemyDatabase"))
        { }

        public override void ParseObject(AssetsReader reader)
        {
            useAsDefault = reader.ReadBoolean();
            reader.AlignTo(4);
            hitEffect = new EnemyHitEffect(reader);
            enemies = reader.ReadArrayOf(r => new EnemyEntry(r));
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            writer.Write(useAsDefault);
            writer.AlignTo(4);
            hitEffect.Write(writer);
            writer.WriteArrayOf(enemies, (e, w) => e.Write(w));
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
        public class EnemyEntry
        {
            public EnemyToughness toughness { get; set; }

            public EnemyEntry()
            { }

            public EnemyEntry(AssetsReader reader)
            {
                Parse(reader);
            }

            private void Parse(AssetsReader reader)
            {
                toughness = reader.ReadEnum<EnemyToughness>();
            }

            public void Write(AssetsWriter writer)
            {
                writer.Write((int)toughness);
            }
        }
    }
}
