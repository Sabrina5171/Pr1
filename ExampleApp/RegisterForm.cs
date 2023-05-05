using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ExampleApp
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();

            userNameField.Text = "Введите имя";
            userNameField.ForeColor = Color.Gray;

            userSurnameField.Text = "Введите фамилию";
            userSurnameField.ForeColor = Color.Gray;

            LoginField.Text = "Введите логин";
            LoginField.ForeColor = Color.Gray;

            PassField.Text = "Введите пароль";
            PassField.ForeColor = Color.Gray;
        }


        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        Point lastPoint;
        private void RegisterForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Left += e.Y - lastPoint.Y;
            }
        }

        private void RegisterForm_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void userNameField_Enter(object sender, EventArgs e)
        {
            if(userNameField.Text=="Введите имя")
            {
                userNameField.Text = "";
                userNameField.ForeColor = Color.Black;
            }
        }

        private void userNameField_Leave(object sender, EventArgs e)
        {
            if (userNameField.Text == "")
            {
                userNameField.Text = "Введите имя";
                userNameField.ForeColor = Color.Gray;
            }
        }

        private void userSurnameField_Enter(object sender, EventArgs e)
        {
            if (userSurnameField.Text == "Введите фамилию")
            {
                userSurnameField.Text = "";
                userSurnameField.ForeColor = Color.Black;
            }
        }

        private void userSurnameField_Leave(object sender, EventArgs e)
        {
            if (userSurnameField.Text == "")
            {
                userSurnameField.Text = "Введите фамилию";
                userSurnameField.ForeColor = Color.Gray;
            }
        }

        private void LoginField_Enter(object sender, EventArgs e)
        {
            if (LoginField.Text == "Введите логин")
            {
                LoginField.Text = "";
                LoginField.ForeColor = Color.Black;
            }
        }

        private void LoginField_Leave(object sender, EventArgs e)
        {
            if (LoginField.Text == "")
            {
                LoginField.Text = "Введите логин";
                LoginField.ForeColor = Color.Gray;
            }
        }

        private void PassField_Enter(object sender, EventArgs e)
        {
            if (PassField.Text == "Введите пароль")
            {
                PassField.Text = "";
                PassField.ForeColor = Color.Black;
            }
        }

        private void PassField_Leave(object sender, EventArgs e)
        {
            if (PassField.Text == "")
            {
                PassField.Text = "Введите пароль";
                PassField.ForeColor = Color.Gray;
            }
        }
       
        private void buttonRegister_Click(object sender, EventArgs e)
        {
            if (userNameField.Text=="Введите имя")
            {
                MessageBox.Show("Введите имя");
                return;
            }
            if (userSurnameField.Text == "Введите фамилию")
            {
                MessageBox.Show("Введите фамилию");
                return;
            }

            if (LoginField.Text == "Введите логин")
            {
                MessageBox.Show("Введите логин");
                return;
            }
            if (PassField.Text == "Введите пароль")
            {
                MessageBox.Show("Введите пароль");
                return;
            }
            if (checUser())
                return;
            if (checkUser())
                return;
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`Login`, `Password`, `name`, `surname`) VALUES (@Login, @Password, @name, @surname);",db.getConnection());

            command.Parameters.Add("@Login", MySqlDbType.VarChar).Value = LoginField.Text;
            command.Parameters.Add("@Password", MySqlDbType.VarChar).Value = PassField.Text;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = userNameField.Text;
            command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = userSurnameField.Text;
          
                this.Hide();
                LoginForm form = new LoginForm();
                form.Show();
         
            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
                MessageBox.Show("Аккаунт был создан");
            else
                MessageBox.Show("Аккаунт не был создан");
            db.closeConnection();
        }
        public Boolean checUser()
        {
            DB db = new DB();
            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `Login`= @ul", db.getConnection());
            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = LoginField.Text;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Такой логин уже есть");
                return true;
            }
            else return false;

        }

        public Boolean checkUser()
        {
            DB db = new DB();
            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `Password`= @ul", db.getConnection());
            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value =PassField.Text;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Такой пароль уже есть");
                return true;
            }
            else return false;

        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {

        }

        private void PassField_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoginField_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
