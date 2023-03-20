using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dota2_Script_Maker.Item_Code;

namespace Dota2_Script_Maker
{
    class Item_Text_Builder
    {
        private const string tab = "\t\t";
        private const string lines = "\n\n";
        private const string dots = "//----------------------------------------------------------------";

        public static string Build_Text(Item item)
        {
            List<string> text = new List<string>();

            text.Add("\"" + item.item_name + "\"");
            text.Add("{");

            text.Add("//General:");
            text.Add(dots);
            text.Add(lines);

            text.Add(tab + "\"BaseClass\"" + tab + "\"item_datadriven\"");
            text.Add(tab + "\"ID\"" + tab + "\"" + item.item_id + "\"");
            text.Add(tab + "\"AbilityBehavior\"" + tab + "\"" + item.ability_behavior + "\"");
            text.Add(tab + "\"AbilityTextureName\"" + tab + "\"" + item.texture_name + "\"");
            text.Add(tab + "\"ItemCost\"" + tab + "\"" + item.gold_cost + "\"");
            text.Add(tab + "\"ItemStackable\"" + tab + "\"" + item.stackable + "\"");
            text.Add(tab + "\"ItemPermanent\"" + tab + "\"" + item.permanent + "\"");
            text.Add(tab + "\"ItemDroppable\"" + tab + "\"" + item.dropable + "\"");
            text.Add(tab + "\"ItemSellable\"" + tab + "\"" + item.sellable + "\"");
            text.Add(tab + "\"ItemKillable\"" + tab + "\"" + item.killable + "\"");
            text.Add(tab + "\"ItemDropable\"" + tab + "\"" + item.dropable + "\"");
            text.Add(tab + "\"ItemRequiresCharges\"" + tab + "\"" + item.requires_charges + "\"");
            text.Add(tab + "\"ItemShareability\"" + tab + "\"" + item.shareability + "\"");
            text.Add(tab + "\"ItemDeclarations\"" + tab + "\"" + item.declarations + "\"");

            text.Add("//Ability Information:");
            text.Add(dots);
            text.Add(lines);

            text.Add(tab + "\"AbilityCooldown\"" + tab + "\"" + item.cooldown + "\"");
            text.Add(tab + "\"AbilityManaCost\"" + tab + "\"" + item.mana_cost + "\"");
            text.Add(tab + "\"ItemBaseLevel\"" + tab + "\"" + item.base_level + "\"");
            text.Add(tab + "\"ItemInitialCharges\"" + tab + "\"" + item.initial_charges + "\"");



            text.Add("//Stock:");
            text.Add(dots);
            text.Add(lines);

            text.Add(tab + "\"ItemStockMax\"" + tab + "\"" + item.stock_max + "\"");
            text.Add(tab + "\"ItemStockTime\"" + tab + "\"" + item.stock_recharge_time + "\"");
            text.Add(tab + "\"ItemStockInitial\"" + tab + "\"" + item.stock_initial + "\"");

            text.Add("//Ability Functions:");
            text.Add(dots);
            text.Add(lines);

            foreach(Item_Function function in item.functions)
            {
                text.Add(tab + function.function_name);
                text.Add(tab + "{");

                for (int i = 0; i < function.function_code.Length; i++)
                {
                    text.Add(tab + tab + function.function_code[i]);
                }

                text.Add(tab + "}");
            }

            text.Add(tab + "\"Modifiers\"");
            text.Add(tab + "{");
            foreach (Modifier modifier in item.modifiers)
            {
                text.Add(tab + tab + "\"" + modifier.modifier_name + "\"");
                text.Add(tab + tab + "{");

                text.Add(tab + tab + tab + "\"Passive\"" + tab + "\"" + modifier.is_passive + "\"");
                text.Add(tab + tab + tab + "\"IsHidden\"" + tab + "\"" + modifier.is_hidden + "\"");
                text.Add(tab + tab + tab + "\"ThinkInterval\"" + tab + "\"" + modifier.think_interval + "\"");
                text.Add(tab + tab + tab + "\"EffectName\"" + tab + "\"" + modifier.effect_name + "\"");
                text.Add(tab + tab + tab + "\"IsBuff\"" + tab + "\"" + modifier.is_buff + "\"");
                text.Add(tab + tab + tab + "\"IsDebuff\"" + tab + "\"" + modifier.is_debuff + "\"");
                text.Add(tab + tab + tab + "\"IsPurgable\"" + tab + "\"" + modifier.is_purgable + "\"");
                text.Add(tab + tab + tab + "\"TextureName\"" + tab + "\"" + modifier.texture + "\"");

                text.Add("\n");

                text.Add(tab + tab + tab + "\"Properties\"");
                text.Add(tab + tab + tab + "{");
                for (int i = 0; i < modifier.attributes.Count; i++)
                {
                    text.Add(tab + tab + tab + tab + modifier.attributes[i]);
                }
                text.Add(tab + tab + tab + "}");

                text.Add("\n");

                text.Add(tab + tab + tab + "\"States\"");
                text.Add(tab + tab + tab + "{");
                for (int i = 0; i < modifier.states.Count; i++)
                {
                    text.Add(tab + tab + tab + tab + modifier.states[i]);
                }
                text.Add(tab + tab + tab + "}");

                foreach (Item_Function function in modifier.modifier_functions)
                {
                    text.Add(tab + tab + tab + function.function_name);
                    text.Add(tab + tab + tab + "{");

                    for (int i = 0; i < function.function_code.Length; i++)
                    {
                        text.Add(tab + tab + tab + tab + function.function_code[i]);
                    }

                    text.Add(tab + tab + tab + "}");
                }

                text.Add(tab + tab + "}");
            }
            text.Add(tab + "}");

            text.Add("}");

            string fin = "";
            for (int i = 0; i < text.Count; i++)
            {
                fin += text[i];
                fin += "\n";
            }

            return fin;
        }
    }
}
