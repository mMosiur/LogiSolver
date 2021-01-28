using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LogiSolver.Core
{
	public partial class Puzzle
	{
		/// <summary>
		/// The class that enables serialization of <see cref="Puzzle"/> class
		/// </summary>
		public class BinarySerializer : IBinarySerializer<Puzzle>
		{
			/// <summary>
			/// Serializes given <paramref name="puzzle"/> data to a <see langword="byte[]"/> array
			/// </summary>
			/// <param name="puzzle">The puzzle to serialize</param>
			/// <returns>Serialized data of given puzzle</returns>
			public byte[] Serialize(Puzzle puzzle)
			{
				List<byte> bytes = new List<byte> { (byte)'l', (byte)'o', (byte)'g', (byte)'i' };
				bytes.Add((byte)puzzle.RowCount);
				bytes.Add((byte)puzzle.ColumnCount);
				bytes.AddRange(puzzle.Grid.Select(cell => (byte)cell));
				foreach (List<ushort> clueList in puzzle.RowClues)
				{
					bytes.Add((byte)clueList.Count);
					bytes.AddRange(clueList.Select(x => (byte)x));
				}
				foreach (List<ushort> clueList in puzzle.ColumnClues)
				{
					bytes.Add((byte)clueList.Count);
					bytes.AddRange(clueList.Select(x => (byte)x));
				}
				return bytes.ToArray();
			}

			/// <summary>
			/// Deserializes given <see langword="byte[]"/> array to an object of <see cref="Puzzle"/> class
			/// </summary>
			/// <param name="bytes">The data to be deserialized</param>
			/// <returns>Deserialized <see cref="Puzzle"/> object, <see langword="null"/> if an error occured</returns>
			public Puzzle Deserialize(byte[] bytes)
			{
				using BinaryReader reader = new BinaryReader(new MemoryStream(bytes));
				try
				{
					if (string.Join("", reader.ReadBytes(4).Select(x => (char)x)) != "logi")
					{
						return null;
					}
					byte rowCount = reader.ReadByte();
					byte colCount = reader.ReadByte();
					if (rowCount * colCount == 0)
					{
						return null;
					}
					CellState[,] grid = new CellState[rowCount, colCount];
					for (int row = 0; row < rowCount; row++)
					{
						for (int col = 0; col < colCount; col++)
						{
							grid[row, col] = (CellState)reader.ReadByte();
						}
					}
					List<ushort>[] rowClues = new List<ushort>[rowCount];
					for (int i = 0; i < rowClues.Length; i++)
					{
						byte clueCount = reader.ReadByte();
						rowClues[i] = new List<ushort>(reader.ReadBytes(clueCount).Select(x => (ushort)x));
					}
					List<ushort>[] columnClues = new List<ushort>[colCount];
					for (int i = 0; i < columnClues.Length; i++)
					{
						byte clueCount = reader.ReadByte();
						columnClues[i] = new List<ushort>(reader.ReadBytes(clueCount).Select(x => (ushort)x));
					}
					return reader.PeekChar() != -1
						? null
						: new Puzzle
						{
							Grid = new Grid<CellState>(grid),
							RowClues = rowClues,
							ColumnClues = columnClues
						};
				}
				catch (Exception)
				{
					return null;
				}
			}
		}
	}
}