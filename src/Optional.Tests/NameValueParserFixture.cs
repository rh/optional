using System;
using Optional.Exceptions;
using Optional.Parsers;
using Xunit;

namespace Optional.Tests
{
    public class NameValueParserFixture
    {
        [Fact]
        public void CreateListOfOptions()
        {
            var parser = new NameValueParser();
            var options = parser.Parse(new[] {"-f", "foo", "-b", "bar", "--baz"});

            Assert.NotEmpty(options);
            Assert.Equal(3, options.Count);

            Assert.True(options.ContainsKey("f"));
            Assert.Equal("foo", options["f"]);

            Assert.True(options.ContainsKey("b"));
            Assert.Equal("bar", options["b"]);

            Assert.True(options.ContainsKey("baz"));
            Assert.Equal(null, options["baz"]);
        }

        [Fact]
        public void CreateListOfOptionsWithoutOption()
        {
            var parser = new NameValueParser();
            try
            {
                parser.Parse(new[] {"foo"});
            }
            catch (MissingOptionException)
            {
                return;
            }
            throw new Exception("MissingOptionException was expected, but was not thrown.");
        }
    }
}