namespace DailyDevDiary.Editing
{
	public static class Markdown
	{
		public static string CreateTitle(string text)
		{
			return $"# {text}";
		}

		public static string CreateSubtitle(string text)
		{
			return $"### {text}";
		}
		
		public static string CreateParagraph(string text)
		{
			return $"{text}\n";
		}

		public static string CreateBlockQuote(string text)
		{
			return $"> {CreateParagraph(text)}";
		}
	}
}