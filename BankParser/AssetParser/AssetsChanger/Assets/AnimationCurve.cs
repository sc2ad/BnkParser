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
    public class AnimationCurve
    {
        // Enemy: 0xC8
        public List<Keyframe> keys { get; set; }
        // Enemy: 0x104
        public int length { get; set; }
        // Enemy: 0x108
        public WrapMode preWrapMode { get; set; }
        // Enemy: 0x10C
        public WrapMode postWrapMode { get; set; }

        //public AnimationCurve(AssetsFile assetsFile) : base(assetsFile, AssetsConstants.ClassID.MeshAssetClassID)
        //{
        //}

        public AnimationCurve(AssetsReader reader)
        {
            Parse(reader);
        }

        private void Parse(AssetsReader reader)
        {
            keys = reader.ReadArrayOf((r) => new Keyframe(r));
            length = reader.ReadInt32();
            preWrapMode = reader.ReadEnum<WrapMode>();
            postWrapMode = reader.ReadEnum<WrapMode>();
        }

        public void Write(AssetsWriter writer)
        {
            writer.WriteArrayOf(keys, (item, w) => item.Write(w));
            writer.Write(length);
            writer.Write((int)preWrapMode);
            writer.Write((int)postWrapMode);
        }
    }
    public enum WrapMode
    {
        Once = 1,
        Loop,
        PingPong = 4,
        Default = 0,
        ClampForever = 8,
        Clamp = 1
    }
}
