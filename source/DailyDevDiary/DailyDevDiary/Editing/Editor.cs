using System;
using System.Collections.Generic;
using DailyDevDiary.Utils;

namespace DailyDevDiary.Editing
{
	public class Editor
	{
		private readonly Action<string> Write;
		
		private const string titlePrompt = "Please enter a title -> ";
		private const string subtitlePrompt = "Please enter a subtitle -> ";
		private const string paragraphPrompt = "Creating paragraph (Type away!)-> ";
		
		private readonly Dictionary<Options, Action> commandHandlers;
		
		public Editor(Action<string> write)
		{
			Write = write;
			commandHandlers	= new Dictionary<Options, Action>
			{
				{ Options.Help, HandleHelpCommand},
				{ Options.Paragraph, CreateParagraph},
				{ Options.Subtitle, CreateSubtitle},
				{ Options.Title, CreateTitle}
			};
		}

		public void Start()
		{
			while (true)
			{
				var option = Input.GetOption();

				if (commandHandlers.ContainsKey(option))
				{
					commandHandlers[option]();
				}
				
				switch (option)
				{
					case Options.Unknown:
						HandleUnknownCommand();
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