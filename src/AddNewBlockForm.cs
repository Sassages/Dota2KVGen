using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dota2_Script_Maker
{
    public partial class AddNewBlockForm : Form
    {
        public AddNewBlockForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CustomCodeBlock Parent = (CustomCodeBlock)NestedBlockDropdown.SelectedItem;
            if (Parent == null)
                return;

            StringBuilder name = new StringBuilder(BlockName.Text);
            if(name[0] != '"')
            {
                name.Insert(0, "\"");
                name.Append("\"");
            }

            CustomCodeBlock b = new CustomCodeBlock(name.ToString());
            Parent.AppendBlock(b);

            this.Close();
        }

        private void AddCodeBlockToList(CustomCodeBlock b)
        {
            NestedBlockDropdown.Items.Add(b);
            foreach (CustomCodeBlock child in b.blocks)
            {
                AddCodeBlockToList(child);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
