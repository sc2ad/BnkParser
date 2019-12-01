using AssetParser.AssetsChanger;
using AssetParser.AssetsChanger.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.PistolWhipAssets
{
    public class OscillatingObjectData
    {
        public Vector3F restPoint { get; set; }
        public Single moveScale { get; set; }
        public Single phase { get; set; }

        public OscillatingObjectData()
        { }

        public OscillatingObjectData(AssetsReader reader)
        {
            Parse(reader);
        }

        private void Parse(AssetsReader reader)
        {
            restPoint = new Vector3F(reader);
            moveScale = reader.ReadSingle();
            phase = reader.ReadSingle();
        }

        public void Write(AssetsWriter writer)
        {
            restPoint.Write(writer);
            writer.Write(moveScale);
            writer.Write(phase);
        }
    }
}
