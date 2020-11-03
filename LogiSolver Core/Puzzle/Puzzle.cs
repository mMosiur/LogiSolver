using System;
using System.Collections.Generic;
using System.Linq;

namespace LogiSolver.Core
{
	/// <summary>
	/// A class that represents the Logi puzzle
	/// </summary>
	public partial class Puzzle
	{
		#region Constructors

		/// <summary>
		/// Constructor that creates sample puzzle.
		/// </summary>
		/// <param name="easy">Whether the created puzzle should be harder (true) or easier (false)</param>
		public Puzzle(bool easy)
		{
			if (easy)
			{
				const ushort rowCount = 10;
				const ushort colCount = 10;
				RowClues = new List<ushort>[rowCount]
				{
					new List<ushort> { 4 },
					new List<ushort> { 6 },
					new List<ushort> { 2, 5 },
					new List<ushort> { 10 },
					new List<ushort> { 2, 1, 5 },
					new List<ushort> { 10 },
					new List<ushort> { 10 },
					new List<ushort> { 8 },
					new List<ushort> { 6 },
					new List<ushort> { 4 }
				};
				ColumnClues = new List<ushort>[colCount]
				{
					new List<ushort>() { 4 },
					new List<ushort>() { 6 },
					new List<ushort>() { 3, 4 },
					new List<ushort>() { 2, 7 },
					new List<ushort>() { 4, 5 },
					new List<ushort>() { 10 },
					new List<ushort>() { 10 },
					new List<ushort>() { 8 },
					new List<ushort>() { 6 },
					new List<ushort>() { 4 }
				};
				byte[,] bytes = new byte[rowCount, colCount]
				{
					{ 0, 0, 2, 0, 0, 0, 0, 0, 0, 0 },
					{ 2, 2, 0, 0, 0, 0, 0, 0, 0, 2 },
					{ 0, 0, 0, 2, 0, 0, 0, 0, 0, 0 },
					{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
					{ 0, 0, 2, 0, 2, 0, 0, 0, 0, 0 },
					{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
					{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
					{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
					{ 0, 0, 0, 0, 0, 0, 0, 0, 2, 0 },
					{ 2, 2, 2, 0, 0, 0, 0, 2, 2, 0 }
				};
				Grid = new CellGrid(bytes);
			}
			else
			{
				const ushort rowCount = 15;
				const ushort colCount = 15;
				RowClues = new List<ushort>[rowCount]
				{
					new List<ushort>() { 1, 6 },
					new List<ushort>() { 3, 6 },
					new List<ushort>() { 11 },
					new List<ushort>() { 12 },
					new List<ushort>() { 6, 3 },
					new List<ushort>() { 6, 5 },
					new List<ushort>() { 15 },
					new List<ushort>() { 10, 3 },
					new List<ushort>() { 15 },
					new List<ushort>() { 7, 4 },
					new List<ushort>() { 6, 2, 1 },
					new List<ushort>() { 6, 1, 3 },
					new List<ushort>() { 3, 2, 1, 4 },
					new List<ushort>() { 1, 2, 6 },
					new List<ushort>() { 3, 6 }
				};
				ColumnClues = new List<ushort>[colCount]
				{
					new List<ushort>() { 12 },
					new List<ushort>() { 13, 1 },
					new List<ushort>() { 14 },
					new List<ushort>() { 10, 1 },
					new List<ushort>() { 1, 12 },
					new List<ushort>() { 14 },
					new List<ushort>() { 4, 4 },
					new List<ushort>() { 4, 3, 1 },
					new List<ushort>() { 9, 4 },
					new List<ushort>() { 9, 1 ,2 },
					new List<ushort>() { 6, 1, 1, 3 },
					new List<ushort>() { 1, 5, 4 },
					new List<ushort>() { 5, 4 },
					new List<ushort>() { 4, 3 },
					new List<ushort>() { 1, 3 }
				};
				Grid = new CellGrid(rowCount, colCount);
			}
		}

		/// <summary>
		/// Constructor that creates an empty puzzle.
		/// </summary>
		public Puzzle()
		{
			Grid = new CellGrid(0, 0);
			ColumnClues = new List<ushort>[0];
			RowClues = new List<ushort>[0];
		}

		#endregion Constructors

		#region Public Properties

		/// <summary>
		/// An array of column clues for the puzzle.
		/// Each element of the array is a list of clues for given column.
		/// </summary>
		public List<ushort>[] ColumnClues { get; private set; }

		/// <summary>
		/// An array of row clues for the puzzle.
		/// Each element of the array is a list of clues for given row.
		/// </summary>
		public List<ushort>[] RowClues { get; private set; }

		/// <summary>
		/// The grid of the puzzle containing the states of each cell in 2D array
		/// </summary>
		public CellGrid Grid { get; private set; }

		/// <summary>
		/// Returns how many rows does the puzzle grid have
		/// </summary>
		public int RowCount => Grid?.RowCount ?? 0;

		/// <summary>
		/// Returns how many columns does the puzzle grid have
		/// </summary>
		public int ColumnCount => Grid?.ColumnCount ?? 0;

		/// <summary>
		/// Checks whether the puzzle in current state is solved or not
		/// </summary>
		public bool Solved
		{
			get
			{
				for (int row = 0; row < RowCount; row++)
				{
					if (!RowSolved(row)) return false;
				}
				for (int col = 0; col < ColumnCount; col++)
				{
					if (!ColumnSolved(col)) return false;
				}

				return true;
			}
		}

		#endregion Public Properties

		#region Public Methods

		/// <summary>
		/// Sets the state of a cell in a given row and column
		/// </summary>
		/// <param name="row">The index of the row of the cell</param>
		/// <param name="col">The index of the column of the cell</param>
		/// <param name="state">The state to which the cell should be set to</param>
		public void SetCell(int row, int col, CellState state)
		{
			Grid[row, col] = state;
		}

		/// <summary>
		/// Checks whether a given row is solved
		/// </summary>
		/// <param name="row">The index of a row to check</param>
		/// <returns>Whether the row with given index is solved</returns>
		public bool RowSolved(int row)
		{
			List<ushort> list = new List<ushort> { 0 };
			foreach (var cell in Grid.CellsInRow(row))
			{
				if (cell == CellState.FilledIn)
					list[^1]++;
				else if (list[^1] > 0)
					list.Add(0);
			}
			if (list[^1] == 0) list.RemoveAt(list.Count - 1);
			return list.SequenceEqual(RowClues[row]);
		}

		/// <summary>
		/// Checks whether a given column is solved
		/// </summary>
		/// <param name="col">The index of a column to check</param>
		/// <returns>Whether the column with given index is solved</returns>
		public bool ColumnSolved(int col)
		{
			List<ushort> list = new List<ushort> { 0 };
			foreach (var cell in Grid.CellsInColumn(col))
			{
				if (cell == CellState.FilledIn)
					list[^1]++;
				else if (list[^1] > 0)
					list.Add(0);
			}
			if (list[^1] == 0) list.RemoveAt(list.Count - 1);
			return list.SequenceEqual(ColumnClues[col]);
		}

		#endregion Public Methods
	}
}