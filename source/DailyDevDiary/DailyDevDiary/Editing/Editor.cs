using System;
using DailyDevDiary.Utils;

namespace DailyDevDiary.Editing
{
	public class Editor
	{
		private readonly Action<string> Write;

		public Editor(Action<string> write)
		{
			Write = write;
		}

		public void Start()
		{
			var option = Input.GetOption();
			Write(option.ToString());
		}

		private void WriteLine(string text)
		{
			Write($"{text}\n");
		}
	}
}