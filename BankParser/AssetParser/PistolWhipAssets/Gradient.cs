using AssetParser.AssetsChanger;
using AssetParser.AssetsChanger.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.PistolWhipAssets
{
    public class Gradient : MonoBehaviourObject
    {
        public List<GradientColorKey> colorKeys { get; set; }
        public List<GradientAlphaKey> alphaKeys { get; set; }
        public GradientMode mode { get; set; }
        public Gradient(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("Gradient"))
        {
        }

        public Gradient(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public override void ParseObject(AssetsReader reader)
        {
            colorKeys = reader.ReadArrayOf((r) => new GradientColorKey(r));
            alphaKeys = reader.ReadArrayOf((r) => new GradientAlphaKey(r));
            mode = reader.ReadEnum<GradientMode>();
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            writer.WriteArrayOf(colorKeys, (item, w) => item.Write(w));
            writer.WriteArrayOf(alphaKeys, (item, w) => item.Write(w));
            writer.Write((int)mode);
        }
    }
    public class GradientAlphaKey
    {
        public Single alpha { get; set; }
        public Single time { get; set; }
        public GradientAlphaKey(AssetsReader reader)
        {
            Parse(reader);
        }
        private void Parse(AssetsReader reader)
        {
            alpha = reader.ReadSingle();
            time = reader.ReadSingle();
        }
        public void Write(AssetsWriter writer)
        {
            writer.Write(alpha);
            writer.Write(time);
        }
    }
    public class GradientColorKey
    {
        public Color color { get; set; }
        public Single time { get; set; }
        public GradientColorKey(AssetsReader reader)
        {
            Parse(reader);
        }
        private void Parse(AssetsReader reader)
        {
            color = new Color(reader);
            time = reader.ReadSingle();
        }
        public void Write(AssetsWriter writer)
        {
            color.Write(writer);
            writer.Write(time);
        }
    }
    public enum GradientMode
    {
        Blend,
        Fixed
    }
}
