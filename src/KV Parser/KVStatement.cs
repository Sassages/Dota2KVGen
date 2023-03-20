using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2_Script_Maker.KV_Parser
{
    public class KVStatement : KVItem
    {
        public string left;
        public string right;
        public bool hide = false; //If the value is empty, should we just not print it out?

        public KVStatement(string left, string right)
        {
            this.left = left;
            this.right = right;
        }

        //Copy constructor.
        public KVStatement(KVStatement s)
        {
            left = s.left;
            right = s.right;
        }

        // "left"       "right"
        public override string ToString()
        {
            return "\"" + left + "\"\t\t\"" + right + "\"";
        }

        public override string GetIndentedString(int tabs)
        {
            return IndentString(ToString(), tabs);
        }
    }
}
