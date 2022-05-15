using System;
using System.Collections.Generic;
using System.Linq;
using Functions.Extensions;
using System.Windows.Forms;
using static System.Console;
#pragma warning disable CS1591
namespace Functions.AdvancedConsole
{

    public static partial class AdvancedConsolePrinter
    {
        public static bool Loaded { get; private set; } = false;

        static AdvancedConsolePrinter()
        {
            if (!Loaded) Load();
        }

        public static void Load()
        {
            if (Loaded) throw new CannotUnloadAppDomainException("The Console printer had already been loaded");
            DefaultFormatters = new List<AdvancedFormatter>
            {
                new AdvancedFormatter("#", DefaultCompletedColor.c, ConsoleColor.Black, FormatterConstruction.Regular),
                new AdvancedFormatter("!", DefaultErrorColor.c, ConsoleColor.Black, fc: FormatterConstruction.Regular),
                new AdvancedFormatter("~", ConsoleColor.Black, DefaultHighlightColor.c, FormatterConstruction.Regular),
                new AdvancedFormatter("`CC", DefCus, ConsoleColor.Black, FormatterConstruction.Regular),
                new AdvancedFormatter("b", ConsoleColor.Blue, ConsoleColor.Black, FormatterConstruction.Scrpting)
            };
            Loaded = true;
        }
        public static List<AdvancedFormatter> DefaultFormatters { get; private set; } = new List<AdvancedFormatter>();
        public static void ResetDefaultFormatters()
        {
            DefaultFormatters.Clear();
            foreach (var color in new List<AdvancedFormatter>
                     {
                         new AdvancedFormatter("#", DefaultCompletedColor.c, ConsoleColor.Black, FormatterConstruction.Regular),
                         new AdvancedFormatter("!", DefaultErrorColor.c, ConsoleColor.Black, fc: FormatterConstruction.Regular),
                         new AdvancedFormatter("~", ConsoleColor.Black, DefaultHighlightColor.c, FormatterConstruction.Regular),
                         new AdvancedFormatter("`CC", DefCus, ConsoleColor.Black, FormatterConstruction.Regular),
                         new AdvancedFormatter("b", ConsoleColor.Blue, ConsoleColor.Black, FormatterConstruction.Scrpting)
                     })
            {
                DefaultFormatters.Add(color);
            }
        }
        public static void Print(string s, AdvancedFormatter acspFormatter, bool newline,
            bool readKey)
        {
            CheckLoaded();
            ForegroundColor = acspFormatter.ForeColor;
            BackgroundColor = acspFormatter.BackColor;
            if (newline) WriteLine(s);
            else Write(s);
            if (readKey) ReadKey(true);
            ResetColor();
        }

        public static void CheckLoaded()
        {
            if (!Loaded)
                throw new TypeLoadException(
                    "Advanced Console Formatter must  be loaded before you can start using it." +
                    " In order to load it call 'AdvancedConsoleFormatter.Load()'.");
        }

        public static void PrintFormat(object? s, bool ln = false, bool rk = false,
            params AdvancedFormatter[] formatters) => PrintFormat(s == null ? "" : s.ToString(), ln, rk,
            formatters);
        public static void PrintFormat(string s, bool ln = false, bool rk = false,
            params AdvancedFormatter[] formatters)
        {
            CheckLoaded();
            formatters ??= Array.Empty<AdvancedFormatter>();
            var v1 = 0;
            var arr = new AdvancedFormatter[formatters.Length+DefaultFormatters.Count];
            formatters = formatters.JoinTogether(DefaultFormatters).ToArray();
            while (!string.IsNullOrEmpty(s))
            {
                v1++;
                var min = s.Length;
                    AdvancedFormatter acspf = null;
                    if (s.StartsWith("$$"))
                    {
                        if (s.StartsWith("$$")) s = s[2..];
                        Print(s, AdvancedFormatter.DefaultFormatter, ln, rk);
                        return;
                    }
                    foreach (var formatter in formatters)
                    {
                        //Print(v1.ToString(), PrintType.Completed, true);
                        var n = s.IndexOf(formatter.StartSeparator, StringComparison.Ordinal);
                        if (n == -1) continue;
                        var m = s.IndexOf(formatter.EndSeparator,
                            formatter.StartSeparator.Length + 1, StringComparison.Ordinal);
                        //Print($"n - {n}\n" + $"m -{m}\n" + $"min -{min}\n" + $"Formatter {formatter.StartSeparator}" + $"s {s}");
                        if (n == -1 || m == -1) continue;
                        if (n >= min) continue;
                        if (n > m) continue;
                        acspf = formatter;
                        min = n;

                    }
                    if (acspf == null)
                    {
                        Print(s, AdvancedFormatter.DefaultFormatter, ln, rk);
                        //Print($"{nameof(acspf)} was null min {min} \nn: {s.IndexOf("<b>", StringComparison.Ordinal)}" + $"\nm: {s.IndexOf("<b>", "<b>".Length, StringComparison.Ordinal)}" + $"\ns: {s}" + $"\ntrue n: {"aaa<b>ello<b>end".IndexOf("<br>", StringComparison.Ordinal)}" );
                        return;
                    }
                    var cs = acspf.StartSeparator;
                    var es = acspf.EndSeparator;
                    var csIndexOriginal = s.IndexOf(cs, StringComparison.Ordinal);
                    switch (csIndexOriginal)
                    {
                        case -1 when acspf.StartSeparator == "":
                            Print(s, acspf, ln, rk);
                            return;
                        case -1:
                            throw new ArgumentException();
                        case 0 when s.IndexOf(es, cs.Length, StringComparison.Ordinal) != 0:
                            s = s.Remove(s.IndexOf(cs), cs.Length);
                            Print(s[..s.IndexOf(es, StringComparison.Ordinal)], acspf, false, false);
                            s = s[(s.IndexOf(es) + es.Length)..];
                            break;
                        default:
                        {
                            Write(s[..csIndexOriginal]);
                            s = s[csIndexOriginal..];
                            break;
                        }
                    }
            }
            if (ln) WriteLine();
            if (rk) ReadKey(false);
        }
    }
}
