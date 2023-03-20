using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dota2_Script_Maker.KV_Parser
{
    public class KVLexer
    {
        public List<ParsingError> errors = new List<ParsingError>();

        public List<Token> LexicalAnalysis(string FileContents)
        {
            List<Token> output = new List<Token>();

            Regex KVMatcher = new Regex(@"([A-Z]|[a-z]|[0-9]|_|\.|/)+");

            int CharIndex = 0;
            int line = 0;
            while(CharIndex < FileContents.Length && errors.Count <= 0)
            {
                char CurrentChar = FileContents[CharIndex];

                switch(CurrentChar)
                {
                    case '"':
                        //Find the matching quotation mark:
                        int EndIndex = CharIndex + 1;
                        char EndChar = FileContents[EndIndex];
                        while(EndChar != '"')
                        {
                            if(EndChar == '\n')
                            {
                                Token error = new Token(line, Token.IDENTIFIER, "");
                                errors.Add(new ParsingError(error, "Could not find closing quotation mark."));
                                return null;
                            }
                            EndIndex++;
                            EndChar = FileContents[EndIndex];
                        }
                        string KV = FileContents.Substring(CharIndex + 1, EndIndex - CharIndex - 1);

                        Match m = KVMatcher.Match(KV);
                        if(m.Length == KV.Length)
                        {
                            Token t = new Token(line, Token.IDENTIFIER, KV);
                            output.Add(t);
                        }
                        else
                        {
                            Token error = new Token(line, Token.IDENTIFIER, KV);
                            errors.Add(new ParsingError(error, "Unexpected/Invalid characters in the string \"" + error.value + "\""));
                            break;
                        }

                        CharIndex = EndIndex;
                        break;

                    case '{':
                        Token NewBlock = new Token(line, Token.BLOCK_OPEN, "{");
                        output.Add(NewBlock);
                        break;

                    case '/':
                        CharIndex++;
                        CurrentChar = FileContents[CharIndex];
                        if(CurrentChar != '/')
                        {
                            Token error = new Token(line, Token.IDENTIFIER, "");
                            errors.Add(new ParsingError(error, "Unexpected characters. Is this supposed to be a comment?"));
                            break;
                        }

                        EndIndex = CharIndex;
                        EndChar = FileContents[EndIndex];
                        while (EndChar != '\n' && EndChar != '\r')
                        {
                            EndIndex++;
                            EndChar = FileContents[EndIndex];
                        }
                        EndIndex--; //Backspace one to get only the comment string.

                        string comment = FileContents.Substring(CharIndex + 1, EndIndex - CharIndex);
                        Token CommentToken = new Token(line, Token.COMMENT, comment);
                        output.Add(CommentToken);

                        CharIndex = EndIndex;
                        break;


                    case '}':
                        Token CloseBlock = new Token(line, Token.BLOCK_CLOSE, "}");
                        output.Add(CloseBlock);
                        break;

                    case '\n':
                        Token NewLine = new Token(line, Token.NEWLINE, "\n");
                        output.Add(NewLine);
                        line++;
                        break;

                    case '\t':
                    case ' ':
                    case '\r':
                        break;

                    default:
                        Token Error = new Token(line, Token.UNKNOWN, "" + FileContents[CharIndex]);
                        ParsingError e = new ParsingError(Error, "Unexpected character.");
                        errors.Add(e);
                        break;
                }

                CharIndex++;
            }

            return output;
        }
    }
}
