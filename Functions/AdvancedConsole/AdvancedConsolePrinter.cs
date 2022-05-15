using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Windows.Forms;
using Functions.Extensions;
using static System.Console;
#pragma warning disable CS1591
namespace Functions
{
    
}
namespace Functions.AdvancedConsole
{
    /// <summary>
    /// This class contains can help with easier advanced printing <see cref="Int32.MaxValue">MAX</see>
    /// Note that here you can also use 
    /// <see href="https://docs.microsoft.com/dotnet/api/system.int32.maxvalue"/>
    /// </summary>
    public static partial class AdvancedConsolePrinter
    {
        private static ColorKeeper DefaultErrorColor { get; set; } = new ColorKeeper(ConsoleColor.Red);
        private static ColorKeeper DefaultCompletedColor { get; set; } = new ColorKeeper(ConsoleColor.Green);
        private static ColorKeeper DefaultHighlightColor { get; set; } = new ColorKeeper(ConsoleColor.Yellow);
        private static ColorKeeper DefaultCustomColor { get; set; } = new ColorKeeper(ConsoleColor.White);
        private static readonly List<ColorKeeper> Colors = new List<ColorKeeper>
        {
            DefaultErrorColor, DefaultCompletedColor, DefaultHighlightColor, DefaultCustomColor
        };
        private static List<ConsoleColor> SavedColors = new List<ConsoleColor>();

        public static ConsoleColor DefErr
        {
            get => DefaultErrorColor.c;
            set => DefaultErrorColor.c = value;
        }
        public static ConsoleColor DefCom
        {
            get => DefaultCompletedColor.c;
            set => DefaultCompletedColor.c = value;
        }
        public static ConsoleColor DefHigh
        {
            get => DefaultHighlightColor.c;
            set => DefaultHighlightColor.c = value;
        }
        public static ConsoleColor DefCus
        {
            get => DefaultCustomColor.c;
            set => DefaultCustomColor.c = value;
        }

