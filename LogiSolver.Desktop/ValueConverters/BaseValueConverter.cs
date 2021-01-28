using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace LogiSolver.Desktop
{
	public abstract class BaseValueConverter<T> : MarkupExtension, IValueConverter
		where T : class, new()
	{
		#region Private members

		private static T Converter = null;

		#endregion Private members

		#region Markup Extension methods

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return Converter ??= new T();
		}

		#endregion Markup Extension methods

		#region Value Converter methods

		public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

		public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

		#endregion Value Converter methods
	}
}