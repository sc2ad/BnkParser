using AssetParser.AssetsChanger;
using AssetParser.PistolWhipAssets;
using AssetParser.Utils;
using AssetParser.Utils.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AssetParser
{
    public class AssetsEngine : IDisposable
    {
        public bool HasChanges
        {
            get
            {
                return Manager.HasChanges;
            }
        }
        private List<string> _assetsLoadOrder = new List<string>();
        private AssetsManager _manager;
        public AssetsManager Manager { get => _manager; }
        public IFileProvider FileProvider
        {
            get
            {
                return _config.RootFileProvider;
            }
        }
        private AssetsConfig _config;
        internal AssetsConfig Config { get => _config; }

        /// <summary>
        /// Create a new instance of the class and open the apk file
        /// </summary>
        public AssetsEngine(AssetsConfig config, string version)
        {
            _config = config;
            _assetsLoadOrder = GetAssetsLoadOrderFile();
            if (_assetsLoadOrder == null)
            {
                _assetsLoadOrder = new List<string>()
                {
                    "globalgamemanagers.assets",
                    "sharedassets0.assets",
                };
            }
            Stopwatch sw = new Stopwatch();
            _manager = new AssetsManager(_config.RootFileProvider, _config.AssetsPath, PistolWhipConst.GetAssetTypeMap(), version);
            Log.LogMsg("Preloading files...");
            sw.Start();
            PreloadFiles();
            sw.Stop();
            Log.LogMsg($"Preload files took {sw.ElapsedMilliseconds}ms");
        }

        public PistolWhipConfig GetCurrentConfig()
        {
            var config = GetConfig();

            return config;
        }

        //for some reason, I think this is a bad idea.
        public bool IsManagerLocked()
        {
            bool entered = Monitor.TryEnter(_manager);
            if (entered)
                Monitor.Exit(_manager);
            return !entered;
        }

        public void Save()
        {
            lock (this)
            {
                if (!Monitor.TryEnter(_manager))
                    throw new Exception("Other operations are in progress, cannot save now.");

                Stopwatch sw = new Stopwatch();
                try
                {
                    Log.LogMsg("Serializing all assets...");
                    sw.Restart();
                    _manager.WriteAllOpenAssets();
                    sw.Stop();
                    Log.LogMsg($"Serialization of assets took {sw.ElapsedMilliseconds}ms");
                    // We already call FileProvider.Save since this is an asset bundle!
                    //Log.LogMsg("Making sure everything is saved...");
                    //FileProvider.Save();
                }
                catch (Exception ex)
                {
                    Log.LogErr("Exception saving assets!", ex);
                    throw new Exception("Failed to save assets!", ex);
                }
                finally
                {
                    Monitor.Exit(_manager);
                }
            }
        }

        private AssetsFile _songsAssetsFileCache;
        internal AssetsFile GetSongsAssetsFile()
        {
            if (_songsAssetsFileCache == null)
            {
                var extrasPack = _manager.MassFirstOrDefaultAsset<LevelData>(x => true, false);
                if (extrasPack == null)
                    throw new Exception("Unable to find the file that ExtrasLevelPack is in!");
                _songsAssetsFileCache = extrasPack.ParentFile;
            }
            return _songsAssetsFileCache;
        }

        private PistolWhipConfig GetConfig()
        {
            Stopwatch sw = new Stopwatch();
            try
            {
                sw.Start();
                lock (this)
                {
                    PistolWhipConfig config = new PistolWhipConfig();

                    return config;
                }
            }
            finally
            {
                sw.Stop();
                Log.LogMsg($"Loading config took {sw.ElapsedMilliseconds}ms");
            }
        }

        private void PreloadFiles()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            _assetsLoadOrder.ForEach(x => _manager.GetAssetsFile(x));
            sw.Stop();
            Log.LogMsg($"Preloading files took {sw.ElapsedMilliseconds}ms");
        }

        private List<string> GetAssetsLoadOrderFile()
        {
            string filename = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "assetsLoadOrder.json");
            if (!File.Exists(filename))
            {
                Log.LogErr($"Can't find {filename}!  Default assets load order will be used.");
                return null;
            }
            List<string> loadOrder = new List<string>();
            try
            {
                using (var jr = new JsonTextReader(new StreamReader(filename)))
                    loadOrder = new JsonSerializer().Deserialize<List<string>>(jr);
            }
            catch (Exception ex)
            {
                Log.LogErr($"Error loading {filename}!  Default assets load order will be used.", ex);
                return null;
            }
            if (loadOrder == null || loadOrder.Count < 1)
                return null;

            return loadOrder;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _manager.Dispose();
                    _manager = null;
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
