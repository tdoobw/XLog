﻿using System;

namespace XLog
{
    public class Logger
    {
        private readonly LogConfig _config;
        public readonly string Tag;

        internal Logger(string tag, LogConfig config)
        {
            Tag = tag;
            _config = config;
        }

        public void Trace(string message)
        {
            Log(LogLevel.Trace, message, 0L, null);
        }

        public void Trace(string message, long category)
        {
            Log(LogLevel.Trace, message, category, null);
        }

        public void Trace(string message, Exception ex)
        {
            Log(LogLevel.Trace, message, 0L, ex);
        }

        public void Trace(string message, long category, Exception ex)
        {
            Log(LogLevel.Trace, message, category, ex);
        }

        public void Debug(string message)
        {
            Log(LogLevel.Debug, message, 0L, null);
        }

        public void Debug(string message, long category)
        {
            Log(LogLevel.Debug, message, category, null);
        }

        public void Debug(string message, Exception ex)
        {
            Log(LogLevel.Debug, message, 0L, ex);
        }

        public void Debug(string message, long category, Exception ex)
        {
            Log(LogLevel.Debug, message, category, ex);
        }

        public void Info(string message)
        {
            Log(LogLevel.Info, message, 0L, null);
        }

        public void Info(string message, long category)
        {
            Log(LogLevel.Info, message, category, null);
        }

        public void Info(string message, Exception ex)
        {
            Log(LogLevel.Info, message, 0L, ex);
        }

        public void Info(string message, long category, Exception ex)
        {
            Log(LogLevel.Info, message, category, ex);
        }

        public void Warn(string message)
        {
            Log(LogLevel.Warn, message, 0L, null);
        }

        public void Warn(string message, long category)
        {
            Log(LogLevel.Warn, message, category, null);
        }

        public void Warn(string message, Exception ex)
        {
            Log(LogLevel.Warn, message, 0L, ex);
        }

        public void Warn(string message, long category, Exception ex)
        {
            Log(LogLevel.Warn, message, category, ex);
        }

        public void Error(string message)
        {
            Log(LogLevel.Error, message, 0L, null);
        }

        public void Error(string message, long category)
        {
            Log(LogLevel.Error, message, category, null);
        }

        public void Error(string message, Exception ex)
        {
            Log(LogLevel.Error, message, 0L, ex);
        }

        public void Error(string message, long category, Exception ex)
        {
            Log(LogLevel.Error, message, category, ex);
        }

        public void Fatal(string message)
        {
            Log(LogLevel.Fatal, message, 0L, null);
        }

        public void Fatal(string message, long category)
        {
            Log(LogLevel.Fatal, message, category, null);
        }

        public void Fatal(string message, Exception ex)
        {
            Log(LogLevel.Fatal, message, 0L, ex);
        }

        public void Fatal(string message, long category, Exception ex)
        {
            Log(LogLevel.Fatal, message, category, ex);
        }

        public void Log(LogLevel logLevel, string message, long category = 0, Exception ex = null)
        {
            LogInternal(logLevel, message, category, ex);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _config.Levels[(int)logLevel];
        }

        private void LogInternal(LogLevel logLevel, string message, long category, Exception ex)
        {
            if (!_config.IsEnabled || !_config.Levels[(int)logLevel])
            {
                return;
            }

            var entry = new Entry(logLevel, Tag, message, category, ex);
            for (int index = 0; index < _config.TargetConfigs.Length; index++)
            {
                var c = _config.TargetConfigs[index];
                if (c.SupportsLevel(logLevel))
                {
                    try
                    {
                        c.Target.Write(entry, _config.Formatter);
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine("Target write failed. --> {0}", e);
                    }
                }
            }
        }
    }
}
