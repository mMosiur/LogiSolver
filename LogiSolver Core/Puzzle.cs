using System.Collections.Generic;

namespace LogiSolver.Core
{
	public enum CellState
	{
		Unknown = 0,
		FilledIn = 1,
		Empty = 2
	}

	public class Puzzle
	{
		private List<List<ushort>> columnClues;
		private List<List<ushort>> rowClues;
		private List<List<CellState>> grid;

		public Puzzle()
		{
			columnClues = new List<List<ushort>>()
			{
				new List<ushort>() { 1, 1 },
				new List<ushort>() { 1, 1 },
				new List<ushort>() { 1, 1, 1, 1 },
				new List<ushort>() { 1, 1, 1 },
				new List<ushort>() { 1, 2, 1 },
				new List<ushort>() { 2, 2, 1 },
				new List<ushort>() { 9 },
				new List<ushort>() { 1, 1, 1, 1 },
				new List<ushort>() { 1, 1, 1 },
				new List<ushort>() { 1, 1, 1 }
			};
			rowClues = new List<List<ushort>>()
			{
				new List<ushort>() { 1, 1, 2, 1, 1 },
				new List<ushort>() { 1, 1, 2, 1 },
				new List<ushort>() { 1, 1, 2, 1 },
				new List<ushort>() { 4, 1 },
				new List<ushort>() { 3 },
				new List<ushort>() { 1 },
				new List<ushort>() { 1, 1 },
				new List<ushort>() { 1 },
				new List<ushort>() { 1 },
				new List<ushort>() { 10 }
			};
			grid = new List<List<CellState>>()
			{
				new List<CellState>() { 0, 0, 0, CellState.Empty, 0, 0, 0, 0, CellState.Empty, 0},
				new List<CellState>() { 0, 0, CellState.Empty, 0, 0, 0, 0, CellState.Empty, 0, 0},
				new List<CellState>() { CellState.Empty, CellState.Empty, 0, CellState.Empty, 0, 0, 0, 0, CellState.Empty, 0 },
				new List<CellState>() { CellState.Empty, CellState.Empty, CellState.Empty, 0, 0, 0, 0, 0, 0, CellState.Empty },
				new List<CellState>() { 0, 0, 0, 0, CellState.Empty, 0, 0, 0, CellState.Empty, 0},
				new List<CellState>() { CellState.Empty, CellState.Empty, 0, CellState.Empty, CellState.Empty, 0, 0, 0, CellState.Empty, CellState.Empty},
				new List<CellState>() { 0, CellState.Empty, 0, 0, 0, 0, 0, 0, CellState.Empty, CellState.Empty},
				new List<CellState>() { 0, 0, CellState.Empty, CellState.Empty, 0, CellState.Empty, 0, 0, 0, CellState.Empty},
				new List<CellState>() { 0, CellState.Empty, CellState.Empty, 0, CellState.Empty, 0, 0, CellState.Empty, 0, 0},
				new List<CellState>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
			};
		}

		public List<List<ushort>> ColumnClues { get => columnClues; }
		public List<List<ushort>> RowClues { get => rowClues; }
		public List<List<CellState>> Grid { get => grid; }

		public void SetCell(int row, int col, CellState state)
		{
			grid[row][col] = state;
		}
	}
}