
using System;
using System.Collections.Generic;
namespace CompilerDesign
{
    /*public class Variable
    {
        public object Value { get; set; }
        public Variable(Object val){
            Value=val;
        }
        public Variable(){
            
        }
    }*/

    class Program
    {

        static List<char> operators = new List<char>() { '+', '=', '-', '*', '/', '%', ';', '?', ':', '{', '}', '<', '>' };
        static List<char> letters = new List<char>() { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        static List<char> numbers = new List<char>() { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static List<float> powerTempVal = new List<float>();
        static Stack<float> parantheStack = new Stack<float>();
        static List<float> ifCondList = new List<float>();
        static List<float> WhileCondList = new List<float>();
        static List<int> WhileControlList = new List<int>();
        static Dictionary<char, float> variables = new Dictionary<char, float>(){
                                                                                     {'a', new float()},{'b', new float()},{'c', new float()},{'d', new float()},
                                                                                     {'e', new float()},{'f', new float()},{'g', new float()},{'h', new float()},
                                                                                     {'i', new float()},{'j', new float()},{'k', new float()},{'l', new float()},
                                                                                     {'m', new float()},{'n', new float()},{'o', new float()},{'p', new float()},
                                                                                     {'q', new float()},{'r', new float()},{'s', new float()},{'t', new float()},
                                                                                     {'u', new float()},{'v', new float()},{'w', new float()},{'x', new float()},
                                                                                     {'y', new float()},{'z', new float()}
                                                                                 };
        static int tokenCounter = 0;
        static char token;
        static char tempTokenVar;
        static string tokenList;
        static string input;
        static int inputLen;
        static float tempEvalue = 0;
        static float tempCarpmaBolme = 0;
        static float tempUslu = 0;
        static int selectorMulDivMod = 0;
        static int selectorSubAdd = 0;
        static int tempWhileControl = -1;
        static int tempIfControl = -1;
        static int whileParantez = 0;
        static char GetToken()
        {
            char tokenpop;
            if (tokenCounter <= inputLen)
            {
                tokenpop = tokenList[tokenCounter];
                if (tempWhileControl>-1)
                {
                    for (int i = 0; i < tempWhileControl+1; i++)
                    {
                        WhileControlList[i]++;
                    }
                }
                tokenCounter++;
            }
            else
            {
                foreach (var let in letters)
                {
                    variables[let] = 0;
                }
                tokenpop = '.';
            }

            return tokenpop;
        }


        static void Main(string[] args)
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
                /*foreach (var item in variables)
                {
                    System.Console.WriteLine(item.Value);
                }*/
            } while (input != "");
        }

        static void Program_()
        {
            if (token != '.')
            {
                Cumle();
            }
            if (token == '.')
            {
                //programı sonlandır
                Console.WriteLine("\nsuccesfuly interpreted");
            }

        }

