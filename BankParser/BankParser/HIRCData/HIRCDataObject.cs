using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankParser.HIRCData
{
    // TODO: http://wiki.xentax.com/index.php/Wwise_SoundBank_(*.bnk)
    public class HIRCDataObject : Parsable
    {
        [JsonIgnore]
        public byte[] header;
        public string headerName;
        public UInt32 size;
        public int count;
        public List<HIRCObject> objects;
        [JsonIgnore]
        public long Size => size + 8;

        public HIRCDataObject(CustomBinaryReader reader)
        {
            Read(reader);
        }

        public void Read(CustomBinaryReader reader)
        {
            header = reader.ReadBytes(4);
            headerName = Encoding.UTF8.GetString(header);
            size = reader.ReadUInt32();
            long _startPosition = reader.Position;
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
                    case HIRCObject.TYPE_MUSICSEGMENT:
                        tmp = new MusicSegmentHIRCObject(reader, objType);
                        break;
                    case HIRCObject.TYPE_MUSICTRACK:
                        tmp = new MusicTrackHIRCObject(reader, objType);
                        break;
                    case HIRCObject.TYPE_MUSICSWITCHCONTAINER:
                        tmp = new MusicSwitchContainerHIRCObject(reader, objType);
                        //tmp = new HIRCObject(reader, objType);
                        //File.WriteAllBytes("tempSwitchContainer.dat", tmp.Data);
                        break;
                    default:
                        tmp = new HIRCObject(reader, objType);
                        break;
                }
                tmp.Offset = offset.ToString("X");
                offset = reader.Position;
                objects.Add(tmp);
            }
            // Double check!
            if (size - (reader.Position - _startPosition) != 0)
            {
                Console.WriteLine("Incorrect IO!");
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
}
