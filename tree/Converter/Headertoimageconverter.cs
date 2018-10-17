

using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace tree
{
    [ValueConversion(typeof(DirectoryItemType), typeof(BitmapImage))]
     public class Headertoimageconverter : IValueConverter
    {
        public static Headertoimageconverter Instance = new Headertoimageconverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var image = "Images/file.png";
            switch((DirectoryItemType)value){

                case DirectoryItemType.Drive:
                    image = "Images/drive.png";break;
                case DirectoryItemType.Folder:
                    image = "Images/folderclose.png"; break;


            } 
            return new BitmapImage(new Uri($"pack://application:,,,/{image}"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
