using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankParser.HIRCData
{
    public class Transition : Parsable
    {
        public uint sourceID;
        private byte[] _unknown3;
        public uint destID;
        public int sourceFadeOutTime;
        public uint sourceFadeOutCurve;
        public int sourceFadeOutOffset;
        public uint exitSource;
        public uint nextCueID;
        public byte nextAction;
        public int destinationFadeInTime;
        public uint destinationFadeInCurve;
        public int destinationFadeInOffset;
        public uint cueFilterID;
        public uint destinationPlaylistID;
        public ushort syncTo;
        public byte playPreEntryDestination;
        public byte customCueFilter;
        public bool transitionObject;
        public uint transitionID;
        public int transitionFadeInTime;
        public uint transitionFadeInCurve;
        public int transitionFadeInOffset;
        public int transitionFadeOutTime;
        public uint transitionFadeOutCurve;
        public int transitionFadeOutOffset;
        public byte playPreEntryTransition;
        public byte playPostExistTransition;

        private byte[] _data;

        public long Size => 76;

        public Transition(CustomBinaryReader reader)
        {
            Read(reader);
        }

        public void Read(CustomBinaryReader reader)
        {
            _data = reader.ReadBytes(66);
            //sourceID = reader.ReadUInt32();
            //// Read 3?
            //_unknown3 = reader.ReadBytes(3);
            ////destID = reader.ReadUInt32();
            //sourceFadeOutTime = reader.ReadInt32();
            //sourceFadeOutCurve = reader.ReadUInt32();
            //sourceFadeOutOffset = reader.ReadInt32();
            //// 0x2DC
            ////exitSource = reader.ReadUInt32();
            ////nextCueID = reader.ReadUInt32();
            ////nextAction = reader.ReadByte();
            //// 0x2E4
            //destinationFadeInTime = reader.ReadInt32();
            //destinationFadeInCurve = reader.ReadUInt32();
            //destinationFadeInOffset = reader.ReadInt32();
            ////cueFilterID = reader.ReadUInt32();
            ////destinationPlaylistID = reader.ReadUInt32();
            //// 0x2F0
            //syncTo = reader.ReadUInt16();
            //playPreEntryDestination = reader.ReadByte();
            //customCueFilter = reader.ReadByte();
            //transitionObject = reader.ReadBoolean();
            //transitionID = reader.ReadUInt32();
            //// 0x2F9
            //transitionFadeInTime = reader.ReadInt32();
            //transitionFadeInCurve = reader.ReadUInt32();
            //transitionFadeInOffset = reader.ReadInt32();
            //transitionFadeOutTime = reader.ReadInt32();
            //transitionFadeOutCurve = reader.ReadUInt32();
            //transitionFadeOutOffset = reader.ReadInt32();
            //// 0x311
            //playPreEntryTransition = reader.ReadByte();
            //playPostExistTransition = reader.ReadByte();
        }
        public void Write(CustomBinaryWriter writer, bool writeData = false)
        {
            writer.Write(_data);
            //writer.Write(sourceID);
            //writer.Write(destID);
            //writer.Write(sourceFadeOutTime);
            //writer.Write(sourceFadeOutCurve);
            //writer.Write(sourceFadeOutOffset);
            //writer.Write(exitSource);
            //writer.Write(nextCueID);
            //writer.Write(nextAction);
            //writer.Write(destinationFadeInTime);
            //writer.Write(destinationFadeInCurve);
            //writer.Write(destinationFadeInOffset);
            //writer.Write(cueFilterID);
            //writer.Write(transitionObject);
            //writer.Write(transitionID);
            //writer.Write(transitionFadeInTime);
            //writer.Write(transitionFadeInCurve);
            //writer.Write(transitionFadeInOffset);
            //writer.Write(transitionFadeOutTime);
            //writer.Write(transitionFadeOutCurve);
            //writer.Write(transitionFadeOutOffset);
            //writer.Write(playPreEntryTransition);
            //writer.Write(playPostExistTransition);
        }
    }
}
