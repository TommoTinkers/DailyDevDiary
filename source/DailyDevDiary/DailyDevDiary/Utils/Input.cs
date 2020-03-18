using System;

namespace DailyDevDiary.Utils
{
	public enum Options
	{
		Title,
		Unknown
	}
	
	public static class Input
	{
		public static Options GetOption()
		{
			var input = GetCharacter();

			switch (input)
			{
				case "t" :
					return Options.Title;
				default:
					return Options.Unknown;
			}
		}

		public static string GetCharacter()
		{
			return Console.ReadKey().KeyChar.ToString().ToLower();
		}
	}
}