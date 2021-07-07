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
    public partial class Sotrudnik : Form
    {
        public Sotrudnik()
        {
            InitializeComponent();
        }
        public static DataSet ds = new DataSet();
        public static int k = -1;
        public static void TableFill(string name, string sql)
        {
            Authorise.connection.Open();
            if (ds.Tables[name] != null)
                ds.Tables[name].Clear();
            NpgsqlDataAdapter da;
            da = new NpgsqlDataAdapter(sql, Authorise.connection);
            da.Fill(ds, name);
            Authorise.connection.Close();
        }
        private void Sotrudnik_Load(object sender, EventArgs e)
        {
            TableFill("Sotrudniki", "SELECT * FROM about_sotrud ORDER BY Номер");
            dataGridView1.DataSource = ds.Tables["Sotrudniki"];
            dataGridView1.AutoResizeColumns();
            dataGridView1.CurrentCell = null;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = null;
            k = -1;
            Sotrud sotrud = new Sotrud();
            sotrud.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
            {
                MessageBox.Show("Вы не выбрали вариант из списка", "Ошибка");
                return;
            }
            Sotrud sotrud = new Sotrud();
            sotrud.Show();
            dataGridView1.CurrentCell = null;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
            {
                MessageBox.Show("Вы не выбрали вариант из списка", "Ошибка");
                return;
            }
            string warning = "Вы точно хотите удалить данного сотрудника?";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(warning, "Удаление", buttons);
            if (result == DialogResult.No) return;
            NpgsqlCommand npgsqlCommand = Authorise.connection.CreateCommand();
            npgsqlCommand.CommandText = "DELETE FROM sotrudniki WHERE st_id = " + ds.Tables["Sotrudniki"].Rows[k]["Номер"].ToString();
            Authorise.connection.Open();
            npgsqlCommand.ExecuteNonQuery();
            Authorise.connection.Close();
            ds.Tables["Sotrudniki"].Rows.RemoveAt(k);
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            k = dataGridView1.CurrentRow.Index;
        }
    }
}
