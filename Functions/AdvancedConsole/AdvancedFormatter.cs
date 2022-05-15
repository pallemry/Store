using System;
using System.Diagnostics.CodeAnalysis;
using static Functions.Calculator.BasicCalculations;
namespace Functions.AdvancedConsole
{
    /// <summary>
    /// 
    /// </summary>
    public enum FormatterConstruction
    {
        Scrpting,
        Regular
    }
    public class AdvancedFormatter
    {
        private readonly FormatterConstruction fc;
        public static readonly AdvancedFormatter DefaultFormatter =
            new AdvancedFormatter
            {
                BackColor = ConsoleColor.Black,
                ForeColor = ConsoleColor.White,
                StartSeparator = string.Empty
            };

        public ConsoleColor ForeColor
        {
            get; set;
        }
        public ConsoleColor BackColor { get; set; }

        public string StartSeparator
        {
            get => (fc == FormatterConstruction.Scrpting ? GetHtmlRepresentation(_startSeparator) : _startSeparator);
            set => _startSeparator = value;
        }

        private string _endOperator;
        private string _startSeparator;

        /// <summary>
        /// nice val
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        [NotNull]
        public string EndSeparator
        {
            get => (fc == FormatterConstruction.Regular ? _endOperator ?? _startSeparator : 
                    GetHtmlRepresentation(_startSeparator, true));
            set
            {
                if (fc == FormatterConstruction.Scrpting)
                    throw new ArgumentException("You can't set a scripting default to custom ending because it is already </{start}>" +
                                                $"For Example: {this}");
                _endOperator = value;
            }
        }

        private AdvancedFormatter(string startSeparator, ConsoleColor foreColor, ConsoleColor backColor, 
            FormatterConstruction fc, string endOperator)
        {
            _startSeparator = startSeparator ?? throw new ArgumentNullException();
            ForeColor = foreColor;
            BackColor = backColor;
            this.fc = fc;
            _endOperator = endOperator ?? startSeparator;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startSeparator"></param>
        /// <param name="foreColor"></param>
        /// <param name="backColor"></param>
        /// <param name="endSeperator"></param>
        public AdvancedFormatter(string startSeparator, ConsoleColor foreColor = ConsoleColor.White,
            ConsoleColor backColor = ConsoleColor.Black, string endSeperator = null) : this(startSeparator, 
            foreColor, backColor, FormatterConstruction.Regular, endSeperator ?? startSeparator)
        {
        }

        /// <summary>
        /// This one can use html and xml formatting.
        /// </summary>
        /// <param name="startSeparator"></param>
        /// <param name="foreColor"></param>
        /// <param name="backColor"></param>
        /// <param name="endSeperator"></param>
        /// <param name="fc"></param>
        public AdvancedFormatter(string startSeparator, ConsoleColor foreColor = ConsoleColor.White,
            ConsoleColor backColor = ConsoleColor.Black, FormatterConstruction fc = FormatterConstruction.Regular)
            : this(startSeparator, foreColor, backColor, fc, startSeparator)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetRawStartSeparator()=> _startSeparator;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetRawEndSeparator() => _endOperator;
        /// <summary>
        /// 
        /// </summary>
        public AdvancedFormatter()
        {
            var a = new Random().Next(2) == 0 ? null : "a";
            fc = FormatterConstruction.Regular;
        }
        /// <summary>
        /// <inheritdoc cref="object.ToString"/>
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"fc: {ForeColor}, " +
                                             $"bc: {BackColor}, ss: \"{StartSeparator}\", es: \"{EndSeparator}\" + ss(raw):" +
                                             $" \"{_startSeparator}\" es(raw): \"{_endOperator ?? "null"}\"";
        /// <summary>
        /// Compares two<see cref="AdvancedFormatter"/>(s) returns the <b>result</b>
        /// <list type="bullet">
        /// <item>
        /// <term><paramref name="a"/> <b>AND</b> <paramref name="b"/> </term>
        /// are both <see langword = "null"></see>
        /// </item>
        /// <br/><br><br><para></para></br></br><b>OR</b>
        /// <item>
        /// <term><paramref name="a"/> <b>AND</b> <paramref name="b"/> </term>
        /// both have the same <b><see cref="BackColor"/></b>, <b><see cref="ForeColor"/></b>
        /// , <b><see cref="FormatterConstruction"/></b> , <b><see cref="StartSeparator"/></b>
        /// , <b><see cref="EndSeparator"/></b> <see langword = "Values"/>
        /// </item>
        /// </list>
        /// Otherwise <see langword = "false"/>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>The <b>result</b> of the comparision specified in the criteria above ^</returns>
        [Obsolete]
        public static bool operator ==(AdvancedFormatter a, AdvancedFormatter b)
        {
            if (a is null && b is null)
                return true;
            if (a is null || b is null)
                return false;
            return a.BackColor == b.BackColor && a.StartSeparator == b.StartSeparator &&
                   a.EndSeparator == b.EndSeparator &&
                   a.ForeColor == b.ForeColor && a.fc == b.fc;
        }
        /// <summary>
        /// Compares two<see cref="AdvancedFormatter"/>(s) returns the <b>result</b>
        /// <br></br><br></br>Returns <see langword= "true"/> in case:
        /// <list type="bullet">
        /// <item>
        /// <term><paramref name="a"/> <b>OR</b> <paramref name="b"/> </term>
        /// in case <paramref name="a"/> or <paramref name="b"/> are <see langword = "null"/> and the other one is not <see langword = "null"/>
        /// </item>
        /// <br/><br><br><para></para></br></br><b>OR</b> if they are both <b>not </b>- <see langword = "null"/>
        /// <item>
        /// <term><paramref name="a"/> <b>AND</b> <paramref name="b"/> </term>
        /// both have different <b><see cref="BackColor"/></b>, <b><see cref="ForeColor"/></b>
        /// , <b><see cref="FormatterConstruction"/></b> , <b><see cref="StartSeparator"/></b>
        /// , <b><see cref="EndSeparator"/></b> <see langword = "Values"/>
        /// </item>
        /// </list>
        /// Otherwise <see langword = "false"/>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>The <b>result</b> of the comparision specified in the criteria above ^
        /// <seealso cref="AdvancedConsolePrinter"/></returns>
        public static bool operator !=(AdvancedFormatter a, AdvancedFormatter b)
        {
            return !(a == b);
        }
    }
}
