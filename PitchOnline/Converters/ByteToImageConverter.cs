using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace PitchOnline
{
    public class ByteToImageConverter : BaseValueConverter<ByteToImageConverter>
    {

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BitmapImage img = new BitmapImage();
            using (MemoryStream memStream = new MemoryStream(value as byte[]))
            {
                img.StreamSource = memStream;
            }
            return img;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
