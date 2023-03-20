using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2_Script_Maker.KV_Parser
{
    public class KVTokenScanner
    {
        int CurrentIndex = 0;
        List<Token> tokens;

        public KVTokenScanner(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public Token Peek()
        {
            if (!IsDone())
                return tokens[CurrentIndex];
            return null;
        }

        public Token Next()
        {
            if(!IsDone())
                return tokens[CurrentIndex++];
            return null;
        }

        public bool IsDone()
        {
            return (CurrentIndex >= tokens.Count);
        }
    }
}
