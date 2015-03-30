using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace laba3
{
    public partial class Option : Form
    {
        public Option(mygame mygame)
        {
            InitializeComponent();
            this.mygame = mygame;
            temp = mygame.LabelColor;
        }
        Color temp;
        private mygame mygame;
        private void button1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            { 
                label1.ForeColor = colorDialog1.Color;
                mygame.LabelColor = colorDialog1.Color;
                refresh();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            info.Save(label1.ForeColor.ToArgb().ToString());
            mygame.LabelColor = label1.ForeColor;
            refresh();
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mygame.LabelColor = temp;
            refresh();
            Close();
        }

        private void refresh()
        {
            mygame.Legeng = !mygame.Legeng;
            mygame.Legeng = !mygame.Legeng;
        }

        private void Option_Load(object sender, EventArgs e)
        {
            string color = info.Load();
            if (color != null)
                label1.ForeColor = Color.FromArgb(Convert.ToInt32(color));
        }
    }
}