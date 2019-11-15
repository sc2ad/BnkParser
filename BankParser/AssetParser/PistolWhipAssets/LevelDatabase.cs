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
    public class LevelDatabase : MonoBehaviourObject, INeedAssetsMetadata
    {
        public bool useAsDefault { get; set; }
        // Align4
        public SmartPtr<WwiseKoreographySet> frontLotKoreoSet { get; set; }
        public List<SmartPtr<WwiseKoreographySet>> koreoSets { get; set; }
        public List<SmartPtr<LevelData>> levelData { get; set; }

        public LevelDatabase(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public LevelDatabase(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("LevelDatabase"))
        { }

        public override void ParseObject(AssetsReader reader)
        {
            useAsDefault = reader.ReadBoolean();
            reader.AlignTo(4);
            frontLotKoreoSet = SmartPtr<WwiseKoreographySet>.Read(ObjectInfo.ParentFile, this, reader);
            koreoSets = reader.ReadArrayOf((r) => SmartPtr<WwiseKoreographySet>.Read(ObjectInfo.ParentFile, this, r));
            levelData = reader.ReadArrayOf((r) => SmartPtr<LevelData>.Read(ObjectInfo.ParentFile, this, r));
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            writer.Write(useAsDefault);
            writer.AlignTo(4);
            frontLotKoreoSet.WritePtr(writer);
            writer.WriteArrayOf(koreoSets, (item, w) => item.WritePtr(w));
            writer.WriteArrayOf(levelData, (item, w) => item.WritePtr(w));
        }
    }
}