        public enum PrintType
        {
            Error,
            Completed,
            HighLight,
            NotificationDone,
            NotificationError,
            Notification,
            Custom,
            None
        }
        /// <include file='C:\Users\yisha\Downloads\Store Solution\Functions\Functions.xml' path='MyDocs//MyMembers[@name="test"]'/>
        public static void Print(string s, PrintType pt = PrintType.None, bool newLine = false)
        {
            // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
            switch (pt)
            {
                case PrintType.NotificationDone:
                    MessageBox.Show(s, @"Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                case PrintType.NotificationError:
                    MessageBox.Show(s, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                case PrintType.Notification:
                    MessageBox.Show(s, @"Message", MessageBoxButtons.OK);
                    return;
                default:
                    ForegroundColor = pt switch
                    {
                        PrintType.Error => DefaultErrorColor.c,
                        PrintType.HighLight => DefaultHighlightColor.c,
                        PrintType.Completed => DefaultCompletedColor.c,
                        PrintType.Custom => DefaultCustomColor.c,
                        _ => ConsoleColor.White
                    };
                    break;
            }
            
            if (newLine) WriteLine(s);
            else Write(s);
            ForegroundColor = ConsoleColor.White;
        }
        private static bool ReCompileString(ref string s, string func, 
             ref PrintType pt, PrintType desiredPrintType, bool ln = false)
        {
            if (!s.StartsWith(func) || !s.EndsWith(func)) return false;
            pt = desiredPrintType;
            s = "`" + s + "`";
            s = s.Replace("`"+func, "");
            s = s.Replace(func+"`", "");
            return true;

        }
        private static void ReCompile(string s, PrintType pt, bool ln = false)
        {
            if (testing)
                WriteLine($@"
s from ReCompile: ""{s}""");
            ReCompileString(ref s, "CC", ref pt, PrintType.Custom);
            ReCompileString(ref s, "MSE", ref pt, PrintType.NotificationError);
            ReCompileString(ref s, "MSD", ref pt, PrintType.NotificationDone);
            
            Print(s, pt, ln);
        }
        private const bool testing = false;
        public static void Println(string s, PrintType pt = PrintType.None) => Print(s, pt, true);
        public static void PrintQ(int num, ConsoleColor quesColor = ConsoleColor.Yellow, 
            ConsoleColor numColor = ConsoleColor.Yellow, ConsoleColor marksColor = ConsoleColor.Yellow)
        {
            //SaveColors();
            DefaultHighlightColor.c = quesColor;
            DefCom = marksColor;
            DefaultErrorColor.c = numColor;
            PrintBasicFormat($"\n#|======|# ~Question~ !#{num}! #|======|#\n", true, DefCus, true);
            WriteColors();
        }

        public static void PrintE(ConsoleColor expColor = ConsoleColor.White,
            ConsoleColor marksColor = ConsoleColor.White, bool readKey = false)
        {
            
            //SaveColors();
            DefaultHighlightColor.c = expColor;
            DefaultErrorColor.c = marksColor;
            PrintBasicFormat("\n!|========|! |~Examples~| !|========|!\n", true, DefaultCustomColor.c, readKey);
            WriteColors();
        }
        /// <summary>
        ///     <para>
        ///         Used To print with special cases
        ///     </para>
        ///     <para>
        ///         <code></code>
        ///         <example>
        ///             For Example: 
        ///             <code>
        ///                 <paramref name="s"/> = `2`
        ///             </code>
        ///             This will result in "s" being sent as a message box.
        ///         </example>
        ///     </para>
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The following <c>Syntax</c> to the method is as following:
        ///         <list type="bullet">
        ///             <item>
        ///                 <term><c>!<paramref name="s"/>!</c></term>
        ///                 <desc>Prints <paramref name="s"/> as an error with the specified <see cref="DefaultErrorColor"/></desc>
        ///             </item>
        ///             <item>
        ///                 <term><c>#<paramref name="s"/>#</c></term>
        ///                 <desc>Prints <paramref name="s"/> as completed with the specified <see cref="DefaultCompletedColor"/></desc>
        ///             </item>
        ///             <item>
        ///                 <term><c>~<paramref name="s"/>~</c></term>
        ///                 <desc>Prints <paramref name="s"/> as mention with the specified <see cref="DefaultHighlightColor"/></desc>
        ///             </item>
        ///             <item>
        ///                 <term><c>!<paramref name="s"/>!</c></term>
        ///                 <desc>Normal Message box containing <paramref name="s"/></desc>
        ///             </item>
        ///             <item>
        ///                 <term><c>`MSE..<paramref name="s"/>..MSE`</c></term>
        ///                 <desc>
        ///                     MSE at the start and end means Message Box
        ///                     <br></br>Error and you can use it to display normal errors
        ///                 </desc>
        ///             </item>
        ///             <item>
        ///                 <term><c>`MSD..<paramref name="s"/>..MSD`</c></term>
        ///                 <desc>
        ///                     MSD at the start and end means Message Box
        ///                     <br></br>Done and you can use it to display normal information to the user
        ///                 </desc>
        ///             </item>
        ///             <item>
        ///                 <term><c>`CC..<paramref name="s"/>..CC`</c></term>
        ///                 <desc>
        ///                     Prints as the color specified in <see cref="DefaultCustomColor"/>
        ///                 </desc>
        ///             </item>
        ///         </list>
        ///     </para>
        ///     <br>
        ///         <c><see langword = "IMPORTANT "/>:</c>
        ///         <br></br>Please Note: In case <c><paramref name="s"/></c> starts with <c>'$$'</c> the different syntax and
        ///         functions are <c><see langword="IGNORED"/></c> and the<br></br> text will be printed as
        ///         it was was in the parameter <paramref name="s"/>
        ///         without any special cases nor colors
        ///     </br>
        /// </remarks>
        /// <param name="s"></param>
        /// <param name="ln"></param>
        public static void PrintBasicFormat([NotNull] string s, bool ln = false, ConsoleColor customColor = ConsoleColor.White, bool ReadKey = false)
        {
            var color = DefaultCustomColor;
            DefCus = customColor;
            if (s.StartsWith("$$"))
            {
                Print(s[2..]);
                return;
            }
            var a = s;
            var var1 = 0;
            var indexOfError = new IndexPrint(PrintType.Error, s.IndexOf('!'), '!');
            var indexOfCompleted = new IndexPrint(PrintType.Completed, s.IndexOf('#'), '#');
            var indexOfHighlight = new IndexPrint(PrintType.HighLight, s.IndexOf('~'), '~');
            var indexOfMessageDone = new IndexPrint(PrintType.Notification, s.IndexOf('`'), '`');
            var indexes = new List<IndexPrint>
            {
                indexOfError,indexOfCompleted, indexOfHighlight, indexOfMessageDone
            };
            while (s.Length > 0)
            {
                var1++;
                if (var1 > a.Length) break;
                try
                {
                    var min = s.Length;
                    IndexPrint ip = null;
                    foreach (var eIndex in indexes)
                    {

                        var n = s.IndexOf(eIndex.Identifier);
                        if (n == -1 || n > min) continue;
                        ip = eIndex;
                        min = n;
                    }
                    if (ip == null)
                    {
                        Print(s);
                        break;
                    }
                    if (s.IndexOf(ip.Identifier, 1) != -1)
                    {
                        if (s.IndexOf(ip.Identifier) == 0)
                        {
                            var ind = s.IndexOf(ip.Identifier);
                            ReCompile(s.SubstringInd(1, s.IndexOf(ip.Identifier, 1)), ip.PrintType);
                            s = s[s.IndexOf(ip.Identifier, ind + 1)..];
                            s = s[1..];
                        }
                        else
                        {
                            bool b;
                            try
                            {
                                var substringInd = s.SubstringInd(0, s.IndexOf(ip.Identifier, 1));
                                b = true;
                            }
                            catch (Exception)
                            {
                                b = false;
                            }
                            if (b)
                            {
                                ReCompile(s.SubstringInd(0, s.IndexOf(ip.Identifier)), PrintType.None);
                                s = s[s.IndexOf(ip.Identifier)..];
                            }
                        }
                    }
                    foreach (var i in indexes)
                        i.UpdateIndex(s);
                    if (testing)
                        WriteLine($@"
s so far: ""{s}""");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
            if (ln) WriteLine();
            DefaultCustomColor = color;
            if (!ReadKey) return;
            Console.ReadKey(true);
            
        }
        private class IndexPrint
        {
            public IndexPrint(PrintType printType, int index, char identifier)
            {
                PrintType = printType;
                Index = index;
                Identifier = identifier;
            }
            public void UpdateIndex(string s)
            {
                Index = s.IndexOf(Identifier);
            }
            public char Identifier { get; set; }
            public int Index { get; set; }
            public PrintType PrintType { get; set; }

        }
        /// <summary>
        /// Resets the default color for the different <c><see cref="PrintType"/></c> passed in the
        /// <c><see cref="Print"/> </c>method
        /// </summary>
        public static void ResetColors()
        {
            DefaultErrorColor.c = ConsoleColor.Red;
            DefaultHighlightColor.c = ConsoleColor.Yellow;
            DefaultCompletedColor.c = ConsoleColor.Green;
        }

        //private static void SaveColors()
        //{
        //    foreach (var color in Colors.Where(color => color != null))
        //    {
        //        SavedColors.Add(color.c);
        //    }
        //}
        
        private static void WriteColors()
        {
            for (var i = 0; i < SavedColors.Count; i++)
            {
                Colors[i].c = SavedColors[i];
            }
           
            SavedColors.Clear();
        }
        
    }
    public class ColorKeeper
    {
        public ConsoleColor c;

        public ColorKeeper(ConsoleColor c)
        {
            this.c = c;
        }
    }
}
