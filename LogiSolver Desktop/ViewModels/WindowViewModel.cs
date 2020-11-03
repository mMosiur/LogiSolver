using System.Windows;
using System.Windows.Input;

namespace LogiSolver.Desktop
{
	public class WindowViewModel : BaseViewModel
	{
		#region Private members

		private readonly Window mWindow;

		#endregion Private members

		#region Constructors

		public WindowViewModel(Window window)
		{
			mWindow = window;

			mWindow.StateChanged += (sender, e) => OnPropertyChanged(nameof(WindowState));

			MinimizeCommand = new RelayCommand(() => { mWindow.WindowState = WindowState.Minimized; });
			MaximizeCommand = new RelayCommand(() => { mWindow.WindowState ^= WindowState.Maximized; });
			CloseCommand = new RelayCommand(() => { mWindow.Close(); });
			MenuCommand = new RelayCommand(() => { SystemCommands.ShowSystemMenu(mWindow, GetMousePosition()); });
		}

		#endregion Constructors

		#region Public properties

		public WindowState WindowState
		{
			get => mWindow.WindowState;
		}

		public string WindowTitle
		{
			get => mWindow.Title;
			set
			{
				mWindow.Title = value;
				OnPropertyChanged();
			}
		}

		#endregion Public properties

		#region Commands

		public ICommand MinimizeCommand { get; private set; }

		public ICommand MaximizeCommand { get; private set; }

		public ICommand CloseCommand { get; private set; }

		public ICommand MenuCommand { get; private set; }

		#endregion Commands

		#region Helpers

		private Point GetMousePosition()
		{
			Point position = Mouse.GetPosition(mWindow);
			return new Point(position.X + mWindow.Left, position.Y + mWindow.Top);
		}

		#endregion Helpers
	}
}