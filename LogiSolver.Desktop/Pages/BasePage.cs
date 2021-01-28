using System.Windows.Controls;

namespace LogiSolver.Desktop
{
	public class BasePage<VM> : Page
		where VM : BaseViewModel, new()
	{
		#region Private members

		private VM mViewModel;

		#endregion Private members

		#region Public Properties

		public VM ViewModel
		{
			get => mViewModel;
			set
			{
				if (mViewModel == value) return;
				mViewModel = value;
				DataContext = mViewModel;
			}
		}

		#endregion Public Properties

		#region Constructors

		public BasePage()
		{
			this.ViewModel = new VM();
		}

		#endregion Constructors
	}
}