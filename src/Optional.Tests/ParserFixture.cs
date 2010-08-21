using System;
using System.Collections.Generic;
using Optional.Parsers;
using Xunit;

namespace Optional.Tests
{
    public class ParserFixture
    {
        [Fact]
        public void ShortOptionWithValueRegex()
        {
            Assert.True(Parser.ShortOptionWithValue.IsMatch("-f:foo"));
            Assert.True(Parser.ShortOptionWithValue.IsMatch("-f:f"));
            Assert.True(Parser.ShortOptionWithValue.IsMatch("-f=foo"));
            Assert.True(Parser.ShortOptionWithValue.IsMatch("-f=f"));
            Assert.False(Parser.ShortOptionWithValue.IsMatch("-f="));
            Assert.False(Parser.ShortOptionWithValue.IsMatch("-f:"));
            Assert.False(Parser.ShortOptionWithValue.IsMatch("-f"));
            Assert.False(Parser.ShortOptionWithValue.IsMatch("--f"));
            Assert.False(Parser.ShortOptionWithValue.IsMatch("-foo"));
        }

        [Fact]
        public void Actions()
        {
            var shorts = 0;
            var longs = 0;
            var values = 0;

            var parser = new Parser
                             {
                                 OnShortOption = name => { shorts++; },
                                 OnLongOption = name => { longs++; },
                                 OnValue = value => { values++; }
                             };

            var args = new[] {"-a", "1", "-b:2", "-c=3", "--aa", "4", "--bb:5", "--cc=6"};
            parser.Parse(args);

            Assert.Equal(3, shorts);
            Assert.Equal(3, longs);
            Assert.Equal(6, values);
        }

        [Fact]
        public void Values()
        {
            var shorts = new List<string>();
            var longs = new List<string>();
            var values = new List<string>();

            var parser = new Parser
                             {
                                 OnShortOption = shorts.Add,
                                 OnLongOption = longs.Add,
                                 OnValue = values.Add
                             };

            var args = new[] {"-0", "0", "-1:1", "-2=2", "--00", "3", "--11:4", "--22=5"};
            parser.Parse(args);

            Assert.Equal(3, shorts.Count);
            Assert.Equal(3, longs.Count);
            Assert.Equal(6, values.Count);

            for (var i = 0; i < shorts.Count; i++)
            {
                Assert.Equal(i.ToString(), shorts[i]);
            }

            for (var i = 0; i < longs.Count; i++)
            {
                Assert.Equal(i.ToString() + i, longs[i]);
            }

            for (var i = 0; i < values.Count; i++)
            {
                Assert.Equal(i.ToString(), values[i]);
            }
        }

        [Fact]
        public void CreateOptions()
        {
            var options = new List<Option>();

            var parser = new Parser
                             {
                                 OnShortOption = name => options.Add(new Option {ShortName = name}),
                                 OnLongOption = name => options.Add(new Option {LongName = name}),
                                 OnValue = value => options[options.Count - 1].AddValue(value)
                             };

            var args = new[] {"-1", "1", "-2:2", "-3=3", "--44", "4", "--55:5", "--66=6"};
            parser.Parse(args);

            Assert.Equal(6, options.Count);
            Assert.Equal("1", options[0].ShortName);
            Assert.Equal("2", options[1].ShortName);
            Assert.Equal("3", options[2].ShortName);
            Assert.Equal("44", options[3].LongName);
            Assert.Equal("55", options[4].LongName);
            Assert.Equal("66", options[5].LongName);

            for (var i = 0; i < options.Count; i++)
            {
                var option = options[i];
                Assert.Equal((i + 1).ToString(), option.Value);
            }
        }

        [Fact]
        public void DuplicateValues()
        {
            var options = new List<Option> {new Option()};
            var parser = new Parser
                             {
                                 OnShortOption = name => { throw new Exception(); },
                                 OnLongOption = name => { throw new Exception(); },
                                 OnValue = value => options[options.Count - 1].AddValue(value)
                             };

            var args = new[] {"1", "2", "3"};
            parser.Parse(args);

            Assert.Equal(1, options.Count);
            Assert.Equal("1", options[0].Value);
            Assert.Equal(3, options[0].Values.Count);
            Assert.Equal("1", options[0].Values[0]);
            Assert.Equal("2", options[0].Values[1]);
            Assert.Equal("3", options[0].Values[2]);
        }
    }
}