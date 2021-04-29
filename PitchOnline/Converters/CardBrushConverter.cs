using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PitchOnline.Core;

namespace PitchOnline
{
    public class CardBrushConverter : IMultiValueConverter
    {
        private static string deckFolder = "Default";

        private static Dictionary<string, Brush> brushes = new Dictionary<string, Brush>();

        public static void SetDeckFolder(string folderName)
        {
            //  Clear the dictionary so we recreate card brushes.
            brushes.Clear();

            //  Set the deck folder.
            deckFolder = folderName;
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            //  Cast the data.
            if (values == null || values.Count() != 2)
                return null;

            //  Cast the values.
            CardType type = (CardType)values[0];
            var isfacedown = (bool)values[1];

            var source = string.Empty;
            //  If the card is face down, we're using the 'Rear' image.
            //  Otherwise it's just the enum value (e.g. C3, SA).
            if (isfacedown)
                source = "Back";
            else
                source = type.ToString();

            //  Turn this string into a proper path.
            source = "pack://application:,,,/PitchOnline;component/Images/Decks/" + deckFolder + "/" + source + ".png";

            //  Do we need to add this brush to the static dictionary?
            if (brushes.ContainsKey(source) == false)
                brushes.Add(source, new ImageBrush(new BitmapImage(new Uri(source))));

            //  Return the brush.
            return brushes[source];
        }

        public object[] ConvertBack(object value, Type[] targetTypes,
               object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}