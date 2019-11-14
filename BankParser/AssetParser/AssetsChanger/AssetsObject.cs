﻿using AssetParser.AssetsChanger.Assets;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.AssetsChanger
{
    public class AssetsObject
    {
        [JsonIgnore]
        public virtual byte[] Data { get; set; }

        [JsonIgnore]
        public IObjectInfo<AssetsObject> ObjectInfo { get; set; }

        public AssetsObject()
        { }

        public AssetsObject(AssetsFile assetsFile, int classID)
        {

            ObjectInfo = ObjectInfo<AssetsObject>.FromClassID(assetsFile, classID, this);

        }

        public AssetsObject(AssetsFile assetsFile, Guid typeHash)
        {
            ObjectInfo = ObjectInfo<AssetsObject>.FromTypeHash(assetsFile, typeHash, this);
        }

        protected AssetsObject(IObjectInfo<AssetsObject> objectInfo)
        {
            ObjectInfo = objectInfo;
        }

        public AssetsObject(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseObject = false)
        {
            ObjectInfo = objectInfo;
            if (parseObject)
            {
                ParseObject(reader);
            }
            else
            {
                Parse(reader);
            }
        }

        protected virtual void ParseBase(AssetsReader reader)
        {
            reader.Seek(ObjectInfo.DataOffset);
        }

        public void Parse(AssetsReader reader)
        {
            ParseBase(reader);
            ParseObject(reader);
        }

        public virtual void ParseObject(AssetsReader reader)
        {
            Data = reader.ReadBytes(ObjectInfo.DataSize);
        }

        protected virtual void WriteBase(AssetsWriter writer)
        {

        }

        public void Write(AssetsWriter writer)
        {
            WriteBase(writer);
            WriteObject(writer);
        }

        protected virtual void WriteObject(AssetsWriter writer)
        {
            writer.Write(Data);
        }

        public int GetSize()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (AssetsWriter writer = new AssetsWriter(ms))
                {
                    WriteObject(writer);
                }
                return (int)ms.Length;
            }
        }
    }
}
