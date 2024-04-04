using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weatherTypeLib;
using System.ComponentModel;

namespace weather
{
    class ViewModel :INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        WeatherReciever reciever;
        Weather _weather;
        string _image;
        public Weather Weather
        {
            get { return _weather; }
            set { _weather = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Weather"));
            }
        }
        public string WeatherImage {
            get => _image;
            set {
                _image = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WeatherImage"));
            }
        }
        public ViewModel() {
            reciever = new WeatherReciever("127.0.0.1", 888);
            reciever.Send += Recieve;
            Task.Run(()=> { reciever.Start(); });
        }
        private void Recieve(Weather weather) {
            Weather = weather;
            WeatherImage = $"images/{(int)weather.WeatherType}.png";
        }
    }
}
