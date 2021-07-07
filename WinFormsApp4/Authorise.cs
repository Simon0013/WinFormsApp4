using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace WinFormsApp4
{
    public partial class Authorise : Form
    {
        public Authorise()
        {
            InitializeComponent();
        }
        public static NpgsqlConnection connection = new NpgsqlConnection("Host = localhost; User Id = postgres; Database = praktika; Port = 5432; Password = 111;");
        public static DataSet ds = new DataSet();
        public static string surname, name, otchestvo;
        private void Authorise_Load(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                textBox2.UseSystemPasswordChar = false;
            else
                textBox2.UseSystemPasswordChar = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool HasLogin = false, HasPass = false;
            connection.Open();
            string sql = "SELECT login FROM administracia";
            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (reader["login"].ToString() == textBox1.Text)
                {
                    HasLogin = true;
                    break;
                }
            }
            connection.Close();
            connection.Open();
            sql = "SELECT password FROM administracia WHERE login = '" + textBox1.Text + "'";
            command = new NpgsqlCommand(sql, connection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (reader["password"].ToString() == textBox2.Text)
                {
                    HasPass = true;
                    break;
                }
            }
            connection.Close();
            if (!HasLogin || !HasPass)
            {
                MessageBox.Show("Неправильный логин или пароль", "Ошибка");
                return;
            }
            else
            {
                sql = "SELECT surname, name, otchestvo FROM administracia WHERE login = '" + textBox1.Text + "' AND password = '" + textBox2.Text + "'";
                command = new NpgsqlCommand(sql, connection);
                connection.Open();
                reader = command.ExecuteReader();
                reader.Read();
                surname = reader["surname"].ToString();
                name = reader["name"].ToString();
                otchestvo = reader["otchestvo"].ToString();
                connection.Close();
                Hide();
                Form1 w = new Form1();
                w.ShowDialog();
                Close();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
