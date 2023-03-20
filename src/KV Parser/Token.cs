using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2_Script_Maker.KV_Parser
{
    public class Token
    {
        public const int UNKNOWN       = -1; //?
        public const int BLOCK_OPEN    =  0; //{
        public const int BLOCK_CLOSE   =  1; //}
        public const int IDENTIFIER    =  2; //"Text23"
        public const int NEWLINE       =  3; //\n
        public const int COMMENT       =  4; ////asd

        public int line = -1;
        public int type = -1;
        public string value = "";

        public Token(int line, int type, string val)
        {
            this.line = line;
            this.type = type;
            value = val;
        }
    }
}
