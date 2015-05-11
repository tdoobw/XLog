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

        public void Trace(string message, Exception ex = null)
        {
            Log(LogLevel.Trace, message, ex);
        }

        public void Trace(string message, params object[] ps)
        {
            Log(LogLevel.Trace, message, ps);
        }

        public void Debug(string message, Exception ex = null)
        {
            Log(LogLevel.Debug, message, ex);
        }

        public void Debug(string message, object arg1)
        {
            Log(LogLevel.Debug, message, arg1, null);
        }

        public void Debug(string message, object arg1, object arg2)
        {
            Log(LogLevel.Debug, message, arg1, arg2, null);
        }

        public void Debug(string message, object arg1, object arg2, object arg3)
        {
            Log(LogLevel.Debug, message, arg1, arg2, arg3, null);
        }

        public void Debug(string message, params object[] ps)
        {
            Log(LogLevel.Debug, message, ps);
        }

        public void Info(string message, Exception ex = null)
        {
            Log(LogLevel.Info, message, ex);
        }

        public void Info(string message, params object[] ps)
        {
            Log(LogLevel.Info, message, ps);
        }

        public void Warn(string message, Exception ex = null)
        {
            Log(LogLevel.Warn, message, ex);
        }

        public void Warn(string message, params object[] ps)
        {
            Log(LogLevel.Warn, message, ps);
        }

        public void Error(string message, Exception ex = null)
        {
            Log(LogLevel.Error, message, ex);
        }

        public void Error(string message, params object[] ps)
        {
            Log(LogLevel.Error, message, ps);
        }

        public void Fatal(string message, Exception ex = null)
        {
            Log(LogLevel.Fatal, message, ex);
        }

        public void Fatal(string message, params object[] ps)
        {
            Log(LogLevel.Fatal, message, ps);
        }

        public void Log(LogLevel logLevel, string message, Exception ex = null)
        {
            LogInternal(logLevel, message, ex);
        }

        public void Log(LogLevel logLevel, string message, object arg1)
        {
            LogInternal(logLevel, string.Format(message, arg1), null);
        }

        public void Log(LogLevel logLevel, string message, object arg1, object arg2)
        {
            LogInternal(logLevel, string.Format(message, arg1, arg2), null);
        }

        public void Log(LogLevel logLevel, string message, object arg1, object arg2, object arg3)
        {
            LogInternal(logLevel, string.Format(message, arg1, arg2, arg3), null);
        }

        public void Log(LogLevel logLevel, string message, params object[] ps)
        {
            LogInternal(logLevel, string.Format(message, ps), null);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _config.Levels[(int)logLevel];
        }

        private void LogInternal(LogLevel logLevel, string message, Exception ex)
        {
            if (!_config.IsEnabled || !_config.Levels[(int)logLevel])
            {
                return;
            }

            var entry = new Entry(logLevel, Tag, message, ex);
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
