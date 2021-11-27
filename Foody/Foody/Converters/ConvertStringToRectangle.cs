using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Foody.Converters
{
    public class ConvertStringToRectangle : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string phrase = (string)value;
            string[] strings = phrase.Split(',');
            int[] array = Array.ConvertAll(strings, s => int.Parse(s));
            if(array.Length == 4)
            {
                return new Rectangle(array[0], array[1], array[2], array[3]);
            }
            else
            {
                return new Rectangle(0, 0, 0, 0);
            }
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
