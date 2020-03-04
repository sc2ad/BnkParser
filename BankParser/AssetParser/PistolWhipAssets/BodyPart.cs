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
    public class BodyPart : MonoBehaviourObject
    {
        public bool applyForces { get; set; }
        // Align4
        [JsonConstructor]
        public BodyPart() { }
        public BodyPart(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public BodyPart(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("BodyPart"))
        { }
        public BodyPart(AssetsFile assetsFile, MonoScriptObject scriptObject) : base(assetsFile, scriptObject)
        {
        }

        public override void ParseObject(AssetsReader reader)
        {
            applyForces = reader.ReadBoolean();
            reader.AlignTo(4);
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            writer.Write(applyForces);
            writer.AlignTo(4);
        }
    }
}
