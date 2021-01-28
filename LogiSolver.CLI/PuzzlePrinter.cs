using LogiSolver.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using static LogiSolver.Core.Puzzle;

namespace LogiSolver.CLI
{
	public class PuzzlePrinter
	{
		public Puzzle Puzzle { get; set; }

		private static string CellStateToString(CellState state)
		{
			return state switch
			{
				CellState.Unknown => "   ",
				CellState.FilledIn => "▐█▌",
				CellState.Empty => " X ",//" • ",
				_ => " � "
			};
		}

		private List<string> GenerateGridString(Puzzle puzzle, int selectedRow, int selectedCol)
		{
			var grid = puzzle.Grid;
			List<string> output = new List<string> { "┏" };
			for (int col = 0; col < grid.ColumnCount; col++)
			{
				bool verticalBold = col % 5 == 4;
				output[^1] += "━━━" + (verticalBold ? '┳' : '┯');
			}
			output[^1] = output[^1].Remove(output[^1].Length - 1);
			output[^1] += "┓";
			for (int row = 0; row < grid.RowCount; row++)
			{
				bool horizontalBold = row % 5 == 4;
				// Upper row edge
				output.Add("┃");
				for (int col = 0; col < grid.ColumnCount; col++)
				{
					var cell = grid[row, col];
					bool verticalBold = col % 5 == 4;
					output[^1] += CellStateToString(cell) + (verticalBold ? '┃' : '│');
				}
				output[^1] = output[^1].Remove(output[^1].Length - 1);
				output[^1] += '┃';
				// Inner row content
				output.Add(horizontalBold ? "┣" : "┠");
				for (int col = 0; col < grid.ColumnCount; col++)
				{
					bool verticalBold = col % 5 == 4;
					char cross;
					if (horizontalBold && verticalBold)
						cross = '╋';
					else if (horizontalBold && !verticalBold)
						cross = '┿';
					else if (!horizontalBold && verticalBold)
						cross = '╂';
					else
						cross = '┼';
					char edge = horizontalBold ? '━' : '─';
					output[^1] += $"{edge}{edge}{edge}{cross}";
				}
				output[^1] = output[^1].Remove(output[^1].Length - 1);
				output[^1] += horizontalBold ? '┫' : '┨';
			}
			output[^1] = "┗";
			for (int col = 0; col < grid.ColumnCount; col++)
			{
				bool verticalBold = col % 5 == 4;
				output[^1] += "━━━" + (verticalBold ? '┻' : '┷');
			}
			output[^1] = output[^1].Remove(output[^1].Length - 1);
			output[^1] += "┛";
			if (selectedRow >= 0 && selectedRow < grid.RowCount && selectedCol >= 0 && selectedCol < grid.ColumnCount)
			{
				output[selectedRow * 2 + 0] = $"{output[selectedRow * 2 + 0].Substring(0, selectedCol * 4 + 0)}╔═══╗{output[selectedRow * 2 + 0].Substring(selectedCol * 4 + 5)}";
				output[selectedRow * 2 + 1] = $"{output[selectedRow * 2 + 1].Substring(0, selectedCol * 4 + 0)}║{output[selectedRow * 2 + 1].Substring(selectedCol * 4 + 1, 3)}║{output[selectedRow * 2 + 1].Substring(selectedCol * 4 + 5)}";
				output[selectedRow * 2 + 2] = $"{output[selectedRow * 2 + 2].Substring(0, selectedCol * 4 + 0)}╚═══╝{output[selectedRow * 2 + 2].Substring(selectedCol * 4 + 5)}";
			}
			return output;
		}

		private void AddClues(Puzzle puzzle, List<string> grid)
		{
			int columnCluesRowCount = puzzle.ColumnClues.Max((list) => list.Count);
			for (int row = 0; row < columnCluesRowCount; row++)
			{
				grid.Insert(0, "");
				for (int col = 0; col < puzzle.ColumnClues.Length; col++)
				{
					List<ushort> clues = puzzle.ColumnClues[col];
					int index = clues.Count - 1 - row;
					if (index < 0)
						grid[0] += "    ";
					else
						grid[0] += $" {clues[index],2} ";
				}
			}
			for (int i = 0; i < puzzle.RowClues.Length; i++)
			{
				foreach (var clue in puzzle.RowClues[i])
				{
					grid[i * 2 + columnCluesRowCount + 1] += $" {clue}";
				}
			}
		}

		public void Print(Puzzle puzzle, int selectedRow, int selectedCol)
		{
			Console.OutputEncoding = System.Text.Encoding.UTF8;
			Console.SetCursorPosition(0, 0);
			List<string> grid = GenerateGridString(puzzle, selectedRow, selectedCol);
			AddClues(puzzle, grid);
			//Console.WindowHeight = grid.Count + 2;
			for (int i = 0; i < grid.Count; i++)
			{
				grid[i] = "   " + grid[i];
				grid[i] = grid[i].PadRight(Console.WindowWidth - 1);
			}
			Console.WriteLine(string.Join('\n', grid));
			Console.Write((puzzle.Solved ? "SOLVED!" : "not solved yet").PadRight(Console.WindowWidth));
		}

		public void Print(int selectedRow, int selectedCol) => Print(Puzzle, selectedRow, selectedCol);

		public void Print(Puzzle puzzle) => Print(puzzle, -1, -1);

		public void Print() => Print(Puzzle, -1, -1);
	}
}