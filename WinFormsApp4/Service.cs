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
    public partial class Service : Form
    {
        public Service()
        {
            InitializeComponent();
        }
        private string st_id;
        private void Service_Load(object sender, EventArgs e)
        {
            NpgsqlCommand command = Authorise.connection.CreateCommand();
            command.CommandText = "SELECT CONCAT_WS(' ', st_familia, st_imya, st_otchestvo) FROM sotrudniki";
            Authorise.connection.Open();
            NpgsqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read()) comboBox1.Items.Add(dataReader[0]);
            Authorise.connection.Close();
            if (Uslugi.k > -1)
            {
                command = Authorise.connection.CreateCommand();
                command.CommandText = "SELECT * FROM about_uslugi WHERE Номер = " + Sotrudnik.ds.Tables["Uslugi"].Rows[Uslugi.k]["Номер"].ToString();
                Authorise.connection.Open();
                dataReader = command.ExecuteReader();
                dataReader.Read();
                comboBox1.Text = dataReader["ФИО_сотрудника"].ToString();
                textBox1.Text = dataReader["Разработка_чертежей"].ToString();
                comboBox2.Text = dataReader["Выдача_заключений"].ToString();
                comboBox3.Text = dataReader["Технический_надзор"].ToString();
                comboBox4.Text = dataReader["Техническая_экспертиза"].ToString();
                Authorise.connection.Close();
                command = Authorise.connection.CreateCommand();
                command.CommandText = "SELECT st_id FROM sotrudniki WHERE CONCAT_WS(' ', st_familia, st_imya, st_otchestvo) = '" + comboBox1.Text + "'";
                Authorise.connection.Open();
                dataReader = command.ExecuteReader();
                dataReader.Read();
                st_id = dataReader["st_id"].ToString();
                Authorise.connection.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (Uslugi.k == -1)
            {
                NpgsqlCommand command = Authorise.connection.CreateCommand();
                command.CommandText = "SELECT MAX(us_id)+1 FROM uslugi";
                Authorise.connection.Open();
                NpgsqlDataReader dataReader = command.ExecuteReader();
                dataReader.Read();
                string id = dataReader[0].ToString();
                Authorise.connection.Close();
                command = Authorise.connection.CreateCommand();
                command.CommandText = "INSERT INTO uslugi VALUES (" + st_id + ", " + id + ", '" + textBox1.Text + "', '" + comboBox2.Text + "', '" + comboBox3.Text + "', " + comboBox4.Text + ")";
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
                Sotrudnik.ds.Tables["Uslugi"].Rows.Add(new object[] { comboBox1.Text, id, textBox1.Text, comboBox2.Text, comboBox3.Text, comboBox4.Text });
            }
            else
            {
                NpgsqlCommand command = Authorise.connection.CreateCommand();
                command.CommandText = "UPDATE uslugi SET sotrudniki_st_id = " + st_id + ", razrabotkahertejei = '" + textBox1.Text + "', vidahazaklyhenii = '" + comboBox2.Text + "', texniheskiinadzor = '" + comboBox3.Text + "', texniheskayaekspertiza = " + comboBox4.Text + " WHERE us_id = " + Sotrudnik.ds.Tables["Uslugi"].Rows[Uslugi.k]["Номер"];
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
                Sotrudnik.ds.Tables["Uslugi"].Rows.Add(new object[] { comboBox1.Text, Sotrudnik.ds.Tables["Uslugi"].Rows[Uslugi.k]["Номер"], textBox1.Text, comboBox2.Text, comboBox3.Text, comboBox4.Text });
                Sotrudnik.ds.Tables["Uslugi"].Rows.RemoveAt(Uslugi.k);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (Uslugi.k == -1)
            {
                NpgsqlCommand command = Authorise.connection.CreateCommand();
                command.CommandText = "SELECT MAX(us_id)+1 FROM uslugi";
                Authorise.connection.Open();
                NpgsqlDataReader dataReader = command.ExecuteReader();
                dataReader.Read();
                string id = dataReader["us_id"].ToString();
                Authorise.connection.Close();
                command = Authorise.connection.CreateCommand();
                command.CommandText = "INSERT INTO uslugi VALUES (" + id + ", '" + comboBox1.Text + "', '" + comboBox3.Text + "', '" + comboBox2.Text + "', " + comboBox4.Text + ")";
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
                Sotrudnik.ds.Tables["Uslugi"].Rows.Add(new object[] { comboBox1.Text, id, textBox1.Text, comboBox2.Text, comboBox3.Text, comboBox4.Text });
            }
            else
            {
                NpgsqlCommand command = Authorise.connection.CreateCommand();
                command.CommandText = "UPDATE uslugi SET sotrudniki_st_id = " + st_id + ", razrabotkahertejei = '" + comboBox1.Text + "', vidahazaklyhenii = '" + comboBox3.Text + "', texniheskiinadzor = '" + comboBox2.Text + "', texniheskayaekspertiza = " + comboBox4.Text + " WHERE us_id = " + Sotrudnik.ds.Tables["Uslugi"].Rows[Uslugi.k]["Номер"];
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
                Sotrudnik.ds.Tables["Uslugi"].Rows.Add(new object[] { comboBox1.Text, Sotrudnik.ds.Tables["Uslugi"].Rows[Uslugi.k]["Номер"], textBox1.Text, comboBox2.Text, comboBox3.Text, comboBox4.Text });
                Sotrudnik.ds.Tables["Uslugi"].Rows.RemoveAt(Uslugi.k);
            }
            Close();
        }
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            NpgsqlCommand command = Authorise.connection.CreateCommand();
            command.CommandText = "SELECT st_id FROM sotrudniki WHERE CONCAT_WS(' ', st_familia, st_imya, st_otchestvo) = '" + comboBox1.SelectedItem.ToString() + "'";
            Authorise.connection.Open();
            NpgsqlDataReader dataReader = command.ExecuteReader();
            dataReader.Read();
            st_id = dataReader["st_id"].ToString();
            Authorise.connection.Close();
        }
    }
}
