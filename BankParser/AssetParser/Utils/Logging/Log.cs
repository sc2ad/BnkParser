﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.Utils.Logging
{
    public static class Log
    {
        private static List<ILog> _logSinks = new List<ILog>();


        public static void ClearLogSinks()
        {
            _logSinks.Clear();
        }

        public static void SetLogSink(ILog logSink)
        {
            _logSinks.Add(logSink);
        }

        public static void ClearSinksOfType<T>(Func<T, bool> filter) where T : class, ILog
        {
            var sinks = _logSinks.Where(x => x as T != null && filter.Invoke(x as T)).ToList();
            _logSinks.RemoveAll(x => sinks.Contains(x));
            foreach (var sink in sinks)
            {
                var disp = sink as IDisposable;
                if (disp != null)
                    disp.Dispose();
            }
        }

        public static void RemoveLogSink(ILog logSink)
        {
            if (_logSinks.Contains(logSink))
                _logSinks.Remove(logSink);
        }

        public static void LogMsg(string message, params object[] args)
        {
            _logSinks.ForEach(x =>
            {
                try
                { x.LogMsg($"MSG: {message}", args); }
                catch { }
            });
        }

        public static void LogErr(string message, Exception ex)
        {
            _logSinks.ForEach(x =>
            {
                try
                {
                    if (ex != null)
                        x.LogErr($"ERR: {message}", ex);
                    else
                        x.LogErr($"ERR: {message}");
                }
                catch { }
            });
        }

        public static void LogErr(string message)
        {
            _logSinks.ForEach(x =>
            {
                try
                { x.LogErr($"ERR: {message}"); }
                catch { }
            });
        }
    }

    public class ConsoleSink : ILog
    {
        public void LogErr(string message, Exception ex)
        {
            Console.WriteLine($"{message} ({ex.Message} {ex.StackTrace}");
        }

        public void LogErr(string message, params object[] args)
        {
            Console.WriteLine(message, args);
        }

        public void LogMsg(string message, params object[] args)
        {
            Console.WriteLine(message, args);
        }
    }
}
