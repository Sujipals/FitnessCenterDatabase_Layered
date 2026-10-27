using System;
using System.IO;

namespace FitnessCenterDatabase.Services
{
    public class LogService
    {
        private readonly string _logFilePath;

        public LogService(string logFilePath)
        {
            _logFilePath = logFilePath;

            string? directory =
                Path.GetDirectoryName(_logFilePath);

            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public void Log(
            string operation,
            string table,
            string details)
        {
            string dateTime =
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string logEntry =
                $"{dateTime} | {operation} | {table} | {details}";

            File.AppendAllText(
                _logFilePath,
                logEntry + Environment.NewLine);
        }
    }
}
