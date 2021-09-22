using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using System.Diagnostics;

namespace Compiler_Construction
{
    public partial class Form1 : Form

    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button_file_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Input Text File";
            theDialog.Filter = "TXT files|*.txt";
            theDialog.InitialDirectory = @"D:\TestingFiles";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                textBox_inputfile.Text=theDialog.FileName.ToString();
            }
        }

        private void button_syntax_Click(object sender, EventArgs e)
        {
            
            string path = textBox_inputfile.Text;
            List<Token> alToken = getAllTokens(path);
            alToken.Add(new Token("$", -1));

            SyntaxAnalyzer syntaxAnalObj = new SyntaxAnalyzer(alToken);
            SemanticAnalyzer semanticAnalyzer = new SemanticAnalyzer();
            
            semanticAnalyzer.print_CT();
            string allTokens = "";
            foreach (var t in alToken)
            {
                allTokens += $"({t.ClassPart},{t.ValuePart},{t.Line})\n";
            }

            bool syntaxresult = syntaxAnalObj.checkRule();
            
            string aa = syntaxresult.ToString();
            MessageBox.Show($"Check Syntax: {syntaxresult}");

            
        }

        private void button_laxical_Click(object sender, EventArgs e)
        {
            string path = textBox_inputfile.Text;
            string[,] token = new string[100, 2];
            List<Token> alToken = getAllTokens(path);

            string allTokens = "";
            foreach (var t in alToken)
            {                
                allTokens += $"({t.ClassPart},{t.ValuePart},{t.Line})\n";
            }

            string outputPath = @"C:\Users\Admin\Desktop\token.txt";
            File.WriteAllText(outputPath, allTokens);
            Process.Start(outputPath);   
        }

        static List<Token> getAllTokens(string a)
        {
            List<Token> alToken = new List<Token>();
            string temp = "";
            bool multiLineComment = false;
            int no = 0;
             
            string[] text = File.ReadAllLines(a);

            for (int i = 0; i < text.Length; i++)
            {
                string str = text[i];
                //Console.WriteLine($"line= {str} length={str.Length}");

                for (int j = 0; j < str.Length; j++)
                {
                    //FOR STRING
                    if (str[j] == '\"' && multiLineComment != true)
                    {
                        temp += str[j];
                        while (j + 1 < str.Length && str[j + 1] != '\"')
                        {
                            if (str[j + 1] == '\\' && j + 2 < str.Length && (str[j + 2] == '\"'))
                            {
                                temp += str[j + 1].ToString() + str[j + 2].ToString();
                                j += 2;
                            }
                            else
                            {
                                j++;
                                temp += str[j];
                            }
                            if (j == str.Length - 1)
                                break;
                        }
                        if (j + 1 < str.Length && str[j + 1] == '\"')
                            temp += '\"';
                        j++;

                        //Console.WriteLine($"temp={temp}");
                        alToken.Add(new Token(temp, i));
                        no++;
                        temp = "";
                    }

                    //FOR CHAR
                    else if (str[j].Equals('\'') && multiLineComment != true)
                    {

                        //Console.WriteLine($"char start");
                        temp += str[j];
                        if (str[j + 1].Equals('\\'))
                        {
                            for (int k = 0; k < 3; k++)
                            {
                                if (j + 1 < str.Length)
                                {
                                    j++;
                                    temp += str[j].ToString();
                                }
                            }

                        }
                        else
                        {
                            for (int k = 0; k < 2; k++)
                            {
                                if (j + 1 < str.Length)
                                {
                                    j++;
                                    temp += str[j].ToString();
                                }

                            }
                        }

                        //Console.WriteLine($"temp={temp}");
                        alToken.Add(new Token(temp, i));
                        no++;
                        temp = "";

                    }

                    //FOR IDENTIFIER
                    else if (Char.IsLetter(str[j]) && multiLineComment != true)
                    {
                        //Console.WriteLine($"word HERE3 {str[j]}");
                        do
                        {
                            temp += str[j];
                            j++;
                            if (j == str.Length)
                            {
                                break;
                            }

                        } while (Char.IsLetter(str[j]) || Char.IsDigit(str[j]) || str[j] == '_' && str[j] != ' ');
                       // Console.WriteLine($"temp={temp}");
                        alToken.Add(new Token(temp, i));
                        j--;
                        no++;
                        temp = "";
                    }

                    // FOR DIGITS
                    else if (multiLineComment != true && j < str.Length && (((str[j] == '+' || str[j] == '-') && (Char.IsDigit(str[j + 1]) || (str[j + 1] == '.' && Char.IsDigit(str[j + 2])))) || (str[j] == '.' && Char.IsDigit(str[j + 1])) || Char.IsDigit(str[j])))
                    {
                        //bool valid = true;
                        //Console.WriteLine($"digit HERE3 {str[j]}");

                        do
                        {
                            if (str[j] == '.' && temp.Contains('.'))
                                break;
                            if (str[j] == '+' && temp.Contains('+'))
                                break;
                            if (str[j] == '-' && temp.Contains('-'))
                                break;
                            if (str[j] == 'e' && temp.Contains('e'))
                                break;
                            temp += str[j];
                            j++;
                            if (j == str.Length)
                                break;
                        } while (Char.IsDigit(str[j]) || str[j] == '.' || str[j] == 'e' || str[j] == '+' || str[j] == '-' || (!Char.IsWhiteSpace(str[j]) && Char.IsLetter(str[j])));
                        //Console.WriteLine($"temp={temp}");
                        alToken.Add(new Token(temp, i));
                        j--;
                        temp = "";
                        no++;
                    }

                    //FOR OPERATORS
                    else if (multiLineComment != true && j < str.Length && (str[j] == '+' || str[j] == '-' || str[j] == '*' || str[j] == '/' || str[j] == '%' || str[j] == '=' || str[j] == '<' || str[j] == '>' || str[j] == '^' || str[j] == '!' || str[j] == '&' || str[j] == '|')) // +-*/=%
                    {
                        //Console.WriteLine($"operator here4 {str[j]}");
                        temp += str[j];

                        if (j < str.Length - 1 && ((str[j] == '+' && str[j + 1] == '+') || (str[j] == '-' && str[j + 1] == '-') || (str[j] == '|' && str[j + 1] == '|') || (str[j] == '&' && str[j + 1] == '&'))) //++ -- && ||
                            temp += str[++j];
                        else if (j < str.Length - 1 && ((str[j] == '|' && str[j + 1] == '=') || (str[j] == '&' && str[j + 1] == '=')))
                            temp += "";
                        else if (j < str.Length - 1 && str[j + 1] == '=') // += -= *= /= == %=
                            temp += str[++j];

                        //Console.WriteLine($"temp={temp}");
                        alToken.Add(new Token(temp, i));
                        no++;
                        temp = "";
                    }

                    //FOR PUNCTUATOR
                    else if ((Char.IsPunctuation(str[j]) && str[j] != ':' && str[j] != '#' && multiLineComment != true) || (multiLineComment != true && (str[j] == '`' || str[j] == '_' || str[j] == '~' || str[j] == '@' || str[j] == '\\' || str[j] == '$')))
                    {
                        //Console.WriteLine($"punctuator Here => {str[j]}");
                        alToken.Add(new Token(str[j].ToString(), i));
                        no++;

                    }

                    //For inheritance symbol
                    else if (multiLineComment != true && j < str.Length && str[j] == ':')
                    {
                        Console.WriteLine($":Here j={j}");
                        if (j < str.Length - 1 && str[j + 1] == ':')
                        {
                            temp += str[j].ToString() + str[j + 1].ToString();
                           // Console.WriteLine($"temp={temp}");
                            alToken.Add(new Token(temp, i));
                            temp = "";
                            j++;
                        }
                        else
                        {
                            //Console.WriteLine($"temp={temp}");
                            alToken.Add(new Token(str[j].ToString(), i));
                        }
                    }

                    //FOR SINGLE COMMENT
                    else if (str[j].Equals('#') && str[j + 1] != '#' && multiLineComment != true)
                    {
                        //Console.WriteLine("single comment");
                        do
                        {
                            j++;
                        } while (j != str.Length);
                    }

                    //FOR MULTI LINE COMMENT
                    else if (str[j].Equals('#') && str[j + 1].Equals('#') || multiLineComment)
                    {
                        //Console.WriteLine($"comment here, multiline = {multiLineComment}");
                        if (multiLineComment)
                        {
                            if (j != str.Length - 1 && str[j] != '#' && str[j + 1] != '#')
                            {
                                do
                                {
                                    j++;
                                    if (j == str.Length - 1)
                                    {
                                        multiLineComment = true;
                                        break;
                                    }

                                } while (str[j] != '#' && str[j + 1] != '#' && j != str.Length - 1);
                                j++;
                                if (j != str.Length && str[j].Equals('#') && str[j + 1].Equals('#'))
                                {
                                    //Console.WriteLine($"outside while j= {j}");
                                    j++;
                                    if (multiLineComment)
                                        multiLineComment = false;
                                }

                            }
                            else
                            {
                                if (str[j] == '#' && str[j + 1] == '#')
                                    multiLineComment = false;

                                j++;
                            }
                        }
                        else
                        {
                            j++;
                            do
                            {
                                j++;
                                if (j == str.Length - 1)
                                {
                                    multiLineComment = true;
                                    break;
                                }
                            } while (str[j] != '#' && str[j + 1] != '#' && j != str.Length - 1);
                            j++;
                            if (j != str.Length && str[j].Equals('#') && str[j + 1].Equals('#'))
                            {
                                j++;
                                if (multiLineComment)
                                    multiLineComment = false;

                            }

                        }
                    }
                }
                alToken.Add(new Token("\\r", i));
            }

            return alToken;
        }

        private void button_semantic_Click(object sender, EventArgs e)
        {
            string outputPath = @"C:\Users\Admin\Desktop\CompilerFinal\maintable.txt";
            Process.Start(outputPath);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }


    class Token
    {
        string valuePart;
        string classPart;
        int line;

        public Token(string ValuePart, int Line)
        {
            valuePart = ValuePart;
            line = Line+1;
            classPart = identifyClass(ValuePart);
        }
        public string ClassPart { get { return classPart; } set { classPart = value; } }
        public string ValuePart { get { return valuePart; } set { valuePart = value; } }
        public int Line { get { return line; } set { line = value; } }

        string identifyClass(string value)
        {
            Regex idrgx = new Regex(@"^[a-zA-Z][\w]*$");
            Regex charrgx = new Regex(@"^'.{1}'$|^'\\.{1}'$");
            Regex digrgx = new Regex(@"(^[+-]?\d+)(?:[eE]([-+]?\d+))?$");
            Regex floatrgx = new Regex(@"^([+-]?\d*[.]{1}\d+)(?:[eE]([-+]?\d+))?$");
            Regex strrgx = new Regex(@"^[""](\W|\w)*[""]$");
            Regex boolrgx = new Regex(@"^true$|^false$");
            
            string[,] keywords = new string[,] { {"int","DT"}, { "double", "DT" }, { "char", "DT" }, { "float", "DT" }, { "bool", "DT" },{ "string", "DT" },{"foreach", "foreach"},{"while","while"},{"return","return"},{"label","label"},
                                                  {"var","var"},{"goto","goto"},{"switch","switch"},{"case","case"},{"default","default"},{"break","break"},{"continue","continue"},{"new","new"},{"void","void"},{"print","print"},
                                                  {"end","end"},{"if","if"},{"else","else"},{"private","private"},{"public","public"},{"protected","protected"},{"class","class"},{"Main","Main"},{"abstract","abstract"},
                                                  {"final","final"},{"static","static"},{"null","null"},{"virtual","virtual"},{"override","override"},{"this","this"},{"in","in"},{"const","const"},{"interface","interface"},{"extends","extends"} };

            string[,] operators = new string[,] { {"+","PM"}, {"-","PM"}, {"*","MDM"}, {"/","MDM"}, {"%","MDM"}, {"&&","AND"}, {"||","OR"}, {"!","!"}, {"<","RO"}, {">","RO"}, {"<=","RO"}, {">=","RO"}, {"!=","RO"}, {"==","RO"},
                                                  {"++","inc_dec"},{"--","inc_dec"},{"+=","CO"},{"-=","CO"},{"*=","CO"},{"/=","CO"},{"=","="}};

            string[,] punctuators = new string[,] { { "\"", "\"" }, { "'", "'" }, { "(", "(" }, { ")", ")" }, { "{", "{" }, { "}", "}" }, { "[", "[" }, { "]", "]" }, { ",", "," }, { ".", "." }, { ":", ":" }, { ";", ";" }, { "?", "?" }, { "::", "inherit" }, { "\\r", "\\r" } };

            if (Char.IsLetter(value[0]))
            {
                if (boolrgx.IsMatch(value))
                    return "bool-const";
                else if (idrgx.IsMatch(value))
                {
                    for (int i = 0; i < keywords.Length / 2; i++)
                    {
                        if (value == keywords[i, 0])
                            return keywords[i, 1];
                    }
                    return "ID";
                }
            }
            else if (charrgx.IsMatch(value))
                return "char-const";
            else if (strrgx.IsMatch(value))
                return "string-const";
            else if (digrgx.IsMatch(value))
                return "int-const";
            else if (floatrgx.IsMatch(value))
                return "float-const";
            for (int i = 0; i < operators.Length / 2; i++)
            {
                if (value == operators[i, 0])
                    return operators[i, 1];
            }
            for (int i = 0; i < punctuators.Length / 2; i++)
            {
                if (value == punctuators[i, 0])
                    return punctuators[i, 1];
            }
            return "ERROR";
        }
    }
}

