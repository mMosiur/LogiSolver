using System;
using System.Collections.Generic;

namespace LogiSolver.Core
{
    public interface IGrid<T> : IEnumerable<T>
    {
        public int RowCount { get; }
        public int ColumnCount { get; }

        public T this[int rowIndex, int columnIndex] { get; }

        public IEnumerable<IEnumerable<T>> Rows { get; }

        public IEnumerable<IEnumerable<T>> Columns { get; }

        public IEnumerable<T> GetRow(int index);

        public IEnumerable<T> GetColumn(int index);
    }
}