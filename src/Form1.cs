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
using Dota2_Script_Maker.KV_Parser;
using ScintillaNET;

namespace Dota2_Script_Maker
{
    public partial class Form1 : Form
    {
        private string game_path;
        public static List<Cosmetic> cosmetics;
        private Unit current_unit;
        private int mode;

        public const int MODE_NEW_UNIT = 0;
        public const int MODE_DUPLICATE_UNIT = 1;
        public const int MODE_EDIT_UNIT = 2;

        private UnitExplorer unit_explorer;

        private bool closed_by_button = false;
        private bool closed_by_error;

        //If empty, then no code is unsaved. Saves the entire text file if unsaved.
        private string UnsavedCodeChanges = "";

        static Form1()
        {
            cosmetics = CosmeticsReader.ReadInSummaryFile();
        }

        public Form1(Unit unit, int mode, UnitExplorer unit_explorer)
        {
            InitializeComponent();
            CenterToScreen();

            foreach (Cosmetic c in cosmetics)
            {
                string hName = c.hero;
                if (!Cosmetic_Hero_Dropdown.Items.Contains(hName))
                    Cosmetic_Hero_Dropdown.Items.Add(hName);
            }

            this.mode = mode;
            this.unit_explorer = unit_explorer;

            if (mode == MODE_NEW_UNIT)
            {
                current_unit = new Unit(UnitExplorer.game_path);
                current_unit.sub_directory = unit_explorer.current_path;
            }

            else if (mode == MODE_DUPLICATE_UNIT)
            {
                current_unit = unit.DeepCopy();
                // Change name:
                current_unit.kv_block.name = current_unit.kv_block.name + "_Copy";
            }

            else
            {
                current_unit = unit;
                closed_by_error = !unit_explorer.reader.reloadUnit(current_unit);

                if (closed_by_error)
                {
                    Application.Exit();
                }

                Unit_Name.ReadOnly = true;
                button1.Text = "Save Changes";
            }

            game_path = UnitExplorer.game_path;

            UpdateGUIFromUnit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = current_unit.kv_block.name + " - Dota 2 Script Manager";

            // Configuring the default style with properties
            // we have common to every lexer style saves time.
            OutputCodeBox.StyleResetDefault();
            OutputCodeBox.Styles[Style.Default].Font = "Consolas";
            OutputCodeBox.Styles[Style.Default].Size = 10;
            OutputCodeBox.StyleClearAll();

            // Configure the CPP (C#) lexer styles
            OutputCodeBox.Styles[Style.Cpp.Default].ForeColor = Color.Silver;
            OutputCodeBox.Styles[Style.Cpp.Comment].ForeColor = Color.FromArgb(0, 128, 0); // Green
            OutputCodeBox.Styles[Style.Cpp.CommentLine].ForeColor = Color.FromArgb(0, 128, 0); // Green
            OutputCodeBox.Styles[Style.Cpp.CommentLineDoc].ForeColor = Color.FromArgb(128, 128, 128); // Gray
            OutputCodeBox.Styles[Style.Cpp.Number].ForeColor = Color.Olive;
            OutputCodeBox.Styles[Style.Cpp.Word].ForeColor = Color.Blue;
            OutputCodeBox.Styles[Style.Cpp.Word2].ForeColor = Color.Blue;
            OutputCodeBox.Styles[Style.Cpp.String].ForeColor = Color.FromArgb(0, 4, 200); // Blue
            OutputCodeBox.Styles[Style.Cpp.Character].ForeColor = Color.FromArgb(163, 21, 21); // Red
            OutputCodeBox.Styles[Style.Cpp.Verbatim].ForeColor = Color.FromArgb(163, 21, 21); // Red
            OutputCodeBox.Styles[Style.Cpp.StringEol].BackColor = Color.Pink;
            OutputCodeBox.Styles[Style.Cpp.Operator].ForeColor = Color.Purple;
            OutputCodeBox.Styles[Style.Cpp.Preprocessor].ForeColor = Color.Maroon;
            OutputCodeBox.Lexer = Lexer.Cpp;
        }

