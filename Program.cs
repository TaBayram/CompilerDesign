

using System;
using System.Collections.Generic;
namespace CompilerDesign
{
    
    public class Compiler{

        public static Compiler Instance;
        public string input;

        public Compiler(string input)
        {
            this.input = input;
        }

        public static Compiler getInstance(String input)
        {
            if (Instance == null)
            {
                Instance = new Compiler(input);
            }
            return Instance;
        }

        List<char> operators = new List<char>() { '+', '=', '-', '*', '/', '%', ';', '?', ':', '{', '}', '<', '>' };
        List<char> letters = new List<char>() { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        List<char> numbers = new List<char>() { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        List<float> tempEvalueList = new List<float>();
        List<List<float>> powerTempList = new List<List<float>>();
        List<int> selectorSubAdd = new List<int>();
        List<int> selectorMulDivMod = new List<int>();
        List<float> ifCondList = new List<float>();
        List<float> WhileCondList = new List<float>();
        List<int> WhileControlList = new List<int>();
        Dictionary<char, float> variables = new Dictionary<char, float>(){
                                                                                 {'a', new float()},{'b', new float()},{'c', new float()},{'d', new float()},
                                                                                 {'e', new float()},{'f', new float()},{'g', new float()},{'h', new float()},
                                                                                 {'i', new float()},{'j', new float()},{'k', new float()},{'l', new float()},
                                                                                 {'m', new float()},{'n', new float()},{'o', new float()},{'p', new float()},
                                                                                 {'q', new float()},{'r', new float()},{'s', new float()},{'t', new float()},
                                                                                 {'u', new float()},{'v', new float()},{'w', new float()},{'x', new float()},
                                                                                 {'y', new float()},{'z', new float()}
                                                                             };
        Dictionary<char, bool> defineControl = new Dictionary<char, bool>(){
                                                                                 {'a', false},{'b', false},{'c', false},{'d', false},
                                                                                 {'e', false},{'f', false},{'g', false},{'h', false},
                                                                                 {'i', false},{'j', false},{'k', false},{'l', false},
                                                                                 {'m', false},{'n', false},{'o', false},{'p', false},
                                                                                 {'q', false},{'r', false},{'s', false},{'t', false},
                                                                                 {'u', false},{'v', false},{'w', false},{'x', false},
                                                                                 {'y', false},{'z', false}
                                                                             };
        int tokenCounter = 0;
        char token;
        char tempTokenVar;
        string tokenList;
        int inputLen;
        float tempCarpmaBolme = 0;
        float tempUslu = 0;
        int tempWhileControl = -1;
        int tempIfControl = -1;
        int counterParanthess = 0;
        bool assignFlag = false;
        
        public void ReadyInputForCompiling()
        {
            tokenList = input.Replace(" ", "");
            inputLen = tokenList.Length - 1;
            tokenCounter = 0;
            token = GetToken();
            Program_();
        }
        
        char GetToken()
        {
            char tokenpop;
            if (tokenCounter <= inputLen)
            {
        
                tokenpop = tokenList[tokenCounter];
                if (tempWhileControl > -1)
                {
                    for (int i = 0; i < tempWhileControl + 1; i++)
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
        
        
        
        
        
        
        void Program_()
        {
            if (token != '.')
            {
                Cumle();
            }
            if (token == '.')
            {
                //program?? sonland??r
                Console.WriteLine("\n-->interpreted with no error<--");
            }
        
        }
        
        // C ??? I | W | A | ?? | G 
        void Cumle()
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
                if (tempWhileControl == WhileControlList.Count)
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
        
        // I   ??? '[' E '?' C{ C } ':' C { C } ']' | '[' E '?' C{C} ']'
        void If()
        {
            int interIf = 0;
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
                        if (token == '[')
                        {
                            interIf++;
                        }
                        while (interIf > 0)
                        {
                            if (token == ']')
                            {
                                interIf--;
                            }
                            token = GetToken();
                        }
                        if (interIf == 0)
                        {
                            token = GetToken();
                        }
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
                            if (token == '[')
                            {
                                interIf++;
                            }
                            while (interIf > 0)
                            {
                                if (token == ']')
                                {
                                    interIf--;
                                }
                                token = GetToken();
                            }
                            if (interIf == 0)
                            {
                                token = GetToken();
                            }
                        }
                    }
                    tempIfControl--;
                    token = GetToken();
                    Cumle();
                }
        
            }
        }
        
        //W ??? '{' E '?'  C{C} '}'
        void Whle()
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
                        if (token == '{')
                        {
                            interWhile++;
                        }
                        token = GetToken();
        
                    }
                }
                if (WhileCondList[tempWhileControl] == 0)
                {
                    while (interWhile > 0)
                    {
                        token = GetToken();
                        if (token == '}')
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
        // A ??? K '=' E ';'
        void Atama()
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
                    //atama i??lemi
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
        // ?? ??? '<' E ';'
        void Cikti()
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
        // G ??? '>' K ';'
        void Girdi()
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
        // E ??? T {('+' | '-') T}
        void Evalue()
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
        // T ??? U {('*' | '/' | '%') U}
        void TerimCarpBol()
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
        // U ??? F '^' U | F
        void Uslu()
        {
            Fgroup();//  a^2^4^5
            while (token == '^')
            {
                token = GetToken();
                Uslu();
            }
        }
        // F ??? '(' E ')' | K | R
        void Fgroup()
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
                    powerTempList[counterParanthess].Add(tempEvalueList[counterParanthess + 1]);
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
        // K ??? 'a' | 'b' | ??? | 'z' 
        void KHarf()
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
        // R ??? '0' | '1' | ??? | '9' 
        void Rakam()
        {
            powerTempList[counterParanthess].Add(Convert.ToSingle(token.ToString()));
            token = GetToken();
        }


    } 
    class Program
    {
        
            static public void Main(String[] args)
            {
                string input;
                do
                {
                    //input ="  n = 0; { n - 2*5 ? < n; n=n+1;}  ";
                    input = Console.ReadLine();
                    Compiler.getInstance(input).ReadyInputForCompiling();
                    Compiler.Instance = null;
                    
                    
                } while (input != "");
            }

            
    }
}




/*
P ??? {C} '.'
C ??? I | W | A | ?? | G 
I ??? '[' E '?' C{ C } ':' C{ C } ']' | '[' E '?' C{C} ']'
W ??? '{' E '?' C{C} '}'
A ??? K '=' E ';'
?? ??? '<' E ';'
G ??? '>' K ';'
E ??? T {('+' | '-') T}
T ??? U {('*' | '/' | '%') U}
U ??? F '^' U | F
F ??? '(' E ')' | K | R
K ??? 'a' | 'b' | ??? | 'z' 
R ??? '0' | '1' | ??? | '9' 
P: program
C: C??mle
I: IF c??mlesi
W: While d??ng??s??A: Atama c??mlesi
??: ????kt?? c??mlesi
G: Girdi c??mlesi
E: Aritmetik ??fade 
T: ??arpma-b??lme-mod terimi
U: ??sl?? ifade
F: Gruplama ifadesi
K: K??????k harfler
R: Rakamlar
*/