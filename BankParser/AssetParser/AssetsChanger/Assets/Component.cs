using AssetParser.Utils;
using AssetParser.Utils.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.AssetsChanger.Assets
{
    public abstract class Component : AssetsObject
    {
        [JsonConstructor]
        public Component()
        {

        }
        protected Component(AssetsFile assetsFile, int classID) : base(assetsFile, classID)
        {
        }

        protected Component(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        protected Component(AssetsFile assetsFile, Guid typeHash)
        {
            ObjectInfo = ObjectInfo<AssetsObject>.FromTypeHash(assetsFile, typeHash, this);
        }

        protected Component(IObjectInfo<AssetsObject> objectInfo) : base(objectInfo)
        {
        }

        protected override void ParseBase(AssetsReader reader)
        {
            base.ParseBase(reader);
            try
            {
                GameObject = SmartPtr<GameObject>.Read(ObjectInfo.ParentFile, this, reader);
            }
            catch (Exception ex)
            {
                Log.LogErr("Component failed to load its GameObject... allowing it to continue because this happens with bundles?", ex);
                GameObject = null;
            }
        }

        protected override void WriteBase(AssetsWriter writer)
        {
            base.WriteBase(writer);
            this.GameObject.Write(writer);
        }

        [JsonIgnore]
        public ISmartPtr<GameObject> GameObject { get; set; } = null;

        [System.ComponentModel.Browsable(false)]
        [JsonIgnore]
        public override byte[] Data { get => throw new InvalidOperationException("Data cannot be accessed from this class!"); set => throw new InvalidOperationException("Data cannot be accessed from this class!"); }

    }
}
