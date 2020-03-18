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
		private const string paragraphPrompt = "Creating paragraph (Type away!) -> ";
		private const string blockQuotePrompt = "Type an inspiring quote -> ";
		private const string orderedListPrompt = "Please enter an item ('end' to stop) -> ";
		private const string codeBlockPrompt = "Please type some code ->";

		private readonly Dictionary<Options, Action> commandHandlers;

		public Editor(Action<string> write)
		{
			Write = write;
			commandHandlers	= new Dictionary<Options, Action>
			{
				{ Options.Help, HandleHelpCommand},
				{ Options.Paragraph, () => CreateParagraph()},
				{ Options.Subtitle, CreateSubtitle},
				{ Options.Title, CreateTitle},
				{ Options.BlockQuote, CreateBlockQuote},
				{ Options.OrderedList, () =>  CreateList("1.")},
				{ Options.UnOrderedList, () =>  CreateList("*")},
				{ Options.CodeBlock, CreateCodeBlock},
				{ Options.EndDay, CreateEndOfDaySummary}
			};
		}

		public void Start(bool isNewFile)
		{
			if (!isNewFile)
			{
				Console.WriteLine("Continuing where we left off.");
			}
			else
			{
				CreateStartOfDaySummary();
			}
			StartNormalEditingLoop();
		}

		private void StartNormalEditingLoop()
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
						return;
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

		private void CreateCodeBlock()
		{
			WriteLine(Markdown.CreateCodeBlock(Input.GetLine(codeBlockPrompt)));
		}

		private void CreateBlockQuote()
		{
			WriteLine(Markdown.CreateBlockQuote(Input.GetLine(blockQuotePrompt)));
		}

		private void CreateParagraph(bool includeTimestamp = true, string prompt = paragraphPrompt)
		{
			var paragraph = Markdown.CreateParagraph(Input.GetLine(prompt));
			var time = DateTime.Now.ToShortTimeString();
			WriteLine(includeTimestamp ? $"[{time}] {paragraph}" : paragraph);
		}

		private void CreateTitle()
		{
			var title = Markdown.CreateTitle(Input.GetLine(titlePrompt));
			WriteLine(title);
		}

		private void CreateList(string listMarker)
		{
			while (true)
			{
				var item = Input.GetLine(orderedListPrompt);
				if (item.ToLower().Equals("end"))
				{
					break;
				}

				WriteLine($"{listMarker} {item}");
			}
			WriteLine(string.Empty);
			WriteLine(string.Empty);
		}

		private static void HandleUnknownCommand()
		{
			Console.WriteLine("I do not know how to do that.");
		}

		private void WriteLine(string text)
		{
			Write($"{text}\n");
		}

		private void CreateStartOfDaySummary()
		{
			Console.WriteLine("This is a new file. Get ready to enjoy your day!");
			Console.WriteLine("Lets set up the title.");
			CreateTitle();
			CreateParagraph(false, "Write a small introduction");
			Console.WriteLine("Write some goals for the day!");
			commandHandlers[Options.UnOrderedList]();
		}

		private void CreateEndOfDaySummary()
		{
			Console.WriteLine("Well done on completing another day!");
			WriteLine(Markdown.CreateSubtitle("Summary"));
			CreateParagraph(false,"Write a short summary of your day -> ");
			WriteLine(Markdown.CreateSubtitle("Goals for tomorrow"));
			Console.WriteLine("What are your goals for tomorrow?");
			commandHandlers[Options.UnOrderedList]();
			Console.WriteLine("You can continue writing this diary if you want, or use (q) to quit.");
			Console.WriteLine("Enjoy the rest of your day!");
		}
	}
}