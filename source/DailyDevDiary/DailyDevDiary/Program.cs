using System;
using System.IO;
using DailyDevDiary.Editing;

namespace DailyDevDiary
{
	internal static class Program
	{
		public static void Main()
		{
			var filename = FileUtils.CreateFilename();
			Action<string> fileWriter = text => { FileUtils.AppendToFile(filename, text); };

			DisplayWelcome(filename);
			new Editor(fileWriter).Start(!File.Exists(filename));
		}

		private static void DisplayWelcome(string filename)
		{
			Console.WriteLine($"DailyDevDiary - Editing file {filename}");
		}
	}
}