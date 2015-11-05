using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testeDatabase
{
    public partial class Form1 : Form
    {
        private String nome;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nome = textBox1.Text;
            textBox2.Text = nome;
            listBox1.Items.Add(" jkasdka");
        }
    }
}
