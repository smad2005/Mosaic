using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace laba3
{
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
          
            mygame1.Legeng = checkBox1.Checked;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            новаяToolStripMenuItem_Click(sender, null);
        }

        private string getPic()
        {
            try
            {
                List<string> spisok = new List<string>();
                DirectoryInfo d = new DirectoryInfo(Environment.CurrentDirectory + "\\pic");

                foreach (FileInfo f in d.GetFiles("*.jpg"))
                    spisok.Add(f.FullName);
                return spisok[new Random().Next(0, spisok.Count)];
            }
            catch
            {
                MessageBox.Show("В каталог pic картинки не найдены");
                return null;
            }

        }

   
       

        private void mygame1_Finish_1(object sender, EventArgs e)
        {
            checkBox1.Enabled = false;
            checkBox1.Checked = false;
            timer1.Stop();
           // MessageBox.Show("fff");
            string o=Microsoft.VisualBasic.Interaction.InputBox("Поздравляю вы выиграли, введите ваше имя", "Поздравляю!!!", "Игрок1",-1,-1);
            if (o!="")
            {
                BD.addNewPlayer(o, mygame1.Countstep, time);
            }
        }

        private void новаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkBox1.Enabled = true;
            checkBox1.Checked = false;
            mygame1.Picture = getPic();
            time = new TimeSpan();
            string color = info.Load();
            if (color!=null)
            mygame1.LabelColor = Color.FromArgb( Convert.ToInt32(color));
            mygame1.init();
            mygame1.Legeng = false;
            if (mygame1.Picture != null)
                pictureBox1.Image = Image.FromFile(mygame1.Picture);
            else
                checkBox1.Checked = true;
            timer1.Start();
            label2.Text = "Шагов: 0";
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            новаяToolStripMenuItem_Click(sender, null);
        }

        TimeSpan time = new TimeSpan();
        private void timer1_Tick(object sender, EventArgs e)
        {
         time= time.Add(new TimeSpan(0,0,0,0,timer1.Interval));
            label1.Text = time.ToString();
        }

        private void mygame1_MoveKvadrat(object sender, EventArgs e)
        {
            label2.Text = "Шагов: " + mygame1.Countstep;
        }

        private void mygame1_Load(object sender, EventArgs e)
        {

        }

        private void статистикаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stat st = new Stat();
            st.ShowDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            статистикаToolStripMenuItem_Click(sender, null);
        }

        private void параметрыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Option opt = new Option(mygame1);
            opt.ShowDialog();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            параметрыToolStripMenuItem_Click(sender, null);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about ab = new about();
            ab.ShowDialog();
        }

        private void справкаToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            try
            {
                Help.ShowHelp(this, "help\\help.hlp");
            }
            catch
            {
                MessageBox.Show("Справка не найдена!");
            }
           
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            справкаToolStripMenuItem1_Click(sender, e);
        }

       

       
    
        

    }



}