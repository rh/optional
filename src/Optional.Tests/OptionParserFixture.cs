using System;
using Optional.Parsers;
using Xunit;

namespace Optional.Tests
{
    public class OptionParserFixture
    {
        [Fact]
        public void Test()
        {
            const string Value = "value";

            var obj = new TestOptions {Foo = Value, Bar = Value, FooBar = Value, Switch = true};
            var options = Options.Create(obj);

            Assert.Equal(4, options.Count);

            foreach (var option in options)
            {
                switch (option.ShortName)
                {
                    case "f":
                        Assert.Equal("foo", option.LongName);
                        Assert.Equal("Sets foo", option.Description);
                        Assert.Equal(typeof(string), option.Type);
                        Assert.True(option.Required);
                        break;
                    case "b":
                        Assert.Equal("bar", option.LongName);
                        Assert.Equal(string.Empty, option.Description);
                        Assert.Equal(typeof(string), option.Type);
                        Assert.False(option.Required);
                        break;
                    case "r":
                        Assert.Equal("foo-bar", option.LongName);
                        Assert.Equal(string.Empty, option.Description);
                        Assert.Equal(typeof(string), option.Type);
                        Assert.False(option.Required);
                        break;
                    case "s":
                        Assert.Equal("switch", option.LongName);
                        Assert.Equal(string.Empty, option.Description);
                        Assert.Equal(typeof(bool), option.Type);
                        Assert.False(option.Required);
                        break;
                    default:
                        throw new InvalidOperationException("Unexpected option: " + option.ShortName);
                }
            }
        }
    }
}