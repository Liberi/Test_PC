using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test_PC
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            for (int i = 0; i < radioButtons.Length; i++)
            {
                radioButtons[i] = new RadioButton();
                //radioButtons[i].Location = new Point(groupBox1.Location.X + 5, 30 + i * 30);
                radioButtons[i].SetBounds(groupBox1.Location.X + 5, 30 + i * 30, groupBox1.Width / 2, 25);
                groupBox1.Controls.Add(radioButtons[i]);
                this.radioButtons[i].Click += new EventHandler(this.chengRadioButton);
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            testStart();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Вы точно хотите выйти?", "ВЫХОД", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes) { Application.Exit(); }
        }
        void radioButtonVisible(bool visibleFT)
        {
            for (int i = 0; i < radioButtons.Length; i++)
            {
                radioButtons[i].Visible = visibleFT;
            }
        }
        string MemberName, MemberGroup;
        private void button2_Click(object sender, EventArgs e)
        {
            if (NumTrueAnswer == NumAnswer) TrueAnswer++;
            if (NumTrueAnswer != NumAnswer) { FalseAnswer++; FalseQuest += $"{QuestionsText.Text} \n"; }
            if (TrueAnswer == QuestСount) FalseQuest = "Все ответы верны";
            if (button2.Text == "Завершить тест")
            {
                redTest.Close();
                radioButtonVisible(false);
                StreamReader redNAME = new StreamReader("NAME.txt");
                MemberName = redNAME.ReadLine();
                MemberGroup = redNAME.ReadLine();
                MessageBox.Show($"Тестирование {MemberName} завершено.\n" +
                                $"Правильных ответов: {TrueAnswer} из {QuestСount}.\n" +
                                $"Оценка в пятибалльной системе: {(TrueAnswer * 5.0F) / QuestСount:F2}.",
                                $"Ваши результаты из группы {MemberGroup}",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Question);
                MessageBox.Show($"Тестирование {MemberName} завершено.\n" +
                                $"Неправильные ответы:\n{FalseQuest}",
                                $"Ваши результаты из группы {MemberGroup}",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Question);
                redNAME.Close();
                File.AppendAllText("Список участников.txt", $"\r{MemberName} из группы {MemberGroup}\n" +
                                 $"{(TrueAnswer * 5.0f) / QuestСount:F2}");
                button2.Text = "Повторить тестирование";
                return;
            }
            if (button2.Text == "Повторить тестирование")
            {
                button2.Text = "Далее";
                QuestionsText.Text = "";
                radioButtonVisible(true);
                testStart(); return;
            }
            if (button2.Text == "Далее")
            {
                nextQuestions();
            }

        }
        string FalseQuest;
        int QuestСount, TrueAnswer, FalseAnswer, NumTrueAnswer, NumAnswer;
        StreamReader redTest;
        RadioButton[] radioButtons = new RadioButton[4];
        void testStart()
        {
            redTest = new StreamReader("TEST.txt");
            this.Text = redTest.ReadLine();
            QuestСount = 0; TrueAnswer = 0; FalseAnswer = 0;
            FalseQuest = "";
            nextQuestions();
        }
        void nextQuestions()
        {
            QuestionsText.Text = redTest.ReadLine();
            QuestionsText.SelectionStart = 0;
            for (int i = 0; i < radioButtons.Length; i++)
            {
                /*RadioButton RB = this.Controls[radioButtons[i]] as RadioButton;
                RB.Text = redTest.ReadLine();*/
                radioButtons[i].Text = redTest.ReadLine();
                radioButtons[i].Checked = false;
            }
            NumTrueAnswer = int.Parse(redTest.ReadLine());
            button2.Enabled = false;
            QuestСount++;
            if (redTest.EndOfStream) { button2.Text = "Завершить тест"; }


        }
        void chengRadioButton(Object sender, EventArgs e)
        {
            button2.Enabled = true; button2.Focus();
            for (int i = 0; i < radioButtons.Length; i++)
            {
                if (radioButtons[i].Checked) NumAnswer = i + 1;
            }

        }

    }
}
