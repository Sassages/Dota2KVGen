using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2_Script_Maker.Item_Code
{
    class Item
    {
        public string item_id;
        public string item_name;
        public string texture_name;
        public string cooldown;
        public string mana_cost;
        public string gold_cost;
        public string sellable;
        public string killable;
        public string dropable;
        public string stock_max;
        public string stock_initial;
        public string stock_recharge_time;

        public string shareability;

        public string initial_charges;
        public string display_charges;
        public string requires_charges;
        public string stackable;
        public string permanent;

        public string ability_behavior;
        public string animation;

        public string cast_on_pickup;

        public string max_upgrade_level;
        public string base_level;

        public string declarations;

        public List<Item_Function> functions;

        public List<Modifier> modifiers;

        public Item()
        {
            functions = new List<Item_Function>();
            modifiers = new List<Modifier>();
        }
    }
}
