using System.Reflection;
using MassTransit;

namespace TestPackages.MassTransit.RabbitMq
{
    [Serializable]
    public class RabbitMqOptions
    {
        public RabbitMqOptions()
        {
            ApplicationName = NewId.NextGuid().ToString();
        }

        public RabbitMqOptions(string connectionString)
        {
            ReadConnectionString(connectionString);

            if (string.IsNullOrEmpty(ApplicationName))
            {
                ApplicationName = NewId.NextGuid().ToString();
            }
        }

        public RabbitMqOptions(string connectionString, string applicationName)
        {
            ReadConnectionString(connectionString);

            if (string.IsNullOrEmpty(ApplicationName))
            {
                ApplicationName = applicationName;
            }
        }

        public RabbitMqOptions(string connectionString, Assembly executingAssembly)
        {
            ReadConnectionString(connectionString);

            if (string.IsNullOrEmpty(ApplicationName))
            {
                ApplicationName = executingAssembly?.GetName().Name ?? string.Empty;
            }
        }

        public string Host { get; set; } = "localhost";
        public ushort Port { get; set; } = 5672;
        public string VHost { get; set; } = "/";
        public string Username { get; set; } = "guest";
        public string Password { get; set; } = "guest";
        public string ApplicationName { get; set; } = string.Empty;

        private void ReadConnectionString(string connectionString)
        {
            var parts = connectionString
                            .Split(';')
                            .Select(x => x.Trim())
                            .Where(x => !string.IsNullOrEmpty(x));

            foreach (var part in parts)
            {
                foreach (var property in GetType().GetProperties())
                {
                    if (part.StartsWith(property.Name))
                    {
                        if (property.Name == "Port")
                            property.SetValue(this, Convert.ToUInt16(part.Replace(property.Name + '=', string.Empty)));
                        else
                            property.SetValue(this, part.Replace(property.Name + '=', string.Empty));

                        break;
                    }
                }
            }
        }
    }
}