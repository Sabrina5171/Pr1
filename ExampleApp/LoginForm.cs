using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace ExampleApp
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

       

        private void PassField_TextChanged(object sender, EventArgs e)
        {
           //this.PassField.Size = new Size(this.PassField.Size.Height, 64);
          //this.PassField.AutoSize = false;
        }

        private void Vxod_Click(object sender, EventArgs e)
        {
            string loginUser = LoginField.Text;
            string passUser = PassField.Text;

            DB db = new DB();
            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM users WHERE `Login`=@ul AND `Password`=@up", db.getConnection());
            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = loginUser;
            command.Parameters.Add("@up", MySqlDbType.VarChar).Value = passUser;

            adapter.SelectCommand=command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                this.Hide();
                RegisterForm form = new RegisterForm();
                form.Show();
            }
            else MessageBox.Show("No");


        }

        private void LoginField_TextChanged(object sender, EventArgs e)
        {

        }

        private void Close_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Autorizlabel_Click(object sender, EventArgs e)
        {

        }
    }
}
