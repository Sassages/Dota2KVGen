using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2_Script_Maker
{
    class Scanner
    {
        private string input;
        private string[] split;
        private int counter;

        public Scanner(string input)
        {
            this.input = input;
            string[] s = null;
            split = input.Split(s, StringSplitOptions.RemoveEmptyEntries);
        }

        public string next()
        {
            counter++;

            if (counter > (split.Length - 1))
            {
                return null;
            }
            else
            {
                return split[counter];
            }
        }

        public string peek()
        {
            if ((counter +1 ) > (split.Length - 1))
            {
                return null;
            }
            else
            {
                return split[counter + 1];
            }
        }
    }
}
