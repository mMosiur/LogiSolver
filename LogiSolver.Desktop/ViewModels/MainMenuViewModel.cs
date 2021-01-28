using System.Windows.Input;

namespace LogiSolver.Desktop
{
	public class MainMenuViewModel : BaseViewModel
	{
		#region Constructors

		public MainMenuViewModel()
		{
			GoToSolvePageCommand = new RelayCommand(() => PageManager.Instance.CurrentPage = ApplicationPage.Solve);
		}

		#endregion Constructors



		#region Commands

		public ICommand GoToSolvePageCommand { get; private set; }

		public ICommand OptionsCommand { get; private set; }

		#endregion Commands
	}
}