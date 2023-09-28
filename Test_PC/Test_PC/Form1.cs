using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Test_PC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (NameText.Text == "" || GrupText.Text == "")
            {
                MessageBox.Show("Не введено ФИО или Группа", "Введите данные", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                StreamWriter SW = new StreamWriter("NAME.txt");
                SW.WriteLine(NameText.Text);
                SW.Write(GrupText.Text);
                SW.Close();
                Form2 form2 = new Form2();
                form2.Show();
                this.Hide();
                form2.Location = this.Location;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Leaders led = new Leaders();
            led.Show();
            this.Hide();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
