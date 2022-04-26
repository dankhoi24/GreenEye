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
    class DecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            Debug.WriteLine("PARSE 3 " + value.ToString());
            if(value.ToString() == "-1")
            {
                return "Invalid";
            }
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.WriteLine("+__________________");
            Debug.WriteLine(value.ToString());
            decimal number;
            if (decimal.TryParse(value.ToString(), out number))
            {
                Debug.WriteLine("PARSE 1");
                return Decimal.Parse(value.ToString());
            }
            else
            {

                Debug.WriteLine("PARSE 2");
                return -1;
            }



        }
    }
}
