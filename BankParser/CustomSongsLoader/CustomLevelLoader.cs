using AssetParser;
using AssetParser.AssetsChanger.Assets;
using AssetParser.PistolWhipAssets;
using AssetParser.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace CustomSongsLoader
{
    public class CustomLevelLoader
    {
        public AssetsEngine Engine { get; }
        public CustomLevelLoader(string path)
        {
            AssetsConfig config = new AssetsConfig()
            {
                RootFileProvider = new BundleFileProvider(path, false),
                AssetsPath = path.GetDirectoryFwdSlash()
            };
            Engine = new AssetsEngine(config, "NONE");
        }
        public IObjectInfo<LevelDatabase> MainDatabase { get; private set; }
        public IObjectInfo<AlbumArtDatabase> AlbumDatabase { get; private set; }
        public void Save()
        {
            Engine.Save();
        }
        public void ReplaceSongPath(string songPath, string nameToReplace, string apkPath)
        {
            MainDatabase = Engine.Manager.MassFirstAsset<LevelDatabase>((oi) => true, false);
            if (MainDatabase == null || MainDatabase.Object == null)
            {
                throw new AssetsException("Could not find main level database!");
            }
            AlbumDatabase = Engine.Manager.MassFirstAsset<AlbumArtDatabase>((oi) => true, false);
            if (AlbumDatabase == null || AlbumDatabase.Object == null)
            {
                throw new AssetsException("Could not find album database!");
            }
            IObjectInfo<Koreography> targetKor = Engine.Manager.MassFirstAsset<Koreography>((o) => o.Object.Name == nameToReplace, false);
            if (targetKor == null || targetKor.Object == null)
            {
                throw new AssetsException("Could not find a Koreography object matching name: " + nameToReplace + "!");
            }
            var fp = new FolderFileProvider(songPath.GetDirectoryFwdSlash(), false);
            //var ac = LoadAudioClipObject(nameToReplace, songPath, fp);
            //MainDatabase.ParentFile.AddObject(ac);
            // Copy Existing Song Info and add it
            CopyExistingSongDataByIndex(indexToCopy);
            // Replace Song of copied data with desired song

            // Apply BNK modifications

            // Copy new song into folder
            //targetKor.Object.SourceClip = ac.PtrFrom(targetKor.Object);
            //targetKor.Object.SourceClipPath = "";
            File.Copy(songPath, apkPath.GetFilenameFwdSlash() + "/" + nameToReplace + ".wem", true);
            fp.Save(); // Save, simply to close
        }
        public void CopyExistingSongDataByIndex(int index)
        {
            var toCopyLD = MainDatabase.Object.levelData[index];
            MainDatabase.Object.levelData.Add(toCopyLD);
            //var toCopyK = MainDatabase.Object.koreoSets[index];
            //MainDatabase.Object.koreoSets.Add(toCopyK);
            AddCoverToDatabase(index);
        }

        public void AddCoverToDatabase(int indexToCopy)
        {
            var toCopy = AlbumDatabase.Object.albumMetadata[indexToCopy];
            // TODO, copy constructor?
            AlbumDatabase.Object.albumMetadata.Add(new AlbumArtDatabase.AlbumArtMetadata()
            {
                art = toCopy.art,
                artIsWIP = toCopy.artIsWIP,
                levelData = toCopy.levelData,
                shortSongName = toCopy.shortSongName,
                songName = toCopy.songName,
                tempo = toCopy.tempo,
                shortSongArtists = toCopy.shortSongArtists,
                songArtists = toCopy.songArtists
            });
        }

        public AudioClipObject LoadAudioClipObject(string songID, string songPath, IFileProvider songFileProvider)
        {
            //string outputFileName = levelData.LevelID + ".ogg";
            int channels;
            int frequency;
            Single length;
            byte[] oggBytes = songFileProvider.Read(songPath);
            unsafe
            {

                GCHandle pinnedArray = GCHandle.Alloc(oggBytes, GCHandleType.Pinned);
                try
                {
                    IntPtr pointer = pinnedArray.AddrOfPinnedObject();
                    int error;
                    StbSharp.StbVorbis.stb_vorbis_alloc alloc;
                    StbSharp.StbVorbis.stb_vorbis v = StbSharp.StbVorbis.stb_vorbis_open_memory((byte*)pointer.ToPointer(), oggBytes.Length, &error, &alloc);
                    channels = v.channels;
                    frequency = (int)v.sample_rate;
                    length = StbSharp.StbVorbis.stb_vorbis_stream_length_in_seconds(v);
                    StbSharp.StbVorbis.stb_vorbis_close(v);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    pinnedArray.Free();
                }
            }
            var audioClip = new AudioClipObject(MainDatabase.ParentFile)
            {
                Name = songID + "_AudioClip",
                LoadType = 1,
                IsTrackerFormat = false,
                Ambisonic = false,
                SubsoundIndex = 0,
                PreloadAudioData = false,
                LoadInBackground = true,
                Legacy3D = true,
                CompressionFormat = 1,
                BitsPerSample = 16,
                Channels = channels,
                Frequency = frequency,
                Length = (Single)length,
                Resource = new StreamedResource(songID + ".ogg", 0, Convert.ToUInt64(songFileProvider.GetFileSize(songPath)))
            };
            return audioClip;
        }
    }
}
