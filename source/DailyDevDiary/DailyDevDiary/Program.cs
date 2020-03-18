using System;
using DailyDevDiary.Editing;

namespace DailyDevDiary
{
	internal static class Program
	{
		public static void Main(string[] args)
		{
			var filename = FileUtils.CreateFilename();
			Action<string> fileWriter = text => { FileUtils.AppendToFile(filename, text); };

			new Editor(fileWriter).Start();
		}
	}
}