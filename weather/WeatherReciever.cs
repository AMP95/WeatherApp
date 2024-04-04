using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TCPConnection;
using weatherTypeLib;

namespace weather
{
    class WeatherReciever
    {
        public event Action<Weather> Send;
        ClientConnection connection;
        public WeatherReciever(string ip, int port) {
            connection = new ClientConnection(ip,port);
        }
        public void Start() {
            while (connection.IsConnected()) {
                string message = connection.RecieveMessage();
                Weather weather = JsonSerializer.Deserialize<Weather>(message);
                Send?.Invoke(weather);
            }
        }
    }
}
