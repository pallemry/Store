using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GraphTest.Base_Classes;

namespace GraphTest.Main_Apps
{
    public partial class Form1 : Form
    {
        public Input Input { get; set; } = new Input(new List<Key>());
        private Input InputPress { get; set; } = new Input(new List<Key>());

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void OpenFileExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            toolStripMenuItem2.Text = folderBrowserDialog1.SelectedPath;
        }

        private void ExportUnderDevToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var g = CreateGraphics();
            g.DrawRectangle(new Pen(Color.Red, 15), 400, 400, 50, 20);
            g.DrawPie(new Pen(color: Color.Blue), new RectangleF(50, 50, 30, 
                30), 150, 30);
            g.Dispose();
            
        }

        /// <summary>
        /// 
        /// </summary>


        /// <summary>
        /// </summary>wwwww
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            var2++;
            toolStripMenuItem1.Text = $"{var1}, {var2}";
            Input.AddEvent(e);
            var s = Input.InputSource.Aggregate("", (current, variable)
                => current + variable.Keytype + " ");
            toolStripMenuItem3.Text = s;
        }
        private int var1, var2;
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            var1++;
            toolStripMenuItem1.Text = $"{var1}, {var2}";
            //InputPress.AddTemporaryEvent(e, Input);
            var s = Input.TempInputSource.Aggregate("", (current, variable)
                => current + variable.epress.KeyChar + " ");
            toolStripMenuItem2.Text = s;
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Input.RemoveEvent(e);
            var s = Input.InputSource.Aggregate("", (current, variable)
                => current + variable.Keytype + " ");
            toolStripMenuItem3.Text = s;
        }
        private void Form1_Leave(object sender, EventArgs e)
        {
            Input.InputSource.Clear();
            InputPress.InputSource.Clear();
            toolStripMenuItem2.Text = "";
            toolStripMenuItem3.Text = "";
        }
    }
    
}