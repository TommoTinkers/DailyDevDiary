using System;
using System.IO;

namespace DailyDevDiary
{
	public static class FileUtils
	{
		public static string CreateFilename()
		{
			var todaysDate = DateTime.Now;

			var day = todaysDate.DayOfWeek.ToString();
			var date = todaysDate.Day;
			var year = todaysDate.Year;
			var month = todaysDate.Month;
			return $"{day}_{date}_{month}_{year}.md";
		}

		public static void AppendToFile(string filename, string text)
		{
			File.AppendAllText(filename, text);
		}
	}
}