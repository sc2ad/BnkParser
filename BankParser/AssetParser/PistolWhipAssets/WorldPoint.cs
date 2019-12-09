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
    public class WorldPoint
    {
        public Vector3F position { get; set; }
        public QuaternionF rotation { get; set; }
        [JsonConstructor]
        public WorldPoint()
        { }

        public WorldPoint(AssetsReader reader)
        {
            Parse(reader);
        }

        private void Parse(AssetsReader reader)
        {
            position = new Vector3F(reader);
            rotation = new QuaternionF(reader);
        }

        public void Write(AssetsWriter writer)
        {
            position.Write(writer);
            rotation.Write(writer);
        }
    }
}
