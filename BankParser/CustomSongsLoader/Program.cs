using AssetParser;
using AssetParser.AssetsChanger;
using AssetParser.AssetsChanger.Assets;
using AssetParser.PistolWhipAssets;
using AssetParser.Utils;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.Serialization.Formatters.Binary;

namespace CustomSongsLoader
{
    class Invocation
    {

    }
    class InvocationResult
    {

    }
    class Program
    {
        static void DumpRaw(IObjectInfo<MonoBehaviourObject> info, string label)
        {
            using (var stream = new FileStream($"raws/{label}/{info.Object.Name}_{info.ObjectID}.dat", FileMode.OpenOrCreate))
            {
                using (AssetsWriter writer = new AssetsWriter(stream))
                {
                    info.Object.Write(writer);
                }
            }
        }
        static void LogPtr(object sp, PropertyInfo fd)
        {
            var fileID = sp.GetType().GetProperty("FileID").GetValue(sp);
            var pathID = sp.GetType().GetProperty("PathID").GetValue(sp);
            Console.WriteLine($"{fd.Name}: Ptr: {fileID} {pathID}");
            var obj = sp.GetType().GetProperty("Target").GetValue(sp);
            if (obj != null)
                Console.WriteLine($"{fd.Name}: Object: {obj}");
            else
                Console.WriteLine($"{fd.Name}: Object null!");
        }
        static void DumpAll<T>(System.Collections.Generic.IEnumerable<IObjectInfo<T>> arr) where T : MonoBehaviourObject
        {
            bool rawFields = false;
            string name = typeof(T).Name;
            Console.WriteLine($"Beginning {name} dump...");
            if (Directory.Exists($"dumps/{name}") && Directory.Exists($"raws/{name}"))
            {
                Console.WriteLine($"Found existing directories for {name}!");
                Console.WriteLine("Checking if their file counts match...");
                // Then we can use the cached, already available files instead.
                var c1 = Directory.GetFiles($"dumps/{name}").Length;
                var c2 = Directory.GetFiles($"raws/{name}").Length;
                if (c1 >= 1 && c2 >= 1)
                {
                    // At least one dump, must match lengths
                    Console.WriteLine($"Completed {name}! Files dumps already exist!");
                    return;
                }
                Console.WriteLine($"File counts mismatch! {c1} != {c2}");
            }
            Directory.CreateDirectory($"dumps/{name}");
            Directory.CreateDirectory($"raws/{name}");
            if (name == "LevelData")
            {
                rawFields = true;
            }
            foreach (var l in arr)
            {
                string json = JsonConvert.SerializeObject(l.Object, Formatting.Indented);
                File.WriteAllText(Path.Join($"dumps/{name}", l.Object.Name + $"_{name}.json"), json);
                if (rawFields)
                {
                    byte[] bts = (byte[])l.Object.GetType().GetProperty("sectionData").GetValue(l.Object, new object[0]);
                    File.WriteAllBytes(Path.Join($"dumps/{name}", l.Object.Name + $"_{name}_SectionData.dat"), bts);
                }
                DumpRaw(l, name);
            }
            Console.WriteLine($"Completed {name}!");
        }
        static void CreateDataDirs(bool replace)
        {
            if (replace)
            {
                if (Directory.Exists("dumps"))
                {
                    Directory.Delete("dumps", true);
                }
                if (Directory.Exists("raws"))
                {
                    Directory.Delete("raws", true);
                }
            }
            if (!Directory.Exists("dumps"))
            {
                Directory.CreateDirectory("dumps");
            }
            if (!Directory.Exists("raws"))
            {
                Directory.CreateDirectory("raws");
            }
        }
        static bool ParseBool(string line, bool def = false)
        {
            var l = line.ToLower();
            if (l == "t" || l == "true" || l == "1")
                return true;
            if (l == "f" || l == "false" || l == "0")
                return false;
            return def;
        }

        static object BinaryStreamHelper(byte[] buffer)
        {
            BinaryFormatter f = new BinaryFormatter();
            using (MemoryStream s = new MemoryStream(buffer))
            {
                return f.Deserialize(s);
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Path to assets bundle...");
            //string path = Console.ReadLine();
            string path = @"C:\Users\adamz\Desktop\Code\AndroidModding\Raws\PistolWhipQuest\APKs\2019-11-11T12-43-55.390Z_0.5.4.6\assets\bin\Data\data.unity3d";
            //Console.WriteLine("Recreate existing files (t/F)");
            Console.WriteLine("Enter song path...");
            string songPath = Console.ReadLine();
            //string songPath = "";
            string apkPath = @"C:\Users\adamz\Desktop\Code\AndroidModding\Raws\PistolWhipQuest\APKs\2019-11-11T12-43-55.390Z_0.5.4.6\assets\Audio\GeneratedSoundBanks\Android";
            //bool recreate = ParseBool(Console.ReadLine(), false);
            //AssetsConfig config = new AssetsConfig()
            //{
            //    RootFileProvider = new BundleFileProvider(path, false),
            //    AssetsPath = path.GetDirectoryFwdSlash()
            //};
            //AssetsEngine engine = new AssetsEngine(config, "NONE");
            //var lds = engine.Manager.MassFindAssets<LevelData>((oi) => true, false);
            //var kors = engine.Manager.MassFindAssets<Koreography>((oi) => true, false);
            //var tds = engine.Manager.MassFindAssets<TrackData>((oi) => true, false);
            //var gs = engine.Manager.MassFindAssets<GeoSet>((oi) => true, false);
            //var ldb = engine.Manager.MassFindAssets<LevelDatabase>((oi) => true, false);
            //var ladb = engine.Manager.MassFindAssets<LevelAssetDatabase>((oi) => true, false);
            //var wwsrs = engine.Manager.MassFindAssets<WwiseStateReference>((oi) => true, false);
            //CreateDataDirs(recreate);
            //Console.WriteLine("Beginning Dump...");
            //DumpAll(lds);
            //DumpAll(kors);
            //DumpAll(tds);
            //DumpAll(gs);
            //DumpAll(ldb);
            //DumpAll(ladb);
            //DumpAll(wwsrs);
            //Console.WriteLine("Success!");

            CustomLevelLoader loader = new CustomLevelLoader(path);
            Console.WriteLine("Attempting to replace song...");
            loader.ReplaceSongPath(songPath, "BlackMagic_Edit", apkPath);
            Console.WriteLine("Saving Assets... THIS TAKES AWHILE!");
            //loader.Engine.Manager.OpenFiles[1].HasChanges = true;
            loader.Save();

            //string jsonString;
            //using (StreamReader reader = new StreamReader(Console.OpenStandardInput(), Console.InputEncoding))
            //{
            //    jsonString = reader.ReadToEnd();
            //}
            //Invocation inv = JsonConvert.DeserializeObject<Invocation>(jsonString);
            //InvocationResult res = Program.RunInvocation(inv);
            //string jsonOut = JsonConvert.SerializeObject(res, Formatting.None);
            //Console.WriteLine(jsonOut);
        }
    }
}
