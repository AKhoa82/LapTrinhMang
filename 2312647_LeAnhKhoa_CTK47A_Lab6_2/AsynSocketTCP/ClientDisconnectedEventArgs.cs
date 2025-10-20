using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncSocketTCP
{
    public class ClientDisconnectedEventArgs
    {
        public string ClientInfo { get; }
        public int RemainingClients { get; }

        public ClientDisconnectedEventArgs(string clientInfo, int remainingClients)
        {
            ClientInfo = clientInfo;
            RemainingClients = remainingClients;
        }
    }
}
