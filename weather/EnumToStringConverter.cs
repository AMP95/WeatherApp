using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using weatherTypeLib;

namespace weather
{
    class EnumToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is WindDirrection direction)
            {
                switch (direction)
                {
                    case WindDirrection.East: return "Восток";
                    case WindDirrection.West: return "Запад";
                    case WindDirrection.North: return "Север";
                    case WindDirrection.South: return "Юг";
                    case WindDirrection.NorthEast: return "Северо-Восток";
                    case WindDirrection.NorthWest: return "Северо-Запад";
                    case WindDirrection.SouthEast: return "Юго-Восток";
                    case WindDirrection.SouthWest: return "Юго-Запад";

                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
