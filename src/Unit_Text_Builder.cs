using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2_Script_Maker
{
    class Unit_Text_Builder
    {
        private const string tab = "\t\t";
        private const string lines = "\n\n";
        private const string dots = "//----------------------------------------------------------------";
        public Unit_Text_Builder()
        {

        }

        public static string Build_Text(Unit unit)
        {
            return unit.kv_block.ToString();
        }
    }
}
