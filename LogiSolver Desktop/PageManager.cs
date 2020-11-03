using System.Windows.Controls;

namespace LogiSolver.Desktop
{
	public class PageManager : BaseViewModel
	{
		private ApplicationPage currentPage = ApplicationPage.MainMenu;


		public static PageManager Instance { get; private set; } = new PageManager();

		public ApplicationPage CurrentPage
		{
			get => currentPage;
			set
			{
				currentPage = value;
				OnPropertyChanged();
			}
		}
	}
}
