using System;
using System.Windows.Forms;

namespace Delegates
{
    public delegate Point Del(Point p1, Point p2);
    public partial class Form1 : Form
    {
        private static Point Add(Point P1, Point P2) => new Point(P1.x + P2.x, P2.y + P1.y);
        private static Point Dev(Point P1, Point P2) => new Point(P1.x / P2.x, P2.y / P1.y);
        private static Point Mul(Point P1, Point P2) => new Point(P1.x * P2.x, P2.y * P1.y);
        private static Point Sub(Point P1, Point P2) => new Point(P1.x - P2.x, P2.y - P1.y);
        private static Point None(Point p, Point d) => throw new ArgumentException("You must choose a valid operation");
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Del operation = comboBox1.Text switch {
                "*" => Mul,
                "+" => Add,
                "-" => Sub,
                "/" => Dev,
                _ => None};
            var p1 = new Point(float.Parse(textBox1.Text), float.Parse(textBox2.Text));
            var p2 = new Point(float.Parse(textBox3.Text), float.Parse(textBox4.Text));
            try 
            { var result = operation(p1, p2); MessageBox.Show($@"{result.x}, {result.y}"); }
            catch (Exception exception) 
            { MessageBox.Show(exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

    public class Point
    {
        public Point(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public float x { get; set; }
        public float y { get; set; }
    }
}
