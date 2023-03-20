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

namespace Dota2_Script_Maker
{
    public partial class Form2 : Form
    {
        private string[] config;
        private StreamWriter config_file;
        
        public Form2()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            CenterToScreen();

            if (File.Exists("Config.txt"))
            {
                config = File.ReadAllLines("Config.txt");
                textBox2.Text = config[0];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UnitExplorer form = new UnitExplorer(textBox2.Text);
            form.Show();
            this.Hide();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //DialogResult result = folderBrowserDialog1.ShowDialog();

            //if (result == DialogResult.OK)
            //{
            //    textBox1.Text = folderBrowserDialog1.SelectedPath;
            //}
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog2.ShowDialog();

            if (result == DialogResult.OK)
            {
                textBox2.Text = folderBrowserDialog2.SelectedPath;
                config_file = File.CreateText("Config.txt");
                config_file.WriteLine(textBox2.Text);
                config_file.Close();
            }
        }
    }
}
