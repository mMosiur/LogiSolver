using LogiSolver.Core;
using System;

namespace LogiSolver.CLI
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var puzzle = new Puzzle();
			bool exit = false;
			int selectedRow = 0;
			int selectedCol = 0;
			GridPrinter.Print(puzzle.Grid, selectedRow, selectedCol);
			while (!exit)
			{
				GridPrinter.Print(puzzle.Grid, selectedRow, selectedCol);
				var key = Console.ReadKey().Key;
				switch (key)
				{
					case ConsoleKey.UpArrow:
						if (selectedRow > 0)
							selectedRow--;
						break;

					case ConsoleKey.DownArrow:
						if (selectedRow < puzzle.Grid.Count - 1)
							selectedRow++;
						break;

					case ConsoleKey.LeftArrow:
						if (selectedCol > 0)
							selectedCol--;
						break;

					case ConsoleKey.RightArrow:
						if (selectedCol < puzzle.Grid[0].Count - 1)
							selectedCol++;
						break;

					case ConsoleKey.Escape:
					case ConsoleKey.Q:
						exit = true;
						break;

					case ConsoleKey.Backspace:
					case ConsoleKey.Delete:
					case ConsoleKey.Spacebar:
						puzzle.SetCell(selectedRow, selectedCol, CellState.Unknown);
						break;

					case ConsoleKey.X:
						puzzle.SetCell(selectedRow, selectedCol, CellState.FilledIn);
						break;

					case ConsoleKey.OemPeriod:
						puzzle.SetCell(selectedRow, selectedCol, CellState.Empty);
						break;

					case ConsoleKey.Enter:
						if (puzzle.Grid[selectedRow][selectedCol] == CellState.Empty)
							puzzle.SetCell(selectedRow, selectedCol, CellState.FilledIn);
						else if (puzzle.Grid[selectedRow][selectedCol] == CellState.FilledIn)
							puzzle.SetCell(selectedRow, selectedCol, CellState.Unknown);
						else if (puzzle.Grid[selectedRow][selectedCol] == CellState.Unknown)
							puzzle.SetCell(selectedRow, selectedCol, CellState.Empty);
						break;
				}
			}
		}
	}
}