using Communication.FastProtocol.Read;
using ExpertAdvisors.Abstract;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Sockets;

namespace ExpertAdvisors._03_Security
{
    public class Se_MetaTrader : AbstractSecurity
    {
        // Attribute
        private IPAddress LocalIP;
        private readonly static IPAddress LocalHostIPv4 = IPAddress.Parse("127.0.0.1");
        private readonly static IPAddress LocalHostIPv6 = IPAddress.Parse("::1");

        // Konstruktor
        public Se_MetaTrader()
        {
            LocalIP = CollectLocalIP();
        }

        // Funktionen

        private IPAddress CollectLocalIP()
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                return endPoint.Address;
            }
        }

        protected override string SecurityStrategy()
        {
            throw new System.NotImplementedException();
        }

        protected override bool Validate(HttpContext httpContext)
        {
            // Nur Nachrichten akzeptieren die lokal abgesendet wurden            
            IPAddress remoteAdd = httpContext.Connection.RemoteIpAddress;
            return (remoteAdd.Equals(LocalIP) || remoteAdd.Equals(LocalHostIPv4) || remoteAdd.Equals(LocalHostIPv6));
        }
    }
}
