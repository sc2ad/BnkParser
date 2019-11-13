using AssetParser.AssetsChanger;
using AssetParser.AssetsChanger.Assets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.Utils
{
    public static class Extensions
    {
        public static ISmartPtr<T> PtrFrom<T>(this T assetObject, AssetsObject owner) where T : AssetsObject
        {
            return new SmartPtr<T>(owner, assetObject);
        }

        public static ISmartPtr<T> PtrFrom<T>(this IObjectInfo<T> objectInfo, AssetsObject owner) where T : AssetsObject
        {
            return new SmartPtr<T>(owner, objectInfo);
        }

        public static RawPtr ToRawPtr<T>(this ISmartPtr<T> ptr) where T : AssetsObject
        {
            return new RawPtr(ptr.FileID, ptr.PathID);
        }

        public static Stream ToStream(this byte[] bytes)
        {
            return new MemoryStream(bytes);
        }

        public static byte[] ReadBytes(this Stream stream, int count)
        {
            byte[] bytes = new byte[count];
            stream.Read(bytes, 0, count);
            return bytes;
        }

        public static void Write<T>(this ISmartPtr<T> ptr, AssetsWriter writer) where T : AssetsObject
        {
            if (ptr == null)
            {
                writer.Write((Int32)0);
                writer.Write((Int64)0);
            }
            else
            {
                ptr.WritePtr(writer);
            }
        }

        public static Array RemoveAt(this Array source, int index)
        {
            char[] b = new char[50];
            var dest = Array.CreateInstance(source.GetType().GetElementType(), source.Length - 1);

            if (index > 0)
                Array.Copy(source, 0, dest, 0, index);

            if (index < source.Length - 1)
                Array.Copy(source, index + 1, dest, index, source.Length - index - 1);

            return dest;
        }

        public static string GetDirectoryFwdSlash(this string path)
        {
            if (!path.Contains("/"))
                return "";

            return path.Substring(0, path.LastIndexOf('/'));
        }

        public static string FullMessage(this Exception exception)
        {
            if (exception == null)
                return "(Exception was null!)";

            string exmsg = "";
            exmsg = $"{exception.Message} {exception.StackTrace}";
            var ex = exception.InnerException;
            while (ex != null)
            {
                exmsg += $"\nInnerException: {ex.Message} {ex.StackTrace}";
                ex = ex.InnerException;
            }
            return exmsg;
        }

        public static string GetFilenameFwdSlash(this string path)
        {
            if (path == "/")
                return "";

            if (!path.Contains("/"))
                return path;

            if (path.EndsWith("/"))
                path = path.TrimEnd('/');

            int idx = path.LastIndexOf('/') + 1;

            return path.Substring(idx, path.Length - idx);
        }

        public static string CombineFwdSlash(this string path1, string path2)
        {
            if (path1 == null || path2 == null)
                return null;

            if (path1.EndsWith("/") && path1.Length > 1)
                path1 = path1.TrimEnd('/');

            path2 = path2.TrimStart('/');

            if (string.IsNullOrWhiteSpace(path1))
                return path2;

            if (string.IsNullOrWhiteSpace(path2))
                throw new ArgumentException("Path 2 is required!");

            return path1 + "/" + path2;
        }

        public static string ReadToString(this IFileProvider provider, string filename)
        {
            var data = provider.Read(filename);
            return System.Text.Encoding.UTF8.GetString(data);
        }

        public static byte[] ToPngBytes(this Texture2DObject texture)
        {
            return ImageUtils.Instance.TextureToPngBytes(texture);
        }

        public static string RemoveSuffix(this string value, string suffix)
        {
            if (!value.EndsWith(suffix))
                return value;
            //don't remove it if that's all there is
            if (value == suffix)
                return value;

            return value.Substring(0, value.Length - suffix.Length);
        }

        private static string FindFirstOfSplit(IFileProvider fp, string assetsFile)
        {
            int lastDot = assetsFile.LastIndexOf('.');
            if (lastDot > 0)
            {
                string afterDot = assetsFile.Substring(lastDot, assetsFile.Length - lastDot);
                string noSplit;
                if (afterDot.ToLower().StartsWith(".split"))
                {
                    noSplit = assetsFile.Substring(0, lastDot);
                    if (fp.FileExists(noSplit))
                        return noSplit;
                }
                else
                {
                    noSplit = assetsFile;
                }
                if (fp.FileExists(noSplit))
                    return noSplit;

                var split0 = noSplit + ".split0";
                if (fp.FileExists(split0))
                    return split0;
            }
            else if (fp.FileExists(assetsFile))
            {
                return assetsFile;
            }
            else if (fp.FileExists(assetsFile + ".split0"))
            {
                return assetsFile + ".split0";
            }

            return null;
        }

        public static string CorrectAssetFilename(this IFileProvider fp, string assetsFile)
        {
            var correctName = FindFirstOfSplit(fp, assetsFile);
            if (correctName != null)
                return correctName;

            //some of the files in ExternalFiles have library/ on them, but they're actually in Resources/
            if (assetsFile.Contains("library/"))
            {
                string whyUnity = assetsFile.Replace("library/", "Resources/");
                correctName = FindFirstOfSplit(fp, whyUnity);
                if (correctName != null)
                    return correctName;
            }

            //some of the files in ExternalFiles have library/ on them, but they're actually in the root path
            var splitPath = assetsFile.Split('/').ToList();
            if (splitPath.Count() > 1)
            {
                splitPath.RemoveAt(splitPath.Count - 2);
                correctName = String.Join("/", splitPath);
                correctName = FindFirstOfSplit(fp, correctName);
                if (correctName != null)
                    return correctName;
            }

            throw new ArgumentException($"The assets file {assetsFile} doesn't exist in with any known name variations!");
        }

        public static Stream ReadCombinedAssets(this IFileProvider fp, string assetsFilePath, out bool wasCombined)
        {
            string actualName = fp.CorrectAssetFilename(assetsFilePath);

            List<string> assetFiles = new List<string>();
            if (actualName.ToLower().EndsWith("split0"))
            {
                assetFiles.AddRange(fp.FindFiles(actualName.Replace(".split0", ".split*"))
                    .OrderBy(x => Convert.ToInt32(x.Split(new string[] { ".split" }, StringSplitOptions.None).Last())));
            }
            else
            {
                wasCombined = false;
                return fp.GetReadStream(actualName);
            }
            wasCombined = true;
            //TODO: property or something on the file provider interface letting this code know if it should use the combined stream
            //      I think combined stream may perform horribly on zip files or cause other issues.
            return new CombinedStream(assetFiles, fp);
        }
    }
}
