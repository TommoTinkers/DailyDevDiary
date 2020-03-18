using System;
using DailyDevDiary.Utils;

namespace DailyDevDiary.Editing
{
	public class Editor
	{
		private readonly Action<string> Write;
		
		private const string titlePrompt = "Please enter a title -> ";
		private const string subtitlePrompt = "Please enter a subtitle -> ";
		private const string paragraphPrompt = "Creating paragraph (Type away!)-> ";
		
		public Editor(Action<string> write)
		{
			Write = write;
		}

		public void Start()
		{
			while (true)
			{
				var option = Input.GetOption();

				switch (option)
				{
					case Options.Help:
						HandleHelpCommand();
						break;
					case Options.Unknown:
						HandleUnknownCommand();
						break;
					case Options.Title:
						CreateTitle();
						break;
					case Options.Subtitle:
						CreateSubtitle();
						break;
					case Options.Paragraph:
						CreateParagraph();
						break;
					case Options.Quit:
					{
						return;
					}
				}
			}
		}

		private void HandleHelpCommand()
		{
			Console.Write(Input.GetHelp());
		}

		private void CreateSubtitle()
		{
			var subtitle = Markdown.CreateSubtitle(Input.GetLine(subtitlePrompt));
			WriteLine(subtitle);
		}

		private void CreateParagraph()
		{
			var paragraph = Markdown.CreateParagraph(Input.GetLine(paragraphPrompt));
			var time = DateTime.Now.ToShortTimeString();
			WriteLine($"[{time}] {paragraph}");
		}

		private void CreateTitle()
		{
			var title = Markdown.CreateTitle(Input.GetLine(titlePrompt));
			WriteLine(title);
			return;
		}

		private static void HandleUnknownCommand()
		{
			Console.WriteLine("I do not know how to do that.");
			return;
		}

		private void WriteLine(string text)
		{
			Write($"{text}\n");
		}
	}
}