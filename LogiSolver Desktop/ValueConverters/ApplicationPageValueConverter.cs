using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace LogiSolver.Desktop
{
	public class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter>
	{
		private Page mMainMenuPage = null;
		private Page mSolvePuzzlePage = null;

		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			switch((ApplicationPage)value)
			{
				case ApplicationPage.MainMenu:
					return mMainMenuPage ??= new MainMenuPage();
				case ApplicationPage.Solve:
					return mSolvePuzzlePage ??= new SolvePuzzlePage();
				default:
					Debugger.Break();
					return null;
			}
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
