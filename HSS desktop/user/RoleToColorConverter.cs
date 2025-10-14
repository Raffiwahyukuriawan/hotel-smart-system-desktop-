using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace HSS_desktop.user
{
    public class RoleToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string role = value?.ToString()?.ToLower();

            return role switch
            {
                "admin" => Brushes.Red,
                "tamu" => Brushes.Green,
                "resepsionis" => Brushes.Blue,
                _ => Brushes.Gray
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
