using AssetParser.AssetsChanger.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.Utils
{
    public interface IImageUtils
    {
        void AssignImageToTexture(byte[] imageData, Texture2DObject targetTexture, int targetWidth, int targetHeight, int targetMips = int.MaxValue, TextureConversionFormat format = TextureConversionFormat.Auto);
        byte[] TextureToPngBytes(Texture2DObject texture);
    }

    public static class ImageUtils
    {
        public static IImageUtils Instance { get; set; }
    }
}
