using AssetParser;
using AssetParser.AssetsChanger;
using AssetParser.AssetsChanger.Assets;
using AssetParser.PistolWhipAssets;
using AssetParser.Utils;
using Newtonsoft.Json;
using System;
using System.IO;

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
        static void Main(string[] args)
        {
            Console.WriteLine("Path to assets bundle...");
            string path = Console.ReadLine();
            AssetsConfig config = new AssetsConfig()
            {
                RootFileProvider = new BundleFileProvider(path, false),
                AssetsPath = path.GetDirectoryFwdSlash()
            };
            AssetsEngine engine = new AssetsEngine(config, "NONE");
            var lds = engine.Manager.MassFindAssets<LevelData>((oi) => true, false);
            var kors = engine.Manager.MassFindAssets<Koreography>((oi) => true, false);
            var tds = engine.Manager.MassFindAssets<TrackData>((oi) => true, false);
            var gs = engine.Manager.MassFindAssets<GeoSet>((oi) => true, false);
            if (Directory.Exists("dumps"))
            {
                Directory.Delete("dumps", true);
            }
            if (Directory.Exists("raws"))
            {
                Directory.Delete("raws", true);
            }
            Directory.CreateDirectory("dumps");
            Directory.CreateDirectory("raws");
            Directory.CreateDirectory("dumps/LevelData");
            Directory.CreateDirectory("raws/LevelData");
            foreach (var l in lds)
            {
                string json = JsonConvert.SerializeObject(l.Object, Formatting.Indented);
                File.WriteAllText(Path.Join("dumps/LevelData", l.Object.Name + "_LevelData.json"), json);
                File.WriteAllBytes(Path.Join("dumps/LevelData", l.Object.Name + "_LevelData_SectionData.dat"), l.Object.sectionData);
                DumpRaw(l, "LevelData");
            }
            Console.WriteLine("Completed LevelData!");
            Directory.CreateDirectory("dumps/Koreography");
            Directory.CreateDirectory("raws/Koreography");
            foreach (var k in kors)
            {
                string json = JsonConvert.SerializeObject(k.Object, Formatting.Indented);
                File.WriteAllText(Path.Join("dumps/Koreography", k.Object.Name + "_Koreography.json"), json);
                DumpRaw(k, "Koreography");
            }
            Console.WriteLine("Completed Koreography!");
            Directory.CreateDirectory("dumps/TrackData");
            Directory.CreateDirectory("raws/TrackData");
            foreach (var t in tds)
            {
                string json = JsonConvert.SerializeObject(t.Object, Formatting.Indented);
                File.WriteAllText(Path.Join("dumps/TrackData", t.Object.Name + "_TrackData.json"), json);
                DumpRaw(t, "TrackData");
            }
            Console.WriteLine("Completed TrackData!");
            Directory.CreateDirectory("dumps/GeoSet");
            Directory.CreateDirectory("raws/GeoSet");
            foreach (var g in gs)
            {
                string json = JsonConvert.SerializeObject(g.Object, Formatting.Indented);
                File.WriteAllText(Path.Join("dumps/GeoSet", g.Object.Name + "_GeoSet.json"), json);
                DumpRaw(g, "GeoSet");
            }
            Console.WriteLine("Completed GeoSet!");
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
