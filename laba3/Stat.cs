using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace laba3
{
    public partial class Stat : Form
    {
        public Stat()
        {
            InitializeComponent();
        }

        private void Stat_Load(object sender, EventArgs e)
        {
            bindingSource1.DataSource = BD.ds.Tables[0];
            dataGridView1.DataSource = bindingSource1.DataSource;
            bindingSource1.Sort = "time";
            bindingSource1.Sort = "step";
            dataGridView1.Columns[0].HeaderText = "Имя";
            dataGridView1.Columns[1].HeaderText = "Кол. шагов";
            dataGridView1.Columns[2].HeaderText = "Время";
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}