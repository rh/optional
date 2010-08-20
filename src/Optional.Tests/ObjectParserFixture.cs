using System;
using Optional.Exceptions;
using Optional.Parsers;
using Xunit;

namespace Optional.Tests
{
    public class ObjectParserFixture
    {
        [Fact]
        public void ShortOptionRegex()
        {
            Assert.True(ObjectParser.ShortOption.IsMatch("-f"));
            Assert.False(ObjectParser.ShortOption.IsMatch("--f"));
            Assert.False(ObjectParser.ShortOption.IsMatch("-f:foo"));
            Assert.False(ObjectParser.ShortOption.IsMatch("-foo"));
        }

        [Fact]
        public void ShortOptionWithValueRegex()
        {
            Assert.True(ObjectParser.ShortOptionWithValue.IsMatch("-f:foo"));
            Assert.True(ObjectParser.ShortOptionWithValue.IsMatch("-f:f"));
            Assert.True(ObjectParser.ShortOptionWithValue.IsMatch("-f=foo"));
            Assert.True(ObjectParser.ShortOptionWithValue.IsMatch("-f=f"));
            Assert.False(ObjectParser.ShortOptionWithValue.IsMatch("-f="));
            Assert.False(ObjectParser.ShortOptionWithValue.IsMatch("-f:"));
            Assert.False(ObjectParser.ShortOptionWithValue.IsMatch("-f"));
            Assert.False(ObjectParser.ShortOptionWithValue.IsMatch("--f"));
            Assert.False(ObjectParser.ShortOptionWithValue.IsMatch("-foo"));
        }

        [Fact]
        public void LongOptionRegex()
        {
            Assert.True(ObjectParser.LongOption.IsMatch("--foo"));
            Assert.False(ObjectParser.LongOption.IsMatch("-f"));
            Assert.False(ObjectParser.LongOption.IsMatch("-f:"));
            Assert.False(ObjectParser.LongOption.IsMatch("--"));
            Assert.False(ObjectParser.LongOption.IsMatch("-foo"));
        }

        [Fact]
        public void LongOptionWithValueRegex()
        {
            Assert.True(ObjectParser.LongOptionWithValue.IsMatch("--foo:bar"));
            Assert.True(ObjectParser.LongOptionWithValue.IsMatch("--foo=bar"));
            Assert.True(ObjectParser.LongOptionWithValue.IsMatch("--foo:x"));
            Assert.True(ObjectParser.LongOptionWithValue.IsMatch("--foo=x"));
            Assert.False(ObjectParser.LongOptionWithValue.IsMatch("--foo:"));
            Assert.False(ObjectParser.LongOptionWithValue.IsMatch("--foo"));
            Assert.False(ObjectParser.LongOptionWithValue.IsMatch("-f"));
            Assert.False(ObjectParser.LongOptionWithValue.IsMatch("-f:"));
            Assert.False(ObjectParser.LongOptionWithValue.IsMatch("--"));
            Assert.False(ObjectParser.LongOptionWithValue.IsMatch("-foo"));
        }

        [Fact]
        public void Parse()
        {
            var parser = new ObjectParser();
            parser.Parse<TestOptions>(new[] {"-f", "foo", "-b", "bar"});
        }

        [Fact]
        public void ParseWithLongNames()
        {
            var parser = new ObjectParser();
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
            var parser = new ObjectParser();
            var options = parser.Parse<TestOptions>(new[] {"-f", "foo"});
            Assert.Equal(options.Foo, "foo");
        }

        [Fact]
        public void ParseValueWithoutOption()
        {
            var parser = new ObjectParser();
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
            var parser = new ObjectParser();
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
            var parser = new ObjectParser();
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
            var parser = new ObjectParser();
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
            var parser = new ObjectParser();
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
            var parser = new ObjectParser();
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
            var parser = new ObjectParser();
            parser.Parse(new[] {"--foo", "foo", "-s"}, options);
            Assert.Equal("foo", options.Foo);
            Assert.True(options.Switch, "Switch should be true.");
        }
    }
}