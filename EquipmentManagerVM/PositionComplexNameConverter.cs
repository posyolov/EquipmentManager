using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace EquipmentManagerVM
{
    /// <summary>
    /// Конвертер для форматирования по разделителям комлексного имени позиции
    /// </summary>
    public class PositionComplexNameConverter : IValueConverter
    {
        char[] formatChars = {'.', '-' };
        const char SEPARATOR = ';';

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            StringBuilder str = new StringBuilder((string)value);
            int j = 0;

            for(int i = 0; i < str.Length && j < formatChars.Length; i++)
                if(str[i] == SEPARATOR)
                {
                    str[i] = formatChars[j];
                    j++;
                }

            return str.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
