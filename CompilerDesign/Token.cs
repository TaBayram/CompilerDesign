using System;
using System.Collections.Generic;
using System.Text;

namespace CompilerDesign
{
    class Token
    {

        string tokenn;
        string lexeme;
        int pos;
       
        public Token(string token,string lexeme)
        {
            this.Tokenn = token.ToLower().Trim();
            this.Lexeme = lexeme.ToLower().Trim();
        }

        public Token(Token token, int pos)
        {
            this.pos = pos;
            this.tokenn = token.Tokenn;
            this.lexeme = token.Lexeme;
        }


        public string Tokenn { get => tokenn; set => tokenn = value; }
        public string Lexeme { get => lexeme; set => lexeme = value; }
        public int Pos { get => pos; set => pos = value; }
    }
}