        // C → I | W | A | Ç | G 
        static void Cumle()
        {
            if (token == '[')
            {
                tempIfControl++;
                token = GetToken();
                If();
            }
            else if (token == '{')
            {
                tempWhileControl++;
                if (tempWhileControl==WhileControlList.Count)
                {
                    WhileControlList.Add(0);
                }
                WhileControlList[tempWhileControl] = 1;
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
        static void If()
        {
            tempEvalue = 0;
            Evalue();
            ifCondList.Add(tempEvalue);
            if (token != '?')
            {
                Console.WriteLine("\nif syntax error");
                Environment.Exit(0);
            }
            else
            {
                token = GetToken();

                while (token != ']' && token != ':')
                {
                    if (ifCondList[tempIfControl] != 0)
                    {
                        Cumle();
                    }
                    else
                    {
                        token = GetToken();
                    }
                }
                if (token == ']')
                {
                    tempIfControl--;
                    token = GetToken();
                    Cumle();
                }
                else if (token == ':')
                {

                    token = GetToken();
                    while (token != ']')
                    {
                        if (ifCondList[tempIfControl] == 0)
                        {
                            Cumle();
                        }
                        else
                        {
                            token = GetToken();
                        }
                    }
                    tempIfControl--;
                    token = GetToken();
                    Cumle();
                }

            }
        }

        //W → '{' E '?'  C{C} '}'
        static void Whle()
        {
            int interWhile = 0;
            tempEvalue = 0;
            Evalue();
            if (tempWhileControl == WhileCondList.Count)
            {
                WhileCondList.Add(0);
            }
            WhileCondList[tempWhileControl] = tempEvalue;
            if (token != '?')
            {
                System.Console.WriteLine("\nwhile syntax error");
                Environment.Exit(0);
            }
            else
            {
                //whileLoop=input.Substring(tokenCounter,input.IndexOf('}'));
                token = GetToken();
                while (token != '}')
                {
                    if (WhileCondList[tempWhileControl] != 0)
                    {
                        Cumle();
                        tokenCounter -= WhileControlList[tempWhileControl];
                        for (int i = 0; i < tempWhileControl; i++)
                        {
                            WhileControlList[i] -= WhileControlList[tempWhileControl];
                        }
                    }
                    else
                    {
                        if (token=='{')
                        {
                            interWhile++;
                        }
                        token = GetToken();

                    }
                }
                if (WhileCondList[tempWhileControl] == 0)
                {
                    while (interWhile>0)
                    {
                        token = GetToken();
                        if (token=='}')
                        {
                            interWhile--;
                        }
                    }
                }
                tempWhileControl--;
                token = GetToken();
                Cumle();
            }
        }
        // A → K '=' E ';'
        static void Atama()
        {

            KHarf();

            if (token == '=')
            {
                token = GetToken();
                Evalue();
                if (token == ';')
                {
                    //atama işlemi
                    variables[tempTokenVar] = tempEvalue;
                    token = GetToken();
                    Cumle();
                }
                else
                {
                    System.Console.WriteLine("\nmissing ';' error near assignment operator");
                    Environment.Exit(0);
                }
            }
            else
            {
                System.Console.WriteLine("\nassignment error");
                Environment.Exit(0);
            }
        }
        // Ç → '<' E ';'
        static void Cikti()
        {
            tempEvalue = 0;
            Evalue();
            if (token == ';')
            {
                System.Console.WriteLine(tempEvalue);
                token = GetToken();
                Cumle();
            }
            else
            {
                System.Console.WriteLine("\nmissing ; on output error");
                Environment.Exit(0);
            }

        }
        // G → '>' K ';'
        static void Girdi()
        {
            KHarf();
            if (token == ';')
            {
                System.Console.WriteLine("\nEnter " + tempTokenVar.ToString() + " value");
                variables[tempTokenVar] = Convert.ToSingle(Console.ReadLine());
                token = GetToken();
                Cumle();

            }
            else
            {
                System.Console.WriteLine("\nmissing ; on input error");
                Environment.Exit(0);
            }

        }
        // E → T {('+' | '-') T}
        static void Evalue()
        {
            TerimCarpBol();
            tempEvalue = tempCarpmaBolme;
            while (token == '+' || token == '-')
            {
                if (token == '+')
                {
                    selectorSubAdd = 0;
                }
                else if (token == '-')
                {
                    selectorSubAdd = 1;
                }
                token = GetToken();
                TerimCarpBol();

                if (selectorSubAdd == 0)
                {
                    tempEvalue += tempCarpmaBolme;
                }
                else if (selectorSubAdd == 1)
                {
                    tempEvalue -= tempCarpmaBolme;
                }
            }

        }
        // T → U {('*' | '/' | '%') U}
        static void TerimCarpBol()
        {
            powerTempVal.Clear();
            Uslu();
            tempUslu = powerTempVal[0];
            for (int i = 1; i < powerTempVal.Count; i++)
            {
                tempUslu = Convert.ToSingle(Math.Pow(tempUslu, powerTempVal[i]));
            }
            tempCarpmaBolme = tempUslu;
            while (token == '*' || token == '/' || token == '%')
            {
                if (token == '*')
                {
                    selectorMulDivMod = 0;
                }
                else if (token == '/')
                {
                    selectorMulDivMod = 1;
                }
                else if (token == '%')
                {
                    selectorMulDivMod = 2;
                }
                powerTempVal.Clear();
                token = GetToken();
                Uslu();
                tempUslu = powerTempVal[0];
                for (int i = 1; i < powerTempVal.Count; i++)
                {
                    tempUslu = Convert.ToSingle(Math.Pow(tempUslu, powerTempVal[i]));
                }
                if (selectorMulDivMod == 0)
                {
                    tempCarpmaBolme *= tempUslu;
                }
                else if (selectorMulDivMod == 1)
                {
                    tempCarpmaBolme /= tempUslu;
                }
                else if (selectorMulDivMod == 2)
                {
                    tempCarpmaBolme %= tempUslu;
                }

            }
        }
        // U → F '^' U | F
        static void Uslu()
        {
            Fgroup();//  a^2^4^5
            while (token == '^')
            {
                token = GetToken();
                Uslu();
            }

        }
        // F → '(' E ')' | K | R
        static void Fgroup()
        {
            if (token == '(')
            {
                token = GetToken();
                tempEvalue = 0;
                Evalue();
                if (token == ')')
                {
                    token = GetToken();
                }
            }
            else if (letters.Contains(token))
            {
                KHarf();
            }
            else if (numbers.Contains(token))
            {
                Rakam();
            }
        }
        // K → 'a' | 'b' | … | 'z' 
        static void KHarf()
        {
            powerTempVal.Add(variables[token]);
            tempTokenVar = token;
            token = GetToken();
        }
        // R → '0' | '1' | … | '9' 
        static void Rakam()
        {
            powerTempVal.Add(Convert.ToSingle(token.ToString()));
            token = GetToken();
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