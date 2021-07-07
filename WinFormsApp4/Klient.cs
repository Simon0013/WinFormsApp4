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
    public partial class Klient : Form
    {
        public Klient()
        {
            InitializeComponent();
        }
        public static int k = -1;
        private void Klient_Load(object sender, EventArgs e)
        {
            Sotrudnik.TableFill("Klient", "SELECT * FROM about_klient ORDER BY Номер");
            dataGridView1.DataSource = Sotrudnik.ds.Tables["Klient"];
            dataGridView1.AutoResizeColumns();
            dataGridView1.CurrentCell = null;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = null;
            k = -1;
            Client cl = new Client();
            cl.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
            {
                MessageBox.Show("Вы не выбрали вариант из списка", "Ошибка");
                return;
            }
            Client cl = new Client();
            cl.Show();
            dataGridView1.CurrentCell = null;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
            {
                MessageBox.Show("Вы не выбрали вариант из списка", "Ошибка");
                return;
            }
            string warning = "Вы точно хотите удалить данного клиента?";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(warning, "Удаление", buttons);
            if (result == DialogResult.No) return;
            NpgsqlCommand npgsqlCommand = Authorise.connection.CreateCommand();
            npgsqlCommand.CommandText = "DELETE FROM klienti WHERE kl_id = " + Sotrudnik.ds.Tables["Klient"].Rows[k]["Номер"].ToString();
            Authorise.connection.Open();
            npgsqlCommand.ExecuteNonQuery();
            Authorise.connection.Close();
            Sotrudnik.ds.Tables["Klient"].Rows.RemoveAt(k);
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            k = dataGridView1.CurrentRow.Index;
        }
    }
}
