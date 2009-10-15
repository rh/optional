using System;
using Optional.Exceptions;
using Optional.Parsers;
using Xunit;

namespace Optional.Tests
{
	public class ParserFixture
	{
		[Fact]
		public void ShortOptionRegex()
		{
			Assert.True(Parser.ShortOption.IsMatch("-f"));
			Assert.False(Parser.ShortOption.IsMatch("--f"));
			Assert.False(Parser.ShortOption.IsMatch("-foo"));
		}

		[Fact]
		public void LongOptionRegex()
		{
			Assert.True(Parser.LongOption.IsMatch("--foo"));
			Assert.False(Parser.LongOption.IsMatch("-f"));
			Assert.False(Parser.LongOption.IsMatch("-f:"));
			Assert.False(Parser.LongOption.IsMatch("--"));
			Assert.False(Parser.LongOption.IsMatch("-foo"));
		}

		[Fact]
		public void Parse()
		{
			var parser = new Parser();
			parser.Parse<TestOptions>(new[] {"-f", "foo", "-b", "bar"});
		}

		[Fact]
		public void ParseWithLongNames()
		{
			var parser = new Parser();
			try
			{
				parser.Parse<TestOptions>(new[] {"--foo", "foo", "--bar", "bar", "--foo-bar", "foobar"});
			}
			catch (InvalidOptionException e)
			{
				Console.WriteLine(e);
				throw;
			}
			catch (MissingOptionException e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		[Fact]
		public void ParseOptionAndValue()
		{
			var parser = new Parser();
			var options = parser.Parse<TestOptions>(new[] {"-f", "foo"});
			Assert.Equal(options.Foo, "foo");
		}

		[Fact]
		public void ParseValueWithoutOption()
		{
			var parser = new Parser();
			try
			{
				parser.Parse<TestOptions>(new[] {"foo"});
			}
			catch (MissingOptionException)
			{
				return;
			}
			throw new Exception("MissingOptionException was expected, but was not thrown.");
		}

		[Fact]
		public void ParseDuplicateOptionWithValue()
		{
			var parser = new Parser();
			try
			{
				parser.Parse<TestOptions>(new[] {"-f", "foo", "-f"});
			}
			catch (DuplicateOptionException)
			{
				return;
			}
			throw new Exception("DuplicateOptionException was expected, but was not thrown.");
		}

		[Fact]
		public void ParseDuplicateOptionWithoutValue()
		{
			var parser = new Parser();
			try
			{
				parser.Parse<TestOptions>(new[] {"-f", "-f"});
			}
			catch (DuplicateOptionException)
			{
				return;
			}
			throw new Exception("DuplicateOptionException was expected, but was not thrown.");
		}

		[Fact]
		public void ParseInvalidOption()
		{
			var parser = new Parser();
			try
			{
				parser.Parse<TestOptions>(new[] {"-x"});
			}
			catch (InvalidOptionException)
			{
				return;
			}
			throw new Exception("InvalidOptionException was expected, but was not thrown.");
		}

		[Fact]
		public void CreateWithObject()
		{
			const string Value = "This is a pre-set value.";

			var options = new TestOptions {Foo = Value};
			var parser = new Parser();
			parser.Parse(new string[] {}, options);
			Assert.Equal(Value, options.Foo);
			Assert.Null(options.Bar);
			Assert.Null(options.FooBar);
			Assert.False(options.Switch);
		}

		[Fact]
		public void CreateWithObject2()
		{
			var options = new TestOptions();
			var parser = new Parser();
			try
			{
				parser.Parse(new string[] {}, options);
			}
			catch (RequirementException)
			{
				return;
			}
			new Exception("RequirementException was expected, but was not thrown.");
		}

		[Fact]
		public void CreateWithObject3()
		{
			var options = new TestOptions();
			var parser = new Parser();
			parser.Parse(new[] {"--foo", "foo", "-s"}, options);
			Assert.Equal("foo", options.Foo);
			Assert.True(options.Switch, "Switch should be true.");
		}
	}
}