using System.Collections.Concurrent;
using Register.Models;

namespace Register.Data
{
    public class SharedDb
    {
        private readonly ConcurrentDictionary<string, UserConnections> _connections =  new ();
        public ConcurrentDictionary<string, UserConnections> connections => _connections;
    }
}
