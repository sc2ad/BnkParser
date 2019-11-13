﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.AssetsChanger.Assets
{
    public interface IObjectInfo<out T>
    {
        Int64 ObjectID { get; set; }
        AssetsFile ParentFile { get; }
        T Object { get; }
        Int32 DataOffset { get; set; }
        Int32 DataSize { get; set; }
        Int32 TypeIndex { get; }
        void Write(AssetsWriter writer);
        AssetsType Type { get; }
        bool IsNew { get; }
        T Clone(AssetsFile toFile = null);
        bool IsLoaded { get; }
        AssetsObject GetObjectForWrite();
        void FreeObject();
    }
    public class ObjectInfo<T> : IObjectInfo<T> where T : AssetsObject
    {
        public Int64 ObjectID { get; set; } = -1;
        public Int32 DataOffset { get; set; } = -1;
        public Int32 DataSize { get; set; } = -1;
        public Int32 TypeIndex { get; private set; } = -1;

        public bool IsNew
        {
            get
            {
                return (DataOffset < 0 || DataSize < 0);
            }
        }
        public AssetsFile ParentFile { get; set; }

        public AssetsType Type
        {
            get
            {
                return ParentFile.Metadata.Types[TypeIndex];
            }
        }

        public T Clone(AssetsFile toFile = null)
        {
            T newObj = null;
            using (var ms = new MemoryStream())
            {
                int typeIndex = TypeIndex;
                if (toFile != null)
                {
                    var type = ParentFile.Metadata.Types[typeIndex];
                    typeIndex = toFile.GetOrCreateMatchingTypeIndex(type);
                }
                //we do want to get the types from the new file
                ObjectInfo<T> newInfo = (ObjectInfo<T>)ObjectInfo<T>.FromTypeIndex(toFile ?? ParentFile, typeIndex, null);

                newInfo.DataOffset = 0;
                newInfo.ObjectID = 0;
                using (var writer = new AssetsWriter(ms))
                {
                    Object.Write(writer);
                }
                newInfo.DataSize = (int)ms.Length;
                ms.Seek(0, SeekOrigin.Begin);

                //parent file has to be set to properly to the original file to create pointers
                if (toFile != null)
                    newInfo.ParentFile = ParentFile;
                using (var reader = new AssetsReader(ms))
                {
                    newObj = (T)Activator.CreateInstance(typeof(T), newInfo, reader);
                }
                //set it back so that things moving forward think it's in the new file.
                if (toFile != null)
                    newInfo.ParentFile = toFile;

                newInfo.DataOffset = -1;
                newInfo.DataSize = -1;
                newInfo._object = newObj;
            }
            return (T)newObj;
        }

        private ObjectInfo()
        { }

        private ObjectInfo(Int64 objectID, Int32 dataOffset, Int32 dataSize, Int32 typeIndex, AssetsFile parentFile, T assetsObject)
        {
            ObjectID = ObjectID;
            DataOffset = dataOffset;
            DataSize = dataSize;
            TypeIndex = typeIndex;
            ParentFile = parentFile;
            _object = assetsObject;
        }
        internal static IObjectInfo<AssetsObject> Parse(AssetsFile file, ObjectRecord record)
        {
            var obji = FromTypeIndex(file, record.TypeIndex, null);
            obji.ObjectID = record.ObjectID;
            obji.DataOffset = record.DataOffset;
            obji.DataSize = record.DataSize;
            return obji;
        }

        public static IObjectInfo<AssetsObject> FromClassID(AssetsFile assetsFile, int classID, AssetsObject assetsObject)
        {
            var foundType = assetsFile.Metadata.Types.FirstOrDefault(x => x.ClassID == classID);
            if (foundType == null)
            {

                Log.LogMsg($"Type with class ID {classID} was not found in file {assetsFile.AssetsFilename}, it will be added.");
                if (classID == AssetsConstants.ClassID.MonoBehaviourScriptType || classID == AssetsConstants.ClassID.MonoScriptType)
                {
                    Log.LogErr("Monoscripts and Monobehaviours can't be created in files that don't already have them by using a class ID.");
                    throw new Exception("Class ID not found in file!");
                }
                assetsFile.Metadata.Types.Add(new AssetsType() { ClassID = classID });
            }
            var typeIndex = assetsFile.Metadata.Types.IndexOf(assetsFile.Metadata.Types.First(x => x.ClassID == classID));
            return FromTypeIndex(assetsFile, typeIndex, assetsObject);
        }
        public static IObjectInfo<AssetsObject> FromTypeHash(AssetsFile assetsFile, Guid typeHash, AssetsObject assetsObject)
        {
            var typeIndex = assetsFile.Metadata.Types.IndexOf(assetsFile.Metadata.Types.First(x => x.TypeHash == typeHash));
            return FromTypeIndex(assetsFile, typeIndex, assetsObject);
        }

        public static IObjectInfo<AssetsObject> FromTypeIndex(AssetsFile assetsFile, int typeIndex, AssetsObject assetsObject)
        {
            var type = GetObjectType(assetsFile, typeIndex);
            var genericInfoType = typeof(ObjectInfo<>).MakeGenericType(type);
            var constructor = genericInfoType.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(Int64), typeof(int), typeof(int), typeof(int), typeof(AssetsFile), type }, null);

            var genericOI = (IObjectInfo<AssetsObject>)constructor.Invoke(new object[] { (Int64)(-1), (int)-1, (int)-1, typeIndex, assetsFile, assetsObject });

            return genericOI;
        }

        private static Type GetObjectType(AssetsFile assetsFile, int typeIndex)
        {
            Type type = null;
            var objectType = assetsFile.Metadata.Types[typeIndex];
            switch (objectType.ClassID)
            {
                case AssetsConstants.ClassID.MonoBehaviourScriptType:
                    var found = assetsFile.Manager.GetScriptObject(objectType.TypeHash);

                    if (found != null && assetsFile.Manager.ClassNameToTypes.ContainsKey(found.ClassName))
                    {
                        Type assetObjectType = assetsFile.Manager.ClassNameToTypes[found.ClassName];
                        if (!assetObjectType.IsSubclassOf(typeof(MonoBehaviourObject)))
                        {
                            throw new ArgumentException("Types provided in scriptHashToTypes must be a subclass of AssetsMonoBehaviourObject.");
                        }
                        type = assetObjectType;
                    }
                    else
                    {
                        type = typeof(MonoBehaviourObject);
                    }
                    break;
                case AssetsConstants.ClassID.AudioClipClassID:
                    type = typeof(AudioClipObject);
                    break;
                case AssetsConstants.ClassID.Texture2DClassID:
                    type = typeof(Texture2DObject);
                    break;
                case AssetsConstants.ClassID.GameObjectClassID:
                    type = typeof(GameObject);
                    break;
                case AssetsConstants.ClassID.TextAssetClassID:
                    type = typeof(TextAsset);
                    break;
                case AssetsConstants.ClassID.SpriteClassID:
                    type = typeof(SpriteObject);
                    break;
                case AssetsConstants.ClassID.MonoScriptType:
                    type = typeof(MonoScriptObject);
                    break;
                default:
                    type = typeof(AssetsObject);
                    break;
            }
            return type;
        }

        public void Write(AssetsWriter writer)
        {
            writer.Write(ObjectID);
            writer.Write(DataOffset);
            writer.Write(DataSize);
            writer.Write(TypeIndex);
        }

        public bool IsLoaded
        {
            get
            {
                return _object != null;
            }
        }

        public void FreeObject()
        {
            _object = null;
        }

        /// <summary>
        /// If the object has already been loaded, it returns the typed, parsed object.  If the object has not yet been loaded, it returns a base AssetsObject with unparsed data so that it's faster to write and doesn't pull in all the pointer refs
        /// </summary>
        public AssetsObject GetObjectForWrite()
        {
            if (_object != null)
                return _object;

            lock (ParentFile)
            {
                using (var reader = ParentFile.GetReaderAtDataOffset())
                {
                    return new AssetsObject(this, reader);
                }
            }
        }

        private void LoadObject()
        {
            lock (ParentFile)
            {
                using (var reader = ParentFile.GetReaderAtDataOffset())
                {
                    _object = (T)Activator.CreateInstance(typeof(T), this, reader);
                }
            }
        }

        private T _object;
        public T Object
        {
            get
            {
                if (_object == null)
                {
                    if (DataOffset < 0 || DataSize < 0)
                    {
                        throw new Exception("Object is not set and DataOffset or DataSize is not set!");
                    }
                    else
                    {
                        LoadObject();
                    }
                }

                return _object;
            }
        }

    }
}
