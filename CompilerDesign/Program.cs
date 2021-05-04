using System;
using System.Collections.Generic;
using System.Linq;

namespace CompilerDesign
{

    class Program
    {
        List<string> operators = new List<string>() { "+", "=", "-" ,"*","/","%",";","(",")"};
        List<string> letters = new List<string>() { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        List<string> numbers = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        List<string> dataType = new List<string>() { "int" , "string"};

        List<Token> ruleTokens = new List<Token>();
        List<Token> inputTokens = new List<Token>();
        int currentTokenPos = 0;

        void Initiate()
        {

            foreach (string op in dataType)
            {
                ruleTokens.Add(new Token("keyword", op));
            }
            foreach (string op in operators)
            {
                ruleTokens.Add(new Token("operator", op));
            }
            foreach (string op in letters)
            {
                ruleTokens.Add(new Token("letter", op));
            }
            foreach (string op in numbers)
            {
                ruleTokens.Add(new Token("number", op));
            }


            PrepareInput();
        }

        void PrepareInput()
        {
            string input = " int a=b+6+string+(5);";
            List<int> usedIndexes = new List<int>();
            foreach (Token ruleToken in ruleTokens)
            {
                int index = 0;
                while (index <= input.Length)
                {
                    index = input.IndexOf(ruleToken.Lexeme, index);
                    if (index != -1)
                    {
                        bool isDuplicate = false;
                        foreach (int used in usedIndexes)
                        {
                            for (int i = 0; i < ruleToken.Lexeme.Length; i++)
                            {
                                if (i + index == used)
                                {
                                    isDuplicate = true;
                                    break;
                                }
                            }
                            if (isDuplicate) break;
                        }

                        if (isDuplicate)
                        {
                            index += ruleToken.Lexeme.Length;
                            continue;
                        }

                        for (int i = 0; i < ruleToken.Lexeme.Length; i++)
                        {
                            usedIndexes.Add(index + i);
                        }
                        this.inputTokens.Add(new Token(ruleToken, index));
                        index += ruleToken.Lexeme.Length;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            this.inputTokens = inputTokens.OrderBy(o => o.Pos).ToList();
            foreach (Token token in inputTokens)
            {
                Console.Write(token.Lexeme + " ");
            }
        }

        Token GetToken()
        {
            return inputTokens[currentTokenPos];
        }


        static void Main(string[] args){

            Program program = new Program();
            program.Initiate();

            Token token = program.GetToken();
            program.P();
               
        }

        public void P()
        {
            Token token = GetToken();
            if (token.Lexeme == ".")
            {
                //programı sonlandır

            }
            else
            {
                C();
            }
            
        }

        public void C()
        {
            Token token = GetToken();

            if (token.Lexeme == "[")
            {
                I();
            }
            else if (token.Lexeme == "{")
            {
                W();
            }
            else if (token.Tokenn == "letter")
            {
                A();
            }
            else if (token.Lexeme == "<")
            {
                Ç();
            }
            else if (token.Lexeme == ">")
            {
                G();
            }

        }

        public void I()
        {
            currentTokenPos++;
            E();

            Token token = GetToken();
            if(token.Lexeme != "?")
            {
                throw (new Exception("Gay"));
            }
            C();
        }

        public void E()
        {

        }

        public void W()
        {

        }

        public void A()
        {

        }

        public void Ç()
        {

        }

        public void G()
        {

        }




    }
}


/*
P → {C} '.'
C → I | W | A | Ç | G 
I → '[' E '?' C{ C } ':' C{ C } ']' | '[' E '?' C{C} ']'
W → '{' E '?' C{C} '}'
A → K '=' E ';'
Ç → '<' E ';'
G → '>' K ';'
E → T {('+' | '-') T}
T → U {('*' | '/' | '%') U}
U → F '^' U | F
F → '(' E ')' | K | R
K → 'a' | 'b' | … | 'z' 
R → '0' | '1' | … | '9' 
P: program
C: Cümle
I: IF cümlesi
W: While döngüsüA: Atama cümlesi
Ç: Çıktı cümlesi
G: Girdi cümlesi
E: Aritmetik İfade 
T: Çarpma-bölme-mod terimi
U: Üslü ifade
F: Gruplama ifadesi
K: Küçük harfler
R: Rakamlar
*/