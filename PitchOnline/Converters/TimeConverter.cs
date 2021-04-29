using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PitchOnline
{
    public class TimeConverter : BaseValueConverter<TimeConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            TimeSpan timeSpan = (TimeSpan)value;
            if (timeSpan.Hours > 0)
                return string.Format("{0:D2}:{1:D2}:{2:D2}",
                        timeSpan.Hours,
                        timeSpan.Minutes,
                        timeSpan.Seconds);
            else
                return string.Format("{0:D2}:{1:D2}",
                        timeSpan.Minutes,
                        timeSpan.Seconds);
        }


        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
