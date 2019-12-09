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
    public class Decoration : MonoBehaviourObject, INeedAssetsMetadata
    {
        public SmartPtr<GameObject> source { get; set; }
        public QuaternionF rotation { get; set; }
        public Vector2F hPos { get; set; }
        public Vector2F vPos { get; set; }
        public Vector3F padding { get; set; }
        public Single maxRotation { get; set; }
        public Single rotationIncrement { get; set; }
        public Vector3F minimumScale { get; set; }
        public Vector3F maximumScale { get; set; }
        public Bounds bounds { get; set; }
        [JsonConstructor]
        public Decoration() { }
        public Decoration(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("Decoration"))
        {
        }

        public Decoration(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public override void ParseObject(AssetsReader reader)
        {
            source = SmartPtr<GameObject>.Read(ObjectInfo.ParentFile, this, reader);
            rotation = new QuaternionF(reader);
            hPos = new Vector2F(reader);
            vPos = new Vector2F(reader);
            padding = new Vector3F(reader);
            maxRotation = reader.ReadSingle();
            rotationIncrement = reader.ReadSingle();
            minimumScale = new Vector3F(reader);
            maximumScale = new Vector3F(reader);
            bounds = new Bounds(reader);
        }
        protected override void WriteObject(AssetsWriter writer)
        {
            source.WritePtr(writer);
            rotation.Write(writer);
            hPos.Write(writer);
            vPos.Write(writer);
            padding.Write(writer);
            writer.Write(maxRotation);
            writer.Write(rotationIncrement);
            minimumScale.Write(writer);
            maximumScale.Write(writer);
            bounds.Write(writer);
        }
    }
}
