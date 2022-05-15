using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using store.ShopManagement;
using store.UserTypes;
using static store.ShopManagement.Shop;

namespace store.Main
{
    
    public enum UserType {client,seller, manager, notchosen }
    public partial class Form1 : Form
    {
        private User user = new User();
        private const bool AreWeTesting = false;
        private readonly AutoCompleteStringCollection _acsc = new AutoCompleteStringCollection();
        private int _currentid;
        private string _currpass="";
        public readonly string defaultLastName = "Enter Lastname", defaultFirstName = "Enter Firstname",
            defaultCellphone = "Enter Cellphone", defaultID = "Enter ID";
        private UserType ut= UserType.notchosen;
        /// <summary>
        /// This function sets all the group boxes to their correct position
        /// <see cref="Client"/>
        /// <code>This looks like code</code> and this doesnt
        /// </summary>
        private void SetLocationsToDefault()
        {
            RemoveItemCodeTB.AutoCompleteCustomSource = _acsc;
            ItemCodeBox.AutoCompleteCustomSource = _acsc;
            ItemCodeClinetTextBox.AutoCompleteCustomSource = _acsc;
            ItemcodeSellertextbox.AutoCompleteCustomSource = _acsc;
            WorkerOptions.Location = new Point(5, 265);
            Has_Club_Card.Location = new Point(5, 272);
            groupBox1.Location = new Point(194, 140);
            groupbox.Location = new Point(194, 54);
            PassAvalibleButton.Location = new Point(174, 55);
            button4.Location = PassAvalibleButton.Location;
            ClientOptions.Location = new Point(245, 84);
            SellerOptions.Location = new Point(245, 84);
            DiscountOptions.Location = new Point(9, 78);
            ManagerGroupBox.Location = new Point(245, 84);
            WorkSalaryGroupBox.Location = new Point(9, 83);
            AddItemGroupBox.Location = new Point(9,83);
            RemoveItemGroupBox.Location = new Point(9, 230);
            label18.Text = label19.Text = NumberFormatInfo.CurrentInfo.CurrencySymbol;
            InitializeTbAutoCompletion(_acsc , RemoveItemCodeTB, ItemcodeSellertextbox, ItemCodeClinetTextBox, ItemCodeBox);
        }
        public Form1()
        {
            InitializeComponent();
            SetLocationsToDefault();
            Has_Club_Card.Visible = false;
            groupbox.Visible = false;
            _acsc.Add("309");
            if (AreWeTesting) return;
            Managers.Add(new Manager(1, "moshe",
                "cohen", "123456789", 50, 40));
            Clients.Add(new Client(4, "shimi", "", "", false, "1"));
            Clients.Add(new Client(5, "levi", "", "", false, "1"));
            Items.Add(new Item(309, "banana", 100, 50, 10));
            Sellers.Add(new Seller(12, "moshil",
                "hahalban", "9999", 20, 10));
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
            Location = new Point((Screen.PrimaryScreen.Bounds.Width- Width)/2,
                                 (Screen.PrimaryScreen.Bounds.Height-Height)/2);
            ActiveControl = label1;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void IDpressed()
        {
            try { CheckId(idinput.Text); }
            catch (Exception e){ MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Error); return; }
            if (EnterPasswordLogInTB.ForeColor == Color.Gray)
            {
                MessageBox.Show("Password cannot be empty.");
                return;
            }
            var pass = EnterPasswordLogInTB.Text;
            var ID = idinput.Text;
            
            
            foreach (var client in from client in Clients 
                     let id = client.Id.ToString() 
                     let password = client.Password 
                     where id.Equals(ID) && pass.Equals(password)
                     select client)
            {
                groupBox1.Location = new Point(24, 140);
                _currentid = client.Id;
                _currpass = client.Password;
                SetMainGroupBoxVisible(ClientOptions);
                ClientOptions.Text = $"Client Options - {client.FirstName}";
                return;
            }
            foreach (var seller in from seller in Sellers
                     let id = seller.Id.ToString()
                     let password = seller.Password
                     where id.Equals(ID) && pass.Equals(password)
                     select seller)
            {
                groupBox1.Location = new Point(24, 140);
                _currentid = seller.Id;
                _currpass = seller.Password;
                SellerOptions.Text = $"Seller Options - {seller.FirstName}";
                SetMainGroupBoxVisible(SellerOptions);
                return;

            }
            foreach (var manger in from manger in Managers
                     let id = manger.Id.ToString()
                     let password = manger.Password
                     where id.Equals(ID) && pass.Equals(password)
                     select manger)
            {
                groupBox1.Location = new Point(24, 140);
                    _currentid = manger.Id;
                    _currpass = manger.Password;
                    ManagerGroupBox.Text = $"Manager Options - {manger.FirstName}";
                    SetMainGroupBoxVisible(ManagerGroupBox);
                    return;
            }

            //DialogResult dr = MessageBox.Show($"Would you like to create one with the" +
            //                                  $"\nID: \"{text}\" " +
            //                                  $"\nPassword: \"{pass}\" " +
            //                                  $"\n(Press OK)" +
            //                                  $"\nOr retype your password and ID? " +
            //                                  $"\n(Press Cancel)",
            //    "User not found",
            //    MessageBoxButtons.OK, MessageBoxIcon.Question);
            //if (dr == DialogResult.Cancel) return;
            MessageBox.Show($"Sorry we couldn't find\nthe user: {ID}\nwith the password" +
                $": {pass} \nThe password or ID may be written wrong." +
                $"\nIf you want to create a new account then Press 'Sign-up' below.",
                "Incorrect ID or password",
                MessageBoxButtons.OK, MessageBoxIcon.Stop);
            _currpass = pass;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            IDpressed();
        }
        private void SetMainGroupBoxVisible(Control gb)
        {
            ClientOptions.Visible = false;
            ManagerGroupBox.Visible = false;
            SellerOptions.Visible = false;
            groupbox.Visible = false;
            gb.Visible = true;
        }
        private void groupBox1_Enter(object sender, EventArgs e) {}
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void CellPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ActiveControl = label1;
        }
        private void SeeProfits_Click(object sender, EventArgs e)
        {
            Profits.Text = $"Profits: {profits:C}";
        }
        private void ApplyButton_Click(object sender, EventArgs e)
        {
            AddItemButton.Visible = true;
            SetDiscount.Visible = true;
            DiscountOptions.Visible = false;
            SeeWorkerSalaryButton.Visible = true;
            try
            {
                ChangePrecentage(PrecentBox.Text, ItemCodeBox.Text);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
            }
        }
        private static void ChangePrecentage(string precent, string code)
        {
            int p, c;
            try
            {
                p = int.Parse(precent);
                c = int.Parse(code);
            }
            catch 
            { throw new InvalidDataException("Precents and the Code can only be numbers"); }

            if (!IsNumberInRange(p, 100, 0))
            { throw new InvalidDataException("Precents can be a number between 0 and 100 exclusive"); }

            foreach (var t in Items)
            {
                if (t.code == c)
                {
                    Manager.SetDiscount(c, p);
                    MessageBox.Show($"{t.price:C} normal price {t.clubPrice:C} club price");
                    return;
                }
            }

            throw new InvalidDataException("The item does not exist (change item code.)");
        }
        private void SeeWorkerSalaryButton_Click(object sender, EventArgs e)
        {
            RemoveItemButton.Visible = false;
            AddItemButton.Visible = false;
            SetDiscount.Visible = false;
            DiscountOptions.Visible = false;
            WorkSalaryGroupBox.Visible = true;
            SeeWorkerSalaryButton.Visible = false;
        }
        private void CloseWindowButton_Click(object sender, EventArgs e)
        {
            RemoveItemButton.Visible = true;
            AddItemButton.Visible = true;
            SetDiscount.Visible = true;
            DiscountOptions.Visible = false;
            WorkSalaryGroupBox.Visible = false;
            SeeWorkerSalaryButton.Visible = true;
        }
        private void SeeTheSalary_Click(object sender, EventArgs e)
        {
            int code;
            try
            {
                code = int.Parse(WorkerIDtextBox.Text);
            }catch{return;}

            foreach (var seller in Sellers)
            {
                if (seller.Id == code)
                {
                    WorkerSalaryLabel.Text = $"Salary: {seller.GetSalary():C}";
                    return;
                }
            }
        }
        private void AddHoursButton_Click(object sender, EventArgs e)
        {
            GetSeller(_currentid).workHours += (int.Parse(UntilHours.Text) - int.Parse(FromHours.Text));
            SetHoursGroupBox.Visible = false;
            SetHoursButton.Visible = true;
            Setdiscountforseller.Visible = true;
        }
        private void SetHoursButton_Click(object sender, EventArgs e)
        {
            SetHoursGroupBox.Visible = true;
            SetHoursGroupBox.Location = new Point(11, 65);
            SetHoursButton.Visible = false;
            Setdiscountforseller.Visible = false;
        }
        private void Setdiscountforseller_Click(object sender, EventArgs e)
        {
            Setdiscountforseller.Visible = false;
            SetDiscountSellerGroupBox.Location = new Point(11, 65);
            SetHoursButton.Visible = false;
            SetDiscountSellerGroupBox.Visible = true;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                ChangePrecentage(precentsellerdiscounttextbox.Text, ItemcodeSellertextbox.Text);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
            }
            Setdiscountforseller.Visible = true;
            SetHoursButton.Visible = true;
            SetDiscountSellerGroupBox.Visible = false;
        }
        private void GetSalaryForSellerButton_Click(object sender, EventArgs e)
        {
            SalaryForSellerLabel.Text = $"Salary: {GetSeller(_currentid).GetSalary():C}";
        }
        private void button4_Click(object sender, EventArgs e)
        {
            TotalPriceLabel.Text = $"Total Price: {GetClient(_currentid).Payment:C}";
        }
        private void BuyItemButton_Click(object sender, EventArgs e)
        {
            var result = DialogResult.Cancel;
            try
            {
                var x = int.Parse(ItemCodeClinetTextBox.Text);
                result = MessageBox.Show((GetClient(_currentid).BoolBuy(x) ? "Successful" : "Failed"), "Purchase result"
                ,MessageBoxButtons.RetryCancel);
            }
            catch { MessageBox.Show("Code isnt right"); }

            if (result == DialogResult.Cancel)
            {
                BuyOptionsGroupBox.Visible = false;
                BuyButton.Visible = true;
            }
            

        }
        private void BuyButton_Click(object sender, EventArgs e)
        {
            BuyOptionsGroupBox.Visible = true;
            BuyButton.Visible = false;
        }
        private void Exit_Click(object sender, EventArgs e)
        { Close();}
        private void idinput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                IDpressed();
            }
        }
        private void AddItemButton_Click(object sender, EventArgs e)
        {
            RemoveItemButton.Visible = false;
            AddItemButton.Visible = false;
            SetDiscount.Visible = false;
            SeeWorkerSalaryButton.Visible = false;
            AddItemGroupBox.Visible = true;
            RemoveItemGroupBox.Visible = false;
        }
        private void ApplyItemButton_Click(object sender, EventArgs e)
        {
            try
            {
                var itemname = AddItemNameTB.Text.Equals(String.Empty)
                    ? throw new InvalidDataException("Name cant be Empty")
                    : AddItemNameTB.Text;
                var code = 0;
                try
                {
                    code = int.Parse(AddItemCodeTB.Text);
                }
                catch (Exception exception)
                {
                    throw new InvalidDataException("Code Can only contain characters");
                }
                
                if (GetItem(code) != null)
                    throw new InvalidDataException($"An item with the " +
                                                   $"code {code}" +
                                                   $" ({GetItem(code).name}) already exists");
                Items.Add(new Item(int.Parse(AddItemCodeTB.Text), itemname, int.Parse(AddItemPriceTB.Text),
                    int.Parse(AddItemClubPriceTB.Text), int.Parse(numericUpDown1.Text)));
                MessageBox.Show($"Item {itemname} Added Successfully.", "DONE", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                _acsc.Add(code.ToString());
                AddItemButton.Visible = true;
                SetDiscount.Visible = true;
                SeeWorkerSalaryButton.Visible = true;
                AddItemGroupBox.Visible = false;
                RemoveItemButton.Visible = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Item \"{AddItemNameTB.Text}\" Failed to Add." +
                                $"\nReason => {exception.Message}", "DONE", MessageBoxButtons.OK,
                    MessageBoxIcon.Hand);
            }
        }
        private void AddItemPriceTB_ValueChanged(object sender, EventArgs e)
        {
            AddItemClubPriceTB.Maximum = int.Parse(AddItemPriceTB.Text);
        }
        private void BackButton_Click(object sender, EventArgs e)
        {
            RemoveItemButton.Visible = true;
            AddItemButton.Visible = true;
            SetDiscount.Visible = true;
            SeeWorkerSalaryButton.Visible = true;
            AddItemGroupBox.Visible = false;
        }
        private void RemoveItemButton_Click(object sender, EventArgs e)
        {
            RemoveItemButton.Visible = false;
            RemoveItemGroupBox.Visible = true;
        }
        private void ConfirmRemove_Click(object sender, EventArgs e)
        {
            try
            {
                var x = Int32.Parse(RemoveItemCodeTB.Text);
                if (GetItem(x) != null)
                {
                    var k = false;
                    var f = MessageBox.Show(
                        $"Are you sure you want to remove {GetItem(x).code} ({GetItem(x).name})" +
                        $"\n there's no going back!", "CONFIRMATION", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (f == DialogResult.Yes)
                    {
                        k = Items.Remove(GetItem(x));
                        MessageBox.Show((k ? "Item Removed Successfully" : "Couldn't remove item")
                            , "Deletion Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                else throw new InvalidDataException($"Item not found Code: {x}");
            }
            catch (Exception exception)
            {
                if (MessageBox.Show($"Failed to remove item \n Reason => {exception.Message}", "Error",
                    MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    return;
            }
            RemoveItemButton.Visible = true;
            RemoveItemGroupBox.Visible = false;

        }
        

        private void SetDiscount_Click(object sender, EventArgs e)
        {
            AddItemButton.Visible = false;
            SetDiscount.Visible = false;
            DiscountOptions.Visible = true;
            WorkSalaryGroupBox.Visible = false;
            SeeWorkerSalaryButton.Visible = false;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.Text.ToLower())
            {
                case "manager":
                    ut = UserType.manager;
                    break;
                case "seller":
                    ut = UserType.seller;
                    break;
                case "client":
                    ut = UserType.client;
                    break;
            }
            // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
            switch (ut)
            {
                case UserType.seller:
                case UserType.manager:
                    button2.Location = new Point(41, 373);
                    WorkerOptions.Visible = true;
                    Has_Club_Card.Visible = false;
                    break;
                case UserType.client:
                    button2.Location = new Point(44, 307);
                    WorkerOptions.Visible = false;
                    Has_Club_Card.Visible = true;
                    break;
            }
        }

        private void EnterPasswordLogInTB_TextChanged(object sender, EventArgs e) { }

        private void label20_MouseEnter(object sender, EventArgs e)
        {
            label20.ForeColor = Color.DarkBlue;
            
        }

        private void label20_MouseLeave(object sender, EventArgs e)
        {
            label20.ForeColor = Color.RoyalBlue;
        }

        private void label20_Click(object sender, EventArgs e)
        {
            CreatePasswordTB.ForeColor = Color.Black;
            CreatePasswordTB.UseSystemPasswordChar = false;
            CreatePasswordTB.Text = GetRandomPassword(15);
        }

        private void CreatePasswordTB_TextChanged(object sender, EventArgs e)
        {

        }
        private void Button2_Click(object sender, EventArgs e)
        {
            int salary=0, workHours=0, salaryPerHour=0;
            try
            {
                CheckId(CreateIDTB.Text);
                CheckName(newFirstName.Text, 10, newFirstName, "First");
                CheckName(LastName.Text, 15, LastName, "Last");
                CheckPhoneNumber(CellPhone.Text);
                if (CreatePasswordTB.ForeColor == Color.Gray || CreatePasswordTB.Text.Equals(String.Empty))
                    throw new InvalidDataException("Password cannot be empty");
                if (ut == UserType.seller || ut == UserType.manager)
                {
                    salary = int.Parse(Salary.Text);
                    workHours = int.Parse(Workhours.Text);
                    salaryPerHour = salary / workHours;
                }
                switch (ut)
                {
                    case UserType.client:
                        if (Clients.Count >= 20)
                        {
                            MessageBox.Show("Maximum clients are 20 and there are already 20", "ERROR",
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button2);
                            return;
                        }
                        Clients.Add(new Client(int.Parse(CreateIDTB.Text), newFirstName.Text,
                            LastName.Text, CellPhone.Text, Has_Club_Card.Checked, CreatePasswordTB.Text));
                        Reset();
                        return;
                    case UserType.seller:
                        if (!IsNumberInRange(salary, 10000, 5000) || !IsNumberInRange(workHours, 160, 15))
                        {
                            MessageBox.Show("The slaray or workhours are not valid", "ERROR",
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button2);
                            return;
                        }
                        if (Clients.Count >= 30)
                        {
                            MessageBox.Show("Maximum sellers are 30 and there are already 30", "ERROR",
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button2);
                            return;
                        }
                        Sellers.Add(new Seller(int.Parse(CreateIDTB.Text), newFirstName.Text, LastName.Text
                                    , CellPhone.Text, salaryPerHour, workHours, CreatePasswordTB.Text));
                        Reset();
                        return;
                    case UserType.manager:
                        if (!IsNumberInRange(salary, 20000, 10000) || !IsNumberInRange(workHours, 160, 15))
                        {
                            MessageBox.Show("The slaray or workhours are not valid", "ERROR",
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button2);
                            return;
                        }
                        if (Managers.Count >= 5)
                        {
                            MessageBox.Show("Maximum managers are 5 and there are already 5", "ERROR",
                                MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button2);
                            return;
                        }
                        Managers.Add(new Manager(int.Parse(CreateIDTB.Text), newFirstName.Text, LastName.Text
                            , CellPhone.Text, salaryPerHour, workHours, CreatePasswordTB.Text));
                        Reset();
                        return;
                    case UserType.notchosen:
                        MessageBox.Show("You Must Choose a user type", "ERROR",
                            MessageBoxButtons.OK, MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button2);
                        return;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
            }
            
        }

        private void BackFromCreatingUser_Click(object sender, EventArgs e)
        {
            Reset();
        }

        

        private void label21_Click(object sender, EventArgs e)
        {
            SetMainGroupBoxVisible(groupbox);
            groupBox1.Visible = false;
        }

        private static void CheckPhoneNumber(string cellPhoneText)
        {
            if (cellPhoneText.Length != 10) 
                throw new InvalidDataException("The cellphone number doesn't " +
                                               "contain 10 characters");
            if (cellPhoneText.StartsWith("05"))
            {
                var number = cellPhoneText.Substring(2);
                foreach (var VARIABLE in number)
                {
                    if (!"1234567890".Contains(VARIABLE))
                    {
                        throw new InvalidDataException("The cellphone contains invalid character(s)");
                    }
                }
            }
            else throw new InvalidDataException("The cellphone must start with 05");
        }

        //Password Management
        private void button4_Click_1(object sender, EventArgs e)
        {
            EnterPasswordLogInTB.UseSystemPasswordChar = !EnterPasswordLogInTB.UseSystemPasswordChar;
            PassAvalibleButton.Visible = true;
            button4.Visible = false;
        }
        private void PassAvalibleButton_Click(object sender, EventArgs e)
        {
            EnterPasswordLogInTB.UseSystemPasswordChar = !EnterPasswordLogInTB.UseSystemPasswordChar;
            PassAvalibleButton.Visible = false;
            button4.Visible = true;
        }
    }
}
