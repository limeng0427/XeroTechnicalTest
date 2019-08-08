using MongoDB.Driver;
using System;
using System.Configuration;

namespace XeroTechnicalTestLibrary
{
    interface ILogger
    {
        void Log(string message);
    }

    class ConsoleLogger: ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"{DateTime.Now}: {message}");
            System.Diagnostics.Trace.WriteLine(message);
        }
    }

    public class Log
    {
       public DateTime DateTime { get; set;}
       public string Message { get; set; }
    }

    class DatabaseLogger : ILogger
    {
        public void Log(string message)
        {
            string connectionString = ConfigurationManager.AppSettings["logdbconnection"];

            //Create a MongoClient object by using the connection string
            var client = new MongoClient(connectionString);

            //Use the MongoClient to access the server
            var database = client.GetDatabase("SystemLog");

            //get mongodb collection
            var collection = database.GetCollection<Log>("logs");
            collection.InsertOneAsync(new Log { DateTime = DateTime.UtcNow, Message = message });
        }

    }

    public class LogManager 
    {
        private ILogger _logger;

        public LogManager()
        {
            string loggerSetting = GetLogMode();

            switch (loggerSetting)
            {
                case "prod":
                    _logger = new DatabaseLogger();
                    break;
                case "dev":
                default:
                    _logger = new ConsoleLogger();
                    break;
            }
        }

        private string GetLogMode()
        {
            var loggerSetting = "dev";
            // get config from app.config file
            //if (Utility.ConfigurationManagerHelper.Exists())
            //    loggerSetting = ConfigurationManager.AppSettings["logmode"];
            return loggerSetting;
        }

        public void Log(string message)
        {
            _logger.Log(message);
        }
    }
}
