using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Principal;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace LogiSolver.Desktop
{
	[ValueConversion(typeof(WindowState), typeof(string))]
	public class WindowStateToStringConverter : BaseValueConverter<WindowStateToStringConverter>
	{

		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (WindowState)value == WindowState.Maximized ? "🗗" : "🗖";
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
