using AssetParser.AssetsChanger;
using AssetParser.AssetsChanger.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.PistolWhipAssets
{
    public class EnemyHitEffect
    {
        public Color color { get; set; }
        public AnimationCurve curve { get; set; }
        public float duration { get; set; }

        public EnemyHitEffect()
        { }

        public EnemyHitEffect(AssetsReader reader)
        {
            Parse(reader);
        }

        private void Parse(AssetsReader reader)
        {
            color = new Color(reader);
            curve = new AnimationCurve(reader);
            duration = reader.ReadSingle();
        }

        public void Write(AssetsWriter writer)
        {
            color.Write(writer);
            curve.Write(writer);
            writer.Write(duration);
        }
    }
}
