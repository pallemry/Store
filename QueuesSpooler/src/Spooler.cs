using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using Functions.Extensions;
using static Functions.AdvancedConsole.AdvancedConsolePrinter;
using Timer = System.Timers.Timer;

namespace QueuesSpooler
{
    public class Spooler
    {
        public int Code { get; private set; }

        public Spooler(int code)
        {
            Code = code;
            LongFilesToPrint = new Queue<FileToPrint>();
            ShortFilesToPrint = new Queue<FileToPrint>();
        }


        public bool Printing { get; private set; }
        private int i = 0;
        private void TOnElapsed(object sender, ElapsedEventArgs e)
        {
            i++;
            Printing = true;
            if (i >= 100)
            {
                PrintFormat("\n#Completed# ~Printing Successfully~\n", false, false);
                Printing = false;
                t?.Stop();
                t = null;
                i = 0;
                return;
            }
            PrintBasicFormat($"#|#");
        }

        public void AddFile(string name, LOrS ls, TimeSpan ts, int code, long fileSize, DateTime dt)
        {
            if (ShortFilesToPrint.JoinTogether(LongFilesToPrint).FirstOrDefault(f => f.Code == code) != null)
                throw new ArgumentException($"File with the code {code} already exists");
            var f = new FileToPrint(name, ls, ts, code, fileSize, dt);
            switch (ls)
            {
                case LOrS.Long:
                    LongFilesToPrint.Enqueue(f);
                    break;
                case LOrS.Short:
                    ShortFilesToPrint.Enqueue(f);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(ls), ls, null);
            }
            AllFiles.Enqueue(f);
            SortFiles();
        }

        public void AddFile(FileToPrint f)
        {
            switch (f.LS)
            {
                case LOrS.Long:
                    LongFilesToPrint.Enqueue(f);
                    break;
                case LOrS.Short:
                    ShortFilesToPrint.Enqueue(f);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(f), f, null);
            }
            AllFiles.Enqueue(f);
            SortFiles();
        }

        private bool LongPrint;
        private Timer t = null;
        public FileToPrint Print(double speed)
        {
            if (speed < 0) throw new ArgumentException("Speed must be 0 or positive");
            var p = NormalPrint();
            var m = p.Span.TotalMilliseconds /100;
            LongPrint = m > 1000;
            Printing = true;
            t = new System.Timers.Timer(m  / speed);
            PrintFormat($"<b>Starting to Print</b> #file#: \n!{p}!\n #(Press# !CTL! #+# !C! #to exit completely at any time)#\n", false, false);
            t.Elapsed += TOnElapsed;
            t.Start();
            while (Printing){}
            Console.ReadKey(true);
            return p;
        }

        public FileToPrint NormalPrint()
        {
            var trash = ShortFilesToPrint.Count == 0 ? LongFilesToPrint.Dequeue() : ShortFilesToPrint.DequeueLast();
            return AllFiles.Dequeue();
        }

        public override string ToString()
        {
            if (!AnyFilesLeft()) return "";
            var res = "   L/S   |    Printing Time    |   File Size   |       Span       |    Name    |    Code    \n\n";
            foreach (var file in AllFiles)
            {
                res += $"{file.LS.ToString().LimitSizeMiddle("   L/S   ".Length)}|{file.EnteredTime.ToString(CultureInfo.CurrentCulture).LimitSizeMiddle("    Printing Time    ".Length)}" +
                       $"|{file.FileSize.ToString().LimitSizeMiddle("   File Size   ".Length)}|" +
                       $"{file.Span.ToString().LimitSizeMiddle("       Span       ".Length)}|{file.Name.LimitSizeMiddle("    Name    ".Length)}|" +
                       $"{file.Code.ToString().LimitSizeMiddle("    Code    ".Length)}\n";
            }
            return res;
        }

        public Queue<FileToPrint> AllFiles { get; set; } = new Queue<FileToPrint>();
        private Queue<FileToPrint> LongFilesToPrint { get; set; }
        private Queue<FileToPrint> ShortFilesToPrint { get; set; }

        public int FilesLeftCount()
        {
            return LongFilesLeft() + ShortFilesLeft();
        }

        public int LongFilesLeft()
        {
            return LongFilesToPrint.Count;
        }

        public int ShortFilesLeft()
        {
            return ShortFilesToPrint.Count;
        }

        public bool AnyFilesLeft()
        {
            return FilesLeftCount() != 0;
        }

        public void SortFiles()
        {
            var temp = AllFiles.ToList();
            temp.Sort();
            var fileToPrints = new Queue<FileToPrint>();
            foreach (var f in temp)
            {
                fileToPrints.Enqueue(f);
            }
            AllFiles = fileToPrints;
        }

        
    }
}