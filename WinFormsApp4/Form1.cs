using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            label2.Text = Authorise.surname + " " + Authorise.name + " " + Authorise.otchestvo;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                Sotrudnik sotr = new Sotrudnik();
                sotr.Show();
            }
            else if (radioButton2.Checked)
            {
                Klient klient = new Klient();
                klient.Show();
            }
            else if (radioButton3.Checked)
            {
                Uslugi uslugi = new Uslugi();
                uslugi.Show();
            }
            else if (radioButton4.Checked)
            {
                Akty akty = new Akty();
                akty.Show();
            }
            else
            {
                MessageBox.Show("Вы не выбрали вариант из списка", "Ошибка");
                return;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
