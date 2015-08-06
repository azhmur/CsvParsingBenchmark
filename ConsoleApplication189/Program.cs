using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using BenchmarkDotNet;
using BenchmarkDotNet.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Sprache;

namespace ConsoleApplication189
{
    [BenchmarkTask(warmupIterationCount: 0, targetIterationCount: 1, processCount: 1, jitVersion: BenchmarkJitVersion.HostJit)]
    public class Program
    {
        static readonly Parser<char> CellSeparator = Parse.Char(',');

        static readonly Parser<char> QuotedCellDelimiter = Parse.Char('"');

        static readonly Parser<char> QuoteEscape = Parse.Char('"');

        static Parser<T> Escaped<T>(Parser<T> following)
        {
            return from escape in QuoteEscape
                   from f in following
                   select f;
        }

        static readonly Parser<char> QuotedCellContent =
            Parse.AnyChar.Except(QuotedCellDelimiter).Or(Escaped(QuotedCellDelimiter));

        static readonly Parser<char> LiteralCellContent =
            Parse.AnyChar.Except(CellSeparator).Except(Parse.String(Environment.NewLine));

        static readonly Parser<string> QuotedCell =
            from open in QuotedCellDelimiter
            from content in QuotedCellContent.Many().Text()
            from end in QuotedCellDelimiter
            select content;

        static readonly Parser<string> NewLine =
            Parse.String(Environment.NewLine).Text();

        static readonly Parser<string> RecordTerminator =
            Parse.Return("").End().XOr(
            NewLine.End()).Or(
            NewLine);

        static readonly Parser<string> Cell =
            QuotedCell.XOr(
            LiteralCellContent.XMany().Text());

        static readonly Parser<IEnumerable<string>> Record =
            from leading in Cell
            from rest in CellSeparator.Then(_ => Cell).Many()
            from terminator in RecordTerminator
            select Cons(leading, rest);

        static readonly Parser<IEnumerable<IEnumerable<string>>> Csv =
            Record.XMany().End();

        static IEnumerable<T> Cons<T>(T head, IEnumerable<T> rest)
        {
            yield return head;
            foreach (var item in rest)
                yield return item;
        }

        private const string source = @"123,2.99, AMO024, Title,""Description, more info"",,123987564";
        private static readonly Regex regex =
                new Regex(@"
        # Parse CVS line. Capture next value in named group: 'val'
        \s*                      # Ignore leading whitespace.
        (?:                      # Group of value alternatives.
          ""                     # Either a double quoted string,
          (?<val>                # Capture contents between quotes.
            [^""]*(""""[^""]*)*  # Zero or more non-quotes, allowing 
          )                      # doubled "" quotes within string.
          ""\s*                  # Ignore whitespace following quote.
        |  (?<val>[^,]*)         # Or... zero or more non-commas.
        )                        # End value alternatives group.
        (?:,|$)                  # Match end is comma or EOS",
        RegexOptions.IgnorePatternWhitespace);

        private static CsvConfiguration csvParserConfiguration = new CsvConfiguration()
        {
            HasHeaderRecord = false
        };
        
        static void Main(string[] args)
        {
            var res = regex.Matches(source);

            var comp = new BenchmarkCompetitionSwitch(new[] { typeof(Program) });
            comp.Run(args);
        }

        [Benchmark]
        public void Regexp()
        {
            regex.Matches(source).Cast<Match>().Select(x => x.Groups[2].Value).ToArray();
        }

        [Benchmark]
        public void Split()
        {
            source.Split(',');
        }

        [Benchmark]
        public void Sprache()
        {
            Csv.Parse(source).ToArray();
        }

        [Benchmark]
        public void CsvHelper()
        {
            // NOTE: CsvParser requires TextReader on creation, so we have to create it from scratch every iteration
            using (var reader = new StringReader(source))
            {
                new CsvParser(reader, csvParserConfiguration).Read();
            }
        }
    }
}
