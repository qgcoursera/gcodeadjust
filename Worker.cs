using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace GCodeAdjust
{
    public class Worker
    {
        Adjuster[] Adjusters;
        readonly Regex RexG1;
        readonly string Text;

        public Worker(
            string _Text)
        {
            Text = _Text;
            RexG1 = new Regex(
                @"(^\s*G1)(\s+X\S+)*(\s+Y\S+)*(\s+Z\S+)*",
                RegexOptions.Multiline);
            Adjusters = new[] { Adjuster.Null, Adjuster.Null, Adjuster.Null };
        }

        public string Adjust(
            decimal diffX,
            decimal diffY,
            decimal diffZ)
        {
            Adjusters = new[] { new Adjuster(diffX), new Adjuster(diffY), new Adjuster(diffZ) };

            return RexG1
                .Replace(
                    Text, 
                    MatchEval);
        }

        string MatchEval(
            Match m)
        {
            var replacement =
                (m.Groups[1].Success
                    ? m.Groups[1].Value
                    : string.Empty)
                + string.Join(
                    string.Empty,
                    Adjusters
                        .Select((s, i) =>
                            m.Groups[i + 2].Success
                            ? " "
                                + m.Groups[i + 2].Value.TrimStart()[0]
                                + s.Do(m.Groups[i + 2].Value.TrimStart().Substring(1))
                            : string.Empty));

            return replacement;
        }
    }

    public class Adjuster
    {
        readonly decimal Diff;

        public Adjuster(
            decimal diff)
        {
            Diff = diff;
        }

        public string Do(
            string text)
        {
            return (decimal
                .Parse(text)
                + Diff)
                .ToString();
        }

        public static Adjuster Null
        {
            get
            {
                return new Adjuster(0M);
            }
        }
    }
}