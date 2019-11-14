using AssetParser.AssetsChanger;
using AssetParser.AssetsChanger.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.PistolWhipAssets
{
    public class CurvePayload : MonoBehaviourObject
    {
        public AnimationCurve val { get; set; }
        public CurvePayload(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("CurvePayload"))
        {
        }

        public CurvePayload(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public override void ParseObject(AssetsReader reader)
        {
            val = new AnimationCurve(reader);
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            val.Write(writer);
        }
    }
}
