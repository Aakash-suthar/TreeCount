using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace tree
{
    [ValueConversion(typeof(ApplicationPage), typeof(Page))]
    public class PageConverter : BaseValueConveter<PageConverter> 
    {
        public static PageConverter Instance = new PageConverter();
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((ApplicationPage)value)
            {
                case ApplicationPage.Homepage1:return new HomePage();
                case ApplicationPage.About: return new About();
                case ApplicationPage.Search: return new SearchPage(); 
                default: return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
