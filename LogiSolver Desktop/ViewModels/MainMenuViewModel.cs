using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace LogiSolver.Desktop
{
	public class MainMenuViewModel : BaseViewModel
	{
		#region Private members

		#endregion Private members

		#region Constructors

		public MainMenuViewModel()
		{
			GoToSolvePageCommand = new RelayCommand(() => PageManager.Instance.CurrentPage = ApplicationPage.Solve );

		}

		#endregion Constructors

		#region Public properties

		#endregion Public properties

		#region Commands

		public ICommand GoToSolvePageCommand { get; private set; }

		public ICommand OptionsCommand { get; private set; }

		#endregion Commands
	}
}
