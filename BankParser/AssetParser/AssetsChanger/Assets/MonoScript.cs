using AssetParser.AssetsChanger.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.AssetsChanger.Assets
{
    public sealed class MonoScriptObject : AssetsObject, IHaveName
    {
        public MonoScriptObject(AssetsFile assetsFile) : base(assetsFile, AssetsConstants.ClassID.MonoScriptType)
        {
        }

        public MonoScriptObject(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader) : base(objectInfo)
        {
            Parse(reader);
        }

        public override void ParseObject(AssetsReader reader)
        {
            Name = reader.ReadString();
            ExecutionOrder = reader.ReadInt32();
            PropertiesHash = reader.ReadGuid();
            ClassName = reader.ReadString();
            Namespace = reader.ReadString();
            AssemblyName = reader.ReadString();
        }

        protected override void WriteBase(AssetsWriter writer)
        {
            base.WriteBase(writer);
            writer.Write(Name);
            writer.Write(ExecutionOrder);
            writer.Write(PropertiesHash);
            writer.Write(ClassName);
            writer.Write(Namespace);
            writer.Write(AssemblyName);
        }

        public string Name { get; set; }
        public Int32 ExecutionOrder { get; set; }
        public Guid PropertiesHash { get; set; }
        public string ClassName { get; set; }
        public string Namespace { get; set; }
        public string AssemblyName { get; set; }

        [System.ComponentModel.Browsable(false)]
        [Newtonsoft.Json.JsonIgnore]
        public override byte[] Data { get => throw new InvalidOperationException("Data cannot be accessed from this class!"); set => throw new InvalidOperationException("Data cannot be accessed from this class!"); }

    }
}
