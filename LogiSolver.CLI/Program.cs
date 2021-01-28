using LogiSolver.Core;
using System;
using static LogiSolver.Core.Puzzle;

namespace LogiSolver.CLI
{
	internal class Program
	{
		private static void Main(/* string[] args */)
		{
			Console.Title = "LogiSolver";
			Console.Clear();
			Puzzle puzzle = Puzzle.Manager.Load("puzzle.logi") ?? new Puzzle(false);
			bool exit = false;
			int selectedRow = 0;
			int selectedCol = 0;
			Console.CursorVisible = false;
			Console.CancelKeyPress += delegate { Environment.Exit(0); };
			PuzzlePrinter printer = new PuzzlePrinter();
			printer.Print(puzzle, selectedRow, selectedCol);

			while (!exit)
			{
				if (puzzle.Solved)
					Console.ForegroundColor = ConsoleColor.Green;
				else
					Console.ResetColor();
				printer.Print(puzzle, selectedRow, selectedCol);
				switch (Console.ReadKey(true).Key)
				{
					case ConsoleKey.UpArrow:
						if (selectedRow > 0)
							selectedRow--;
						break;

					case ConsoleKey.DownArrow:
						if (selectedRow < puzzle.Grid.RowCount - 1)
							selectedRow++;
						break;

					case ConsoleKey.LeftArrow:
						if (selectedCol > 0)
							selectedCol--;
						break;

					case ConsoleKey.RightArrow:
						if (selectedCol < puzzle.Grid.ColumnCount - 1)
							selectedCol++;
						break;

					case ConsoleKey.Escape:
					case ConsoleKey.Q:
						exit = true;
						break;

					case ConsoleKey.Backspace:
					case ConsoleKey.Delete:
						puzzle.SetCell(selectedRow, selectedCol, CellState.Unknown);
						break;

					case ConsoleKey.X:
					case ConsoleKey.OemPeriod:
						puzzle.SetCell(selectedRow, selectedCol, CellState.Empty);
						break;

					case ConsoleKey.Spacebar:
					case ConsoleKey.Enter:
						puzzle.SetCell(selectedRow, selectedCol, CellState.FilledIn);
						break;

					case ConsoleKey.S:
						Puzzle.Manager.Save(puzzle, "puzzle.logi");
						break;

					case ConsoleKey.R:
						Puzzle newPuzzle = Puzzle.Manager.Load(@"puzzles\puzzle.logi");
						if (newPuzzle != null) puzzle = newPuzzle;
						break;
				}
			}
			Console.WriteLine();
		}
	}
}