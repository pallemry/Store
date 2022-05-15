using System.Windows.Forms;

namespace GraphTest.Base_Classes
{
    public class Key
    {
        public KeyEventArgs e { get;  }
        public KeyPressEventArgs epress { get; }
        public bool Pressed { get; set; }
        public Keys Keytype { get; }
        public Key(Keys keytype, bool pressed, KeyEventArgs e = null)
        {
            Keytype = keytype;
            Pressed = pressed;
            if (e == null)
            {
                e = new KeyEventArgs(keytype);
            }
            else
            {
                this.e = e;
            }
        }

        public Key(KeyPressEventArgs e)
        {
            e = new KeyPressEventArgs(e.KeyChar);
            epress = e;
            Pressed = true;
        }
    }
}
