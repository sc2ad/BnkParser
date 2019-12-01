using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankParser.HIRCData
{
    public class HIRCObject : Parsable
    {
        public const int TYPE_SOUNDSFXVOICE = 0x2;
        public const int TYPE_EVENTACTION = 0x3;
        public const int TYPE_EVENT = 0x4;
        public const int TYPE_MUSICSEGMENT = 0xA;
        public const int TYPE_MUSICTRACK = 0xB;
        public const int TYPE_MUSICSWITCHCONTAINER = 0xC;

        public string Offset;
        private byte ObjType { get; }
        public string ObjTypeString
        {
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
                    case 0x6:
                        return "Switch Container";
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
        internal byte[] Data { get; private set; }

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
            writer.Write(Data.Length + 4);
            writer.Write(ID);
            if (writeData)
                WriteData(writer, 0);
        }
    }
}
