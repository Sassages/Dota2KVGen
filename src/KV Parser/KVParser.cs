using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2_Script_Maker.KV_Parser
{
    public class KVParser
    {
        public ParsingError Error = null;

        private KVTokenScanner scanner;

        public KVParser(List<Token> tokens)
        {
            scanner = new KVTokenScanner(tokens);
        }

        public KVBlock Parse()
        {
            Token BlockStart = SearchForImportantToken();
            switch(BlockStart.type)
            {
                case Token.UNKNOWN:
                    Error = new ParsingError(BlockStart, "Unkown error.");
                    break;

                case Token.BLOCK_OPEN:
                    Error = new ParsingError(BlockStart, "Unexpected '{'");
                    break;

                case Token.BLOCK_CLOSE:
                    Error = new ParsingError(BlockStart, "Unexpected '}'");
                    break;

                case Token.IDENTIFIER:
                    scanner.Next();
                    return MatchBlock(BlockStart);
            }

            return null;
        }

        private KVBlock MatchBlock(Token BlockIdentifier)
        {
            KVBlock ThisBlock = new KVBlock(BlockIdentifier.value);

            Token NextToken = SearchForImportantToken();
            if(NextToken.type != Token.BLOCK_OPEN)
            {
                Error = new ParsingError(NextToken, "Expected '{', instead got '" + NextToken.value + "'");
                return null;
            }

            scanner.Next();
            while (!scanner.IsDone() && Error == null)
            {
                Token CurrentToken = SearchForImportantToken();

                switch (CurrentToken.type)
                {
                    case Token.COMMENT:
                    case Token.NEWLINE: //error
                        return null;

                    case Token.UNKNOWN:
                        Error = new ParsingError(CurrentToken, "Unknown error.");
                        break;

                    case Token.BLOCK_OPEN:
                        Error = new ParsingError(CurrentToken, "Unexpected '{'");
                        break;

                    case Token.BLOCK_CLOSE:
                        return ThisBlock;

                    case Token.IDENTIFIER:
                        scanner.Next();
                        NextToken = SearchForImportantToken();

                        if (scanner.IsDone()) //Error
                            break;

                        switch(NextToken.type)
                        {
                            case Token.BLOCK_OPEN:
                                KVBlock NewBlock = MatchBlock(CurrentToken);
                                if (NewBlock != null)
                                    ThisBlock.AppendItem(NewBlock);
                                else
                                    return null;
                                break;

                            case Token.IDENTIFIER:
                                ThisBlock.AppendItem(new KVStatement(CurrentToken.value, NextToken.value));
                                break;

                            case Token.BLOCK_CLOSE:
                                Error = new ParsingError(NextToken, "Unexpected '}'");
                                break;

                            case Token.UNKNOWN:
                                Error = new ParsingError(NextToken, "Unknown error");
                                break;

                            default: break;
                        }
                        break;
                }
                scanner.Next();
            }

            return ThisBlock;
        }

        public Token GetNextNonComment()
        {
            while(scanner.Peek().type == Token.COMMENT)
            {
                Token CurrentToken = scanner.Peek();
                scanner.Next();

                if(scanner.IsDone())
                {
                    Error = new ParsingError(CurrentToken, "Unexpected end of file.");
                    return CurrentToken;
                }
            }

            return scanner.Peek();
        }
        
        //Finds the next important token. Can find the token it is currently on.
        //Important = Brackets/Identifiers/Unknown.
        public Token SearchForImportantToken()
        {
            Token CurrentToken = scanner.Peek();
            while (CurrentToken.type == Token.NEWLINE || CurrentToken.type == Token.COMMENT)
            {
                CurrentToken = scanner.Peek();
                scanner.Next();

                if (scanner.IsDone())
                {
                    Error = new ParsingError(CurrentToken, "Unexpected end of file.");
                    return CurrentToken;
                }

                CurrentToken = scanner.Peek();
            }

            return scanner.Peek();
        }
    }
}
