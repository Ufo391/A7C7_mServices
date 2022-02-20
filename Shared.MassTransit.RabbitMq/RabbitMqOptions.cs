using System.Reflection;
using System.Linq;
using MassTransit;

namespace Shared.RabbitMq
{
    [Serializable]
    public class RabbitMqOptions
    {
        public string Host { get; set; } = "localhost";
        public ushort Port { get; set; } = 5672;
        public string VHost { get; set; } = "/";
        public string Username { get; set; } = "guest";
        public string Password { get; set; } = "guest";
        public string ApplicationName { get; set; } = string.Empty;

        public RabbitMqOptions()
        {
            ApplicationName = NewId.NextGuid().ToString();
        }

        public RabbitMqOptions(string connectionStr)
        {
            ReadConnectionString(connectionStr);
            if (string.IsNullOrEmpty(ApplicationName))
            {
                ApplicationName = NewId.NextGuid().ToString();
            }
        }

        public RabbitMqOptions(string connectionStr, string applicationName)
        {
            ReadConnectionString(connectionStr);
            if (string.IsNullOrEmpty(ApplicationName))
            {
                ApplicationName = applicationName;
            }
        }

        public RabbitMqOptions(string connectionStr, Assembly executingAssembly)
        {
            ReadConnectionString(connectionStr);
            if (string.IsNullOrEmpty(ApplicationName))
            {
                ApplicationName = executingAssembly?.GetName().Name ?? string.Empty;
            }
        }

        private void ReadConnectionString(string connectionStr)
        {
            var parts = connectionStr.Split(';').Select(x => x.Trim()).Where(x => !string.IsNullOrEmpty(x));
            foreach(var part in parts)
            {
                foreach(var prop in GetType().GetProperties())
                {
                    if (part.StartsWith(prop.Name))
                    {
                        if (prop.Name.Equals("Port"))
                        {
                            prop.SetValue(this, Convert.ToUInt16(part.Replace(prop.Name + '=', string.Empty)));
                        }
                        else
                        {
                           prop.SetValue(this, part.Replace(prop.Name + '=', string.Empty));
                        }
                    }
                }
            }
        }
    }
}
