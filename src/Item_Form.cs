using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScintillaNET;

using Dota2_Script_Maker.Item_Code;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Dota2_Script_Maker
{
    public partial class Item_Form : Form
    {
        private Scintilla s;
        private string default_function_block;
        public Item_Form()
        {
            InitializeComponent();
        }

        private void Reset()
        {
            Item_ID.Text = "2000";
            Item_Name.Text = "";
            Texture_Name.Text = "";
            Declarations.Text = "";
            Gold_Cost.Text = "0";
            Initial_Stock.Text = "0";
            Max_Stock.Text = "0";
            Stock_Recharge_Time.Text = "0";
            Shareability.Text = "";
            Sellable.Checked = false;
            Killable.Checked = false;
            Dropable.Checked = false;
            Permanent.Checked = false;
            Function_Name.Text = "";
            Function_Code.Text = "";

            for(int i = 0; i < Added_Functions.Items.Count; i++)
            {
                Function_Name.Items.Add(Added_Functions.Items[i].ToString());
            }

            Added_Functions.Items.Clear();
            Ability_Animation.Text = "";
            Ability_Behavior.Text = "";
            Cooldown.Text = "0";
            Mana_Cost.Text = "0";
            Initial_Charges.Text = "0";
            Base_Level.Text = "0";
            Max_Level.Text = "0";
            Display_Charges.Checked = false;
            Requires_Charges.Checked = false;
            Stackable.Checked = false;
            Cast_On_Pickup.Checked = false;

            Added_Modifiers.Items.Clear();
        }

        private void Item_Form_Load(object sender, EventArgs e)
        {
            default_function_block = Function_Code.Text;
            Reset();

            // Configuring the default style with properties
            // we have common to every lexer style saves time.
            Function_Code.StyleResetDefault();
            Function_Code.Styles[Style.Default].Font = "Consolas";
            Function_Code.Styles[Style.Default].Size = 10;
            Function_Code.StyleClearAll();

            // Configure the CPP (C#) lexer styles
            Function_Code.Styles[Style.Cpp.Default].ForeColor = Color.Silver;
            Function_Code.Styles[Style.Cpp.Comment].ForeColor = Color.FromArgb(0, 128, 0); // Green
            Function_Code.Styles[Style.Cpp.CommentLine].ForeColor = Color.FromArgb(0, 128, 0); // Green
            Function_Code.Styles[Style.Cpp.CommentLineDoc].ForeColor = Color.FromArgb(128, 128, 128); // Gray
            Function_Code.Styles[Style.Cpp.Number].ForeColor = Color.Olive;
            Function_Code.Styles[Style.Cpp.Word].ForeColor = Color.Blue;
            Function_Code.Styles[Style.Cpp.Word2].ForeColor = Color.Blue;
            Function_Code.Styles[Style.Cpp.String].ForeColor = Color.FromArgb(0, 4, 200); // Blue
            Function_Code.Styles[Style.Cpp.Character].ForeColor = Color.FromArgb(163, 21, 21); // Red
            Function_Code.Styles[Style.Cpp.Verbatim].ForeColor = Color.FromArgb(163, 21, 21); // Red
            Function_Code.Styles[Style.Cpp.StringEol].BackColor = Color.Pink;
            Function_Code.Styles[Style.Cpp.Operator].ForeColor = Color.Purple;
            Function_Code.Styles[Style.Cpp.Preprocessor].ForeColor = Color.Maroon;
            Function_Code.Lexer = Lexer.Cpp;

            // Configuring the default style with properties
            // we have common to every lexer style saves time.
            Modifier_Function_Code.StyleResetDefault();
            Modifier_Function_Code.Styles[Style.Default].Font = "Consolas";
            Modifier_Function_Code.Styles[Style.Default].Size = 10;
            Modifier_Function_Code.StyleClearAll();

            // Configure the CPP (C#) lexer styles
            Modifier_Function_Code.Styles[Style.Cpp.Default].ForeColor = Color.Silver;
            Modifier_Function_Code.Styles[Style.Cpp.Comment].ForeColor = Color.FromArgb(0, 128, 0); // Green
            Modifier_Function_Code.Styles[Style.Cpp.CommentLine].ForeColor = Color.FromArgb(0, 128, 0); // Green
            Modifier_Function_Code.Styles[Style.Cpp.CommentLineDoc].ForeColor = Color.FromArgb(128, 128, 128); // Gray
            Modifier_Function_Code.Styles[Style.Cpp.Number].ForeColor = Color.Olive;
            Modifier_Function_Code.Styles[Style.Cpp.Word].ForeColor = Color.Blue;
            Modifier_Function_Code.Styles[Style.Cpp.Word2].ForeColor = Color.Blue;
            Modifier_Function_Code.Styles[Style.Cpp.String].ForeColor = Color.FromArgb(0, 4, 200); // Blue
            Modifier_Function_Code.Styles[Style.Cpp.Character].ForeColor = Color.FromArgb(163, 21, 21); // Red
            Modifier_Function_Code.Styles[Style.Cpp.Verbatim].ForeColor = Color.FromArgb(163, 21, 21); // Red
            Modifier_Function_Code.Styles[Style.Cpp.StringEol].BackColor = Color.Pink;
            Modifier_Function_Code.Styles[Style.Cpp.Operator].ForeColor = Color.Purple;
            Modifier_Function_Code.Styles[Style.Cpp.Preprocessor].ForeColor = Color.Maroon;
            Modifier_Function_Code.Lexer = Lexer.Cpp;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selected_index = Added_Functions.SelectedIndex;

            Function_Code.Text = "";
            Function_Name.Text = "";

            if (selected_index == -1)
            {
                Add_Function.Text = "Add Function";
                return;
            }
            else
            {
                Add_Function.Text = "Update Function";
            }

            Item_Function function = (Item_Function)Added_Functions.Items[selected_index];

            string s = "";

            for (int i = 0; i < function.function_code.Length; i++)
            {
                s += function.function_code[i];
            }

            Function_Code.Text = s;
        }

        private void Add_Function_Click(object sender, EventArgs e)
        {
            if (Added_Functions.SelectedIndex != -1) // Edit an existing function
            {
                Item_Function function = (Item_Function)Added_Functions.Items[Added_Functions.SelectedIndex];

                string[] lines = new string[Function_Code.Lines.Count];

                for (int i = 0; i < lines.Length; i++)
                {
                    lines[i] = Function_Code.Lines[i].Text;
                }

                function.function_code = lines;
                Added_Functions.SelectedIndex = -1;
            }
            else
            {

                string input = Function_Name.Text;

                if (input == "")
                    return;

                Item_Function new_function = new Item_Function();
                new_function.function_name = Function_Name.Text;

                string[] lines = new string[Function_Code.Lines.Count];

                for (int i = 0; i < lines.Length; i++)
                {
                    lines[i] = Function_Code.Lines[i].Text;
                }

                new_function.function_code = lines;
                Added_Functions.Items.Add(new_function);

                object to_delete = null;
                foreach (object o in Function_Name.Items)
                {
                    string function = (string)o;

                    if (function == new_function.function_name)
                    {
                        to_delete = o;
                        continue;
                    }
                }
                Function_Name.Items.Remove(to_delete);

                Function_Name.Text = "";
                Function_Code.Text = default_function_block;
            }
        }

        private void Remove_Function_Click(object sender, EventArgs e)
        {
            if (Added_Functions.SelectedIndex != -1)
            {
                Item_Function function = (Item_Function)Added_Functions.Items[Added_Functions.SelectedIndex];
                Function_Name.Items.Add(function.function_name);
                Added_Functions.Items.RemoveAt(Added_Functions.SelectedIndex);
            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Item item = new Item();

            item.item_id = Item_ID.Text;
            item.item_name = Item_Name.Text;
            item.texture_name = Texture_Name.Text;
            item.gold_cost = Gold_Cost.Text;
            item.stock_initial = Initial_Stock.Text;
            item.stock_max = Max_Stock.Text;
            item.stock_recharge_time = Stock_Recharge_Time.Text;
            item.shareability = Shareability.Text;
            item.sellable = (Sellable.Checked) ? "1" : "0";
            item.killable = (Killable.Checked) ? "1" : "0";
            item.dropable = (Dropable.Checked) ? "1" : "0";
            item.permanent = (Permanent.Checked) ? "1" : "0";

            foreach(object o in Added_Functions.Items)
            {
                Item_Function function = (Item_Function)o;
                item.functions.Add(function);
            }

            item.ability_behavior = Ability_Behavior.Text;
            item.animation = Ability_Animation.Text;
            item.cooldown = Cooldown.Text;
            item.mana_cost = Mana_Cost.Text;
            item.initial_charges = Initial_Charges.Text;
            item.base_level = Base_Level.Text;
            item.max_upgrade_level = Max_Level.Text;
            item.display_charges = (Display_Charges.Checked) ? "1" : "0";
            item.requires_charges = (Requires_Charges.Checked) ? "1" : "0";
            item.stackable = (Stackable.Checked) ? "1" : "0";
            item.cast_on_pickup = (Cast_On_Pickup.Checked) ? "1" : "0";
            item.declarations = Declarations.Text;

            foreach(object o in Added_Modifiers.Items)
            {
                Modifier m = (Modifier)o;
                item.modifiers.Add(m);
            }

            string item_output = Item_Text_Builder.Build_Text(item);
            System.IO.File.WriteAllText("Test_Item.txt", item_output);

            Reset();
        }

        private void Function_Code_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void Added_Modifiers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Remove_Selected_Modifier_Click(object sender, EventArgs e)
        {
            if (Added_Modifiers.SelectedIndex != -1)
            Added_Modifiers.Items.RemoveAt(Added_Modifiers.SelectedIndex);
        }

        private void Add_Modifier_Click(object sender, EventArgs e)
        {
            Modifier modifier = new Modifier();
            modifier.modifier_name = Modifier_Name.Text;
            modifier.effect_name = Modifier_Effect_Name.Text;
            modifier.texture = Modifier_Texture_Name.Text;
            modifier.duration = Modifier_Duration.Text;
            modifier.think_interval = Modifier_Think_Interval.Text;
            modifier.is_purgable = (Modifier_Purgeable.Checked) ? "1" : "0";
            modifier.is_passive = (Modifier_Passive.Checked) ? "1" : "0";
            modifier.is_hidden = (Modifier_Hidden.Checked) ? "1" : "0";
            modifier.is_buff = (Modifier_Buff.Checked) ? "1" : "0";
            modifier.is_debuff = (Modifier_Debuff.Checked) ? "1" : "0";

            foreach(object o in Modifier_Added_Functions.Items)
            {
                Item_Function function = (Item_Function)o;

                modifier.modifier_functions.Add(function);
            }

            Added_Modifiers.Items.Add(modifier);
            ResetModifiers();
        }

        private void Modifier_Add_Function_Click(object sender, EventArgs e)
        {
            if (Modifier_Added_Functions.SelectedIndex != -1) // Edit an existing function
            {
                Item_Function function = (Item_Function)Modifier_Added_Functions.Items[Modifier_Added_Functions.SelectedIndex];

                string[] lines = new string[Modifier_Function_Code.Lines.Count];

                for (int i = 0; i < lines.Length; i++)
                {
                    lines[i] = Modifier_Function_Code.Lines[i].Text;
                }

                function.function_code = lines;
                Modifier_Function_Code.Text = "";
                Modifier_Added_Functions.SelectedIndex = -1;
            }
            else
            {

                string input = Modifier_Function_Name.Text;

                if (input == "")
                    return;

                Item_Function new_function = new Item_Function();
                new_function.function_name = Modifier_Function_Name.Text;
                string[] lines = new string[Modifier_Function_Code.Lines.Count];

                for(int i = 0; i < lines.Length; i++)
                {
                    lines[i] = Modifier_Function_Code.Lines[i].Text;
                }

                new_function.function_code = lines;
                Modifier_Added_Functions.Items.Add(new_function);

                object to_delete = null;
                foreach (object o in Modifier_Function_Name.Items)
                {
                    string function = (string)o;

                    if (function == new_function.function_name)
                    {
                        to_delete = o;
                        continue;
                    }
                }
                Modifier_Function_Name.Items.Remove(to_delete);

                Modifier_Function_Name.Text = "";
                Modifier_Function_Code.Text = default_function_block;
            }
        }

        private void Modifier_Remove_Function_Click(object sender, EventArgs e)
        {
            if (Modifier_Added_Functions.SelectedIndex != -1)
            {
                Item_Function function = (Item_Function)Modifier_Added_Functions.Items[Modifier_Added_Functions.SelectedIndex];
                Modifier_Function_Name.Items.Add(function.function_name);
                Modifier_Added_Functions.Items.RemoveAt(Modifier_Added_Functions.SelectedIndex);
                Modifier_Added_Functions.SelectedIndex = -1;
                Modifier_Function_Code.Text = "";
            }
        }

        private void Modifier_Added_Functions_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selected_index = Modifier_Added_Functions.SelectedIndex;

            Modifier_Function_Name.Text = "";
            Modifier_Function_Code.Text = "";

            if (selected_index == -1)
            {
                Modifier_Add_Function.Text = "Add Function";
                return;
            }
            else
            {
                Modifier_Add_Function.Text = "Update Function";
            }

            Item_Function function = (Item_Function)Modifier_Added_Functions.Items[selected_index];

            string s = "";

            for(int i = 0; i < function.function_code.Length; i++)
            {
                s += function.function_code[i];
            }

            Modifier_Function_Code.Text = s;
        }

        private void Modifier_Added_Functions_MouseCaptureChanged(object sender, EventArgs e)
        {
            
        }

        private void Modifier_Function_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            Modifier_Added_Functions.SelectedIndex = -1;
        }

        private void Modifier_Function_Code_Click(object sender, EventArgs e)
        {

        }

        private void Modifier_Function_Name_Leave(object sender, EventArgs e)
        {
        }

        private void Function_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            Added_Functions.SelectedIndex = -1;
        }

        private void Reset_Button_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Added_Modifiers_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (Added_Modifiers.SelectedIndex == -1)
            {
                ResetModifiers();
                return;
            }

            Modifier m = (Modifier)Added_Modifiers.Items[Added_Modifiers.SelectedIndex];

            for(int i = 0; i < m.modifier_functions.Count; i++)
            {
                Modifier_Added_Functions.Items.Add(m.modifier_functions[i]);
            }
        }

        private void ResetModifiers()
        {
            Modifier_Name.Text = "";
            Modifier_Effect_Name.Text = "";
            Modifier_Texture_Name.Text = "";
            Modifier_Duration.Text = "0";
            Modifier_Think_Interval.Text = "0";
            Modifier_Purgeable.Checked = false;
            Modifier_Passive.Checked = false;
            Modifier_Hidden.Checked = false;
            Modifier_Debuff.Checked = false;
            Modifier_Buff.Checked = false;

            Modifier_Function_Name.Text = "";
            Modifier_Function_Code.Text = "";

            for (int i = 0; i < Modifier_Added_Functions.Items.Count; i++)
            {
                Modifier_Function_Name.Items.Add(Modifier_Added_Functions.Items[i].ToString());
            }

            Modifier_Added_Functions.Items.Clear();
        }

        private void Remove_Selected_Modifier_Click_1(object sender, EventArgs e)
        {
            if (Added_Modifiers.SelectedIndex == -1)
            {
                return;
            }

            Added_Modifiers.Items.RemoveAt(Added_Modifiers.SelectedIndex);
            Added_Modifiers.SelectedIndex = -1;
        }

        private void Function_Code_TextChanged_1(object sender, EventArgs e)
        {
           
        }

        private void Function_Code_CharAdded(object sender, CharAddedEventArgs e)
        {
            if (e.Char == '{')
            {
                int line_length = Function_Code.LineFromPosition(Function_Code.AnchorPosition);
                string spaces = "";

                for(int i = 0; i < Function_Code.Lines[line_length].Length - 2; i++)
                {
                    spaces += " ";
                }

                Function_Code.InsertText(Function_Code.AnchorPosition, "\n\n" + spaces + "}");
                spaces += "   ";
                Function_Code.CurrentPosition += 1;
                Function_Code.AnchorPosition += 1;
                Function_Code.InsertText(Function_Code.AnchorPosition, spaces);
            }
        }

        private void Function_Code_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void Modifier_Function_Code_CharAdded(object sender, CharAddedEventArgs e)
        {
            if (e.Char == '{')
            {
                int line_length = Modifier_Function_Code.LineFromPosition(Modifier_Function_Code.AnchorPosition);
                string spaces = "";

                for (int i = 0; i < Modifier_Function_Code.Lines[line_length].Length - 2; i++)
                {
                    spaces += " ";
                }

                Modifier_Function_Code.InsertText(Modifier_Function_Code.AnchorPosition, "\n\n" + spaces + "}");
                spaces += "   ";
                Modifier_Function_Code.CurrentPosition += 1;
                Modifier_Function_Code.AnchorPosition += 1;
                Modifier_Function_Code.InsertText(Modifier_Function_Code.AnchorPosition, spaces);
            }
        }
    }
}
