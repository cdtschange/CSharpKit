using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;

namespace Cdts.Core
{
    public class SelectorParser
    {
        struct Token
        {
            public TokenId id;
            public string text;
            public int pos;
        }

        enum TokenId
        {
            Unknown,
            End,
            Identifier,
            OpenParen,
            CloseParen,
            Comma
        }
        string text;
        int textPos;
        int textLen;
        char ch;
        Token token;

        public SelectorParser(string selector)
        {
            text = selector;
            textLen = text.Length;
            SetTextPos(0);
            NextToken();
        }

        void SetTextPos(int pos)
        {
            textPos = pos;
            ch = textPos < textLen ? text[textPos] : '\0';
        }

        void NextChar()
        {
            if (textPos < textLen) textPos++;
            ch = textPos < textLen ? text[textPos] : '\0';
        }
        void NextToken()
        {
            while (Char.IsWhiteSpace(ch)) NextChar();
            TokenId t;
            int tokenPos = textPos;
            switch (ch)
            {
                case '(':
                    NextChar();
                    t = TokenId.OpenParen;
                    break;
                case ')':
                    NextChar();
                    t = TokenId.CloseParen;
                    break;
                case ',':
                    NextChar();
                    t = TokenId.Comma;
                    break;                
                default:
                    if (Char.IsLetter(ch) || ch == '_')
                    {
                        do
                        {
                            NextChar();
                        } 
                        while (Char.IsLetterOrDigit(ch) || ch == '_');
                        t = TokenId.Identifier;
                        break;
                    }
                    if (textPos == textLen)
                    {
                        t = TokenId.End;
                        break;
                    }
                    throw ParseError(textPos, Res.InvalidCharacter, ch);
            }
            token.id = t;
            token.text = text.Substring(tokenPos, textPos - tokenPos);
            token.pos = tokenPos;
        }
        Exception ParseError(string format, params object[] args)
        {
            return ParseError(token.pos, format, args);
        }
        Exception ParseError(int pos, string format, params object[] args)
        {
            return new ParseException(string.Format(System.Globalization.CultureInfo.CurrentCulture, format, args), pos);
        }
        void ValidateToken(TokenId t, string errorMessage)
        {
            if (token.id != t) throw ParseError(errorMessage);
        }
        void ValidateToken(TokenId t)
        {
            if (token.id != t) throw ParseError(Res.SyntaxError);
        }

        public string Parse()
        {
            int exprPos = token.pos;
            string expr = ParseExpression();

            ValidateToken(TokenId.End, Res.SyntaxError);
            expr = expr.Replace("~~.", "");
            if (expr.StartsWith("("))
            {
                expr = "new" + expr;
            }
            else if (!expr.StartsWith("new("))
            {
                expr = "new(" + expr + ")";
            }
            return expr;
        }


        string ParseExpression()
        {
            return ParsePrimaryStart();
        }

        string ParsePrimaryStart()
        {
            switch (token.id)
            {
                case TokenId.Identifier:
                    return ParseIdentifier();
                //case TokenId.StringLiteral:
                //    return ParseStringLiteral();
                //case TokenId.IntegerLiteral:
                //    return ParseIntegerLiteral();
                //case TokenId.RealLiteral:
                //    return ParseRealLiteral();
                case TokenId.OpenParen:
                    return ParseParenExpression();
                default:
                    throw ParseError(Res.ExpressionExpected);
            }
        }
        string ParseParenExpression()
        {
            ValidateToken(TokenId.OpenParen, Res.OpenParenExpected);
            NextToken();
            string e = "new(" + ParseExpression();
            ValidateToken(TokenId.CloseParen, Res.CloseParenOrOperatorExpected);
            e += ")";
            NextToken();
            return e;
        }

        string ParseIdentifier()
        {
            ValidateToken(TokenId.Identifier);
            string exp = "~~." + token.text;
            Token id = token;
            NextToken();
            if (token.id == TokenId.OpenParen)
            {
                exp = string.Format("~~.{0}==null?null:", id.text);
                token.pos = id.pos;
                //NextToken();
                exp += ParseExpression().Replace("~~.", "~~." + id.text + ".") + " as " + id.text;
                //NextToken();
                //if (token.id != TokenId.CloseParen)
                //{
                //    throw ParseError(Res.CloseParenOrCommaExpected);
                //}
                //return exp;
            }
            if (token.id == TokenId.Comma)
            {
                exp += ",";
                NextToken();
                exp += ParseExpression();
                return exp;
            }
            return exp;
            //throw ParseError(Res.UnknownIdentifier, token.text);
        }

    }
}
