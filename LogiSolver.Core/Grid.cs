using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LogiSolver.Core
{
    public class Grid<T> : IGrid<T>
    {
        private T[,] _grid;

        public Grid(int rowCount, int colCount)
        {
            _grid = new T[rowCount, colCount];
        }

        public Grid(T[,] grid)
        {
            _grid = grid;
        }

        public T this[int rowIndex, int columnIndex]
        {
            get => _grid[rowIndex, columnIndex];
            set
            {
                _grid[rowIndex, columnIndex] = value;
            }
        }

        public int RowCount => _grid.GetLength(0);

        public int ColumnCount => _grid.GetLength(1);

        public IEnumerable<IEnumerable<T>> Rows
        {
            get
            {
                for (int r = 0; r < RowCount; r++)
                {
                    yield return GetRow(r);
                }
            }
        }

        public IEnumerable<IEnumerable<T>> Columns
        {
            get
            {
                for (int c = 0; c < ColumnCount; c++)
                {
                    yield return GetColumn(c);
                }
            }
        }

        public IEnumerable<T> GetColumn(int index)
        {
            for (int r = 0; r < RowCount; r++)
            {
                yield return _grid[r, index];
            }
        }

        public IEnumerable<T> GetRow(int index)
        {
            for (int c = 0; c < ColumnCount; c++)
            {
                yield return _grid[index, c];
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _grid.Cast<T>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}