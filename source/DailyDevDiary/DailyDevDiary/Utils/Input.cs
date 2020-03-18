using System;
using System.Collections.Generic;
using System.Text;

namespace DailyDevDiary.Utils
{
	public class Command
	{
		public Options Option { get; }

		public string Description { get; }

		public Command(Options option, string description)
		{
			Option = option;
			Description = description;
		}
	}
	
	public static class Input
	{
		private static readonly Dictionary<string, Command> commands = new Dictionary<string, Command>
		{
			{"t", new Command(Options.Title, "Inserts a document title.")},
			{"s", new Command(Options.Subtitle, "Inserts a subtitle.")},
			{"p", new Command(Options.Paragraph, "Inserts a timestamped paragraph.")},
			{"b", new Command(Options.BlockQuote, "Inserts a blockquote.")},
			{"c", new Command(Options.CodeBlock, "Inserts a block of code") },
			{"o", new Command(Options.OrderedList, "Inserts an ordered list.")},
			{"u", new Command(Options.UnOrderedList, "Inserts an un-ordered list.")},
			{"q", new Command(Options.Quit, "Quits the application.")},
			{"h", new Command(Options.Help, "Shows this list of commands.")}
		};

		private const string optionPrompt = "Please type a letter (h) for help -> ";

		public static Options GetOption()
		{
			var input = GetCharacter(optionPrompt);

			return commands.ContainsKey(input) ? commands[input].Option : Options.Unknown;
		}

		private static string GetCharacter(string prompt)
		{
			Console.Write(prompt);
			var key = Console.ReadKey().KeyChar.ToString().ToLower();
			Console.WriteLine();
			return key;
		}

		public static string GetLine(string prompt)
		{
			while (true)
			{
				Console.Write(prompt);
				var input = Console.ReadLine();
				if (string.IsNullOrWhiteSpace(input))
				{
					Console.WriteLine("Please enter something.");
				}
				else
				{
					return input;
				}
			}
		}

		public static string GetHelp()
		{
			var sb = new StringBuilder();
			
			foreach(var key in commands.Keys)
			{
				sb.AppendLine($"{key}\t{commands[key].Description}");
			}

			return sb.ToString();
		}
	}
}