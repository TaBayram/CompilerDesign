using System;
using System.Collections.Generic;

namespace CompilerDesign
{

    class Program
    {
        List<string> operators = new List<string>() { "+", "=", "-" ,"*","/","%",";"};
        List<string> letters = new List<string>() { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        List<string> numbers = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        List<string> dataType = new List<string>() { "int" , "string"};

        public void GetTokens(string input)
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
            
            

        }


        static void Main(string[] args)
        {
            Program program = new Program();
            program.GetTokens(" int a=b+5*2");
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