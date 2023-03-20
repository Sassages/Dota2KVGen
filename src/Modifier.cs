using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dota2_Script_Maker.Item_Code;

namespace Dota2_Script_Maker
{
    class Modifier
    {
        public string modifier_name;
        public List<string> attributes;
        public List<string> states;
        public string duration;
        public string effect_name;
        public string is_buff;
        public string is_debuff;
        public string is_hidden;
        public string is_purgable;
        public string is_passive;
        public string texture;
        public string think_interval;

        public List<Item_Function> modifier_functions;

        public Modifier()
        {
            attributes = new List<string>();
            states = new List<string>();
            modifier_functions = new List<Item_Function>();
        }

        public override string ToString()
        {
            return modifier_name;
        }
    }
}
