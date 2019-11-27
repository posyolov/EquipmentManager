using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace EquipmentManagerVM
{
    public class LongToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //object[] parameters = (object[])parameter;
            //long b = (long)value;
            //int bitNumber = int.Parse(parameters[0].ToString());
            //bool ret = (b & (1 << bitNumber)) > 0;
            //return ret;

            long b = (long)value;
            int bitNumber = int.Parse(parameter.ToString());
            bool ret = (b & (1 << bitNumber)) > 0;
            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //object[] parameters = (object[])parameter;
            //int bitNumber = int.Parse(parameters[0].ToString());
            //byte byteOrigVal = (byte)parameters[1];


            //byte ret;
            //if ((bool)value)
            //{
            //    ret = (byte)(byteOrigVal | (byte)Math.Pow(2, bitNumber));
            //}
            //else
            //{
            //    ret = BitOperations.ZeroBit(byteOrigVal, bitNumber);
            //}
            //return ret;
            return 0;
        }
    }
}
