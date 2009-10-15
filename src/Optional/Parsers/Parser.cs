using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Optional.Exceptions;

namespace Optional.Parsers
{
	public class Parser
	{
		public static Regex ShortOption = new Regex("^-[a-zA-Z0-9]{1}$");
		public static Regex LongOption = new Regex("^--[-a-zA-Z0-9]{1,}$");

		private IList<Option> availableOptions = new List<Option>();
		private readonly IList<Option> setOptions = new List<Option>();

		public T Parse<T>(string[] args, T options)
		{
			availableOptions = Options.Create(options);
			Option current = null;

			for (var i = 0; i < args.Length; i++)
			{
				var arg = args[i];
				if (ShortOption.IsMatch(arg))
				{
					var name = arg.Substring(1);

					var option = FindByShortName(setOptions, name);
					if (option != null)
					{
						throw new DuplicateOptionException(option);
					}

					option = FindByShortName(availableOptions, name);
					if (option == null)
					{
						throw new InvalidOptionException(name);
					}

					// TODO: duplicate code
					if (option.Type == typeof(bool))
					{
						option.Property.SetValue(options, true, null);
					}

					current = option;
					setOptions.Add(current);
					availableOptions.Remove(current);
				}
				else if (LongOption.IsMatch(arg))
				{
					var name = arg.Substring(2);

					var option = FindByLongName(setOptions, name);
					if (option != null)
					{
						throw new DuplicateOptionException(option);
					}

					option = FindByLongName(availableOptions, name);
					if (option == null)
					{
						throw new InvalidOptionException(name);
					}

					// TODO: duplicate code
					if (option.Type == typeof(bool))
					{
						option.Property.SetValue(options, true, null);
					}

					current = option;
					setOptions.Add(current);
					availableOptions.Remove(current);
				}
				else
				{
					if (current == null)
					{
						throw new MissingOptionException(arg);
					}
					current.Value = arg;
					current.Property.SetValue(options, arg, null);
					current = null;
				}
			}

			CheckRequiredOptions(options);
			return options;
		}

		public T Parse<T>(string[] args) where T : new()
		{
			return Parse(args, new T());
		}

		private void CheckRequiredOptions<T>(T options)
		{
			foreach (var option in availableOptions)
			{
				var value = option.Property.GetValue(options, null);
				if (option.Required && (value == null || String.IsNullOrEmpty(value.ToString())))
				{
					throw new RequirementException(option);
				}
			}

			foreach (var option in setOptions)
			{
				if (option.Required && String.IsNullOrEmpty(option.Value))
				{
					throw new RequirementException(option);
				}
			}
		}

		private static Option FindByShortName(IEnumerable<Option> options, string shortName)
		{
			foreach (var option in options)
			{
				if (option.ShortName.Equals(shortName))
				{
					return option;
				}
			}
			return null;
		}

		private static Option FindByLongName(IEnumerable<Option> options, string longName)
		{
			foreach (var option in options)
			{
				if (option.LongName.Equals(longName))
				{
					return option;
				}
			}
			return null;
		}
	}
}