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
        static void Main(string[] args)
        {
            string jsonString;
            using (StreamReader reader = new StreamReader(Console.OpenStandardInput(), Console.InputEncoding))
            {
                jsonString = reader.ReadToEnd();
            }
            Invocation inv = JsonConvert.DeserializeObject<Invocation>(jsonString);
            InvocationResult res = Program.RunInvocation(inv);
            string jsonOut = JsonConvert.SerializeObject(res, Formatting.None);
            Console.WriteLine(jsonOut);
        }
    }
}
