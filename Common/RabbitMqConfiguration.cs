using System;
using System.Linq;

namespace Common
{
    public class RabbitMqConfiguration
    {
        public string Hostname { get; set; }
        public ushort Port { get; set; }
        public string ClusterNodes { get; set; }
        public string[] ClusterNodesList
        {
            get
            {
                if (string.IsNullOrEmpty(ClusterNodes))
                    return new string[0];
                return ClusterNodes.Split(",").Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.ToLower().Trim())
                    .Distinct().ToArray();
            }
        }
        public string VirtualHost { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool UseTlsPolicy { get; set; }
        public bool IsTlsConnection { get; set; }
        public int HeartbeatInterval { get; set; }
        public string SchedulerQueue { get; set; }
    }
}
