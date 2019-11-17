using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankParser
{
    class HIRCData : Parsable
    {
        [JsonIgnore]
        public byte[] header;
        public string headerName;
        public UInt32 size;
        public int count;
        public List<HIRCObject> objects;
        [JsonIgnore]
        public long Size => size + 8;

        public HIRCData(CustomBinaryReader reader)
        {
            Read(reader);
        }

        public void Read(CustomBinaryReader reader)
        {
            header = reader.ReadBytes(4);
            headerName = Encoding.UTF8.GetString(header);
            size = reader.ReadUInt32();
            count = reader.ReadInt32();
            objects = new List<HIRCObject>();
            long offset = reader.Position;
            for (int i = 0; i < count; i++)
            {
                byte objType = reader.ReadByte();
                HIRCObject tmp;
                switch (objType)
                {
                    case HIRCObject.TYPE_SOUNDSFXVOICE:
                        tmp = new SFXHIRCObject(reader, objType);
                        break;
                    case HIRCObject.TYPE_EVENT:
                        tmp = new EventHIRCObject(reader, objType);
                        break;
                    default:
                        tmp = new HIRCObject(reader, objType);
                        break;
                }
                tmp.Offset = offset;
                offset = reader.Position;
                objects.Add(tmp);
            }
        }

        public void Write(CustomBinaryWriter writer, bool writeData = false)
        {
            writer.Write(header);
            writer.Write(size);
            writer.Write(objects.Count);
            // Each object writes their own ObjectInfo
            writer.WriteMany(objects);
        }
    }
    class HIRCObject : Parsable
    {
        public const int TYPE_SOUNDSFXVOICE = 0x2;
        public const int TYPE_EVENTACTION = 0x3;
        public const int TYPE_EVENT = 0x4;

        public long Offset;
        private byte ObjType { get; }
        public string ObjTypeString {
            get
            {
                switch (ObjType)
                {
                    case TYPE_SOUNDSFXVOICE:
                        return "Sound SFX/Sound Voice";
                    case TYPE_EVENTACTION:
                        return "Event Action";
                    case TYPE_EVENT:
                        return "Event";
                    case 0x5:
                        return "Random Container or Sequence Container";
                    case 0x7:
                        return "Actor-Mixer";
                    case 0xA:
                        return "Music Segment";
                    case 0xB:
                        return "Music Track";
                    case 0xC:
                        return "Music Switch Container";
                    case 0xD:
                        return "Music Playlist Container";
                    case 0xE:
                        return "Attenuation";
                    case 0x12:
                        return "Effect";
                    case 0x13:
                        return "Auxiliary Bus";
                }
                return ObjType.ToString();
            }
        }

        long Parsable.Size => (long)Size + 8;

        public uint Size;
        public uint ID;
        [JsonIgnore]
        private byte[] Data;

        private long _startPosition;
        internal HIRCObject(byte obj)
        {
            ObjType = obj;
        }

        public HIRCObject(CustomBinaryReader reader, byte obj)
        {
            ObjType = obj;
            Read(reader);
        }

        internal void ReadData(CustomBinaryReader reader)
        {
            Data = reader.ReadBytes((int)(Size - (reader.Position - _startPosition)));
        }

        internal void WriteData(CustomBinaryWriter writer, int startingIndex)
        {
            writer.Write(Data, startingIndex, Data.Length - startingIndex);
        }

        // TODO: Populate Offset
        public virtual void Read(CustomBinaryReader reader, bool readData = true)
        {
            Size = reader.ReadUInt32();
            _startPosition = reader.Position;
            ID = reader.ReadUInt32();
            if (readData)
                ReadData(reader);
        }

        public void Read(CustomBinaryReader reader)
        {
            Size = reader.ReadUInt32();
            _startPosition = reader.Position;
            ID = reader.ReadUInt32();
            ReadData(reader);
        }

        public virtual void Write(CustomBinaryWriter writer, bool writeData = true)
        {
            writer.Write(ObjType);
            writer.Write(Size);
            writer.Write(ID);
            if (writeData)
                WriteData(writer, 0);
        }
    }

    class EventHIRCObject : HIRCObject
    {
        public EventHIRCObject(CustomBinaryReader reader, byte obj) : base(obj)
        {
            Read(reader);
        }

        public override void Read(CustomBinaryReader reader, bool readData = true)
        {
            base.Read(reader, false);
            //EventCount = reader.ReadInt32();
            //EventIDs = new List<int>();
            //for (int i = 0; i < EventCount; i++)
            //{
            //    EventIDs.Add(reader.ReadInt32());
            //}
            if (readData)
                ReadData(reader);
        }

        public override void Write(CustomBinaryWriter writer, bool writeData = true)
        {
            base.Write(writer, false);
            if (writeData)
                WriteData(writer, 0);
        }
    }

    class SFXHIRCObject : HIRCObject
    {
        public uint _unknown;
        public uint State;
        public uint IDAudio;
        public uint IDSource;
        public byte SoundType;

        public SFXHIRCObject(CustomBinaryReader reader, byte obj) : base(obj)
        {
            Read(reader);
        }
        
        public override void Read(CustomBinaryReader reader, bool readData = true)
        {
            base.Read(reader, false);
            _unknown = reader.ReadUInt32();
            State = reader.ReadUInt32();
            IDAudio = reader.ReadUInt32();
            IDSource = reader.ReadUInt32();
            SoundType = reader.ReadByte();
            if (readData)
                ReadData(reader);
        }

        public override void Write(CustomBinaryWriter writer, bool writeData = true)
        {
            base.Write(writer, false);
            writer.Write(_unknown);
            writer.Write(State);
            writer.Write(IDAudio);
            writer.Write(IDSource);
            writer.Write(SoundType);
            if (writeData)
                WriteData(writer, 17);
        }
    }
}
