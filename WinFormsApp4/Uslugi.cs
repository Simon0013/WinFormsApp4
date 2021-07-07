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
    public partial class Uslugi : Form
    {
        public Uslugi()
        {
            InitializeComponent();
        }
        public static int k = -1;
        private void Uslugi_Load(object sender, EventArgs e)
        {
            Sotrudnik.TableFill("Uslugi", "SELECT * FROM about_uslugi ORDER BY Номер");
            dataGridView1.DataSource = Sotrudnik.ds.Tables["Uslugi"];
            dataGridView1.AutoResizeColumns();
            dataGridView1.CurrentCell = null;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = null;
            k = -1;
            Service service = new Service();
            service.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
            {
                MessageBox.Show("Вы не выбрали вариант из списка", "Ошибка");
                return;
            }
            Service service = new Service();
            service.Show();
            dataGridView1.CurrentCell = null;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
            {
                MessageBox.Show("Вы не выбрали вариант из списка", "Ошибка");
                return;
            }
            string warning = "Вы точно хотите удалить данную услугу?";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(warning, "Удаление", buttons);
            if (result == DialogResult.No) return;
            NpgsqlCommand npgsqlCommand = Authorise.connection.CreateCommand();
            npgsqlCommand.CommandText = "DELETE FROM uslugi WHERE us_id = " + Sotrudnik.ds.Tables["Uslugi"].Rows[k]["Номер"].ToString();
            Authorise.connection.Open();
            npgsqlCommand.ExecuteNonQuery();
            Authorise.connection.Close();
            Sotrudnik.ds.Tables["Uslugi"].Rows.RemoveAt(k);
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            k = dataGridView1.CurrentRow.Index;
        }
    }
}
