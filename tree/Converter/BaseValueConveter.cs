using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace tree
{
    public abstract class BaseValueConveter<T> : MarkupExtension, IValueConverter
        where T:class,new()
    {
        private static T mconverter = null;

        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return mconverter ?? (mconverter = new T());
        }
    }
}
