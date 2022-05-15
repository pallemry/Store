using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace store.Main
{
    partial class Form1
    {

        private static void CheckId(string text)
        {
            text = text.Trim();
            if (text.Length != 9 && AreWeTesting)
            {
                throw new InvalidDataException("ID cannot be less than 9 characters!");
            }
            if (text.Any(character => !"1234567890".Contains(character)))
            {
                throw new InvalidDataException("id can only contain numbers");
            }
        }

        private void Reset()
        {
            Has_Club_Card.Visible = false;
            WorkerOptions.Visible = false;
            button2.Location = new Point( 32, 268);
            CreatePasswordTB.Text = "Enter Password";
            CreateIDTB.Text = "Create ID";
            CreateIDTB.ForeColor = Color.Gray;
            CreatePasswordTB.ForeColor = Color.Gray;
            SetMainGroupBoxVisible(groupBox1);
            EnterPasswordLogInTB.ForeColor = Color.Gray;
            EnterPasswordLogInTB.Text = "Enter Password";
            PassAvalibleButton.Visible = true;
            button4.Visible = false;
            EnterPasswordLogInTB.UseSystemPasswordChar = false;
            Has_Club_Card.Checked = false;
            groupBox1.Location = new Point(158, 140);
            comboBox1.SelectedItem = null;
            newFirstName.Text = defaultFirstName;
            LastName.Text = defaultLastName;
            CellPhone.Text = defaultCellphone;
            idinput.Text = defaultID;
            idinput.ForeColor = Color.Gray;
            newFirstName.ForeColor = Color.Gray;
            CellPhone.ForeColor = Color.Gray;
            LastName.ForeColor = Color.Gray;
            Salary.Text = "";
            Workhours.Text = "";
            groupbox.Visible = false;
        }
        private static void InitializeTbAutoCompletion(AutoCompleteStringCollection ascs, params TextBox[] a)
        {
            if (a.Length < 1) throw new InvalidDataException("Could not initialize to text boxes");
            foreach (var variable in a)
            {
                variable.AutoCompleteSource = AutoCompleteSource.CustomSource;
                variable.AutoCompleteMode = AutoCompleteMode.Suggest;
                variable.AutoCompleteCustomSource = ascs;
            }
        }
        /// <summary>
        /// This Functions checks for a valid name, and if a Control is given it also
        /// Checks that it's not the default color which in that case it can throw an exception.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="max"></param>
        /// <param name="tb"></param>
        /// <param name="addition"></param>
        /// <returns>
        /// Void / <see cref="InvalidDataException"/> in case the input does not correspond to the
        /// given arguments limitations.
        /// </returns>
        private static void CheckName(string text, int max = 10, Control tb = null, string addition = "")
        {
            text = text.Trim();
            if (tb.ForeColor == Color.Gray)  throw new InvalidDataException($"{addition} name cant be empty"); 
            if (string.IsNullOrEmpty(text)) throw new InvalidDataException($"{addition} name cant be empty");
            if (tb != null && text.Length > max) throw new InvalidDataException($"{addition} name is too long");
        }
        private static bool IsNumberInRange(int value, int max, int min)
        {
            return value <= max && value >= min;
        }
        private void idinput_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(idinput.Text)) return;
            idinput.ForeColor = Color.Gray;
            idinput.Text = "Enter ID";
        }
        private void idinput_Enter(object sender, EventArgs e)
        {
            if (idinput.ForeColor != Color.Gray) return;
            idinput.Text = "";
            idinput.ForeColor = Color.Black;
        }
        private void CreateIDTB_Enter(object sender, EventArgs e)
        {
            if (CreateIDTB.ForeColor != Color.Gray) return;
            CreateIDTB.Text = "";
            CreateIDTB.ForeColor = Color.Black;
        }

        private void CreateIDTB_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CreateIDTB.Text)) return;
            CreateIDTB.ForeColor = Color.Gray;
            CreateIDTB.Text = "Enter ID";
        }

        private void CreatePasswordTB_Enter(object sender, EventArgs e)
        {
            if (CreatePasswordTB.ForeColor != Color.Gray) return;
            CreatePasswordTB.Text = "";
            CreatePasswordTB.ForeColor = Color.Black;
        }

        private void CreatePasswordTB_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CreatePasswordTB.Text)) return;
            CreatePasswordTB.ForeColor = Color.Gray;
            CreatePasswordTB.Text = "Enter Password";
        }
        private void CellPhone_Enter(object sender, EventArgs e)
        {
            if (CellPhone.ForeColor != Color.Gray) return;
            CellPhone.Text = "";
            CellPhone.ForeColor = Color.Black;
        }
        private void CellPhone_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CellPhone.Text)) return;
            CellPhone.ForeColor = Color.Gray;
            CellPhone.Text = "Enter Cellphone";
        }
        private void LastName_Enter(object sender, EventArgs e)
        {
            if (LastName.ForeColor != Color.Gray) return;
            LastName.Text = "";
            LastName.ForeColor = Color.Black;
        }
        private void LastName_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(LastName.Text)) return;
            LastName.ForeColor = Color.Gray;
            LastName.Text = @"Enter Lastname";
        }
        private void newFirstName_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(newFirstName.Text)) return;
            newFirstName.ForeColor = Color.Gray;
            newFirstName.Text = @"Enter Firstname";
        }

        private void EnterPasswordLogInTB_Enter(object sender, EventArgs e)
        {
            if (EnterPasswordLogInTB.ForeColor != Color.Gray) return;
            EnterPasswordLogInTB.Text = "";
            EnterPasswordLogInTB.ForeColor = Color.Black;
        }

        private static string GetRandomPassword(int numOfChars, 
            string stringsToChooseFrom = "qwertyuiopasdfghjklzxcvbnm`1234567890-=[]{};?/!@#$%^&*(),<>")
        {
            var sb = new StringBuilder();
            var r = new Random();
            for (var i = 0; i < numOfChars; i++)
            {
                var chartoadd = stringsToChooseFrom.Substring(r.Next(stringsToChooseFrom.Length), 1).ToCharArray()[0];
                chartoadd = (char)(chartoadd >= 97 && chartoadd <= 122  &&  r.Next(2) == 1 ?  chartoadd - 32 : chartoadd); 
                sb.Append(chartoadd);
            }
            return sb.ToString();
        }

        private void EnterPasswordLogInTB_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(EnterPasswordLogInTB.Text)) return;
            EnterPasswordLogInTB.ForeColor = Color.Gray;
            EnterPasswordLogInTB.Text = "Enter Password";
        }
        private void newFirstName_Enter(object sender, EventArgs e)
        {
            if (newFirstName.ForeColor != Color.Gray) return;
            newFirstName.Text = "";
            newFirstName.ForeColor = Color.Black;
        }

        
    }
}
