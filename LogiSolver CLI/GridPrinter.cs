using LogiSolver.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LogiSolver.CLI
{
	public static class GridPrinter
	{
		public enum OutlineStyle : byte
		{
			Normal = 0,
			Rounded = 1,
			Bold = 2
		}

		private static OutlineStyle outline = OutlineStyle.Rounded;

		public static OutlineStyle Outline
		{
			get => outline;
			set
			{
				if ((int)value > Enum.GetValues(outline.GetType()).GetUpperBound(0))
					value = 0;
				outline = value;
			}
		}

		private static string CellStateToString(CellState state)
		{
			return state switch
			{
				CellState.Unknown => "   ",
				CellState.FilledIn => "▐█▌",
				CellState.Empty => " • ",
				_ => " � ",
			};
		}

		public static void Print(List<List<CellState>> grid, int selectedRow, int selectedCol)
		{
			System.Console.Clear();
			List<string> output = new List<string>();
			char horizontalEdge = "──━"[(int)outline];
			char verticalEdge = "││┃"[(int)outline];
			char topLeftCorner = "┌╭┏"[(int)outline];
			char topCross = "┬┬┯"[(int)outline];
			char topRightCorner = "┐╮┓"[(int)outline];
			char rightCross = "┤┤┨"[(int)outline];
			char bottomRightCorner = "┘╯┛"[(int)outline];
			char bottomCross = "┴┴┷"[(int)outline];
			char bottomLeftCorner = "└╰┗"[(int)outline];
			char leftCross = "├├┠"[(int)outline];
			output.Add($"{topLeftCorner}");
			for (int i = 0; i < grid[0].Count; i++)
				output[^1] += $"{horizontalEdge}{horizontalEdge}{horizontalEdge}{topCross}";
			output[^1] = output[^1].Remove(output[^1].Length - 1);
			output[^1] += $"{topRightCorner}";
			for (int row = 0; row < grid.Count; row++)
			{
				output.Add($"{verticalEdge}");
				foreach (var cell in grid[row])
				{
					output[^1] += $"{CellStateToString(cell)}│";
				}
				output[^1] = output[^1].Remove(output[^1].Length - 1);
				output[^1] += $"{verticalEdge}";
				output.Add($"{leftCross}");
				for (int i = 0; i < grid[row].Count; i++)
					output[^1] += "───┼";
				output[^1] = output[^1].Remove(output[^1].Length - 1);
				output[^1] += $"{rightCross}";
			}
			output[^1] = $"{bottomLeftCorner}";
			for (int i = 0; i < grid.Count; i++)
				output[^1] += $"{horizontalEdge}{horizontalEdge}{horizontalEdge}{bottomCross}";
			output[^1] = output[^1].Remove(output[^1].Length - 1);
			output[^1] += $"{bottomRightCorner}";
			if(selectedRow >= 0 && selectedRow < grid.Count && selectedCol >= 0 && selectedCol < grid[0].Count)
			{
				output[selectedRow * 2 + 0] = output[selectedRow * 2 + 0].Substring(0, selectedCol * 4 + 0) + "╔═══╗" + output[selectedRow * 2 + 0].Substring(selectedCol * 4 + 5);
				output[selectedRow * 2 + 1] = output[selectedRow * 2 + 1].Substring(0, selectedCol * 4 + 0) + "║" + output[selectedRow * 2 + 1].Substring(selectedCol * 4 + 1, 3) + "║" + output[selectedRow * 2 + 1].Substring(selectedCol * 4 + 5);
				output[selectedRow * 2 + 2] = output[selectedRow * 2 + 2].Substring(0, selectedCol * 4 + 0) + "╚═══╝" + output[selectedRow * 2 + 2].Substring(selectedCol * 4 + 5);

			}
			Console.OutputEncoding = System.Text.Encoding.UTF8;
			Console.SetCursorPosition(0, 0);
			Console.WriteLine(string.Join('\n', output));
		}
	}
}