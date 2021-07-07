using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Npgsql;

namespace WinFormsApp4
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
        }
        private void Client_Load(object sender, EventArgs e)
        {
            if (Klient.k > -1)
            {
                NpgsqlCommand command = Authorise.connection.CreateCommand();
                command.CommandText = "SELECT * FROM klienti WHERE kl_id = " + Sotrudnik.ds.Tables["Klient"].Rows[Klient.k]["Номер"].ToString();
                Authorise.connection.Open();
                NpgsqlDataReader dataReader = command.ExecuteReader();
                dataReader.Read();
                textBox1.Text = dataReader["kl_familia"].ToString();
                textBox2.Text = dataReader["kl_name"].ToString();
                textBox3.Text = dataReader["kl_otchestvo"].ToString();
                textBox4.Text = dataReader["kl_nomer_zakaza"].ToString();
                Authorise.connection.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (Klient.k == -1)
            {
                NpgsqlCommand command = Authorise.connection.CreateCommand();
                command.CommandText = "SELECT MAX(kl_id)+1 FROM klienti";
                Authorise.connection.Open();
                NpgsqlDataReader dataReader = command.ExecuteReader();
                dataReader.Read();
                string id = dataReader[0].ToString();
                Authorise.connection.Close();
                command = Authorise.connection.CreateCommand();
                command.CommandText = "INSERT INTO klienti VALUES (" + id + ", '" + textBox1.Text + "', '" + textBox3.Text + "', '" + textBox2.Text + "', " + textBox4.Text + ")";
                Authorise.connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (NpgsqlException ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка");
                    Authorise.connection.Close();
                    return;
                }
                Authorise.connection.Close();
                Sotrudnik.ds.Tables["Klient"].Rows.Add(new object[] { id, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text });
            }
            else
            {
                NpgsqlCommand command = Authorise.connection.CreateCommand();
                command.CommandText = "UPDATE klienti SET kl_familia = '" + textBox1.Text + "', kl_otchestvo = '" + textBox3.Text + "', kl_name = '" + textBox2.Text + "', kl_nomer_zakaza = " + textBox4.Text + " WHERE kl_id = " + Sotrudnik.ds.Tables["Klient"].Rows[Klient.k]["Номер"];
                Authorise.connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (NpgsqlException ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка");
                    Authorise.connection.Close();
                    return;
                }
                Authorise.connection.Close();
                Sotrudnik.ds.Tables["Klient"].Rows.Add(new object[] { Sotrudnik.ds.Tables["Klient"].Rows[Klient.k]["Номер"], textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text });
                Sotrudnik.ds.Tables["Klient"].Rows.RemoveAt(Klient.k);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (Klient.k == -1)
            {
                NpgsqlCommand command = Authorise.connection.CreateCommand();
                command.CommandText = "SELECT MAX(kl_id)+1 FROM klienti";
                Authorise.connection.Open();
                NpgsqlDataReader dataReader = command.ExecuteReader();
                dataReader.Read();
                string id = dataReader["kl_id"].ToString();
                Authorise.connection.Close();
                command = Authorise.connection.CreateCommand();
                command.CommandText = "INSERT INTO klienti VALUES (" + id + ", '" + textBox1.Text + "', '" + textBox3.Text + "', '" + textBox2.Text + "', " + textBox4.Text + ")";
                Authorise.connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (NpgsqlException ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка");
                    Authorise.connection.Close();
                    return;
                }
                Authorise.connection.Close();
                Sotrudnik.ds.Tables["Klient"].Rows.Add(new object[] { id, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text });
            }
            else
            {
                NpgsqlCommand command = Authorise.connection.CreateCommand();
                command.CommandText = "UPDATE klienti SET kl_familia = '" + textBox1.Text + "', kl_otchestvo = '" + textBox3.Text + "', kl_name = '" + textBox2.Text + "', kl_nomer_zakaza = " + textBox4.Text + " WHERE kl_id = " + Sotrudnik.ds.Tables["Klient"].Rows[Klient.k]["Номер"];
                Authorise.connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (NpgsqlException ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка");
                    Authorise.connection.Close();
                    return;
                }
                Authorise.connection.Close();
                Sotrudnik.ds.Tables["Klient"].Rows.Add(new object[] { Sotrudnik.ds.Tables["Klient"].Rows[Klient.k]["Номер"], textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text });
                Sotrudnik.ds.Tables["Klient"].Rows.RemoveAt(Klient.k);
            }
            Close();
        }
    }
}
