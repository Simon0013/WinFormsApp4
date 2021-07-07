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
    public partial class Sotrud : Form
    {
        public Sotrud()
        {
            InitializeComponent();
        }
        private void Sotrud_Load(object sender, EventArgs e)
        {
            if (Sotrudnik.k > -1)
            {
                NpgsqlCommand command = Authorise.connection.CreateCommand();
                command.CommandText = "SELECT * FROM sotrudniki WHERE st_id = " + Sotrudnik.ds.Tables["Sotrudniki"].Rows[Sotrudnik.k]["Номер"].ToString();
                Authorise.connection.Open();
                NpgsqlDataReader dataReader = command.ExecuteReader();
                dataReader.Read();
                textBox1.Text = dataReader["st_familia"].ToString();
                textBox2.Text = dataReader["st_imya"].ToString();
                textBox3.Text = dataReader["st_otchestvo"].ToString();
                textBox4.Text = dataReader["st_doljnost"].ToString();
                textBox5.Text = dataReader["st_address"].ToString();
                textBox6.Text = dataReader["st_zarplata"].ToString();
                Authorise.connection.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (Sotrudnik.k == -1)
            {
                NpgsqlCommand command = Authorise.connection.CreateCommand();
                command.CommandText = "SELECT MAX(st_id)+1 FROM sotrudniki";
                Authorise.connection.Open();
                NpgsqlDataReader dataReader = command.ExecuteReader();
                dataReader.Read();
                string id = dataReader[0].ToString();
                Authorise.connection.Close();
                command = Authorise.connection.CreateCommand();
                command.CommandText = "INSERT INTO sotrudniki VALUES (" + id + ", '" + textBox1.Text + "', '" + textBox3.Text + "', '" + textBox2.Text + "', '" + textBox4.Text + "', '" + textBox5.Text + "', '" + textBox6.Text + "')";
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
                Sotrudnik.ds.Tables["Sotrudniki"].Rows.Add(new object[] {id, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text});
            }
            else
            {
                NpgsqlCommand command = Authorise.connection.CreateCommand();
                command.CommandText = "UPDATE sotrudniki SET st_familia = '" + textBox1.Text + "', st_otchestvo = '" + textBox3.Text + "', st_imya = '" + textBox2.Text + "', st_doljnost = '" + textBox4.Text + "', st_address = '" + textBox5.Text + "', st_zarplata = '" + textBox6.Text + "' WHERE st_id = " + Sotrudnik.ds.Tables["Sotrudniki"].Rows[Sotrudnik.k]["Номер"];
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
                Sotrudnik.ds.Tables["Sotrudniki"].Rows.Add(new object[] { Sotrudnik.ds.Tables["Sotrudniki"].Rows[Sotrudnik.k]["Номер"], textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text });
                Sotrudnik.ds.Tables["Sotrudniki"].Rows.RemoveAt(Sotrudnik.k);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (Sotrudnik.k == -1)
            {
                NpgsqlCommand command = Authorise.connection.CreateCommand();
                command.CommandText = "SELECT MAX(st_id)+1 FROM sotrudniki";
                Authorise.connection.Open();
                NpgsqlDataReader dataReader = command.ExecuteReader();
                dataReader.Read();
                string id = dataReader["st_id"].ToString();
                Authorise.connection.Close();
                command = Authorise.connection.CreateCommand();
                command.CommandText = "INSERT INTO sotrudniki VALUES (" + id + ", '" + textBox1.Text + "', '" + textBox3.Text + "', '" + textBox2.Text + "', '" + textBox4.Text + "', '" + textBox5.Text + "', '" + textBox6.Text + "')";
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
                Sotrudnik.ds.Tables["Sotrudniki"].Rows.Add(new object[] { id, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text });
            }
            else
            {
                NpgsqlCommand command = Authorise.connection.CreateCommand();
                command.CommandText = "UPDATE sotrudniki SET st_familia = '" + textBox1.Text + "', st_otchestvo = '" + textBox3.Text + "', st_imya = '" + textBox2.Text + "', st_doljnost = '" + textBox4.Text + "', st_address = '" + textBox5.Text + "', st_zarplata = '" + textBox6.Text + "' WHERE st_id = " + Sotrudnik.ds.Tables["Sotrudniki"].Rows[Sotrudnik.k]["Номер"];
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
                Sotrudnik.ds.Tables["Sotrudniki"].Rows.Add(new object[] { Sotrudnik.ds.Tables["Sotrudniki"].Rows[Sotrudnik.k]["Номер"], textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text });
                Sotrudnik.ds.Tables["Sotrudniki"].Rows.RemoveAt(Sotrudnik.k);
            }
            Close();
        }
    }
}