        private void UpdateGUIFromUnit()
        {
            Globals.CurrentUnit = current_unit;

            KVBlock block = current_unit.kv_block;

            Unit_Name.Text = block.name;

            string[] unit_keys = current_unit.GetUnitKeysForGUI();

            Model.Text = unit_keys[0];
            Model_Scale.Text = unit_keys[1];
            Level.Text = unit_keys[2];
            Inventory.Checked = unit_keys[3] != "0";
            Sound_Set.Text = unit_keys[4];
            Bounty_XP.Text = unit_keys[25];
            Bounty_Min.Text = unit_keys[13];
            Bounty_Max.Text = unit_keys[14];
            Is_Hero.Checked = unit_keys[28] != "0";
            MS.Text = unit_keys[26];
            Movement_Capabilities.Text = unit_keys[27];
            Damage_Min.Text = unit_keys[10];
            Damage_Max.Text = unit_keys[11];
            Armour.Text = unit_keys[5];
            Magic.Text = unit_keys[6];
            AS.Text = unit_keys[12];
            HP.Text = unit_keys[15];
            HP_Regen.Text = unit_keys[16];
            Mana.Text = unit_keys[17];
            Mana_Regen.Text = unit_keys[18];
            No_Attack.Checked = unit_keys[7] == "DOTA_UNIT_CAP_NO_ATTACK";
            Melee.Checked = unit_keys[7] == "DOTA_UNIT_CAP_MELEE_ATTACK";
            Ranged.Checked = unit_keys[7] == "DOTA_UNIT_CAP_RANGED_ATTACK";
            Projectile_Path.Text = unit_keys[36];
            Projectile_Speed.Text = unit_keys[37];

            if (Ranged.Checked)
            {
                Projectile_Path.Visible = true;
                Projectile_Speed.Visible = true;
                Projectile_Path.Text = unit_keys[36];
                Projectile_Speed.Text = unit_keys[37];
            }

            Range.Text = unit_keys[8];
            Damage_Type.Text = unit_keys[9];

            Ability1.Text = unit_keys[19];
            Ability2.Text = unit_keys[20];
            Ability3.Text = unit_keys[21];
            Ability4.Text = unit_keys[22];
            Ability5.Text = unit_keys[23];
            Ability6.Text = unit_keys[24];

            Primary_Attribute.Text = unit_keys[29];
            Strength.Text = unit_keys[30];
            Strength_Gain.Text = unit_keys[31];
            Agility.Text = unit_keys[34];
            Agility_Gain.Text = unit_keys[35];
            Intelligence.Text = unit_keys[32];
            Intelligence_Gain.Text = unit_keys[33];

            Cosmetics_Right.Items.Clear();
            string[] cosmetic_list = current_unit.kv_block.GetValuesMultiple("ItemDef");
            foreach (string s in cosmetic_list)
            {
                int id = Int32.Parse(s);

                foreach (Cosmetic c in cosmetics)
                {
                    if (id == c.id)
                    {
                        Cosmetics_Right.Items.Add(c);
                    }
                }
            }
        }

