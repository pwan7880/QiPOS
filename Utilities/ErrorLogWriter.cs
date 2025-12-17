using System;
using System.IO;

namespace QiPOS
{
    public sealed class ErrorLogWriter
    {
        private static readonly Lazy<ErrorLogWriter> _instance = new(() => new ErrorLogWriter());
        private readonly string logPath;
        private readonly object fileLock = new();

        public static ErrorLogWriter Instance => _instance.Value;

        private ErrorLogWriter()
        {
            string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            logPath = Path.Combine(dir, $"error_{DateTime.Now:yyyyMMdd}.log");
        }

        public void Log(string message)
        {
            string entry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
            lock (fileLock)
            {
                File.AppendAllText(logPath, entry + Environment.NewLine);
            }
        }

        public void Log(Exception ex, string context = null)
        {
            string entry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]";
            if (!string.IsNullOrEmpty(context))
                entry += $" [{context}]";

            entry += Environment.NewLine + ex.ToString();

            lock (fileLock)
            {
                File.AppendAllText(logPath, entry + Environment.NewLine + Environment.NewLine);
            }
        }
    }
}
