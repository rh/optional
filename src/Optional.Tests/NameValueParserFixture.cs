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
            var options = parser.Parse(new[] {"-f", "foo", "-b", "bar", "--baz", "-x", "--last", "-y:z", "-z=y", "--abc:abcdefgh", "--def=x", "value1", "value2"});

            Assert.NotEmpty(options);
            Assert.Equal(11, options.Count);

            Assert.Equal("f", options[0].ShortName);
            Assert.Equal(string.Empty, options[0].LongName);
            Assert.Equal("foo", options[0].Value);

            Assert.Equal("b", options[1].ShortName);
            Assert.Equal(string.Empty, options[1].LongName);
            Assert.Equal("bar", options[1].Value);

            Assert.Equal(string.Empty, options[2].ShortName);
            Assert.Equal("baz", options[2].LongName);
            Assert.Equal(string.Empty, options[2].Value);

            Assert.Equal("x", options[3].ShortName);
            Assert.Equal(string.Empty, options[3].LongName);
            Assert.Equal(string.Empty, options[3].Value);

            Assert.Equal(string.Empty, options[4].ShortName);
            Assert.Equal("last", options[4].LongName);
            Assert.Equal(string.Empty, options[4].Value);

            Assert.Equal("y", options[5].ShortName);
            Assert.Equal(string.Empty, options[5].LongName);
            Assert.Equal("z", options[5].Value);

            Assert.Equal("z", options[6].ShortName);
            Assert.Equal(string.Empty, options[6].LongName);
            Assert.Equal("y", options[6].Value);

            Assert.Equal(string.Empty, options[7].ShortName);
            Assert.Equal("abc", options[7].LongName);
            Assert.Equal("abcdefgh", options[7].Value);

            Assert.Equal(string.Empty, options[8].ShortName);
            Assert.Equal("def", options[8].LongName);
            Assert.Equal("x", options[8].Value);

            Assert.Equal(string.Empty, options[9].ShortName);
            Assert.Equal(string.Empty, options[9].LongName);
            Assert.Equal("value1", options[9].Value);

            Assert.Equal(string.Empty, options[10].ShortName);
            Assert.Equal(string.Empty, options[10].LongName);
            Assert.Equal("value2", options[10].Value);
        }

        [Fact]
        public void CreateOneOptionWithoutValue()
        {
            var parser = new NameValueParser();
            var options = parser.Parse(new[] {"foo"});

            Assert.NotEmpty(options);
            Assert.Equal(1, options.Count);

            Assert.True(options[0].ShortName == string.Empty);
            Assert.True(options[0].LongName == string.Empty);
            Assert.Equal("foo", options[0].Value);
        }

        [Fact]
        public void CreateTwoOptionWithoutValues()
        {
            var parser = new NameValueParser();
            var options = parser.Parse(new[] {"foo", "bar"});

            Assert.NotEmpty(options);
            Assert.Equal(2, options.Count);

            Assert.True(options[0].ShortName == string.Empty);
            Assert.True(options[0].LongName == string.Empty);
            Assert.Equal("foo", options[0].Value);

            Assert.True(options[1].ShortName == string.Empty);
            Assert.True(options[1].LongName == string.Empty);
            Assert.Equal("bar", options[1].Value);
        }
    }
}