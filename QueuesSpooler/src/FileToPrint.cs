using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Functions.Extensions;

namespace QueuesSpooler
{
    public enum LOrS
    {
        Long,
        Short
    }

    public class FileToPrint : IComparable<FileToPrint>
    {
        public FileToPrint(string name, LOrS ls, TimeSpan ts, int code, long fileSize, DateTime dt)
        {
            if (code < 0  || fileSize < 0)
                throw new ArgumentOutOfRangeException("All numbers cannot be negative",
                    new ArgumentException());
            Span = ts;
            FileSize = fileSize;
            Code = code;
            LS = ls;
            Name = name;
            EnteredTime = dt;
        }
        public FileToPrint(string name, LOrS ls, TimeSpan ts, int code, long fileSize)
        {
            if (code < 0 || fileSize < 0)
                throw new ArgumentOutOfRangeException("All numbers cannot be negative",
                    new ArgumentException());
            Span = ts;
            FileSize = fileSize;
            Code = code;
            LS = ls;
            Name = name;
            EnteredTime = DateTime.Now;
        }

        public TimeSpan Span { get; set; }
        public string Name { get; }
        public LOrS LS { get; }
        public int Code { get; }
        public long FileSize { get; }
        public DateTime EnteredTime { get; set; }

        public int CompareTo(FileToPrint other)
        {
            if (other == null) return 1;
            return LS.CompareTo(other.LS) != 0 ? LS.CompareTo(other.LS) : 
                EnteredTime.CompareTo(other.EnteredTime) != 0 ? EnteredTime.CompareTo(other.EnteredTime) :
                FileSize.CompareTo(other.FileSize) != 0 ? FileSize.CompareTo(other.FileSize) : 
                    Span.CompareTo(other.Span) !=0 ? Span.CompareTo(other.Span) : string.Compare(Name, other.Name, StringComparison.Ordinal) != 0 ?
                        string.Compare(Name, other.Name, StringComparison.Ordinal) : Code.CompareTo(other.Code);
        }

        public override string ToString()
        {
            return $"ls: {LS} | dt: {EnteredTime} | fs: {FileSize} | ts: {Span} | n : {Name} | c: {Code} ";
        }
    }
}
