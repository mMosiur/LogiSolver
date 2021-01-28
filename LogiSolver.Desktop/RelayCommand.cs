using System;
using System.Windows.Input;

namespace LogiSolver.Desktop
{
	public class RelayCommand : ICommand
	{
		#region Private members

		private readonly Action<object> mAction;

		private readonly Func<object, bool> mCanExecute;

		#endregion Private members

		#region Constructors

		public RelayCommand(Action action)
		{
			mAction = new Action<object>(parameter => action?.Invoke());
			mCanExecute = null;
		}

		public RelayCommand(Action action, Func<bool> canExecute)
		{
			mAction = new Action<object>(parameter => action?.Invoke());
			mCanExecute = new Func<object, bool>(parameter => canExecute?.Invoke() ?? true);
		}

		public RelayCommand(Action action, Func<object, bool> canExecute)
		{
			mAction = new Action<object>(parameter => action?.Invoke());
			mCanExecute = canExecute;
		}

		public RelayCommand(Action<object> action)
		{
			mAction = action;
			mCanExecute = null;
		}

		public RelayCommand(Action<object> action, Func<bool> canExecute)
		{
			mAction = action;
			mCanExecute = new Func<object, bool>(parameter => canExecute?.Invoke() ?? true);
		}

		public RelayCommand(Action<object> action, Func<object, bool> canExecute)
		{
			mAction = action;
			mCanExecute = canExecute;
		}

		#endregion Constructors

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public bool CanExecute(object parameter)
		{
			return mCanExecute?.Invoke(parameter) ?? true;
		}

		public void Execute(object parameter)
		{
			mAction?.Invoke(parameter);
		}
	}

	public class RelayCommand<T> : ICommand
	{
		#region Private members

		private readonly Action<T> mAction;

		private readonly Func<T, bool> mCanExecute;

		#endregion Private members

		#region Constructors

		public RelayCommand(Action action)
		{
			mAction = new Action<T>(parameter => action?.Invoke());
			mCanExecute = null;
		}

		public RelayCommand(Action action, Func<bool> canExecute)
		{
			mAction = new Action<T>(parameter => action?.Invoke());
			mCanExecute = new Func<T, bool>(parameter => canExecute?.Invoke() ?? true);
		}

		public RelayCommand(Action action, Func<T, bool> canExecute)
		{
			mAction = new Action<T>(parameter => action?.Invoke());
			mCanExecute = canExecute;
		}

		public RelayCommand(Action<T> action)
		{
			mAction = action;
			mCanExecute = null;
		}

		public RelayCommand(Action<T> action, Func<bool> canExecute)
		{
			mAction = action;
			mCanExecute = new Func<T, bool>(parameter => canExecute?.Invoke() ?? true);
		}

		public RelayCommand(Action<T> action, Func<T, bool> canExecute)
		{
			mAction = action;
			mCanExecute = canExecute;
		}

		#endregion Constructors

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public bool CanExecute(object parameter)
		{
			return mCanExecute?.Invoke((T)parameter) ?? true;
		}

		public void Execute(object parameter)
		{
			mAction?.Invoke((T)parameter);
		}
	}
}