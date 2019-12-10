using AssetParser.AssetsChanger.Assets;
using AssetParser.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.AssetsChanger
{
    public class AssetsMetadata
    {
        public AssetsMetadata(AssetsFile owner)
        {
            Types = new List<AssetsType>();
            ObjectInfos = new List<IObjectInfo<AssetsObject>>();
            Adds = new List<RawPtr>();
            ExternalFiles = new List<ExternalFile>();
            ParentFile = owner;
        }


        public string Version { get; set; }
        public Int32 Platform { get; set; }
        public bool HasTypeTrees { get; set; }
        public List<AssetsType> Types { get; set; }
        public List<IObjectInfo<AssetsObject>> ObjectInfos { get; set; }
        //TODO: figure out what adds are
        public List<RawPtr> Adds { get; set; }
        public List<ExternalFile> ExternalFiles { get; set; }
        public AssetsFile ParentFile { get; set; }

        private string[] versionSplit;
        //this is crap, but should work for unity probably, or I can fix it later
        public bool VersionGte(string checkVer)
        {
            if (versionSplit == null)
                versionSplit = Version.Split('.');
            var checkSplit = checkVer.Split('.');
            for (int i = 0; i < versionSplit.Length && i < checkSplit.Length; i++)
            {
                int v;
                int c;
                if (Int32.TryParse(versionSplit[i], out v) && Int32.TryParse(checkSplit[i], out c))
                {
                    if (v < c)
                        return false;
                }
                else
                {
                    if (checkSplit[i].CompareTo(versionSplit[i]) < 0)
                        return false;
                }
            }
            return true;
        }

        private int PreloadObjectOrder(ObjectRecord record)
        {
            switch (Types[record.TypeIndex].ClassID)
            {
                case AssetsConstants.ClassID.MonoScriptType:
                    return 0;
                default:
                    return 10000;
            }
        }

        private bool ShouldForceLoadObject(ObjectRecord record)
        {
            if (Types[record.TypeIndex].ClassID == AssetsConstants.ClassID.MonoScriptType)
                return true;
            return false;
        }

        public void Parse(AssetsReader reader)
        {
            Version = reader.ReadCStr();
            Platform = reader.ReadInt32();
            HasTypeTrees = reader.ReadBoolean();
            int numTypes = reader.ReadInt32();
            for (int i = 0; i < numTypes; i++)
            {
                Types.Add(new AssetsType(reader, HasTypeTrees));
            }

            List<ObjectRecord> records = new List<ObjectRecord>();
            int numObj = reader.ReadInt32();

            for (int i = 0; i < numObj; i++)
            {
                reader.AlignTo(4);
                var obj = new ObjectRecord(reader);
                records.Add(obj);
            }

            int numAdds = reader.ReadInt32();
            for (int i = 0; i < numAdds; i++)
            {
                reader.AlignTo(4);
                Adds.Add(new RawPtr(reader));
            }
            int numExt = reader.ReadInt32();
            for (int i = 0; i < numExt; i++)
            {
                ExternalFiles.Add(new ExternalFile(reader));
            }
            reader.ReadCStr();

            //load the object infos in order based on their type
            foreach (var record in records.OrderBy(x => PreloadObjectOrder(x)).ThenBy(x => x.ObjectID))
            {
                var obj = ObjectInfo<AssetsObject>.Parse(ParentFile, record);
                ObjectInfos.Add(obj);
                if (ShouldForceLoadObject(record))
                {
                    var o = obj.Object;
                }
            }
        }

        public void Write(AssetsWriter writer)
        {
            writer.WriteCString(Version);
            writer.Write(Platform);
            writer.Write(HasTypeTrees);
            writer.Write(Types.Count());
            Types.ForEach(x => x.Write(writer));
            writer.Write(ObjectInfos.Count());
            ObjectInfos.ForEach(x => {
                writer.AlignTo(4);
                x.Write(writer);
            });
            writer.Write(Adds.Count());
            Adds.ForEach(x => {
                writer.AlignTo(4);
                x.Write(writer);
            });
            writer.Write(ExternalFiles.Count());
            ExternalFiles.ForEach(x => x.Write(writer));
            writer.WriteCString("");
        }
    }
}
