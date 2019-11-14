using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.AssetsChanger.Assets
{
    public class Keyframe
    {
        public Single time { get; set; }
        public Single value { get; set; }
        public Single inTangent { get; set; }
        public Single outTangent { get; set; }
        public WeightedMode weightedMode { get; set; }
        public int tangentMode { get; set; }
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
    public enum WeightedMode
    {
        None,
        In,
        Out,
        Both
    }
}
