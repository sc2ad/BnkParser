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
    public class Debris
    {
        public int seed { get; set; }
        public Distribution distribution { get; set; }
        public Projection projection { get; set; }
        public Spread spread { get; set; }
        public float density { get; set; }
        public Vector2I interval { get; set; }
        public float uniformity { get; set; }
        public Vector3I minSize { get; set; }
        public Vector3I maxSize { get; set; }
        public Shape shape { get; set; }
        public bool buildOnAir { get; set; }
        // Align4
        public Timing timing { get; set; }
        [JsonConstructor]
        public Debris()
        { }

        public Debris(AssetsReader reader)
        {
            Parse(reader);
        }

        private void Parse(AssetsReader reader)
        {
            seed = reader.ReadInt32();
            distribution = reader.ReadEnum<Distribution>();
            projection = reader.ReadEnum<Projection>();
            spread = reader.ReadEnum<Spread>();
            density = reader.ReadSingle();
            interval = new Vector2I(reader);
            uniformity = reader.ReadSingle();
            minSize = new Vector3I(reader);
            maxSize = new Vector3I(reader);
            shape = reader.ReadEnum<Shape>();
            buildOnAir = reader.ReadBoolean();
            reader.AlignTo(4);
            timing = reader.ReadEnum<Timing>();
        }

        public void Write(AssetsWriter writer)
        {
            writer.Write(seed);
            writer.Write((int)distribution);
            writer.Write((int)projection);
            writer.Write((int)spread);
            writer.Write(density);
            interval.Write(writer);
            writer.Write(uniformity);
            minSize.Write(writer);
            maxSize.Write(writer);
            writer.Write((int)shape);
            writer.Write(buildOnAir);
            writer.AlignTo(4);
            writer.Write((int)timing);

        }
        public enum Distribution
        {
            Ground,
            PlayerLevel,
            Projected
        }

        public enum Projection
        {
            Down,
            Outward,
            Forward,
            Up
        }

        public enum Spread
        {
            Random,
            Spread
        }

        public enum Shape
        {
            Cube,
            Pile
        }

        public enum Timing
        {
            Normal,
            AfterRegions
        }
    }
}
