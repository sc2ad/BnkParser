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
    public class MeleeBodyPart : BodyPart
    {
        [JsonConstructor]
        public MeleeBodyPart() { }
        public MeleeBodyPart(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public MeleeBodyPart(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("MeleeBodyPart"))
        { }
    }
}
