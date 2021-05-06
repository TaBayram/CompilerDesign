
using System;
using System.Collections.Generic;

namespace CompilerDesign
{
    public class Variable
    {
        public object Value { get; set; }
    }

    public class Program
    {

        public static List<char> operators = new List<char>() { '+', '=', '-', '*', '/', '%', ';', '?', ':', '{', '}', '<', '>' };
        public static List<char> letters = new List<char>() { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        public static List<char> numbers = new List<char>() { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        public static Dictionary<char, Variable> variables = new Dictionary<char, Variable>(){
                                                                                                {'a', new Variable()},{'b', new Variable()},{'c', new Variable()},{'d', new Variable()},
                                                                                                {'e', new Variable()},{'f', new Variable()},{'g', new Variable()},{'h', new Variable()},
                                                                                                {'i', new Variable()},{'j', new Variable()},{'k', new Variable()},{'l', new Variable()},
                                                                                                {'m', new Variable()},{'n', new Variable()},{'o', new Variable()},{'p', new Variable()},
                                                                                                {'q', new Variable()},{'r', new Variable()},{'s', new Variable()},{'t', new Variable()},
                                                                                                {'u', new Variable()},{'v', new Variable()},{'w', new Variable()},{'x', new Variable()},
                                                                                                {'y', new Variable()},{'z', new Variable()}
                                                                                            };
        public static int tokenCounter = 0;
        public static char token;
        public static char tempTokenVar;
        public static string tokenList;
        public static string input;
        public static int inputLen;
        public static Variable tempVariable = new Variable();

        public static char GetToken()
        {
            char tokenpop;
            if (tokenCounter <= inputLen)
            {
                tokenpop = tokenList[tokenCounter];
                tokenCounter++;
            }
            else
            {
                tokenpop = '.';
            }

            return tokenpop;
        }


        public static void Main(string[] args)
        {
            do
            {
                //input ="  n = 0; { n - 2*5 ? < n; n=n+1}  ";
                input = Console.ReadLine();
                inputLen = input.Length - 1;
                tokenList = input.Replace(" ", "");
                tokenCounter = 0;
                token = GetToken();
                Program_();
            } while (input != "");
        }

        public static void Program_()
        {

            if (token != '.')
            {
                Cumle();
            }
            if (token == '.')
            {
                //programı sonlandır
                Console.WriteLine("succesfuly interpreted");
            }
        }

        // C → I | W | A | Ç | G 
        public static void Cumle()
        {
            if (token == '[')
            {
                token = GetToken();
                If();
            }
            else if (token == '{')
            {
                token = GetToken();
                Whle();
            }
            else if (letters.Contains(token))
            {
                Atama();
            }
            else if (token == '<')
            {
                token = GetToken();
                Cikti();
            }
            else if (token == '>')
            {
                token = GetToken();
                Girdi();
            }
        }

        // I   → '[' E '?' C{ C } ':' C { C } ']' | '[' E '?' C{C} ']'
        public static void If()
        {
            Evalue();
            token = GetToken();
            if (token != '?')
            {
                Console.WriteLine("if syntax error");
                Environment.Exit(0);
            }
            else
            {
                token = GetToken();
                while (token != ']' && token != ':')
                {
                    Cumle();
                    token = GetToken();
                }
                if (token == ']')
                {
                    System.Console.WriteLine("if");
                    //if-then işlemi
                }
                else if (token == ':')
                {

                    token = GetToken();

                    while (token != ']')
                    {
                        Cumle();
                        token = GetToken();
                    }

                    //if-then-else işlemi
                }
            }
        }

        //W → '{' E '?'  C{C} '}'
        public static void Whle()
        {
            Evalue();
            token = GetToken();
            if (token != '?')
            {
                System.Console.WriteLine("while syntax error");
                Environment.Exit(0);
            }
            else
            {
                token = GetToken();
                while (token != '}')
                {
                    Cumle();
                    token = GetToken();
                }
                System.Console.WriteLine("while");

            }
        }
        // A → K '=' E ';'
        public static void Atama()
        {
            KHarf();
            token = GetToken();
            if (token == '=')
            {
                token = GetToken();
                Evalue();
                token = GetToken();
                if (token == ';')
                {
                    //atama
                }
                else
                {
                    System.Console.WriteLine("missing ';' error near assignment operator");
                    Environment.Exit(0);
                }
            }
            else
            {
                System.Console.WriteLine("assignment error");
                Environment.Exit(0);
            }
        }
        // Ç → '<' E ';'
        public static void Cikti()
        {
            Evalue();
        }
        // G → '>' K ';'
        public static void Girdi()
        {
            KHarf();
        }
        // E → T {('+' | '-') T}
        public static void Evalue()
        {
            TerimCarpBol();
            token = GetToken();
            while (token == '+' || token == '-')
            {
                token = GetToken();
                TerimCarpBol();
            }
        }
        // T → U {('*' | '/' | '%') U}
        public static void TerimCarpBol()
        {
            Uslu();
            token = GetToken();
            while (token == '*' || token == '/' || token == '%')
            {
                token = GetToken();
                Uslu();
            }
        }
        // U → F '^' U | F
        public static void Uslu()
        {
            Fgroup();
            token = GetToken();
            if (token == '^')
            {
                token = GetToken();
                Uslu();
            }

        }
        // F → '(' E ')' | K | R
        public static void Fgroup()
        {
            if (token == '(')
            {
                token = GetToken();
                Evalue();
                token = GetToken();
                if (token == ')')
                {
                    // hesaplama işlemleri
                }
                else if (letters.Contains(token))
                {
                    KHarf();
                    // hesaplama işlemleri
                }
                else if (numbers.Contains(token))
                {
                    Rakam();
                    // hesaplama işlemleri
                }
            }
        }
        // K → 'a' | 'b' | … | 'z' 
        public static void KHarf()
        {
            tempTokenVar = token;
        }
        // R → '0' | '1' | … | '9' 
        public static void Rakam()
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