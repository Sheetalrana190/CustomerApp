using System;
using System.Data;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CustomerApp
{
    public partial class MainWindow : Window
    {
        private readonly string connectionString = "Data Source=CustomerDB.sqlite;Version=3;";
        private int _editingId = -1;

        public MainWindow()
        {
            InitializeComponent();
            InitializeDatabase();
            customerGrid.Visibility = Visibility.Hidden; // Ensure DataGrid is hidden initially
        }

        private void InitializeDatabase()
        {
            try
            {
                if (!System.IO.File.Exists("CustomerDB.sqlite"))
                {
                    SQLiteConnection.CreateFile("CustomerDB.sqlite");
                }

                using (SQLiteConnection con = new SQLiteConnection(connectionString))
                {
                    con.Open();
                    string createTableQuery = "CREATE TABLE IF NOT EXISTS Customers (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL, AccountNo TEXT NOT NULL, Telephone TEXT NOT NULL)";
                    SQLiteCommand cmd = new SQLiteCommand(createTableQuery, con);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing database: {ex.Message}");
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && (textBox.Text == "Enter Name" || textBox.Text == "Enter Account No" || textBox.Text == "Enter Telephone"))
            {
                textBox.Text = "";
                textBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && string.IsNullOrWhiteSpace(textBox.Text))
            {
                if (textBox.Name == "txtName") textBox.Text = "Enter Name";
                else if (textBox.Name == "txtAccountNo") textBox.Text = "Enter Account No";
                else if (textBox.Name == "txtTelephone") textBox.Text = "Enter Telephone";
                textBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void AddCustomer(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate empty fields
                if (string.IsNullOrWhiteSpace(txtName.Text) || txtName.Text == "Enter Name" ||
                    string.IsNullOrWhiteSpace(txtAccountNo.Text) || txtAccountNo.Text == "Enter Account No" ||
                    string.IsNullOrWhiteSpace(txtTelephone.Text) || txtTelephone.Text == "Enter Telephone")
                {
                    MessageBox.Show("All fields are required. Please fill in Name, Account No, and Telephone.");
                    return;
                }

                // Validate telephone format
                string phoneNumber = txtTelephone.Text.Trim();
                string pattern = @"^(\+\d{1,3} \d{1,4} \d{4,10}|\(\d{3}\)-\d{3} \d{4}|\d{10})$";
                if (!Regex.IsMatch(phoneNumber, pattern))
                {
                    MessageBox.Show("Invalid phone number format. Allowed formats: (XXX)-XXX XXXX, +XX XXX XXX XXXX, or XXXXXXXXXX");
                    txtTelephone.Focus();
                    return;
                }

                using (SQLiteConnection con = new SQLiteConnection(connectionString))
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Customers (Name, AccountNo, Telephone) VALUES (@name, @accountNo, @telephone)", con);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@accountNo", txtAccountNo.Text);
                    cmd.Parameters.AddWithValue("@telephone", txtTelephone.Text);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Customer added successfully.");
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding customer: {ex.Message}");
            }
        }

        private void UpdateCustomer(object sender, RoutedEventArgs e)
        {
            if (_editingId == -1)
            {
                MessageBox.Show("No customer selected for editing.");
                return;
            }

            // Validate empty fields
            if (string.IsNullOrWhiteSpace(txtName.Text) || txtName.Text == "Enter Name" ||
                string.IsNullOrWhiteSpace(txtAccountNo.Text) || txtAccountNo.Text == "Enter Account No" ||
                string.IsNullOrWhiteSpace(txtTelephone.Text) || txtTelephone.Text == "Enter Telephone")
            {
                MessageBox.Show("All fields are required. Please fill in Name, Account No, and Telephone.");
                return;
            }

            // Validate telephone format
            string phoneNumber = txtTelephone.Text.Trim();
            string pattern = @"^(\+\d{1,3} \d{1,4} \d{4,10}|\(\d{3}\)-\d{3} \d{4}|\d{10})$";
            if (!Regex.IsMatch(phoneNumber, pattern))
            {
                MessageBox.Show("Invalid phone number format. Allowed formats: (XXX)-XXX XXXX, +XX XXX XXX XXXX, or XXXXXXXXXX");
                txtTelephone.Focus();
                return;
            }

            try
            {
                using (SQLiteConnection con = new SQLiteConnection(connectionString))
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand("UPDATE Customers SET Name=@name, AccountNo=@accountNo, Telephone=@telephone WHERE Id=@id", con);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@accountNo", txtAccountNo.Text);
                    cmd.Parameters.AddWithValue("@telephone", txtTelephone.Text);
                    cmd.Parameters.AddWithValue("@id", _editingId);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Customer updated successfully.");
                ClearFields();
                ViewData(sender, e);
                _editingId = -1;
                btnUpdate.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating customer: {ex.Message}");
            }
        }

        private void DeleteCustomer(object sender, RoutedEventArgs e)
        {
            if (customerGrid.SelectedItem is DataRowView row)
            {
                int id = Convert.ToInt32(row["Id"]);
                using (SQLiteConnection con = new SQLiteConnection(connectionString))
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand("DELETE FROM Customers WHERE Id=@id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Customer deleted successfully.");
                ViewData(sender, e);
            }
            else
            {
                MessageBox.Show("Please select a customer to delete.");
            }
        }

        private void ViewData(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(connectionString))
                {
                    con.Open();
                    SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT * FROM Customers", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    customerGrid.ItemsSource = dt.DefaultView;
                    customerGrid.Visibility = Visibility.Visible; // Show DataGrid
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error viewing data: {ex.Message}");
            }
        }

        private void EditCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is DataRowView row)
            {
                _editingId = Convert.ToInt32(row["Id"]);
                txtName.Text = row["Name"].ToString();
                txtAccountNo.Text = row["AccountNo"].ToString();
                txtTelephone.Text = row["Telephone"].ToString();
                txtName.Foreground = txtAccountNo.Foreground = txtTelephone.Foreground = new SolidColorBrush(Colors.Black);
                btnUpdate.Visibility = Visibility.Visible;
            }
        }

        private void ClearFields()
        {
            txtName.Text = "Enter Name";
            txtAccountNo.Text = "Enter Account No";
            txtTelephone.Text = "Enter Telephone";
            txtName.Foreground = txtAccountNo.Foreground = txtTelephone.Foreground = new SolidColorBrush(Colors.Gray);
        }
    }
}