using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2_Script_Maker
{
    class TokenScanner
    {
        int CurrentIndex = 0;
        List<string> tokens;

        public TokenScanner(List<string> tokens)
        {
            this.tokens = tokens;
        }

        public string Peek()
        {
            if (IsDone())
                return "";
            return tokens[CurrentIndex];
        }

        public string PeekBack()
        {
            if (CurrentIndex == 0)
                return tokens[CurrentIndex];
            else
                return tokens[CurrentIndex - 1];
        }

        public string Next()
        {
            if (IsDone())
                return "";
            return tokens[CurrentIndex++];
        }

        //Returns whether the current token matches the given string. If true, moves onto the next token.
        public bool Match(string s)
        {
            if(Peek().Equals(s))
            {
                CurrentIndex++;
                return true;
            }
            return false;
        }

        public bool IsDone()
        {
            return (CurrentIndex >= tokens.Count);
        }
    }
}
