
using System;
using System.IO;

namespace Stregsystemet
{
    public class StregsystemLogger : ILogger
    {
        private readonly string _filename;
        public StregsystemLogger()
        {
            _filename = "./log.txt";
            File.Create(_filename).Dispose();
            Info("Logging started");
        }

        public void Error(string message)
        {
            using (StreamWriter streamWriter = File.AppendText(_filename))
            {
                streamWriter.WriteLine($"ERROR:   {message} <{Time()}>");
            }
        }

        public void Fatal(string message)
        {
            using (StreamWriter streamWriter = File.AppendText(_filename))
            {
                streamWriter.WriteLine($"FATAL:   {message} <{Time()}>");
            }
        }

        public void Info(string message)
        {
            using (StreamWriter streamWriter = File.AppendText(_filename))
            {
                streamWriter.WriteLine($"INFO:    {message} <{Time()}>");
            }
        }

        public void Warn(string message)
        {
            using (StreamWriter streamWriter = File.AppendText(_filename))
            {
                streamWriter.WriteLine($"WARNING: {message} <{Time()}>");
            }
        }

        private string Time()
        {
            return DateTime.Now.ToString();
        }
    }
}