using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankParser.HIRCData
{
    public class MusicSwitchContainerHIRCObject : HIRCObject
    {
        public SoundStructure soundStruct;
        // 0x7D
        public uint numChildren;
        public List<uint> children;
        // 4 zeros
        private byte[] _zeros0;
        private float _unknown;
        // 8 zeros
        private byte[] _zeros1;
        public float tempo;
        // 0xF9
        public byte timeSignature1;
        public byte timeSignature2;
        private byte _always1;
        // 4 zeros
        private byte[] _zeros2;
        public uint numTransitions;
        public List<Transition> transitions;
        private byte[] _unknown5;
        public uint switchType;
        public uint switchGroupID;
        public uint switchByteCount;
        public bool continueToPlay;
        // Pairs of IDs
        public List<(uint, uint, uint)> switches;

        public MusicSwitchContainerHIRCObject(CustomBinaryReader reader, byte obj) : base(obj)
        {
            Read(reader, true);
        }

        public override void Read(CustomBinaryReader reader, bool readData = true)
        {
            base.Read(reader, false);
            soundStruct = new SoundStructure(reader);
            numChildren = reader.ReadUInt32();
            children = reader.ReadMany((r) => r.ReadUInt32(), numChildren);
            _zeros0 = reader.ReadBytes(4);
            _unknown = reader.ReadSingle();
            _zeros1 = reader.ReadBytes(8);
            tempo = reader.ReadSingle();
            timeSignature1 = reader.ReadByte();
            timeSignature2 = reader.ReadByte();
            _always1 = reader.ReadByte();
            _zeros2 = reader.ReadBytes(4);
            numTransitions = reader.ReadUInt32();
            transitions = reader.ReadMany((r) => new Transition(r), numTransitions);
            // crazy
            _unknown5 = reader.ReadBytes(5);
            switchType = reader.ReadUInt32();
            switchGroupID = reader.ReadUInt32();
            switchByteCount = reader.ReadUInt32();
            continueToPlay = reader.ReadBoolean();
            switches = reader.ReadMany((r) => (r.ReadUInt32(), r.ReadUInt32(), r.ReadUInt32()), switchByteCount / 12);
            if (readData)
                ReadData(reader);
        }

        public override void Write(CustomBinaryWriter writer, bool writeData = true)
        {
            base.Write(writer, false);
            soundStruct.Write(writer);
            writer.Write(children.Count);
            writer.WriteMany(children, c => writer.Write(c));
            writer.Write(_zeros0);
            writer.Write(_unknown);
            writer.Write(_zeros1);
            writer.Write(tempo);
            writer.Write(timeSignature1);
            writer.Write(timeSignature2);
            writer.Write(_always1);
            writer.Write(_zeros2);
            writer.Write(transitions.Count);
            writer.WriteMany(transitions);
            // crazy
            writer.Write(_unknown5);
            writer.Write(switchType);
            writer.Write(switchGroupID);
            writer.Write(switches.Count * 12);
            writer.Write(continueToPlay);
            writer.WriteMany(switches, (item) =>
            {
                writer.Write(item.Item1);
                writer.Write(item.Item2);
                writer.Write(item.Item3);
            });
            if (writeData)
                WriteData(writer, 0);
        }
    }
}
