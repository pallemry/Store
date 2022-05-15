using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Windows.Forms;
using GraphTest.Extensions;
using Timer = System.Timers.Timer;

namespace GraphTest.Base_Classes
{
    public class Input
    {
        public readonly IList<Key> InputSource;
        public readonly IList<Key> TempInputSource = new List<Key>();
        private List<InputTimer> timers = new List<InputTimer>();
        public Input(IList<Key> inputSource)
        {
            InputSource = inputSource;
        }

        public Key this[int i]
        {
            get
            {
                try
                {
                    return InputSource[i];
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            set 
            {
                try
                { 
                    InputSource[i] = value;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }


        public bool IsKeyDown(Keys e)
        {
            InputSource.AddKeyEvent(new KeyEventArgs(e));
            return InputSource.Any(v => v.Keytype == e && v.Pressed);
        }

        public bool AddEvent(KeyEventArgs e)
        {
            var c = e.KeyCode.ToString().Length > 1 ? ' ' : e.KeyCode.ToString()[0];
            AddTemporaryEvent(new KeyPressEventArgs(c), this);
            return InputSource.AddKeyEvent(e);
        }

        public bool RemoveEvent(KeyEventArgs e)
        {
            return InputSource.RemoveKeyEvent(e);
        }


        public void AddTemporaryEvent(KeyPressEventArgs e, Input i)
        {
            var k = new Key(e);
            var x = e.KeyChar.ToString().ToUpper()[0];
            // ReSharper disable once BetterName
            if (InputSource.Any(key => key.Keytype.ToString()[0] == x &&
                                         key.Keytype.ToString().Length == 1))
            {
               return;
            }
            TempInputSource.Add(k);
            var t = new InputTimer(5);
            t.keyPressed = k;
            t.Start();
            t.Elapsed += TimerElapsed;
            timers.Add(t);
        }

        private void TimerElapsed(object? sender, ElapsedEventArgs e)
        {
            var s = sender as InputTimer;
            TempInputSource.RemoveKeyEvent(s.keyPressed);
            s.Stop();
            //MessageBox.Show(@"stopped");
        }

        class InputTimer : Timer
        {
            public Key keyPressed;
            
            public InputTimer()
            {
                keyPressed = new Key(Keys.None, true);
            }
            public InputTimer(double i) : base(i)
            {
                keyPressed = new Key(Keys.None, true);
            }
            public InputTimer(Key k)
            {
                keyPressed = k;
            }
            public InputTimer(double i, Key k) : base(i)
            {
                keyPressed = k;
            }
        }
    }
}
