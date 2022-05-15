using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using store.UserTypes;
namespace Test
{
    public class Tes : IComparer<Tes>, IComparable<string>, IComparable<Tes>
    {

        public Tes(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public int Compare(Tes x, Tes y)
        {
            return string.Compare(x?.Name, y?.Name, StringComparison.Ordinal);
        }

        public int CompareTo(string s)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(Tes other)
        {
            throw new NotImplementedException();
        }
    }
    public delegate string Del2();
    public delegate bool Del(object o);
    public static class ExtraLinkedList
    {
        public static void k<T>(this LinkedList<T> s){}
    }

    public delegate void kk();
    public class Kid : Person
    {
        private kk kkk;
        private LinkedList<a> aa = new LinkedList<a>();
        public enum Gender
        {
            Male,
            Female,
            Unsure
        }

        private Del del;
        public int age;
        public string name;
        public Color skin;
        public Gender gender;
        private int v = 3;

        public string x() { aa.k(); return "false"; }
        public string y() { return "wdawdw"; }
        protected virtual unsafe void VIRMETH(ref int* a) { VIRMETH(ref a); var bark = a->GetType(); age.AddNum(2); }

        public Kid(int age, [NotNull] string nam, bool isBlack, Gender g, string FirstName = "", string Lastname = "")
            : base(0, nam, nam, "", "")
        {
            Console.CursorSize = Convert.ToInt32(MathF.PI);
            this.age = age;
            name = nam;
            gender = g;
            skin = isBlack ? Color.Black : Color.White;
            var t = new Test();
            var k = t[2];
        }

        ~Kid()
        {
            Console.WriteLine(skin.Name.EnumerateRunes().GetEnumerator().Count().ToString());
        }
    }

    internal class Test : ArrayList
    {
        private static ArrayList k = new ArrayList();
        
        private object r { get; set; }
        private IndexerNameAttribute l = new IndexerNameAttribute("A");
        private static List<int> a = new List<int>
        {
            22, 2, 2, 2, 2
        };
        [IndexerName("A")]
        public override object? this[int index]
        {
            get => Int32.MaxValue;
            set => r = value;
        }
        //private static async Task Main(string[] args)
        //{
        //    var x = k[2];   
        //    a.ForEach(Console.WriteLine);
        //    var kid = new Kid(1, "E", false, Kid.Gender.Female);
        //    var s = a.Aggregate((s1, i) => s1 + i);
        //    var r1 = new Random(1);
        //    var r2 = new Random(1);
        //    Console.WriteLine(@"Hello World!");
        //    List<Kid> myKids = new List<Kid>();

        //    Del2 m = kid.y;
        //    Del2 r = kid.y;
        //    r += kid.x;
        //    m += kid.y + r;
            
        //    Console.WriteLine(m());
        //    AddNum(15, myKids, new Kid(10, "michal", true,
        //        Kid.Gender.Male));
        //    await SynMethod();

        //    for (var i = 0; i < myKids.Count; i++)
        //    {
        //        Kid k = myKids[i];
        //        Console.WriteLine($@"({i}) skin color: {k.skin} age: {k.age} name: {k.name} gender: {k.gender}");
        //    }

        //    Console.Read();
        //}

        private static Task SynMethod()
        {
            var objects = new List<object>(89);
            if (objects == null) throw new ArgumentNullException(nameof(objects));
            for (var i = 0; i < 500; i++)
            {
                objects.Add(i);
                var t = i.GetType() == typeof(a);
                return t ? Task.CompletedTask : new Task(p, CancellationToken.None, TaskCreationOptions.HideScheduler);
            }
            return Task.CompletedTask;
        }

        private static void p(){}
        

        private static void AddNum(int numOfElementsToAd, List<Kid> list, Kid k)
        {
            for (var i = 0; i < numOfElementsToAd; i++)
            {
                list.Add(new Kid(k.age + i * 2, k.name,
                    new Random().Next(2) == 0,
                          new Random().Next(2) == 0 ? Kid.Gender.Male : Kid.Gender.Female));
            }
        }
    }
}