using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GreenEye.ViewModel.Converter
{
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string workingDirectory = Environment.CurrentDirectory;

            Debug.WriteLine("CONVERTER");
            Debug.WriteLine(workingDirectory + @"\img\store\" + value);
            return workingDirectory + @"\img\store\" + value;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.WriteLine("CONVERTER BACK");
            return value;

        }
    }
}
