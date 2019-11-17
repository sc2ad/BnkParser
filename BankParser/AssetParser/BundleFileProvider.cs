using AssetParser.AssetsChanger;
using AssetParser.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AssetParser
{
    public class BundleFileProvider : IFileProvider
    {
        private FileStream _fileStream;
        private BundleFile _bundleFile;
        private bool ReadOnly;
        public bool UseCombinedStream { get; private set; }
        private string _bundleFilename;

        public BundleFileProvider(string bundleFile, bool readOnly = true, bool useCombinedStream = false)
        {
            _bundleFilename = bundleFile;
            ReadOnly = readOnly;
            _fileStream = File.Open(bundleFile, FileMode.Open, readOnly ? FileAccess.ReadWrite : FileAccess.Read);
            _bundleFile = new BundleFile(_fileStream);
            UseCombinedStream = useCombinedStream;
        }

        public string SourceName
        {
            get
            {
                return Path.GetFileName(_bundleFilename);
            }
        }

        private void CheckRO()
        {
            if (ReadOnly)
                throw new NotSupportedException("Cannot modify a read only file.");
        }

        public bool DirectoryExists(string path)
        {
            return _bundleFile.Entries.Any(x => x.Filename.StartsWith(path.TrimEnd('/') + "/"));
        }

        private static bool FilePatternMatch(string filename, string pattern)
        {
            Regex mask = new Regex(pattern.Replace(".", "[.]").Replace("*", ".*").Replace("?", "."));
            return mask.IsMatch(filename);
        }

        public bool FileExists(string filename)
        {
            return _bundleFile.Entries.Any(x => x.Filename.ToLower() == filename.ToLower());
        }

        public List<string> FindFiles(string pattern)
        {
            List<DirectoryEntry> found = new List<DirectoryEntry>();
            foreach (var entry in _bundleFile.Entries)
            {
                if (FilePatternMatch(entry.Filename.ToLower(), pattern.ToLower()))
                    found.Add(entry);
            }
            return found.Select(x => x.Filename).ToList();
        }

        private DirectoryEntry GetEntry(string filename)
        {
            var entry = _bundleFile.Entries.FirstOrDefault(x => x.Filename.ToLower() == filename.ToLower());
            if (entry == null)
                throw new FileNotFoundException($"{filename} was not found in the bundle.");
            return entry;
        }

        public long GetFileSize(string filename)
        {
            return GetEntry(filename).Size;
        }

        public Stream GetReadStream(string filename, bool bypassCache = false)
        {
            return new MemoryStream(GetEntry(filename).Data);
        }

        public byte[] Read(string filename)
        {
            return GetEntry(filename).Data;
        }

        public void RmRfDir(string path)
        {
            CheckRO();
            // Removes all files starting with path
            _bundleFile.Entries.RemoveAll((de) => de.Filename.StartsWith(path.TrimEnd('/') + "/"));
        }

        public void Delete(string filename)
        {
            CheckRO();
            // Deletes the first file matching
            _bundleFile.Entries.Remove(_bundleFile.Entries.FirstOrDefault((de) => de.Filename == filename));
        }

        public void DeleteFiles(string pattern)
        {
            CheckRO();
            // Remove all matching entries, they won't get saved to the stream as a result
            _bundleFile.Entries.RemoveAll((e) => FilePatternMatch(e.Filename.ToLower(), pattern.ToLower()));
        }

        public void Save(string toFile = null)
        {
            _fileStream.Close();
            FileStream saveStream = null;
            if (toFile == null)
                saveStream = File.Open(_bundleFilename, FileMode.OpenOrCreate, FileAccess.Write);
            if (toFile != null)
                saveStream = File.Open(toFile, FileMode.Open, FileAccess.Write);

            _bundleFile.Save(saveStream);
        }

        public void Write(string filename, byte[] data, bool overwrite = true, bool compressData = true)
        {
            CheckRO();
            DirectoryEntry existing = _bundleFile.Entries.FirstOrDefault(e => e.Filename == filename);
            if (!overwrite && existing != null)
            {
                throw new Exception($"An entry named {filename} already exists and overwrite is false");
            }
            if (existing != null)
            {
                // Assuming uncompressed data.Length == existing.Size
                existing.Size = data.Length;
                existing.Data = data;
                // We NEED to make sure we update all of our DirectoryEntry's offsets
            } else
            {
                _bundleFile.Entries.Add(new DirectoryEntry()
                {
                    Filename = filename,
                    Data = data,
                    Size = data.Length
                });
            }
        }

        public void WriteFile(string sourceFilename, string targetFilename, bool overwrite = true, bool compressData = true)
        {
            CheckRO();
            DirectoryEntry src = _bundleFile.Entries.FirstOrDefault(e => e.Filename == sourceFilename);
            if (src == null)
            {
                throw new Exception($"Could not find file {sourceFilename}");
            }
            DirectoryEntry dst = _bundleFile.Entries.FirstOrDefault(e => e.Filename == targetFilename);
            if (!overwrite && dst != null)
            {
                throw new Exception($"An entry named {targetFilename} already exists and overwrite is false");
            }
            if (dst != null)
            {
                // Assuming uncompressed data.Length == existing.Size
                dst.Size = src.Size;
                dst.Data = src.Data;
                // We NEED to make sure we update all of our DirectoryEntry's offsets
            } else
            {
                _bundleFile.Entries.Add(new DirectoryEntry()
                {
                    Filename = targetFilename,
                    Data = src.Data,
                    Size = src.Size
                });
            }
        }

        #region "Not implemented things"

        public void MkDir(string path, bool recursive = false)
        {
            throw new NotImplementedException("Cannot create directories in Bundle File!");
        }
        public Stream GetWriteStream(string filename)
        {
            throw new NotImplementedException("Cannot GetWriteStream of a filename of an asset bundle!");
        }

        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_fileStream != null)
                    {
                        _fileStream.Dispose();
                        _fileStream = null;
                    }
                    _bundleFile = null;
                }
                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