        private void UpdateUnitFromGUI()
        {
            string attack_capabilities = "";

            if (No_Attack.Checked)
            {
                attack_capabilities = "DOTA_UNIT_CAP_NO_ATTACK";
            }
            else if (Melee.Checked)
            {
                attack_capabilities = "DOTA_UNIT_CAP_MELEE_ATTACK";
            }
            else
            {
                attack_capabilities = "DOTA_UNIT_CAP_RANGED_ATTACK";
            }

            string[] keys = new string[] {
                    "Model",
                    "ModelScale",
                    "Level",
                    "HasInventory",
                    "SoundSet",
                    "ArmorPhysical",
                    "MagicalResistance",
                    "AttackCapabilities",
                    "AttackRange",
                    "AttackDamageType",
                    "AttackDamageMin",
                    "AttackDamageMax",
                    "AttackRate",
                    "BountyGoldMin",
                    "BountyGoldMax",
                    "StatusHealth",
                    "StatusHealthRegen",
                    "StatusMana",
                    "StatusManaRegen",
                    "Ability1",
                    "Ability2",
                    "Ability3",
                    "Ability4",
                    "Ability5",
                    "Ability6",
                    "BountyXP",
                    "MovementSpeed",
                    "MovementCapabilities",
                    "ConsideredHero",
                    "AttributePrimary",
                    "AttributeBaseStrength",
                    "AttributeStrengthGain",
                    "AttributeBaseIntelligence",
                    "AttributeIntelligenceGain",
                    "AttributeBaseAgility",
                    "AttributeAgilityGain",
                    "ProjectileModel",
                    "ProjectileSpeed"};

            string projectile_model = Projectile_Path.Text;
            string projectile_speed = Projectile_Speed.Text;

            if (projectile_model == "Projectile Model...")
                projectile_model = "";

            if (projectile_speed == "Projectile Speed...")
                projectile_speed = "";

            string hero = Is_Hero.Checked ? "1" : "0";
            string inventory = Inventory.Checked ? "1" : "0";

            string[] values = new string[] {
                    Model.Text,
                    Model_Scale.Text,
                    Level.Text,
                    inventory,
                    Sound_Set.Text,
                    Armour.Text,
                    Magic.Text,
                    attack_capabilities,
                    Range.Text,
                    Damage_Type.Text,
                    Damage_Min.Text,
                    Damage_Max.Text,
                    AS.Text,
                    Bounty_Min.Text,
                    Bounty_Max.Text,
                    HP.Text,
                    HP_Regen.Text,
                    Mana.Text,
                    Mana_Regen.Text,
                    Ability1.Text,
                    Ability2.Text,
                    Ability3.Text,
                    Ability4.Text,
                    Ability5.Text,
                    Ability6.Text,
                    Bounty_XP.Text,
                    MS.Text,
                    Movement_Capabilities.Text,
                    hero,
                    Primary_Attribute.Text,
                    Strength.Text,
                    Strength_Gain.Text,
                    Intelligence.Text,
                    Intelligence_Gain.Text,
                    Agility.Text,
                    Agility_Gain.Text,
                    projectile_model,
                    projectile_speed};

            string block_path = "";

            current_unit.kv_block.SetValues(keys, values, block_path,RemoveIfEmpty:true);
            current_unit.kv_block.name = Unit_Name.Text;

            //Clear out cosmetics:
            current_unit.kv_block.RemoveChildBlock("Creature/AttachWearables/");

            //Re-add cosmetics
            block_path = "Creature/AttachWearables/Wearable";
            for (int i = 0; i < Cosmetics_Right.Items.Count; i++)
            {
                Cosmetic c = (Cosmetic)Cosmetics_Right.Items[i];
                current_unit.kv_block.SetValue("ItemDef", c.id + "", block_path + (i + 1), true);
            }

            current_unit.unit_path = game_path + current_unit.kv_block.name + ".txt";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            unit_explorer.Visible = true;
            unit_explorer.Refresh_Units();
            closed_by_button = true;
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (closed_by_error)
            {
                unit_explorer.Visible = true;
                unit_explorer.Refresh_Units();
                return;
            }

            if (!closed_by_button)
            {
                    var save = MessageBox.Show("Would you like to save your changes before closing?",
                    "Save Changes",
                    MessageBoxButtons.YesNo);

                    if (save == DialogResult.Yes)
                    {
                        WriteToFile();
                    }
            }
            else
            {
                WriteToFile();
            }

            unit_explorer.Visible = true;
            unit_explorer.Refresh_Units();
        }

        private void WriteToFile()
        {
            UpdateUnitFromGUI();

            string unit_output = Unit_Text_Builder.Build_Text(current_unit);

            string unit_path = current_unit.sub_directory + "\\";

            System.IO.File.WriteAllText(unit_path + Unit_Name.Text + ".txt", unit_output);
        }

        private void textBox1_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void Armour_TextChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Projectile_Path.Visible = true;
            Projectile_Speed.Visible = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Projectile_Path.Visible = false;
            Projectile_Speed.Visible = false;
        }

        private void Projectile_Path_Click(object sender, EventArgs e)
        {
            Projectile_Path.Text = "";
        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }

        private void Additional_Code_Unit_TextChanged(object sender, EventArgs e)
        {

        }

        private void Projectile_Speed_Click(object sender, EventArgs e)
        {
            Projectile_Speed.Text = "";
        }

        private void Projectile_Speed_TextChanged(object sender, EventArgs e)
        {

        }

        private void Model_Scale_TextChanged(object sender, EventArgs e)
        {

        }

