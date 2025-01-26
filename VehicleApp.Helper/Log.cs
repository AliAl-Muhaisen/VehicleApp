using System;
using System.IO;
using System.Reflection;

namespace VehicleApp.Helper
{
    public class Log
    {
        private static readonly string appLocation = AppContext.BaseDirectory; 
        private static readonly object lockObject = new object();

        private static string CreateFile(string fileName)
        {
            try
            {
                string logDirectory = Path.Combine(appLocation, "logs");
                Directory.CreateDirectory(logDirectory);

                string logFilePath = Path.Combine(logDirectory, $"{fileName}.txt");
                return logFilePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating log file: {ex.Message}");
                throw;
            }
        }

        public static void Error(Exception exception)
        {
            try
            {
                string logFilePath = CreateFile("Error.Log");

                // To ensure thread safety when writing to the log file
                lock (lockObject)
                {
                    using (StreamWriter streamWriter = new StreamWriter(logFilePath, true))
                    {
                        streamWriter.WriteLine("-----   Error   -----");
                        streamWriter.WriteLine($"DateTime: {DateTime.Now}");
                        streamWriter.WriteLine($"Type: {exception.GetType()}");
                        streamWriter.WriteLine($"Message: {exception.Message}");
                        streamWriter.WriteLine($"Stack Trace: {exception.StackTrace}");
                        streamWriter.WriteLine(new string('-', 50));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while logging exception: {ex.Message}");
            }
        }

        public static void Info(string infoMessage)
        {
            try
            {
                string logFilePath = CreateFile("Info.Log");

                // To ensure thread safety when writing to the log file
                lock (lockObject)
                {
                    using (StreamWriter streamWriter = new StreamWriter(logFilePath, true))
                    {
                        streamWriter.WriteLine("-----   Info   -----");
                        streamWriter.WriteLine($"DateTime: {DateTime.Now}");
                        streamWriter.WriteLine($"Message: {infoMessage}");
                        streamWriter.WriteLine(new string('-', 50));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while logging info: {ex.Message}");
            }
        }
    }
}
