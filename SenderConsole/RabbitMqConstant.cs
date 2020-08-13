using System.Collections.Generic;

namespace SenderConsole
{
    public class RabbitMqConstant
    {
        public const string HostName = "localhost";
        public const string VirtualHost = "dev";
        public const string Port = "15672";
        //public const string RabbitMqUri = "http://localhost:15672/";
        public const string UserName = "guest";
        public const string Password = "guest";
        public const string SimpleServiceQueue = "SimpleService";

        //public const List<string> ClusterNodesList = new List<string>();
    }
}
