using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LogiSolver.Core
{
	/// <summary>
	/// Constants for possible cell states
	/// </summary>
	public enum CellState
	{
		/// <summary>
		/// The state of the cell is unknown. Cell is unmarked.
		/// </summary>
		Unknown = 0,

		/// <summary>
		/// The cell is filled in.
		/// </summary>
		FilledIn = 1,

		/// <summary>
		/// The cell is marked as empty.
		/// </summary>
		Empty = 2
	}

	/// <summary>
	/// The class that represents the grid of the <see cref="Puzzle"/>.
	/// </summary>
	public class CellGrid : IEnumerable<CellState>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CellGrid"/> class
		/// with specified number of rows and columns
		/// </summary>
		/// <param name="rows">The number of rows that the grid is supposed to have</param>
		/// <param name="cols">The number of column that the grid is supposed to have</param>
		public CellGrid(int rows, int cols)
		{
			cells = new CellState[rows, cols];
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CellGrid"/> class
		/// with a specified grid of cells.
		/// </summary>
		/// <param name="cellStates">The grid of cell states to be used as Grids initalizer</param>
		public CellGrid(CellState[,] cellStates)
		{
			cells = (CellState[,])cellStates.Clone();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CellGrid"/> class
		/// with a specified grid of bytes, where each byte is casted to <see cref="CellState"/>.
		/// </summary>
		/// <param name="cellStates">The grid of bytes to be casted to <see cref="CellState"/>
		/// and used as Grids initalizer</param>
		public CellGrid(byte[,] bytes)
		{
			cells = new CellState[bytes.GetLength(0), bytes.GetLength(1)];
			Array.Copy(bytes, cells, bytes.Length);
		}

		#endregion Constructors

		#region Private Members

		/// <summary>
		/// The actual grid (2d array) that hold the data of the grid
		/// </summary>
		private readonly CellState[,] cells;

		#endregion Private Members

		#region Public Properties

		/// <summary>
		/// Gets the number of rows the <see cref="CellGrid"/> contains
		/// </summary>
		public int RowCount => cells.GetLength(0);

		/// <summary>
		/// Gets the number of columns the <see cref="CellGrid"/> contains
		/// </summary>
		public int ColumnCount => cells.GetLength(1);

		/// <summary>
		/// Gets the rows of the grid as an <see cref="IEnumerable"/>.
		/// Each row is an <see cref="IEnumerable"/> of cells as <see cref="CellState"/>.
		/// </summary>
		public IEnumerable<IEnumerable<CellState>> Rows
		{
			get
			{
				for (int row = 0; row < RowCount; row++)
				{
					yield return CellsInRow(row);
				}
			}
		}

		/// <summary>
		/// Gets the columns of the grid as an <see cref="IEnumerable"/>.
		/// Each column is an <see cref="IEnumerable"/> of cells as <see cref="CellState"/>.
		/// </summary>
		public IEnumerable<IEnumerable<CellState>> Columns
		{
			get
			{
				for (int col = 0; col < ColumnCount; col++)
				{
					yield return CellsInColumn(col);
				}
			}
		}

		#endregion Public Properties

		#region Operators

		/// <summary>
		/// Gets or sets the state of the cell in specified row and column
		/// </summary>
		/// <param name="row">The zero-based index of the row of the cell</param>
		/// <param name="col">The zero-based index of the column of the cell</param>
		/// <returns>The <see cref="CellState"/> of specified cell</returns>
		public CellState this[int row, int col]
		{
			get => cells[row, col];
			set => cells[row, col] = value;
		}

		#endregion Operators

		#region Public Methods

		/// <summary>
		/// Gets the cells in a row of a given index as <see cref="IEnumerable"/> of <see cref="CellState"/> type.
		/// </summary>
		/// <param name="row">The zero-based index of row of the cells to get</param>
		/// <returns>An <see cref="IEnumerable"/> of cells as <see cref="CellState"/></returns>
		public IEnumerable<CellState> CellsInRow(int row)
		{
			for (int col = 0; col < ColumnCount; col++)
				yield return cells[row, col];
		}

		/// <summary>
		/// Gets the cells in a column of a given index as <see cref="IEnumerable"/> of <see cref="CellState"/> type.
		/// </summary>
		/// <param name="row">The zero-based index of column of the cells to get</param>
		/// <returns>An <see cref="IEnumerable"/> of cells as <see cref="CellState"/></returns>
		public IEnumerable<CellState> CellsInColumn(int col)
		{
			for (int row = 0; row < RowCount; row++)
				yield return cells[row, col];
		}

		/// <summary>
		/// Returns an enumerator that iterates over the cell grid
		/// </summary>
		/// <returns>An enumerator that cas be used to iterate over the cell grid</returns>
		public IEnumerator<CellState> GetEnumerator()
		{
			return cells.Cast<CellState>().GetEnumerator();
		}

		/// <summary>
		/// Returns an <see cref="IEnumerator"/> for the cell grid <see cref="Array"/>
		/// </summary>
		/// <returns>An <see cref="IEnumerator"/> for the cell grid <see cref="Array"/></returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return cells.GetEnumerator();
		}

		#endregion Public Methods
	}
}