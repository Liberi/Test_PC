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
    public partial class Leaders : Form
    {
        public Leaders()
        {
            InitializeComponent();
        }

        private void Leaders_Load(object sender, EventArgs e)
        {
            StreamReader redMember = new StreamReader("Список участников.txt");
            string name = "";
            float number = 0;
            Dictionary<string, float> liders = new Dictionary<string, float>();
            while (!redMember.EndOfStream)
            {
                name = redMember.ReadLine();
                number = Single.Parse(redMember.ReadLine());
                liders.Add(name, number);
            }
            redMember.Close();

            float Max = liders.ElementAt(0).Value;
            string nameKey = "";
            float numberValue = 0;
            for (int i = 0; i < 3; i++)
            {
                foreach (var item in liders)
                {
                    if (Max <= item.Value)
                    {
                        Max = item.Value;
                        nameKey = item.Key;
                        numberValue = item.Value;
                    }
                }
                MemberText.Text += $"Участник: {nameKey} с балом {numberValue}\r\n";
                liders.Remove(nameKey);
                Max = liders.ElementAt(0).Value;
            }
            MemberText.SelectionStart = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void Leaders_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
