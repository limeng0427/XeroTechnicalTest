using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    class DatabaseLogger : ILogger
    {
        public void Log(string message)
        {
            //const string connectionString = "mongodb://localhost:27017";

            // Create a MongoClient object by using the connection string
            //var client = new MongoClient(connectionString);

            //Use the MongoClient to access the server
            //var database = client.GetDatabase("SystemLog");

            //get mongodb collection
            //var collection = database.GetCollection<Log>("logs");
            //await collection.InsertOneAsync(new Log { DateTime = DateTime.UtcNow, Message = message }) ;
        }
    }

    public class LogManager 
    {
        private ILogger _logger;
        public LogManager(ILogger logger)
        {
            _logger = logger;
        }

        public LogManager()
        {
            string loggerSetting = "dev";
            switch (loggerSetting)
            {
                case "dev":
                    _logger = new ConsoleLogger();
                    break;
                case "prod":
                default:
                    _logger = new DatabaseLogger();
                    break;
            }
        }

        public void Log(string message)
        {
            _logger.Log(message);
        }
    }
}
