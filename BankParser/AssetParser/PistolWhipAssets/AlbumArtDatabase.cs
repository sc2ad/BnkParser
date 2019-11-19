using AssetParser.AssetsChanger;
using AssetParser.AssetsChanger.Assets;
using AssetParser.AssetsChanger.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.PistolWhipAssets
{
    public class AlbumArtDatabase : MonoBehaviourObject, INeedAssetsMetadata
    {
        public List<AlbumArtMetadata> albumMetadata { get; set; }
        public AlbumArtDatabase(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("AlbumArtDatabase"))
        {
        }

        public AlbumArtDatabase(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public override void ParseObject(AssetsReader reader)
        {
            albumMetadata = reader.ReadArrayOf((r) => new AlbumArtMetadata(ObjectInfo.ParentFile, this, r));
        }

        protected override void WriteObject(AssetsWriter writer)
        {
        }

        public class AlbumArtMetadata
        {
            public SmartPtr<LevelData> levelData { get; set; }
            public string songName { get; set; }
            public string shortSongName { get; set; }
            public string songArtists { get; set; }
            public string shortSongArtists { get; set; }
            public int tempo { get; set; }
            public SmartPtr<SpriteObject> art { get; set; }
            public bool artIsWIP { get; set; }
            // Align4

            private AssetsFile assetsFile;
            private AssetsObject owner;

            public AlbumArtMetadata()
            {

            }
            public AlbumArtMetadata(AssetsFile file, AssetsObject o, AssetsReader reader)
            {
                assetsFile = file;
                owner = o;
                Parse(reader);
            }

            private void Parse(AssetsReader reader)
            {
                levelData = SmartPtr<LevelData>.Read(assetsFile, owner, reader);
                songName = reader.ReadString();
                shortSongName = reader.ReadString();
                songArtists = reader.ReadString();
                shortSongArtists = reader.ReadString();
                tempo = reader.ReadInt32();
                art = SmartPtr<SpriteObject>.Read(assetsFile, owner, reader);
                artIsWIP = reader.ReadBoolean();
                reader.AlignTo(4);
            }

            public void Write(AssetsWriter writer)
            {
                levelData.WritePtr(writer);
                writer.Write(songName);
                writer.Write(shortSongName);
                writer.Write(songArtists);
                writer.Write(shortSongArtists);
                writer.Write(tempo);
                art.WritePtr(writer);
                writer.Write(artIsWIP);
                writer.AlignTo(4);
            }
        }
    }
}