        private void Cosmetic_Hero_Dropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCosmetics();
        }

        private void LoadCosmetics()
        {
            Cosmetics_Left.Items.Clear();

            string hero = Cosmetic_Hero_Dropdown.Text;

            foreach(Cosmetic c in cosmetics)
            {
                if (c.hero == hero)
                {
                    Cosmetics_Left.Items.Add(c);
                }
            }
        }

        private void Cosmetics_Left_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void RemoveCosmetic_Click(object sender, EventArgs e)
        {
            ListBox.SelectedIndexCollection collection = Cosmetics_Right.SelectedIndices;

            if (collection.Count <= 0)
                return;

            int Select = 0;
            if (collection[0] > 0)
                Select = collection[0] - 1;

            for (int i = collection.Count - 1; i >= 0; i--)
            {
                Cosmetics_Right.Items.RemoveAt(collection[i]);
            }

            if(Cosmetics_Right.Items.Count > 0)
                Cosmetics_Right.SelectedIndex = Select;
        }

        private void AddCosmetic_Click(object sender, EventArgs e)
        {
            ListBox.SelectedIndexCollection collection = Cosmetics_Left.SelectedIndices;

            if (collection.Count <= 0)
                return;

            int Select = collection[collection.Count - 1] + 1;
            if(Select >= Cosmetics_Left.Items.Count)
                Select = collection[0] - 1;
            if (Select < 0)
                Select = 0;

            foreach (int index in collection) 
            {
                object o = Cosmetics_Left.Items[index];
                bool add = true;
                foreach(object o2 in Cosmetics_Right.Items)
                {
                    if (o.Equals(o2))
                        add = false;
                }
                if(add)
                    Cosmetics_Right.Items.Add(o);
            }

            Cosmetics_Left.SelectedIndex = -1;
            Cosmetics_Left.SelectedIndex = Select;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddNewBlockForm f = new AddNewBlockForm();
            f.Show();
        }

        private void MS_TextChanged(object sender, EventArgs e)
        {

        }

        private void Code_Block_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabControl2_StyleChanged(object sender, EventArgs e)
        {

        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl2.SelectedIndex == 5)
            {
                if (UnsavedCodeChanges.Length == 0)
                    UpdateUnitFromGUI(); //In case we changed things in the GUI
                RefreshCustomCodeBox();
            }
        }

        private void Code_Block_Leave(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            if (Ranged.Checked)
            {
                Projectile_Path.Visible = true;
                Projectile_Speed.Visible = true;
                Projectile_Path.Text = "Projectile Model...";
                Projectile_Speed.Text = "Projectile Speed...";
            }
            else
            {
                Projectile_Path.Visible = false;
                Projectile_Speed.Visible = false;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            closed_by_button = false;
            this.Close();
            unit_explorer.Visible = true;
            unit_explorer.Refresh_Units();
        }

        private void Projectile_Path_TextChanged(object sender, EventArgs e)
        {

        }

        private void Projectile_Path_Enter(object sender, EventArgs e)
        {
            if (Projectile_Path.Text == "Projectile Model...")
            {
                Projectile_Path.Text = "";
            }
        }

        private void Projectile_Path_Leave(object sender, EventArgs e)
        {
            if (Projectile_Path.Text == "")
            {
                Projectile_Path.Text = "Projectile Model...";
            }
        }

        private void Projectile_Speed_Enter(object sender, EventArgs e)
        {
            if (Projectile_Speed.Text == "Projectile Speed...")
            {
                Projectile_Speed.Text = "";
            }
        }

        private void Projectile_Speed_Leave(object sender, EventArgs e)
        {
            if (Projectile_Speed.Text == "")
            {
                Projectile_Speed.Text = "Projectile Speed...";
            }
        }

        private void Ability1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Cosmetic_Hero_Dropdown_SelectedIndexChanged_1(object sender, EventArgs e)
        {
             
        }

        private void Cosmetics_Left_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void AddCosmetic_Click_1(object sender, EventArgs e)
        {

        }

        private void RemoveCosmetic_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            unit_explorer.Visible = true;
            unit_explorer.Refresh_Units();
            closed_by_button = true;
            this.Close();
        }

        private void Cosmetics_Right_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void RefreshCustomCodeBox()
        {
            if (UnsavedCodeChanges.Length == 0)
                OutputCodeBox.Text = current_unit.kv_block.ToString();
            else
                OutputCodeBox.Text = UnsavedCodeChanges;

        }

        private void OutputCodeBox_Leave(object sender, EventArgs e)
        {
            UnsavedCodeChanges = OutputCodeBox.Text;
        }

        private void AcceptCodeChangeButton_Click(object sender, EventArgs e)
        {
            string text = OutputCodeBox.Text;
            KVLexer l = new KVLexer();
            List<Token> tokens = l.LexicalAnalysis(text);

            if (l.errors.Count > 0)
            {
                MessageBox.Show(l.errors[0].GetErrorMessage(), "Parsing Results");
                return;
            }

            KVParser parser = new KVParser(tokens);
            KVBlock NewUnit = parser.Parse();

            if (parser.Error != null)
            {
                MessageBox.Show(parser.Error.GetErrorMessage(), "Parsing Results");
                return;
            }

            current_unit.kv_block = NewUnit;
            UpdateGUIFromUnit();

            UnsavedCodeChanges = "";
        }

        private void DiscardChangesButton_Click(object sender, EventArgs e)
        {
            UnsavedCodeChanges = "";
            UpdateUnitFromGUI();
            RefreshCustomCodeBox();
        }

        private void Unit_Name_TextChanged(object sender, EventArgs e)
        {
            Text = Unit_Name.Text + " - Dota 2 Script Manager";
        }
    }
}
