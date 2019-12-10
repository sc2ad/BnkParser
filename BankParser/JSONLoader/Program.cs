using AssetParser;
using AssetParser.AssetsChanger;
using AssetParser.AssetsChanger.Assets;
using AssetParser.PistolWhipAssets;
using AssetParser.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JSONLoader
{
    class Program
    {
        #region Dumps
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
        private class MyContractResolver : DefaultContractResolver
        {
            protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
            {
                var list = base.CreateProperties(type, memberSerialization);
                
                Type t = type;
                var types = new List<Type>();
                while (t != null)
                {
                    types.Add(t);
                    t = t.BaseType;
                } 
                foreach (var prop in list)
                {
                    if (types.Contains(prop.DeclaringType))
                    {
                        var ca = type.GetProperty(prop.PropertyName).GetCustomAttribute<JsonIgnoreAttribute>(true);
                        if (ca == null)
                            prop.Ignored = false; // Only public and non-ignored properties are taken
                    }
                }

                return list;
            }
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
                //    new JsonSerializerSettings()
                //{
                //    ContractResolver = new MyContractResolver()
                //});
                File.WriteAllText(Path.Combine($"dumps/{name}", l.Object.Name + $"_{name}.json"), json);
                if (rawFields)
                {
                    byte[] bts = (byte[])l.Object.GetType().GetProperty("sectionData").GetValue(l.Object, new object[0]);
                    File.WriteAllBytes(Path.Combine($"dumps/{name}", l.Object.Name + $"_{name}_SectionData.dat"), bts);
                }
                DumpRaw(l, name);
            }
            Console.WriteLine($"Completed {name}!");
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
        static void CreateDataDirs()
        {
            if (Directory.Exists("dumps"))
            {
                Console.WriteLine("Would you like to replace an already existing 'dumps' folder? (t/F)");
                Directory.Delete("dumps", ParseBool(Console.ReadLine()));
            }
            if (Directory.Exists("raws"))
            {
                Console.WriteLine("Would you like to replace an already existing 'raws' folder? (t/F)");
                Directory.Delete("raws", ParseBool(Console.ReadLine()));
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
        private static Dictionary<string, Type> KnownTypes => new Dictionary<string, Type>
        {
            { "LevelData", typeof(LevelData) },
            { "Koreography", typeof(Koreography) },
            { "TrackData", typeof(TrackData) },
            { "MeshObject", typeof(MeshObject) },
            //{ "GeoSet", typeof(GeoSet) },
            { "LevelDatabase", typeof(LevelDatabase) },
            { "LevelAssetDatabase", typeof(LevelAssetDatabase) },
            { "WwiseStateReference", typeof(WwiseStateReference) }
        };
        private static void DumpKnowns()
        {
            Console.WriteLine("Finding assets...");

            var lds = Engine.Manager.MassFindAssets<LevelData>((oi) => true, false);
            var kors = Engine.Manager.MassFindAssets<Koreography>((oi) => true, false);
            var tds = Engine.Manager.MassFindAssets<TrackData>((oi) => true, false);
            //var gs = Engine.Manager.MassFindAssets<GeoSet>((oi) => true, false);
            var ldb = Engine.Manager.MassFindAssets<LevelDatabase>((oi) => true, false);
            var ladb = Engine.Manager.MassFindAssets<LevelAssetDatabase>((oi) => true, false);
            var wwsrs = Engine.Manager.MassFindAssets<WwiseStateReference>((oi) => true, false);
            Console.WriteLine("All assets found!");
            CreateDataDirs();
            Console.WriteLine("Beginning Dump...");
            DumpAll(lds);
            DumpAll(kors);
            DumpAll(tds);
            //DumpAll(gs);
            DumpAll(ldb);
            DumpAll(ladb);
            DumpAll(wwsrs);
            Console.WriteLine("Success!");
        }
        #endregion
        #region Load
        private static Type ParseDirectoryType(string name)
        {
            return KnownTypes[name];
        }
        private static void ReplaceObject(string typeName, object o)
        {
            object asset = null;
            switch (typeName)
            {
                //case "LevelData":
                //    asset = Engine.Manager.MassFirstAsset<LevelData>((oi) => oi.Object.Name == (o as LevelData).Name, false);
                //    break;
                //case "Koreography":
                //    asset = Engine.Manager.MassFirstAsset<Koreography>((oi) => oi.Object.Name == (o as Koreography).Name, false);
                //    break;
                //case "TrackData":
                //    asset = Engine.Manager.MassFirstAsset<TrackData>((oi) => oi.Object.Name == (o as TrackData).Name, false);
                //    break;
                //case "MeshObject":
                //    asset = Engine.Manager.MassFirstAsset<MeshObject>((oi) => oi.Object.Name == (o as MeshObject).Name, false);
                //    break;
                //case "GeoSet":
                //    asset = Engine.Manager.MassFirstAsset<GeoSet>((oi) => oi.Object.Name == (o as GeoSet).Name, false);
                //    break;
                case "LevelDatabase":
                    asset = Engine.Manager.MassFirstAsset<LevelDatabase>((oi) => oi.Object.Name == (o as LevelDatabase).Name, false);
                    break;
                //case "LevelAssetDatabase":
                //    asset = Engine.Manager.MassFirstAsset<LevelAssetDatabase>((oi) => oi.Object.Name == (o as LevelAssetDatabase).Name, false);
                //    break;
                //case "WwiseStateReference":
                //    asset = Engine.Manager.MassFirstAsset<WwiseStateReference>((oi) => oi.Object.Name == (o as WwiseStateReference).Name, false);
                //    break;
            }
            if (asset == null)
                return;
            var obj = asset.GetType().GetProperty("Object").GetValue(asset);
            foreach (var p in obj.GetType().GetProperties())
            {
                try
                {
                    // Only copy fields that aren't JsonIgnored and public
                    var ignored = p.GetCustomAttribute<JsonIgnoreAttribute>(true);
                    if (ignored == null)
                        p.SetValue(obj, p.GetValue(o));
                } catch (Exception)
                {
                    // Skip this property because of an exception, or because it is Data and we don't want to set it anyways
                }
            }
            asset.GetType().GetProperty("DataOffset").SetValue(asset, -1);
            asset.GetType().GetProperty("DataSize").SetValue(asset, -1);
        }
        private static void LoadDumps()
        {
            Console.WriteLine("ENSURE THAT NO FILE NAMES HAVE BEEN CHANGED!");
            Console.WriteLine("ALSO MAKE SURE THAT YOUR data.unity3d FILE IS BACKED UP! THIS PROGRAM WILL NOT BACK IT UP FOR YOU!");
            Console.WriteLine("Enter the path to your existing 'dumps' folder (dumped from this tool) or drag and drop it into this window and press enter");
            string dumpsPath = Console.ReadLine();
            var subDirs = Directory.GetDirectories(dumpsPath);
            foreach (var sd in subDirs)
            {
                // Parse directory bottom path for type of object
                Type type = ParseDirectoryType(Path.GetFileName(sd));
                Console.WriteLine($"Loading type: {type.Name} from dump...");
                foreach (var f in Directory.GetFiles(sd))
                {
                    // Only deserialize .json files
                    if (f.EndsWith(".json"))
                        ReplaceObject(type.Name, JsonConvert.DeserializeObject(File.ReadAllText(f), type));
                }
                // Let's try to save after writing each type, perhaps this will help limit the memory footprint
            }
            Console.WriteLine("Complete!");
        }
        #endregion
        private static AssetsEngine Engine;
        static void Main(string[] args)
        {
            string bundleFile;
            if (args.Length == 2)
                bundleFile = args[1];
            else
            {
                Console.WriteLine("Enter the path to your unity3d file or drag and drop your unity3d file into this window and press enter...");
                Console.WriteLine("THIS PATH MUST HAVE NO SPACES!");
                bundleFile = Console.ReadLine();
            }
            bundleFile = bundleFile.Replace("\"", "");
            try
            {
                AssetsConfig config = new AssetsConfig()
                {
                    RootFileProvider = new BundleFileProvider(bundleFile, false),
                    AssetsPath = bundleFile.GetDirectoryFwdSlash()
                };
                Engine = new AssetsEngine(config, "NONE");
                Console.WriteLine("Press (1) for dumps (2) for parse");
                Console.WriteLine("Dumps will be created in: dumps/ and raws/");
                var key = Console.ReadKey();
                switch (key.KeyChar)
                {
                    case '1':
                        DumpKnowns();
                        break;
                    case '2':
                        LoadDumps();
                        Engine.Save();
                        break;
                    default:
                        throw new InvalidOperationException("Must press either 1 or 2!");
                }
            } catch (Exception e)
            {
                Console.Write(e);
            } finally
            {
                Console.WriteLine("\nPress enter to close...");
                Console.ReadLine();
            }
        }
    }
}
