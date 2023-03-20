using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2_Script_Maker.KV_Parser
{
    public class ParsingError
    {
        public Token t;
        string message;

        public ParsingError(Token t, string message)
        {
            this.t = t;
            this.message = message;
        }

        public string GetErrorMessage()
        {
            return "Error at line " + (t.line + 1) + ": " + message;
        }
    }
}
