using System;
using System.Collections.Generic;
using System.IO;

namespace LogiSolver.Core
{
	public partial class Puzzle
	{
		/// <summary>
		/// The class that enables saving and loading of objects of <see cref="Puzzle"/> class
		/// </summary>
		public static class Manager
		{
			#region Private Members

			private static string directory = null;

			#endregion Private Members

			#region Public Properties

			/// <summary>
			/// The path to the directory where puzzles are loaded from and saved to.
			/// Setting the directory creates path as needed.
			/// </summary>
			public static string Directory
			{
				get => directory ??= "puzzles";
				set
				{
					try
					{
						DirectoryInfo dir = new DirectoryInfo(value);
						if (!dir.Exists)
							dir.Create();
						directory = value;
					}
					catch (Exception) { }
				}
			}

			#endregion Public Properties

			#region Private Methods

			/// <summary>
			/// Loads a puzzle from a file with a given path
			/// </summary>
			/// <param name="filename">A path to file with a puzzle</param>
			/// <returns>New puzzle loaded from file. Null if there has been an error.</returns>
			private static Puzzle LoadFromFile(string filename)
			{
				try
				{
					FileInfo file = new FileInfo(filename);
					byte[] bytes = File.ReadAllBytes(file.FullName);
					Puzzle puzzle = Serializer.DecodeFromBytes(bytes);
					file.LastAccessTimeUtc = DateTime.UtcNow;
					return puzzle;
				}
				catch (Exception)
				{
					return null;
				}
			}

			#endregion Private Methods

			#region Public Methods

			/// <summary>
			/// Loads a puzzle with a given name.
			/// The file to be loaded is the file in <see cref="Directory"/> with ".logi" extension.
			/// </summary>
			/// <param name="name">The name of the puzzle to load</param>
			/// <returns>New puzzle loaded from file, <see langword="null"/> if there has been an error.</returns>
			public static Puzzle Load(string name)
			{
				string path = Directory + name + ".logi";
				return LoadFromFile(path);
			}

			/// <summary>
			/// Loads a puzzle that has been accessed most recently.
			/// </summary>
			/// <returns>New puzzle that has been accessed recently, <see langword="null"/> if there has been an error.</returns>
			public static Puzzle LoadLast()
			{
				DirectoryInfo dir = new DirectoryInfo(Directory);
				EnumerationOptions options = new EnumerationOptions { RecurseSubdirectories = true };
				var files = new List<FileInfo>(dir.GetFiles(Directory + "*.logi", options));
				files.Sort((x, y) => x.LastAccessTimeUtc.CompareTo(y.LastAccessTimeUtc));
				return LoadFromFile(files[0].FullName);
			}

			/// <summary>
			/// Saves given <paramref name="puzzle"/> in a file given in a <paramref name="filepath"/>.
			/// </summary>
			/// <param name="puzzle">The puzzle to be saved</param>
			/// <param name="filepath">The path to the file, where the puzzle should be saved</param>
			/// <returns>Whether the puzzle has been succesfully saved.</returns>
			public static bool Save(Puzzle puzzle, string filepath)
			{
				try
				{
					DirectoryInfo dir = new FileInfo(filepath).Directory;
					if (!dir.Exists)
						dir.Create();

					File.WriteAllBytes(filepath, Serializer.EncodeToBytes(puzzle));
				}
				catch (Exception)
				{
					return false;
				}
				return true;
			}

			#endregion Public Methods
		}
	}
}