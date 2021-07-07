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
    public partial class Akty : Form
    {
        public Akty()
        {
            InitializeComponent();
        }
        private string id, id1, kl_id;
        private void FillAkt(string idf)
        {
            NpgsqlCommand npgsqlCommand = Authorise.connection.CreateCommand();
            npgsqlCommand.CommandText = "SELECT * FROM about_akty WHERE Номер = " + idf;
            Authorise.connection.Open();
            NpgsqlDataReader dataReader = npgsqlCommand.ExecuteReader();
            dataReader.Read();
            if (dataReader.HasRows)
            {
                id = dataReader["Номер"].ToString();
                comboBox1.Text = dataReader["Клиент"].ToString();
                comboBox2.Text = dataReader["Услуга"].ToString();
                comboBox3.Text = dataReader["Установка_котла"].ToString();
                textBox1.Text = dataReader["Цена"].ToString();
                dateTimePicker1.Text = dataReader["Дата"].ToString();
                label1.Text = "Акт работ №" + id + " от " + dateTimePicker1.Value.ToShortDateString();
                Authorise.connection.Close();
                npgsqlCommand = Authorise.connection.CreateCommand();
                npgsqlCommand.CommandText = "SELECT klienty_kl_id FROM akty_rabot WHERE ak_id = " + idf;
                Authorise.connection.Open();
                dataReader = npgsqlCommand.ExecuteReader();
                dataReader.Read();
                kl_id = dataReader[0].ToString();
            }
            else
            {
                id = null;
                comboBox1.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
                textBox1.Text = "";
                dateTimePicker1.Text = DateTime.Now.ToShortDateString();
                label1.Text = "Новый акт работ";
                kl_id = null;
            }
            Authorise.connection.Close();
        }
        private void Akty_Load(object sender, EventArgs e)
        {
            FillAkt("1");
            NpgsqlCommand npgsqlCommand = Authorise.connection.CreateCommand();
            npgsqlCommand.CommandText = "SELECT concat_ws(' ', kl_familia, kl_name, kl_otchestvo) FROM klienti";
            Authorise.connection.Open();
            NpgsqlDataReader dataReader = npgsqlCommand.ExecuteReader();
            while (dataReader.Read()) comboBox1.Items.Add(dataReader[0]);
            Authorise.connection.Close();
            npgsqlCommand = Authorise.connection.CreateCommand();
            npgsqlCommand.CommandText = "SELECT us_id FROM uslugi";
            Authorise.connection.Open();
            dataReader = npgsqlCommand.ExecuteReader();
            while (dataReader.Read()) comboBox2.Items.Add(dataReader[0]);
            Authorise.connection.Close();
            npgsqlCommand = Authorise.connection.CreateCommand();
            npgsqlCommand.CommandText = "SELECT concat('Акт работ №', ak_id, ' от ', ak_data) FROM akty_rabot";
            Authorise.connection.Open();
            dataReader = npgsqlCommand.ExecuteReader();
            while (dataReader.Read()) comboBox4.Items.Add(dataReader[0]);
            Authorise.connection.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (id == null)
            {
                NpgsqlCommand command = Authorise.connection.CreateCommand();
                command.CommandText = "SELECT MAX(ak_id)+1 FROM akty_rabot";
                Authorise.connection.Open();
                NpgsqlDataReader dataReader = command.ExecuteReader();
                dataReader.Read();
                id = dataReader[0].ToString();
                Authorise.connection.Close();
                command = Authorise.connection.CreateCommand();
                command.CommandText = "INSERT INTO akty_rabot VALUES (" + kl_id + ", " + comboBox2.Text + ", " + id + ", '" + comboBox3.Text + "', '" + textBox1.Text + "', '" + dateTimePicker1.Value.ToShortDateString() + "')";
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
                FillAkt(id);
            }
            else
            {
                NpgsqlCommand command = Authorise.connection.CreateCommand();
                command.CommandText = "UPDATE akty_rabot SET klienty_kl_id = " + kl_id + ", uslugi_us_id = " + comboBox2.Text + ", ak_ustanovka_kotla = '" + comboBox3.Text + "', ak_cena = '" + textBox1.Text + "', ak_data = '" + dateTimePicker1.Value.ToShortDateString() + "' WHERE ak_id = " + id;
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
                label1.Text = "Акт работ №" + id + " от " + dateTimePicker1.Value.ToShortDateString();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            FillAkt(id1);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            FillAkt("0");
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (id == null)
            {
                MessageBox.Show("Невозможно удалить несуществующий объект!", "Ошибка");
                return;
            }
            string warning = "Вы точно хотите удалить данный акт?";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(warning, "Удаление", buttons);
            if (result == DialogResult.No) return;
            NpgsqlCommand npgsqlCommand = Authorise.connection.CreateCommand();
            npgsqlCommand.CommandText = "DELETE FROM akty_rabot WHERE ak_id = " + id;
            Authorise.connection.Open();
            npgsqlCommand.ExecuteNonQuery();
            Authorise.connection.Close();
            FillAkt((Convert.ToInt32(id)-1).ToString());
        }
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            NpgsqlCommand npgsqlCommand = Authorise.connection.CreateCommand();
            npgsqlCommand.CommandText = "SELECT kl_id FROM klienti WHERE concat_ws(' ', kl_familia, kl_name, kl_otchestvo) = '" + comboBox1.SelectedItem.ToString() + "'";
            Authorise.connection.Open();
            NpgsqlDataReader dataReader = npgsqlCommand.ExecuteReader();
            dataReader.Read();
            kl_id = dataReader[0].ToString();
            Authorise.connection.Close();
        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            NpgsqlCommand npgsqlCommand = Authorise.connection.CreateCommand();
            npgsqlCommand.CommandText = "SELECT ak_id FROM akty_rabot WHERE concat('Акт работ №', ak_id, ' от ', ak_data) = '" + comboBox4.SelectedItem.ToString() + "'";
            Authorise.connection.Open();
            NpgsqlDataReader dataReader = npgsqlCommand.ExecuteReader();
            dataReader.Read();
            id1 = dataReader[0].ToString();
            Authorise.connection.Close();
        }
    }
}
