using System;
using System.Drawing;
using Dyes.Commands;
using Moq;
using Xunit;

namespace Dyes.Tests
{
    public class CommandLineParserTest
    {
        public class VersionCmd
        {
            [Theory]
            [InlineData("version")]
            [InlineData("--version")]
            [InlineData("-v")]
            public void GivenMatchingInput_ReturnsVersionCmd(params string[] args)
            {
                var parser = new CommandLineParser();

                var cmd = parser.Parse(args);

                Assert.IsType<Commands.VersionCmd>(cmd);
            }

            [Theory]
            [InlineData("verion")]
            [InlineData("--v")]
            [InlineData("-ver")]
            [InlineData("-version")]
            public void GivenWrongInput_ThrowsArgumentException(params string[] args)
            {
                var parser = new CommandLineParser();

                Assert.Throws<ArgumentException>(() => parser.Parse(args));
            }
        }

        public class HelpCmd
        {
            [Theory]
            [InlineData("help")]
            [InlineData("--help")]
            [InlineData("-h")]
            public void GivenMatchingInput_ReturnsHelpCmd(params string[] args)
            {
                var parser = new CommandLineParser();

                var cmd = parser.Parse(args);

                Assert.IsType<Commands.HelpCmd>(cmd);
            }

            [Theory]
            [InlineData("hel")]
            [InlineData("--h")]
            [InlineData("-help")]
            public void GivenWrongInput_ThrowsArgumentException(params string[] args)
            {
                var parser = new CommandLineParser();

                Assert.Throws<ArgumentException>(() => parser.Parse(args));
            }
        }

        public class CheckTrueColorSupportCmd
        {
            [Theory]
            [InlineData("check-truecolor-support")]
            public void GivenMatchingInput_ReturnsCheckTrueColorSupportCmd(params string[] args)
            {
                var parser = new CommandLineParser();

                var cmd = parser.Parse(args);

                Assert.IsType<CheckTrueColorSupport>(cmd);
            }
        }


        public class ViewCmd
        {
            [Theory]
            [InlineData("view", "#fff")]
            public void GivenMatchingInput_ReturnsViewCmd(params string[] args)
            {
                var colorParser = new ColorParser();
                var parser = new CommandLineParser(colorParser);

                var cmd = parser.Parse(args);

                Assert.IsType<Commands.ViewCmd>(cmd);
                Assert.Equal(Color.White.ToArgb(), ((Commands.ViewCmd) cmd).Color.ToArgb());
            }
        }

        public class CopyCmd
        {
            [Theory]
            [InlineData("copy", "#fff")]
            public void GivenMatchingInput_ReturnsCopyCmd(params string[] args)
            {
                var colorParser = new ColorParser();
                var parser = new CommandLineParser(colorParser);

                var cmd = parser.Parse(args);

                Assert.IsType<Commands.CopyCmd>(cmd);
                Assert.Equal(Color.White.ToArgb(), ((Commands.CopyCmd) cmd).Color.ToArgb());
            }
        }

        public class ConvertCmd
        {
            [Theory]
            [InlineData("convert", "#fff", "hex")]
            [InlineData("convert", "#fff", "rgb")]
            [InlineData("convert", "#fff", "hsl")]
            [InlineData("convert", "#fff", "hsluv")]
            [InlineData("convert", "#fff", "hpluv")]
            public void GivenMatchingInput_ReturnsConvertCmd(params string[] args)
            {
                var colorParser = new ColorParser();
                var parser = new CommandLineParser(colorParser);

                var cmd = parser.Parse(args);

                Assert.IsType<Commands.ConvertCmd>(cmd);
                Assert.Equal(Color.White.ToArgb(), ((Commands.ConvertCmd) cmd).Color.ToArgb());
                Assert.IsAssignableFrom<ColorNotation>(((Commands.ConvertCmd) cmd).ColorNotation);
            }

            [Theory]
            [InlineData("convert", "#ffff", "hex")]
            [InlineData("convert", "#fff", "hexa")]
            [InlineData("convert", "#fff", "rbb")]
            [InlineData("convert", "#fff", "hsp")]
            public void GivenBadInput_ThrowsArgumentException(params string[] args)
            {
                var colorParser = new ColorParser();
                var parser = new CommandLineParser(colorParser);

                Assert.Throws<ArgumentException>(() => parser.Parse(args));
            }
        }
    }
}