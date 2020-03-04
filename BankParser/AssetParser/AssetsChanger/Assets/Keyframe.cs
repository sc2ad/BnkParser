using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.AssetsChanger.Assets
{
    public class Keyframe
    {
        // Enemy: 0xCC, E8
        public Single time { get; set; }
        // Enemy: 0xD0, EC
        public Single value { get; set; }
        // Enemy: 0xD4, F0
        public Single inTangent { get; set; }
        // Enemy: 0xD8, F4
        public Single outTangent { get; set; }
        // Enemy: 0xDC, F8
        public WeightedMode weightedMode { get; set; }
        // Enemy: 0xE0, FC
        public int tangentMode { get; set; }
        // Enemy: 0xE4, 100
        public int tangentModeInternal { get; set; }
        public Keyframe(AssetsReader reader)
        {
            Parse(reader);
        }
        private void Parse(AssetsReader reader)
        {
            time = reader.ReadSingle();
            value = reader.ReadSingle();
            inTangent = reader.ReadSingle();
            outTangent = reader.ReadSingle();
            weightedMode = reader.ReadEnum<WeightedMode>();
            tangentMode = reader.ReadInt32();
            tangentModeInternal = reader.ReadInt32();
        }

        public void Write(AssetsWriter writer)
        {
            writer.Write(time);
            writer.Write(value);
            writer.Write(inTangent);
            writer.Write(outTangent);
            writer.Write((int)weightedMode);
            writer.Write(tangentMode);
            writer.Write(tangentModeInternal);
        }
    }
    public enum WeightedMode : int
    {
        None,
        In,
        Out,
        Both
    }
}
