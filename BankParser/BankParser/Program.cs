using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankParser
{
    class Program
    {
        //TODO Add JSON serialization/deserialization, perhaps a GUI
        static void Main(string[] args)
        {
            string bank = "";
            uint search = 0;
            if (args.Length < 2)
            {
                Console.WriteLine("Enter path to global.bnk...");
                bank = Console.ReadLine();
                Console.WriteLine("Enter ID to dump (or leave blank for nothing)...");
                uint.TryParse(Console.ReadLine(), out search);
            } else
            {
                bank = args[1];
            }
            var stream = new FileStream(bank, FileMode.Open);
            var reader = new CustomBinaryReader(stream);
            BnkData data = new BnkData(reader);
            Console.WriteLine($"Position: {reader.Position} / {reader.BaseStream.Length}");
            File.WriteAllText("out.json", JsonConvert.SerializeObject(data, Formatting.Indented));
            var dmp = data.DumpID(search);
            if (dmp != null)
            {
                File.WriteAllBytes(search + ".dat", dmp);
            }
            Console.WriteLine("Writing new bank...");
            stream.Dispose();
            reader.Dispose();
            using (stream = new FileStream("out.bnk", FileMode.OpenOrCreate))
            {
                var writer = new CustomBinaryWriter(stream);
                data.Write(writer);
                writer.Close();
            }
        }
    }
}
