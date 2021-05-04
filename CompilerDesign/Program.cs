using System;
using System.Collections.Generic;

namespace CompilerDesign
{

    class Program
    {
        static List<string> operators = new List<string>() { "+", "=", "-", "*", "/", "%", ";", "?", ":" , "{" , "}" , "<" , ">" };
        static List<string> letters = new List<string>() { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        static List<string> numbers = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        static List<string> dataType = new List<string>() { "int" , "string"};

        static List<string> tokenList;


        public bool IsTerminal(string value)
        {
            if (IsOperator(value)) { return true; }
            else if (IsLetter(value)) { return true; }
            else if (IsNumber(value)) { return true; }
            else if (IsDataType(value)) { return true; }
            else { return false; }
        }

        public bool IsOperator(string value)
        {
            if (operators.Contains(value)) { return true; }
            else { return false; }
        }

        public static bool IsLetter(string value)
        {
            if (letters.Contains(value)) { return true; }
            else { return false; }
        }

        public bool IsNumber(string value)
        {
            if (numbers.Contains(value)) { return true; }
            else { return false; }
        }

        public bool IsDataType(string value)
        {
            if (dataType.Contains(value)) { return true; }
            else { return false; }
        }
        public List<string> GetTokens(string input)
        {
            input = input.Trim();
            var opTokens = new List<string>();
            var letTokens = new List<string>();
            var numTokens = new List<string>();
            var dtTokens = new List<string>();
            var tokens = new List<string>();
  
            for(int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (operators.Contains(c.ToString())){
                    opTokens.Add(c.ToString());
                    tokens.Add(c.ToString());
                }
                else if (letters.Contains(c.ToString()))
                {
                    bool isType = false;
                    foreach (string type in dataType)
                    {
                        if (input.Length >= i+type.Length && input.Substring(i, type.Length) == type)
                        {
                            dtTokens.Add(type);
                            tokens.Add(type);
                            i += type.Length;
                            isType = true;
                            break;
                        }
                    }
                    if (isType) continue;
                    letTokens.Add(c.ToString());
                    tokens.Add(c.ToString());
                }
                else if(numbers.Contains(c.ToString()))
                {
                    numTokens.Add(c.ToString());
                    tokens.Add(c.ToString());
                }

            }
            

            foreach (string tok in tokens)
            {
                Console.WriteLine(tok);
            }

            return tokens;
            

        }


        static void Main(string[] args)
        {
            Program program = new Program();
            tokenList = program.GetTokens("n = 0; { n - 2*5 ? < n; n=n+1}");
            P();
        }

        public static void P()
        {
            foreach(String token in tokenList)
            {
                if(token == ".") 
                {
                    //programı sonlandır

                }
                else
                {
                    C(token);
                }
            }
        }

        public static void C(string token)
        {
            if(token == "[")
            {
                I();
            }
            else if (token == "{")
            {
                W();
            }
            else if (IsLetter(token))
            {
                A();
            }
            else if (token == "<")
            {
                Ç();
            }
            else if (token == ">")
            {
                G();
            }

        }

        public static void I()
        {

        }

        public static void W()
        {

        }

        public static void A()
        {

        }

        public static void Ç()
        {

        }

        public static void G()
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