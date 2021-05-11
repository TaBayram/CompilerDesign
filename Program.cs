
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
        static List<float> tempEvalueList = new List<float>();
        static List<List<float>> powerTempList = new List<List<float>>();
        static List<int> selectorSubAdd = new List<int>();
        static List<int> selectorMulDivMod = new List<int>();
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
        static Dictionary<char, bool> defineControl = new Dictionary<char, bool>(){
                                                                                     {'a', false},{'b', false},{'c', false},{'d', false},
                                                                                     {'e', false},{'f', false},{'g', false},{'h', false},
                                                                                     {'i', false},{'j', false},{'k', false},{'l', false},
                                                                                     {'m', false},{'n', false},{'o', false},{'p', false},
                                                                                     {'q', false},{'r', false},{'s', false},{'t', false},
                                                                                     {'u', false},{'v', false},{'w', false},{'x', false},
                                                                                     {'y', false},{'z', false}
                                                                                 };
        static int tokenCounter = 0;
        static char token;
        static char tempTokenVar;
        static string tokenList;
        static string input;
        static int inputLen;
        static float tempCarpmaBolme = 0;
        static float tempUslu = 0;
        static int tempWhileControl = -1;
        static int tempIfControl = -1;
        static int counterParanthess = 0;
        static bool assignFlag = false;
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
                Console.WriteLine("\n-->interpreted with no error<--");
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
            tempEvalueList.Clear();
            tempEvalueList.Add(0);

            selectorSubAdd.Clear();
            selectorSubAdd.Add(-1);

            selectorMulDivMod.Clear();
            selectorMulDivMod.Add(-1);

            powerTempList.Add(new List<float>());

            Evalue();

            powerTempList.Clear();
            if (counterParanthess > 0)
            {
                System.Console.WriteLine("\n!!!missing ')' error in if condition!!!");
                Environment.Exit(0);
            }
            else if (counterParanthess < 0)
            {
                System.Console.WriteLine("\n!!!missing '(' error in if condition!!!");
                Environment.Exit(0);
            }
            if (tempIfControl == ifCondList.Count)
            {
                ifCondList.Add(0);
            }
            ifCondList[tempIfControl] = tempEvalueList[0];
            if (token != '?')
            {
                Console.WriteLine("\n!!!if syntax error!!!");
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
            tempEvalueList.Clear();
            tempEvalueList.Add(0);

            selectorSubAdd.Clear();
            selectorSubAdd.Add(-1);

            selectorMulDivMod.Clear();
            selectorMulDivMod.Add(-1);

            powerTempList.Add(new List<float>());

            Evalue();

            powerTempList.Clear();
            if (counterParanthess > 0)
            {
                System.Console.WriteLine("\n!!!missing ')' error in while condition!!!");
                Environment.Exit(0);
            }
            else if (counterParanthess < 0)
            {
                System.Console.WriteLine("\n!!!missing '(' error in while condition!!!");
                Environment.Exit(0);
            }
            if (tempWhileControl == WhileCondList.Count)
            {
                WhileCondList.Add(0);
            }
            WhileCondList[tempWhileControl] = tempEvalueList[0];
            if (token != '?')
            {
                System.Console.WriteLine("\n!!!while syntax error!!!");
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
            assignFlag = true;
            KHarf();
            assignFlag = false;
            if (token == '=')
            {
                token = GetToken();

                tempEvalueList.Clear();
                tempEvalueList.Add(0);

                selectorSubAdd.Clear();
                selectorSubAdd.Add(-1);

                selectorMulDivMod.Clear();
                selectorMulDivMod.Add(-1);

                powerTempList.Add(new List<float>());
                
                Evalue();

                powerTempList.Clear();
                if (token == ';')
                {
                    //atama işlemi
                    if (counterParanthess > 0)
                    {
                        System.Console.WriteLine("\n!!!missing ')' error in assignment operator!!!");
                        Environment.Exit(0);
                    }
                    else if (counterParanthess < 0)
                    {
                        System.Console.WriteLine("\n!!!missing '(' error in assignment operator!!!");
                        Environment.Exit(0);
                    }
                    else 
                    {
                        
                        variables[tempTokenVar] = tempEvalueList[0];
                        defineControl[tempTokenVar] = true;
                        powerTempList.Clear();
                        token = GetToken();
                        Cumle();
                    
                    }
                }
                else
                {
                    System.Console.WriteLine("\n!!!missing ';' error near assignment operator!!!");
                    Environment.Exit(0);
                }
            }
            else
            {
                System.Console.WriteLine("\n!!!assignment error!!!");
                Environment.Exit(0);
            }
            
        }
        // Ç → '<' E ';'
        static void Cikti()
        {
            tempEvalueList.Clear();
            tempEvalueList.Add(0);

            selectorSubAdd.Clear();
            selectorSubAdd.Add(-1);

            selectorMulDivMod.Clear();
            selectorMulDivMod.Add(-1);

            powerTempList.Add(new List<float>());

            Evalue();

            powerTempList.Clear();
            if (token == ';')
            {
                if (counterParanthess < 0)
                { 
                    System.Console.WriteLine("\n!!!missing ')' error in output!!! "); 
                    Environment.Exit(0); 
                }
                else if (counterParanthess > 0)
                { 
                    System.Console.WriteLine("\n!!!missing '(' error in output!!! "); 
                    Environment.Exit(0); 
                }
                else
                {
                    System.Console.WriteLine(tempEvalueList[0]);
                    token = GetToken();
                    Cumle();
                }
            }
            else
            {
                System.Console.WriteLine("\n!!!missing ';' error in output!!!");
                Environment.Exit(0);
            }

        }
        // G → '>' K ';'
        static void Girdi()
        {
            assignFlag = true;
            KHarf();
            assignFlag = false;
            if (token == ';')
            {
                System.Console.WriteLine("\nEnter " + tempTokenVar.ToString() + " value");
                variables[tempTokenVar] = Convert.ToSingle(Console.ReadLine());
                defineControl[tempTokenVar] = true;
                token = GetToken();
                Cumle();

            }
            else
            {
                System.Console.WriteLine("\n!!!missing ';' on input error!!!");
                Environment.Exit(0);
            }

        }
        // E → T {('+' | '-') T}
        static void Evalue()
        {
            TerimCarpBol();
            tempEvalueList[counterParanthess] = tempCarpmaBolme;
            while (token == '+' || token == '-')
            {    
                if (token == '+')
                    selectorSubAdd[counterParanthess] = 0;
                else if (token == '-')
                    selectorSubAdd[counterParanthess] = 1;
                token = GetToken();
                TerimCarpBol();

                if (selectorSubAdd[counterParanthess] == 0)
                    tempEvalueList[counterParanthess] += tempCarpmaBolme;
                else if (selectorSubAdd[counterParanthess] == 1)
                    tempEvalueList[counterParanthess] -= tempCarpmaBolme;
            }
        }
        // T → U {('*' | '/' | '%') U}
        static void TerimCarpBol()
        {
            powerTempList[counterParanthess].Clear();
            Uslu();
            tempUslu = powerTempList[counterParanthess][0];
            for (int i = 1; i < powerTempList[counterParanthess].Count; i++)
                tempUslu = Convert.ToSingle(Math.Pow(tempUslu, powerTempList[counterParanthess][i]));
            tempCarpmaBolme = tempUslu;
            while (token == '*' || token == '/' || token == '%')
            {
                if (token == '*')
                    selectorMulDivMod[counterParanthess] = 0;
                else if (token == '/')
                    selectorMulDivMod[counterParanthess] = 1;
                else if (token == '%')
                    selectorMulDivMod[counterParanthess] = 2;
                powerTempList[counterParanthess].Clear();
                token = GetToken();
                Uslu();
                tempUslu = powerTempList[counterParanthess][0];
                for (int i = 1; i < powerTempList[counterParanthess].Count; i++)
                    tempUslu = Convert.ToSingle(Math.Pow(tempUslu, powerTempList[counterParanthess][i]));
                if (selectorMulDivMod[counterParanthess] == 0)
                    tempCarpmaBolme *= tempUslu;
                else if (selectorMulDivMod[counterParanthess] == 1)
                    tempCarpmaBolme /= tempUslu;
                else if (selectorMulDivMod[counterParanthess] == 2)
                    tempCarpmaBolme %= tempUslu;
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
                counterParanthess++;
                token = GetToken();
                selectorSubAdd.Add(-1);
                selectorMulDivMod.Add(-1);
                tempEvalueList.Add(0);
                powerTempList.Add(new List<float>());
                tempEvalueList[counterParanthess] = 0;
                Evalue();
                if (token == ')')
                {
                    counterParanthess--;
                    powerTempList[counterParanthess].Add(tempEvalueList[counterParanthess+1]);
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
            if (!assignFlag) 
            {
                if (defineControl[token])
                {
                    powerTempList[counterParanthess].Add(variables[token]);
                }
                else if (!defineControl[token])
                {
                    Console.WriteLine("\n!!!using non-assigned variable error!!!");
                    Environment.Exit(0);
                }
            }
            tempTokenVar = token;
            token = GetToken();
        }
        // R → '0' | '1' | … | '9' 
        static void Rakam()
        {
            powerTempList[counterParanthess].Add(Convert.ToSingle(token.ToString()));
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