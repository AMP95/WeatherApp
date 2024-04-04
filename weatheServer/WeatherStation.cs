using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TCPConnection;
using weatherTypeLib;

namespace weatheServer
{
    public class WeatherStation : IServerWorker
    {
        StreamReader _reader;
        StreamWriter _writer;
        Random random;
        string message;
        public void StartWork(TcpClient _tcpClient)
        {
            _reader = new StreamReader(_tcpClient.GetStream());
            _writer = new StreamWriter(_tcpClient.GetStream());
            while (_tcpClient.Connected) {
                random = new Random();
                var type = (WeatherType)random.Next(0, 5);
                var temperature = random.Next(0, 40);
                if (type == WeatherType.Snow)
                    temperature = random.Next(-40, 0);
                int minHum = 30;
                if (type == WeatherType.Rain || type == WeatherType.Storm)
                    minHum = 70;
                int minWind = 0;
                if (type == WeatherType.Storm)
                    minWind = 10;
                Weather weather = new Weather() {
                    WeatherType = type,
                    Temperature = temperature,
                    Pressure = random.Next(750, 770),
                    Humidity = random.Next(minHum, 100),
                    WindSpeed = random.Next(minWind,20),
                    WindDirrection = (WindDirrection)random.Next(0, 8)
                };
                _writer.WriteLine(weather.ToString());
                try
                {
                    _writer.Flush();
                }
                catch {
                    _tcpClient.Close();
                    _tcpClient.Dispose();
                    break;
                }
                Thread.Sleep(10000);
            }
        }
    }

    public class WeatheStationPool : ServerWokersPool {
        public WeatheStationPool(int count) : base(count) {
            _workers = new System.Collections.Concurrent.ConcurrentQueue<IServerWorker>();
            for (int i = 0; i < ClientsCount; i++)
            {
                _workers.Enqueue(new WeatherStation());
            }
        }
    }
}
