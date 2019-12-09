using AssetParser.AssetsChanger.Interfaces;
using AssetParser.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.AssetsChanger.Assets
{
    public class MonoBehaviourObject : Component, IHaveName
    {
        [JsonConstructor]
        public MonoBehaviourObject() { }
        public MonoBehaviourObject(AssetsFile assetsFile, MonoScriptObject scriptObject) : base(assetsFile, scriptObject.PropertiesHash)
        {
            Enabled = 1;
            MonoscriptTypePtr = new SmartPtr<MonoScriptObject>(this, (IObjectInfo<MonoScriptObject>)scriptObject.ObjectInfo);
        }

        public MonoBehaviourObject(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        protected MonoBehaviourObject(IObjectInfo<AssetsObject> objectInfo) : base(objectInfo)
        { Enabled = 1; }

        private byte[] _scriptParametersData;


        public int Enabled { get; set; } = 1;

        public SmartPtr<MonoScriptObject> MonoscriptTypePtr { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public virtual byte[] ScriptParametersData
        {
            get
            {
                return _scriptParametersData;
            }
            set
            {
                _scriptParametersData = value;
            }
        }

        [System.ComponentModel.Browsable(false)]
        [JsonIgnore]
        public override byte[] Data
        {
            get
            {
                throw new InvalidOperationException("Data cannot be accessed from this class.");
            }
            set
            {
                throw new InvalidOperationException("Data cannot be accessed from this class.");
            }
        }

        public override void ParseObject(AssetsReader reader)
        {
            var readLength = ObjectInfo.DataSize - (reader.Position - ObjectInfo.DataOffset);
            ScriptParametersData = reader.ReadBytes(readLength);
            reader.AlignTo(4);
        }

        protected override void ParseBase(AssetsReader reader)
        {
            base.ParseBase(reader);
            Enabled = reader.ReadInt32();
            MonoscriptTypePtr = SmartPtr<MonoScriptObject>.Read(ObjectInfo.ParentFile, this, reader);
            Name = reader.ReadString();
        }

        protected override void WriteBase(AssetsWriter writer)
        {
            base.WriteBase(writer);
            writer.Write(Enabled);
            MonoscriptTypePtr.Write(writer);
            writer.Write(Name);
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            writer.Write(ScriptParametersData);
            writer.AlignTo(4);
        }
    }
}
