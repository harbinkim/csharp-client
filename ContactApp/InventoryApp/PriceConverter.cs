using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace InventoryApp
{
    [ValueConversion(typeof(decimal),typeof(string))]
    class PriceConverter:IValueConverter
    {
        public object Convert(object val, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(val == null) && !String.IsNullOrEmpty(val.ToString()))
            {
                decimal price = (decimal)(double)val;
                return price.ToString("C");
            }
            else
                return null;
        }

        public object ConvertBack(object val, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string price = val.ToString();

            decimal result;
            if (Decimal.TryParse(price, NumberStyles.Any, null, out result))
            {
                return result;
            }
            return val;
        }

    }
}
