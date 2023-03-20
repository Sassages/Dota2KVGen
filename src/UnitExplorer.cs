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
    public partial class UnitExplorer : Form
    {
        public static string game_path;
        public Unit_Reader reader;
        private List<Unit> existing_units;
        public string current_path;

        public UnitExplorer(string game_path_input)
        {
            InitializeComponent();
            CenterToScreen();
            game_path = game_path_input;
            game_path += "\\npc\\units";
            //current_path = game_path;
            reader = new Unit_Reader();
        }

        private void UnitExplorer_Load(object sender, EventArgs e)
        {
            Create_Unit_Tooltip.SetToolTip(Create_New_Unit, "Create a new blank unit in the current folder");
            Duplicate_Tooltip.SetToolTip(Create_Duplicate, "Create a new unit using the existing values of another unit");
            Edit_Unit_Tooltip.SetToolTip(Edit_Unit, "Edit an existing unit");

            Refresh_Units();
        }

        private void Create_New_Unit_Click(object sender, EventArgs e)
        {
                Form1 form = new Form1((Unit)Existing_Units.SelectedItem, 0, this);
                form.Show();
                this.Hide();
        }

        private void Create_Duplicate_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1((Unit)Existing_Units.SelectedItem, 1, this);
            form.Show();
            this.Hide();
        }

        private void Edit_Unit_Click(object sender, EventArgs e)
        {
            if (Existing_Units.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select a Unit to Edit", "Select Unit", MessageBoxButtons.OK);
                return;
            }

            Form1 form = new Form1((Unit)Existing_Units.SelectedItem, 2, this);
            form.Show();
            this.Hide();
        }

        private void UnitExplorer_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        public void AddNewUnit(Unit unit)
        {
            existing_units.Add(unit);
            Existing_Units.Items.Add(unit);
        }

        private void Delete_Unit_Click(object sender, EventArgs e)
        {
            Unit unit = (Unit)Existing_Units.SelectedItem;
            System.IO.File.Delete(unit.unit_path);
            Existing_Units.Items.RemoveAt(Existing_Units.SelectedIndex);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayUnits();
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            Refresh_Units();
        }

        public void Refresh_Units()
        {
            // Populate existing units:
            existing_units = reader.ReadExistingUnits(game_path);

            List<string> sub_directories = reader.all_sub_directories;

            Current_Folder.Items.Clear();
            for (int i = 0; i < sub_directories.Count; i++)
            {
                string s = "";
                if (sub_directories[i].Length == game_path.Length)
                {
                    s = "Root";
                }
                else
                {
                    s = sub_directories[i].Substring(game_path.Length + 1, sub_directories[i].Length - game_path.Length - 1);
                }
                
                if (!Current_Folder.Items.Contains(s))
                {
                    Current_Folder.Items.Add(s);
                }
            }

            DisplayUnits();
        }

        private void DisplayUnits()
        {
            if (Current_Folder.Text == "Root" || Current_Folder.Text == "")
            {
                current_path = game_path;
            }
            else
            {
                current_path = game_path + "\\" + Current_Folder.Text;
            }

            Existing_Units.Items.Clear();

            for (int i = 0; i < existing_units.Count(); i++)
            {
                if (Current_Folder.Text == "Root")
                {
                    if (existing_units[i].sub_directory.Length == game_path.Length)
                        Existing_Units.Items.Add(existing_units[i]);
                }
                else if (existing_units[i].sub_directory.Length > game_path.Length)
                {
                    string s = existing_units[i].sub_directory.Substring(game_path.Length + 1, existing_units[i].sub_directory.Length - game_path.Length - 1);

                    if (s == Current_Folder.Text)
                    {
                        Existing_Units.Items.Add(existing_units[i]);
                    }
                }
            }
        }

        private void Existing_Units_DoubleClick(object sender, EventArgs e)
        {
            if (Existing_Units.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select a Unit to Edit", "Select Unit", MessageBoxButtons.OK);
                return;
            }

            Form1 form = new Form1((Unit)Existing_Units.SelectedItem, 2, this);
            form.Show();
            this.Hide();
        }

        private void Create_Unit_Tooltip_Popup(object sender, PopupEventArgs e)
        {

        }
    }
}
