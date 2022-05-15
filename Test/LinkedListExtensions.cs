using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace Test
{
    public static class LinkedListExtensions
    {
        public static int AddNum(this LinkedList<Kid>ll)
        {
            return ll.Count;
        }
        
    }

    public static class Int32Extensions
    {
        private const int ax = 3;
        public static int AddNum(this ref int ll, int ap = ax)
        {
            ll += ap;
            return ll+ap;
        }
        

    }

    public class MyClass : IMTTEST
    {
        
        public virtual void methodG()
        {
            var i = prop;
            prop += i.AddNum(1);
        }

        public int prop { get; set; }
    }
    public class a : MyClass, IMTTEST
    {
        private int _prop;

        public override void methodG()
        {
            _prop = prop;
            _prop.AddNum(2);
            prop = _prop;
            var a = new
            {
                c = "a",
                bx = new LinkedList<IMTTEST>()
                
            };
        }
    }

    public class j : a{}

    public class res : IDisposable
    {

        public void Dispose()
        {
            var c = new MailMessage();
            var x = new Random().ToString();
            Console.Write(x.Length > 2);
        }
    }
    public interface IMTTEST
    {
        int prop { get; set; }
        //void methodG()
    }
}

