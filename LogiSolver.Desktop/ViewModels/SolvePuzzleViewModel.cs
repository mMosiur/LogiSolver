using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace LogiSolver.Desktop
{
	public class SolvePuzzleViewModel : BaseViewModel
	{
		#region Private members

		#endregion Private members

		#region Constructors

		public SolvePuzzleViewModel()
		{
			GoToMainMenuPageCommand = new RelayCommand(() => PageManager.Instance.CurrentPage = ApplicationPage.MainMenu);

		}

		#endregion Constructors

		#region Public properties

		#endregion Public properties

		#region Commands

		public ICommand GoToMainMenuPageCommand { get; private set; }

		#endregion Commands
	}
}
