using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2_Script_Maker.KV_Parser
{
    public abstract class KVItem
    {
        public KVBlock parent;

        public string IndentString(string s, int tabs)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < tabs; i++)
                sb.Append("\t");
            if (s != null)
            sb.Append(s);
            return sb.ToString();
        }

        public abstract string GetIndentedString(int tabs);
    }
}
