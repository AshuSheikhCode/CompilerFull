using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Data;
//using static Compiler_Construction.Program;
using Compiler_Construction;
using System.Windows.Forms;

namespace Compiler_Construction
{
    class SyntaxAnalyzer
    {
        SemanticAnalyzer semantic = new SemanticAnalyzer();
        string outputPath = @"C:\Users\Admin\Desktop\CompilerFinal\syntax.txt";
        string cat;
        string name;
        string parent;
        List<Token> token = new List<Token>();
        static int index = 0;

        

        public SyntaxAnalyzer(List<Token> t)
        {
            this.token = t;
        }

        public bool checkRule()
        {
            if (Start())
            {
                //Console.WriteLine(token[index].ClassPart);
                Console.WriteLine("No syntax Error");
                //return true;
                if (token[index].ValuePart == "$")
                {
                    Console.WriteLine("No syntax Error");
                    return true;
                }
            }
            MessageBox.Show($"RETURNING FALSE {token[index].ClassPart} {token[index].Line}");
            return false;
        }



        public bool Start()
        {
            if (token[index].ClassPart == "class" || token[index].ClassPart == "abstract" || token[index].ClassPart == "final")
            {
                if (TM2(out cat))
                {
                    if (token[index].ClassPart == "class")
                    {
                        index++;
                        if (token[index].ClassPart.Equals("ID"))
                        {
                            name = token[index].ValuePart;
                            index++;
                            if (token[index].ClassPart.Equals(":"))
                            {
                                index++;
                                semantic.create_CT();
                                File.WriteAllText(outputPath, $"{semantic.insert_MT(name, "class", "internal", cat, "none")}");
                                if (token[index].ClassPart.Equals("\\r"))
                                {
                                    index++;
                                    if (MAINCLASS())
                                    {
                                        if (token[index].ClassPart.Equals("end"))
                                        {
                                            index++;
                                            if (token[index].ClassPart.Equals("\\r"))
                                            {
                                                index++;
                                                //Console.WriteLine(token[index].ClassPart);
                                                if (CLASSES())
                                                {
                                                    return true;
                                                }

                                            }

                                        }

                                    }
                                }

                            }

                        }



                    }
                }

            }

            //if (token[index].ClassPart == "abstract")
            //{
            //    if (TM2(out cat)) // check class type(final,abstract,general)
            //    {
            //        if (token[index].ClassPart == "class")
            //        {
            //            index++;
            //            if (token[index].ClassPart.Equals("ID"))
            //            {
            //                name = token[index].ValuePart;
            //                index++;
            //                if (token[index].ClassPart.Equals(":"))
            //                {
            //                    index++;
            //                    File.WriteAllText(outputPath, $"{semantic.insert_MT(name, "class", "internal", cat, "none")}");
            //                    if (token[index].ClassPart.Equals("\\r"))
            //                    {
            //                        index++;
            //                        if (MST())
            //                        {
            //                            if (token[index].ClassPart.Equals("end"))
            //                            {
            //                                index++;
            //                                if (token[index].ClassPart.Equals("\\r"))
            //                                {
            //                                    index++;
            //                                    //Console.WriteLine(token[index].ClassPart);
            //                                    if (CLASSES())// check this 1
            //                                    {
            //                                        return true;
            //                                    }
            //                                }
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

            return false;
        }



  

        //    if (token[index].ClassPart == "class" || token[index].ClassPart == "abstract" || token[index].ClassPart == "final" )
        //    {
        //        if (TM2(out cat)) // check class type(final,abstract,general)
        //        {
        //            if (token[index].ClassPart == "class")
        //            {
        //                index++;
        //                if (token[index].ClassPart.Equals("ID"))
        //                {
        //                    name = token[index].ValuePart;
        //                    index++;
        //                    if (token[index].ClassPart.Equals(":"))
        //                    {
        //                        index++;
        //                        File.WriteAllText(outputPath, $"{semantic.insert_MT(name,"class", "internal", cat, "none")}");
        //                        if (token[index].ClassPart.Equals("\\r"))
        //                        {
        //                            index++;
        //                            if (MAINCLASS())
        //                            {
        //                                if (token[index].ClassPart.Equals("end"))
        //                                {
        //                                    index++;
        //                                    if (token[index].ClassPart.Equals("\\r"))
        //                                    {
        //                                        index++;
        //                                        //Console.WriteLine(token[index].ClassPart);
        //                                        if (CLASSES())// check this 1
        //                                        {
        //                                            return true;
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return false;
        //}

        public bool MAINCLASS()
        {
            if (token[index].ClassPart.Equals("public") || token[index].ClassPart.Equals("private") || token[index].ClassPart.Equals("protected") ||
               token[index].ClassPart.Equals("abstract") || token[index].ClassPart.Equals("const") || token[index].ClassPart.Equals("final") ||
               token[index].ClassPart.Equals("virtual") || token[index].ClassPart.Equals("override") || token[index].ClassPart.Equals("void") ||
               token[index].ClassPart.Equals("DT") || token[index].ClassPart.Equals("this") || token[index].ClassPart.Equals("ID") || token[index].ClassPart.Equals("static"))
            {
                if (MAIN_FN())
                {
                    if (CL_BODY())
                    {
                        // Console.WriteLine("MainClass");
                        return true;
                    }

                }

            }

            return false;
        }

        public bool CLASSES()
        
        {
            if (token[index].ClassPart.Equals("abstract") || token[index].ClassPart.Equals("final") || token[index].ClassPart.Equals("class"))
            {
               

                if (CDEC())
                {
                    if (CLASSES())
                        return true;
                }
            }
            if (token[index].ValuePart == "$")
                return true;

            return false;
        }

        public bool MAIN_FN()
        {
            if (token[index].ClassPart.Equals("static") || token[index].ClassPart.Equals("public") || token[index].ClassPart.Equals("private") || token[index].ClassPart.Equals("protected"))
            {
                if (AM())
                {
                    if (token[index].ClassPart.Equals("static"))
                    {
                        index++;
                        if (token[index].ClassPart.Equals("void"))
                        {
                            index++;
                            if (token[index].ClassPart.Equals("Main"))
                            {
                                index++;
                                if (token[index].ClassPart.Equals("("))
                                {
                                    index++;
                                    if (token[index].ClassPart.Equals(")"))
                                    {
                                        index++;
                                        if (token[index].ClassPart.Equals(":"))
                                        {
                                            index++;
                                            if (token[index].ClassPart.Equals("\\r"))
                                            {
                                                index++;
                                                if (MST())
                                                {
                                                    if (token[index].ClassPart.Equals("end"))
                                                    {
                                                        index++;
                                                        if (token[index].ClassPart.Equals("\\r"))
                                                        {
                                                            index++;
                                                            return true;
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }

                }

            }

            return false;

        }

        public bool ALLDEC2()
        {
            if (token[index].ClassPart.Equals("abstract"))
            {
                index++;
                if (TYPES())
                {
                    if (FN())
                        return true;
                }

            }
            if (token[index].ClassPart.Equals("virtual") || token[index].ClassPart.Equals("override") || token[index].ClassPart.Equals("void") ||
                     token[index].ClassPart.Equals("DT") || token[index].ClassPart.Equals("static") || token[index].ClassPart.Equals("final") ||
                     token[index].ClassPart.Equals("ID"))
            {
                if (TM3())
                {
                    Console.WriteLine($"ALLDEC2 TM3 {token[index].ClassPart} {token[index].Line} ******************************************************");
                    if (ALLDEC3())
                    {
                        Console.WriteLine($"Alldec2 2 cond {token[index].ClassPart}");
                        return true;
                    }

                }

            }
            if (token[index].ClassPart.Equals("const"))
            {
                if (CONST_DEC())
                    return true;
            }

            return false;
        }

        public bool ALLDEC3()
        {
            Console.WriteLine($"ALLDEC3 {token[index].ClassPart}");
            if (token[index].ClassPart.Equals("virtual") || token[index].ClassPart.Equals("override"))
            {
                if (FN_MOD())
                {
                    if (CONST_DEC())
                        return true;
                }
            }
            if (token[index].ClassPart.Equals("void") || token[index].ClassPart.Equals("DT") || token[index].ClassPart.Equals("ID"))
            {
                Console.WriteLine("AllDec3 condition2");
                if (ALLDEC4())
                {
                    return true;
                }
            }

            return false;
        }

        public bool ALLDEC4()
        {
            Console.WriteLine($"ALLDEC4 {token[index].Line}");
            if (token[index].ClassPart.Equals("void") || token[index].ClassPart == "print" || token[index].ClassPart.Equals("override") || token[index].ClassPart.Equals("virtual"))
            {
                index++;
                if (FN())
                    return true;
            }
            if (token[index].ClassPart.Equals("DT"))
            {
                Console.WriteLine($"ALLDEC4 {token[index].Line} DT DT DT DT");
                index++;
                Console.WriteLine($"ALLDEC4 {token[index].Line} {token[index].ClassPart}");
                if (ALLDEC5())
                    return true;
            }
            if (token[index].ClassPart.Equals("ID"))
            {
                index++;
                if (ALLDEC7())
                {
                    return true;
                }

            }

            return false;
        }

        public bool ALLDEC5()
        {
            Console.WriteLine("ALLDEC5)");
            if (token[index].ClassPart.Equals("ID"))
            {
                index++;
                if (ALLDEC6())
                    return true;
            }
            if (token[index].ClassPart.Equals("["))
            {
                if (ARR_DEC())
                    return true;
            }
            return false;
        }

        public bool ALLDEC6()
        {
            Console.WriteLine($"ALLDEC6) {token[index].ClassPart}  {token[index].Line}");
            if (token[index].ClassPart.Equals("("))
            {
                Console.WriteLine($"FN_DEC  {token[index].ClassPart}  {token[index].Line}*********************************************** ");
                if (FN_DEC())
                {
                    Console.WriteLine($"FN_DEC RETURNING TRUE {token[index].ClassPart}  {token[index].Line}*********************************************** ");
                    return true;
                }

            }
            if (token[index].ClassPart.Equals("=") || token[index].ClassPart.Equals("\\r") || token[index].ClassPart.Equals(","))
            {
                if (DEC())
                    return true;

            }

            return false;
        }
        public bool ALLDEC7()
        {
            int backtracking = index;
            Console.WriteLine("INSIDE AL");
            if (token[index].ClassPart.Equals("["))
            {
                Console.WriteLine("BEFORE ARR_DEC FROM ALL-DEC7");
                if (ARR_DEC())
                {
                    return true;
                }

                else
                {
                    index = backtracking;
                    if (X())
                    {
                        Console.WriteLine("BEFORE ALLDEC8 FROM ALLDEC7");
                        if (ALLDEC8())
                            return true;
                    }
                }
            }
            else if (token[index].ClassPart.Equals("ID") || token[index].ClassPart == "=" || token[index].ClassPart == "," || token[index].ClassPart == "\\r")
            {
                if (OBJ_CALL())
                {
                    return true;
                }
                else
                {
                    index = backtracking;
                    if (X())
                    {
                        if (ALLDEC8())
                            return true;
                    }
                }
            }
            else
            {
                index = backtracking;
                Console.WriteLine("CO BEFORE X FROM ALLDEC7");
                if (X())
                {
                    if (ALLDEC8())
                        return true;
                }
            }
            return false;

        }

        //public bool ALLDEC7()
        //{
        //    int backtracking = index;
        //    if (token[index].ClassPart.Equals("["))
        //    {
        //        if (ARR_DEC())
        //            return true;
        //    }
        //    if (token[index].ClassPart.Equals("("))
        //    {
        //        if (OBJ_CALL())
        //            return true;
        //    }
        //    else
        //    {
        //        index = backtracking;
        //        if (X())
        //        {
        //            if (ALLDEC8())
        //                return true;
        //        }
        //    }
        //    return false;

        //}

        public bool ALLDEC8()
        {
            if (token[index].ClassPart.Equals("("))
            {
                index++;
                if (PL_PR())
                {
                    if (token[index].ClassPart.Equals(")"))
                    {
                        index++;
                        return true;
                    }
                }

            }
            if (token[index].ClassPart.Equals("=") || token[index].ClassPart.Equals("CO"))
            {
                if (ASGN_ST())
                {
                    return true;
                }
            }
            if (token[index].ClassPart.Equals("inc_dec"))
            {
                index++;
                if (token[index].ClassPart.Equals("\\r"))
                {
                    index++;
                    return true;
                }
            }

            return false;
        }

        public bool TYPES()
        {
            if (token[index].ClassPart.Equals("void") || token[index].ClassPart.Equals("DT"))
            {
                index++;
                return true;
            }
            return false;
        }

        public bool CDEC()
        {
            if (token[index].ClassPart.Equals("abstract") || token[index].ClassPart.Equals("final") || token[index].ClassPart.Equals("class"))
            {
                if (TM2(out cat))
                {
                    if (token[index].ClassPart.Equals("class"))
                    {
                        index++;
                        if (token[index].ClassPart.Equals("ID"))
                        {
                            name = token[index].ValuePart;
                            index++;
                            //if (token[index].ClassPart.Equals(":"))
                            //{
                            if (INHERIT(out parent))
                            {
                                //Console.WriteLine($"Inherit Passes {token[index].ValuePart}");
                                semantic.create_CT();
                                semantic.insert_MT(name,"class", "internal", cat, parent);
                                if (token[index].ClassPart.Equals(":"))
                                {
                                    index++;
                                    if (token[index].ClassPart.Equals("\\r"))
                                    {
                                        index++;
                                        if (CL_BODY())
                                        {
                                            if (token[index].ClassPart.Equals("end"))
                                            {
                                                index++;
                                                if (token[index].ClassPart.Equals("\\r"))
                                                {
                                                    index++;
                                                    return true;
                                                }
                                            }

                                        }

                                    }
                                }
                            }

                            //}

                        }


                    }
                }
            }
            return false;
        }

        public bool CTOR()
        {
            Console.WriteLine($"Ctor { token[index].ValuePart}");
            if (token[index].ClassPart.Equals("ID"))
            {
                index++;
                if (token[index].ClassPart.Equals("("))
                {
                    index++;
                    if (PL())
                    {
                        if (token[index].ClassPart.Equals(")"))
                        {
                            index++;
                            if (token[index].ClassPart.Equals(":"))
                            {
                                index++;
                                if (token[index].ClassPart.Equals("\\r"))
                                {
                                    index++;
                                    if (MST())
                                    {
                                        if (token[index].ClassPart.Equals("end"))
                                        {
                                            index++;
                                            if (token[index].ClassPart.Equals("\\r"))
                                            {
                                                index++;
                                                return true;
                                            }

                                        }
                                    }

                                }

                            }
                        }
                    }

                }

            }
            return false;
        }

        public bool INHERIT(out string parent)
        {
            //Console.WriteLine($"inherit func {token[index].ClassPart}");
            if (token[index].ClassPart.Equals("inherit"))
            {
                index++;
                if (token[index].ClassPart.Equals("ID"))
                {
                    parent = token[index].ValuePart;
                    index++;
                    return true;
                    //if (token[index].ClassPart.Equals(":"))
                    //{
                    //index++;
                    //    if (token[index].ClassPart.Equals("\\r"))
                    //    {
                    //        index++;
                    //        return true;
                    //    }
                    //}
                }
            }
            if (token[index].ClassPart.Equals(":"))
            {
                parent = "none";
                return true;
            }
            parent = "none";
            return false;
        }

        public bool CL_BODY()
        {
            if (token[index].ClassPart.Equals("abstract") || token[index].ClassPart.Equals("const") || token[index].ClassPart.Equals("static") || token[index].ClassPart.Equals("final") ||
                    token[index].ClassPart.Equals("virtual") || token[index].ClassPart.Equals("override") || token[index].ClassPart.Equals("void") || token[index].ClassPart.Equals("DT") ||
                    token[index].ClassPart.Equals("ID") || token[index].ClassPart.Equals("public") || token[index].ClassPart.Equals("private") || token[index].ClassPart.Equals("protected"))
            {
                Console.WriteLine($"inside CLBODY {token[index].ClassPart}");
                if (AM())
                {
                    Console.WriteLine($"clbody AM crosss {token[index].ClassPart}");
                    if (CL_BODY1())
                    {
                        return true;
                    }
                }

            }
            if (token[index].ClassPart.Equals("end"))
            {
                Console.WriteLine("CLBODY");
                return true;
            }

            return false;
        }

        public bool CL_BODY1()
        {
            Console.WriteLine($"inside CLBODY1 {token[index].ClassPart}");

            int backTrackingIndex = index;
            if (token[index].ClassPart.Equals("abstract") || token[index].ClassPart.Equals("const") || token[index].ClassPart.Equals("static") || token[index].ClassPart.Equals("final") ||
                    token[index].ClassPart.Equals("virtual") || token[index].ClassPart.Equals("override") || token[index].ClassPart.Equals("void") || token[index].ClassPart.Equals("DT")
                   || token[index].ClassPart.Equals("ID"))
            {
                Console.WriteLine($"inside CLBODY1 cond1 {token[index].ClassPart} {token[index].Line}");
                if (ALLDEC2())
                {
                    Console.WriteLine($"CL_BODY1 AFTER ALLDEC2 {token[index].Line}");
                    if (CL_BODY())
                        return true;
                }
                else
                {
                    index = backTrackingIndex;
                    if (CTOR())
                    {
                        Console.WriteLine($"inside CLBODY1 ifnotCtor {token[index].ClassPart}");

                        if (CL_BODY())
                            return true;
                    }
                }
            }
            if (token[index].ClassPart.Equals("ID"))
            {
                index = backTrackingIndex;
                if (CTOR())
                {
                    Console.WriteLine($"inside CLBODY1 ifnotCtor {token[index].ClassPart}");

                    if (CL_BODY())
                        return true;
                }
            }

            return false;

        }

        public bool PL()
        {
            if (token[index].ClassPart.Equals("DT"))
            {
                index++;
                if (token[index].ClassPart.Equals("ID"))
                {
                    index++;
                    if (LIST())
                        return true;
                }

            }
            if (token[index].ClassPart.Equals(")"))
            {
                return true;
            }

            return false;
        }

        public bool LIST()
        {
            if (token[index].ClassPart.Equals(","))
            {
                index++;
                if (PL0())
                    return true;
            }
            else if (token[index].ClassPart.Equals(")"))
            {
                return true;
            }

            return false;

        }

        public bool PL0()
        {
            if (token[index].ClassPart == "DT")
            {
                index++;
                if (token[index].ClassPart.Equals("ID"))
                {
                    index++;
                    if (LIST())
                        return true;
                }

            }
            return false;
        }

        public bool OBJ_CALL()
        {
            if (token[index].ClassPart.Equals("ID"))
            {
                index++;
                if (OBJ_DEC1())
                    return true;
            }
            else if (token[index].ClassPart.Equals("=") || token[index].ClassPart.Equals("\\r") || token[index].ClassPart.Equals(","))
            {
                if (OBJ_DEC1())
                    return true;
            }

            return false;
        }

        public bool OBJ_DEC1()
        {
            if (token[index].ClassPart.Equals("="))
            {
                index++;
                if (token[index].ClassPart.Equals("new"))
                {
                    index++;
                    if (token[index].ClassPart.Equals("ID"))
                    {
                        index++;
                        if (token[index].ClassPart.Equals("("))
                        {
                            index++;
                            if (PL_PR())
                            {
                                if (token[index].ClassPart.Equals(")"))
                                {
                                    index++;
                                    if (L2())
                                        return true;
                                }
                            }

                        }
                    }

                }
            }
            if (token[index].ClassPart.Equals(",") || token[index].ClassPart.Equals("\\r"))
            {
                if (L2())
                    return true;
            }
            if (token[index].ClassPart.Equals("\\r"))
            {
                index++;
                return true;
            }

            return false;
        }

        public bool L2()
        {
            if (token[index].ClassPart.Equals(","))
            {
                index++;
                if (token[index].ClassPart.Equals("ID"))
                {
                    index++;
                    if (OBJ_DEC1())
                        return true;
                }
            }
            if (token[index].ClassPart.Equals("\\r"))
            {
                index++;
                return true;
            }

            return false;
        }

        public bool PL_PR()
        {
            if (token[index].ClassPart.Equals("ID") || token[index].ClassPart.Equals("this") || token[index].ClassPart.Equals("int-const") ||
                token[index].ClassPart.Equals("char-const") || token[index].ClassPart.Equals("bool-const") || token[index].ClassPart.Equals("string-const") ||
                token[index].ClassPart.Equals("float-const") || token[index].ClassPart.Equals("(") || token[index].ClassPart.Equals("!") ||
                token[index].ClassPart.Equals("new") || token[index].ClassPart.Equals("inc_dec"))
            {
                if (OE())
                {
                    if (PL2())
                        return true;
                }
            }
            if (token[index].ClassPart.Equals(")"))
            {
                return true;
            }

            return false;
        }

        public bool PL2()
        {
            if (token[index].ClassPart.Equals(","))
            {
                index++;
                if (OE())
                {
                    if (PL2())
                        return true;
                }
            }
            else if (token[index].ClassPart.Equals(")"))
            {
                return true;
            }

            return false;
        }

        public bool OE()
        {
            if (token[index].ClassPart.Equals("ID") || token[index].ClassPart.Equals("this") || token[index].ClassPart.Equals("(") || token[index].ClassPart.Equals("!") ||
               token[index].ClassPart.Equals("new") || token[index].ClassPart.Equals("inc_dec") || token[index].ClassPart.Equals("int-const") || token[index].ClassPart.Equals("char-const")
               || token[index].ClassPart.Equals("bool-const") || token[index].ClassPart.Equals("string-const") || token[index].ClassPart.Equals("float-const"))
            {
                if (AE())
                {
                    if (OE1())
                        return true;
                }
            }

            return false;
        }

        public bool OE1()
        {
            if (token[index].ClassPart.Equals("OR"))
            {
                index++;
                if (AE())
                {
                    if (OE1())
                        return true;
                }
            }
            if (token[index].ClassPart.Equals(",") || token[index].ClassPart.Equals(")") || token[index].ClassPart.Equals("\\r")
                    || token[index].ClassPart.Equals("]") || token[index].ClassPart.Equals("}") || token[index].ClassPart.Equals("=") || token[index].ClassPart.Equals(":"))
            {
                Console.WriteLine($"OE1 NULL {token[index].ClassPart}");
                return true;
            }

            return false;
        }

        public bool AE()
        {
            if (token[index].ClassPart.Equals("ID") || token[index].ClassPart.Equals("this") || token[index].ClassPart.Equals("(") || token[index].ClassPart.Equals("!") ||
               token[index].ClassPart.Equals("new") || token[index].ClassPart.Equals("inc_dec") || token[index].ClassPart.Equals("int-const") || token[index].ClassPart.Equals("char-const")
               || token[index].ClassPart.Equals("bool-const") || token[index].ClassPart.Equals("string-const") || token[index].ClassPart.Equals("float-const"))
            {
                if (RE())
                {
                    if (AE1())
                        return true;
                }

            }
            return false;
        }

        public bool AE1()
        {
            if (token[index].ClassPart.Equals("AND"))
            {
                index++;
                if (RE())
                {
                    if (AE1())
                        return true;
                }
            }
            if (token[index].ClassPart.Equals("OR") || token[index].ClassPart.Equals(")") || token[index].ClassPart.Equals("\\r") || token[index].ClassPart.Equals(",")
                    || token[index].ClassPart.Equals("]") || token[index].ClassPart.Equals("}") || token[index].ClassPart.Equals("=") || token[index].ClassPart.Equals(":"))
            {
                Console.WriteLine($"AE1 NULL {token[index].ClassPart}");
                return true;
            }

            return false;
        }

        public bool RE()
        {
            if (token[index].ClassPart.Equals("ID") || token[index].ClassPart.Equals("this") || token[index].ClassPart.Equals("(") || token[index].ClassPart.Equals("!") ||
               token[index].ClassPart.Equals("new") || token[index].ClassPart.Equals("inc_dec") || token[index].ClassPart.Equals("int-const") || token[index].ClassPart.Equals("char-const")
               || token[index].ClassPart.Equals("bool-const") || token[index].ClassPart.Equals("string-const") || token[index].ClassPart.Equals("float-const"))
            {
                if (E())
                {
                    if (RE1())
                        return true;
                }

            }
            return false;
        }

        public bool RE1()
        {
            if (token[index].ClassPart.Equals("RO"))
            {
                index++;
                if (E())
                {
                    if (RE1())
                        return true;
                }
            }
            else if (token[index].ClassPart.Equals("AND") || token[index].ClassPart.Equals("OR") || token[index].ClassPart.Equals(")") || token[index].ClassPart.Equals("\\r")
                    || token[index].ClassPart.Equals("]") || token[index].ClassPart.Equals("}") || token[index].ClassPart.Equals("=") || token[index].ClassPart.Equals(":")
                    || token[index].ClassPart.Equals(","))
            {
                Console.WriteLine($"RE1 NULL {token[index].ClassPart}");
                return true;
            }

            return false;
        }

        public bool E()
        {
            if (token[index].ClassPart.Equals("ID") || token[index].ClassPart.Equals("this") || token[index].ClassPart.Equals("(") || token[index].ClassPart.Equals("!") ||
                token[index].ClassPart.Equals("new") || token[index].ClassPart.Equals("inc_dec") || token[index].ClassPart.Equals("int-const") || token[index].ClassPart.Equals("char-const")
                || token[index].ClassPart.Equals("bool-const") || token[index].ClassPart.Equals("string-const") || token[index].ClassPart.Equals("float-const"))
            {
                if (T())
                {
                    if (E1())
                        return true;

                }
            }

            return false;
        }

        public bool E1()
        {
            if (token[index].ClassPart.Equals("PM"))
            {
                index++;
                if (T())
                {
                    if (E1())
                        return true;
                }
            }
            else if (token[index].ClassPart.Equals("RO") || token[index].ClassPart.Equals("AND") || token[index].ClassPart.Equals("OR") ||
                    token[index].ClassPart.Equals(")") || token[index].ClassPart.Equals("\\r") || token[index].ClassPart.Equals("]") || token[index].ClassPart.Equals("}") ||
                    token[index].ClassPart.Equals("=") || token[index].ClassPart.Equals(":") || token[index].ClassPart.Equals(","))
            {
                Console.WriteLine($"E1 NULL {token[index].ClassPart}");
                return true;
            }

            return false;
        }

        public bool T()
        {
            if (token[index].ClassPart.Equals("ID") || token[index].ClassPart.Equals("this") || token[index].ClassPart.Equals("(") || token[index].ClassPart.Equals("!") ||
                token[index].ClassPart.Equals("new") || token[index].ClassPart.Equals("inc_dec") || token[index].ClassPart.Equals("int-const") || token[index].ClassPart.Equals("char-const")
                || token[index].ClassPart.Equals("bool-const") || token[index].ClassPart.Equals("string-const") || token[index].ClassPart.Equals("float-const"))
            {
                if (F())
                {
                    if (T1())
                        return true;
                }

            }
            return false;
        }

        public bool T1()
        {
            if (token[index].ClassPart.Equals("MDM"))
            {
                index++;
                if (F())
                {
                    if (T1())
                        return true;
                }

            }
            else if (token[index].ClassPart.Equals("PM") || token[index].ClassPart.Equals("RO") || token[index].ClassPart.Equals("AND") || token[index].ClassPart.Equals("OR") ||
                    token[index].ClassPart.Equals(")") || token[index].ClassPart.Equals("\\r") || token[index].ClassPart.Equals("]") || token[index].ClassPart.Equals("}") ||
                    token[index].ClassPart.Equals("=") || token[index].ClassPart.Equals(":") || token[index].ClassPart.Equals(","))
            {
                Console.WriteLine($"T1 NULL {token[index].ClassPart}");
                return true;
            }

            return false;
        }


        bool Z()
        {
            if (token[index].ClassPart.Equals("."))
            {
                index++;
                if (token[index].ClassPart.Equals("ID"))
                {
                    index++;
                    Console.WriteLine("z .ID");
                    if (Z())
                    {
                        return true;
                    }
                }
            }

            else if (token[index].ClassPart.Equals("("))
            {
                index++;
                if (PL_PR())
                {
                    if (token[index].ClassPart.Equals(")"))
                    {
                        index++;
                        if (S())
                        {
                            if (Z())
                                return true;
                        }
                    }
                }

            }
            else if (token[index].ClassPart.Equals("["))
            {
                index++;
                if (INDEX())
                {
                    if (token[index].ClassPart.Equals("]"))
                    {
                        index++;
                        if (S())
                        {
                            if (Z())
                                return true;
                        }
                    }
                }
            }

            else if (token[index].ClassPart.Equals("inc_dec"))
            {
                index++;
                return true;
            }

            else if (token[index].ClassPart.Equals("MDM") || token[index].ClassPart.Equals("PM") || token[index].ClassPart.Equals("RO") ||
               token[index].ClassPart.Equals("AND") || token[index].ClassPart.Equals("OR") || token[index].ClassPart.Equals(")") || token[index].ClassPart.Equals("\\r") ||
               token[index].ClassPart.Equals("]") || token[index].ClassPart.Equals("}") || token[index].ClassPart.Equals("=") || token[index].ClassPart.Equals(":") ||
               token[index].ClassPart.Equals(",") || token[index].ClassPart.Equals("("))
            {
                Console.WriteLine($"Z NULL {token[index].ClassPart}");
                return true;
            }

            return false;
        }

        public bool F()
        {
            if (token[index].ClassPart.Equals("ID"))
            {
                Console.WriteLine("inside F id");
                index++;
                if (Z())
                    return true;
            }
            else if (token[index].ClassPart.Equals("this"))
            {
                index++;
                if (token[index].ClassPart.Equals("."))
                {
                    index++;
                    if (token[index].ClassPart.Equals("ID"))
                    {
                        index++;
                        if (Z())
                            return true;
                    }

                }
            }
            else if (token[index].ClassPart.Equals("int-const") || token[index].ClassPart.Equals("char-const") || token[index].ClassPart.Equals("bool-const") ||
                token[index].ClassPart.Equals("string-const") || token[index].ClassPart.Equals("float-const"))
            {
                if (CONST())
                    return true;
            }
            else if (token[index].ClassPart.Equals("("))
            {
                Console.WriteLine($"inside f {token[index].ClassPart}");
                index++;
                if (OE())
                {
                    Console.WriteLine($"oE CROSS {token[index].ClassPart}");
                    if (token[index].ClassPart.Equals(")"))
                    {
                        index++;
                        return true;
                    }

                }
            }
            else if (token[index].ClassPart.Equals("inc_dec"))
            {
                index++;
                if (token[index].ClassPart.Equals("ID"))
                {
                    index++;
                    if (X())
                        return true;
                }
            }
            else if (token[index].ClassPart.Equals("!"))
            {
                index++;
                if (F())
                    return true;
            }
            else if (token[index].ClassPart.Equals("new"))
            {
                index++;
                if (token[index].ClassPart.Equals("ID"))
                {
                    index++;
                    if (token[index].ClassPart.Equals("("))
                    {
                        index++;
                        if (PL_PR())
                        {
                            if (token[index].ClassPart.Equals(")"))
                            {
                                index++;
                                return true;
                            }
                        }

                    }
                }
            }

            return false;
        }

        /*public bool F1()
        {
            if (token[index].ClassPart.Equals(".") || token[index].ClassPart.Equals("(") || token[index].ClassPart.Equals("[") || token[index].ClassPart.Equals("MDM") ||
                token[index].ClassPart.Equals("PM") || token[index].ClassPart.Equals("RO") || token[index].ClassPart.Equals("AND") || token[index].ClassPart.Equals("OR") ||
                token[index].ClassPart.Equals(")") || token[index].ClassPart.Equals("\\r") || token[index].ClassPart.Equals("]") || token[index].ClassPart.Equals("}") ||
                token[index].ClassPart.Equals("=") || token[index].ClassPart.Equals(":") || token[index].ClassPart.Equals(","))
            {
                if (X())
                    return true;
            }
            else if (token[index].ClassPart.Equals("inc_dec"))
            {
                index++;
                return true;
            }

            return false;
        }
        */
        public bool ASGN_ST()
        {
            if (token[index].ClassPart.Equals("=") || token[index].ClassPart.Equals("CO"))
            {
                if (ASGN_OPT())
                {
                    if (OE())
                    {
                        if (token[index].ClassPart.Equals("\\r"))
                        {
                            index++;
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public bool ASGN_OPT()
        {
            if (token[index].ClassPart.Equals("=") || token[index].ClassPart.Equals("CO"))
            {
                index++;
                return true;
            }

            return false;
        }

        public bool X()
        {
            if (token[index].ClassPart.Equals("."))
            {
                index++;
                if (token[index].ClassPart.Equals("ID"))
                {
                    index++;
                    if (X())
                        return true;
                }
            }
            else if (token[index].ClassPart.Equals("("))
            {
                index++;
                if (PL_PR())
                {
                    if (token[index].ClassPart.Equals(")"))
                    {
                        index++;
                        if (token[index].ClassPart.Equals("."))
                        {
                            index++;
                            if (token[index].ClassPart.Equals("ID"))
                            {
                                index++;
                                if (X())
                                    return true;
                            }

                        }
                    }
                }

            }
            else if (token[index].ClassPart.Equals("["))
            {
                index++;
                if (INDEX())
                {
                    if (token[index].ClassPart.Equals("]"))
                    {
                        index++;
                        if (S())
                            return true;
                    }
                }
            }
            else if (token[index].ClassPart.Equals("MDM") || token[index].ClassPart.Equals("inc_dec") || token[index].ClassPart.Equals("PM") || token[index].ClassPart.Equals("RO") ||
               token[index].ClassPart.Equals("AND") || token[index].ClassPart.Equals("OR") || token[index].ClassPart.Equals(")") || token[index].ClassPart.Equals("\\r") ||
               token[index].ClassPart.Equals("]") || token[index].ClassPart.Equals("}") || token[index].ClassPart.Equals("=") || token[index].ClassPart.Equals(":") ||
               token[index].ClassPart.Equals(",") || token[index].ClassPart.Equals("(") || token[index].ClassPart.Equals("CO"))
            {
                Console.WriteLine("X NULL");
                return true;
            }
            /*else if(token[index].ClassPart.Equals(".") || token[index].ClassPart.Equals("MDM") || token[index].ClassPart.Equals("inc_dec") ||
                 token[index].ClassPart.Equals("PM") || token[index].ClassPart.Equals("RO") || token[index].ClassPart.Equals("AND") || token[index].ClassPart.Equals("OR") ||
                 token[index].ClassPart.Equals(")") || token[index].ClassPart.Equals("\\r") || token[index].ClassPart.Equals("]") || token[index].ClassPart.Equals("}") ||
                 token[index].ClassPart.Equals("=") || token[index].ClassPart.Equals(":") || token[index].ClassPart.Equals(",") || token[index].ClassPart.Equals("abstract") ||
                 token[index].ClassPart.Equals("const") || token[index].ClassPart.Equals("static") || token[index].ClassPart.Equals("final") || token[index].ClassPart.Equals("virtual") 
                 || token[index].ClassPart.Equals("override") || token[index].ClassPart.Equals("void") || token[index].ClassPart.Equals("DT") ||token[index].ClassPart.Equals("ID") ||
                  token[index].ClassPart.Equals("this") || token[index].ClassPart.Equals("if") || token[index].ClassPart.Equals("while") || token[index].ClassPart.Equals("foreach")||
                   token[index].ClassPart.Equals("switch") || token[index].ClassPart.Equals("label") || token[index].ClassPart.Equals("goto") || token[index].ClassPart.Equals("end") ||
                    token[index].ClassPart.Equals("return") || token[index].ClassPart.Equals("else") || token[index].ClassPart.Equals("break") || token[index].ClassPart.Equals("continue"))
             {
                 index++;
                 return true;
             }*/

            return false;
        }

        public bool S()
        {
            if (token[index].ClassPart.Equals("."))
            {
                index++;
                if (token[index].ClassPart.Equals("ID"))
                {
                    index++;
                    return true;
                }
            }
            else if (token[index].ClassPart.Equals("MDM") || token[index].ClassPart.Equals("inc_dec") || token[index].ClassPart.Equals("PM") || token[index].ClassPart.Equals("RO") ||
                token[index].ClassPart.Equals("AND") || token[index].ClassPart.Equals("OR") || token[index].ClassPart.Equals(")") || token[index].ClassPart.Equals("\\r") ||
                token[index].ClassPart.Equals("]") || token[index].ClassPart.Equals("}") || token[index].ClassPart.Equals("=") || token[index].ClassPart.Equals(":") ||
                token[index].ClassPart.Equals(",") || token[index].ClassPart.Equals("(") || token[index].ClassPart.Equals("CO"))
            {
                return true;
            }

            return false;
        }

        public bool THIS_ST()
        {
            if (token[index].ClassPart.Equals("this"))
            {
                index++;
                if (token[index].ClassPart.Equals("."))
                {
                    index++;
                    if (token[index].ClassPart.Equals("ID"))
                    {
                        index++;
                        Console.WriteLine($"inside This {token[index].ClassPart}");

                        if (X())
                        {
                            if (ASGN_ST())
                                return true;
                        }
                    }
                }

            }
            if (token[index].ClassPart.Equals("ID"))
            {

                return true;
            }

            return false;
        }

        public bool AM()
        {
            if (token[index].ClassPart.Equals("public"))
            {
                index++;
                return true;
            }
            if (token[index].ClassPart.Equals("private"))
            {
                index++;
                return true;
            }
            if (token[index].ClassPart.Equals("protected"))
            {
                index++;
                return true;
            }
            if (token[index].ClassPart.Equals("abstract") || token[index].ClassPart.Equals("const") || token[index].ClassPart.Equals("static") || token[index].ClassPart.Equals("final") ||
                    token[index].ClassPart.Equals("virtual") || token[index].ClassPart.Equals("override") || token[index].ClassPart.Equals("void") || token[index].ClassPart.Equals("DT") ||
                    token[index].ClassPart.Equals("ID"))
            {

                return true;
            }

            return false;
        }

        public bool TM3()
        {
            if (token[index].ClassPart.Equals("static"))
            {
                index++;
                return true;
            }
            else if (token[index].ClassPart.Equals("final"))
            {
                index++;
                return true;
            }
            
            if (token[index].ClassPart.Equals("virtual") || token[index].ClassPart.Equals("override") || token[index].ClassPart.Equals("void") ||
                    token[index].ClassPart.Equals("DT") || token[index].ClassPart.Equals("this") || token[index].ClassPart.Equals("ID"))
            {
                return true;
            }

            return false;
        }

        public bool TM2(out string cat)
        {
            if (token[index].ClassPart.Equals("abstract"))
            {
                index++;
                cat = "abstract";
                return true;
            }
            if (token[index].ClassPart.Equals("final"))
            {
                index++;
                cat = "final";
                return true;
            }
            if (token[index].ClassPart.Equals("class"))
            {
                cat = "general";
                return true;
            }
            cat = null;
            return false;
        }




        //FUNCTION DECLARATION
        public bool FN()
        {
            
            if (token[index].ClassPart == "ID")
            {
                index++;
                if (FN_DEC())
                    return true;
            }
            return false;
        }

        public bool FN_DEC()
        {
            if (token[index].ClassPart == "(")
            {
                index++;
                if (PL())
                {
                    if (token[index].ClassPart == ")")
                    {
                        index++;
                        if (token[index].ClassPart == ":")
                        {
                            index++;
                            if (token[index].ClassPart == "\\r")
                            {
                                index++;
                                if (MST())
                                {
                                    Console.WriteLine("FnDEC mST TRUE");
                                    if (RETURN())
                                    {
                                        Console.WriteLine("FNDEC reTURN TRUE");
                                        if (token[index].ClassPart == "end")
                                        {
                                            index++;
                                            if (token[index].ClassPart == "\\r")
                                            {
                                                index++;
                                                return true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
            return false;
        }

        public bool RETURN()
        {
            if (token[index].ClassPart == "return")
            {
                index++;
                if (OE())
                {
                    if (token[index].ClassPart == "\\r")
                    {
                        index++;
                        return true;
                    }
                }

            }
            else
            {
                if (token[index].ClassPart == "end")
                {
                    return true;
                }

            }
            return false;
        }

        public bool FN_MOD()
        {
            if (token[index].ClassPart == "virtual")
            {
                index++;
                return true;
            }
            else
            {
                if (token[index].ClassPart == "override")
                {
                    index++;
                    return true;
                }
            }

            return false;
        }

        // ARRAY DECLARATION
        bool ARR_DEC()
        {
            if (token[index].ClassPart == "[")
            {
                index++;
                if (DIMENSION())
                {
                    if (token[index].ClassPart == "]")
                    {
                        index++;
                        if (token[index].ClassPart == "ID")
                        {
                            index++;
                            if (ARR_DEC1())
                            {
                                return true;
                            }

                        }
                    }
                }

            }
            //else
            //{
            //    if (token[index].ClassPart == "ID")
            //    {
            //        index++;
            //        if (ARR_DEC1())
            //            return true;
            //    }
            //}
            return false;
        }

        public bool ARR_DEC1()
        {

            if (token[index].ClassPart == "=")
            {
                index++;
                if (token[index].ClassPart == "new")
                {
                    index++;
                    if (ARR_TYPE())
                    {
                        if (token[index].ClassPart == "[")
                        {
                            index++;
                            if (LENGTH())
                            {
                                Console.WriteLine($"Arrdec1 length defined {token[index].ClassPart}");
                                if (token[index].ClassPart == "]")
                                {
                                    index++;
                                    if (AR_INIT())
                                        if (L3())
                                            return true;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (L3())
                    return true;
            }
            return false;
        }

        bool L3()
        {
            Console.WriteLine($"inside L3 {token[index].ClassPart}");

            if (token[index].ClassPart == ",")
            {
                index++;
                if (token[index].ClassPart.Equals("ID"))
                {
                    index++;
                    if (L31())
                    {
                        return true;
                    }
                }
                //if (ARR_DEC())
                //    return true;
            }
            else
            {
                if (token[index].ClassPart == "\\r")
                {
                    index++;
                    return true;
                }
            }
            return false;
        }

        public bool L31()
        {
            if (token[index].ClassPart.Equals("=") || token[index].ClassPart.Equals(",") || token[index].ClassPart.Equals("\\r"))
            {
                if (ARR_DEC1())
                    return true;
            }
            else
            {
                if (token[index].ClassPart.Equals(",") || token[index].ClassPart.Equals("\\r"))
                {
                    if (L3())
                        return true;
                }
            }

            return false;
        }

        bool AR_INIT()
        {

            if (token[index].ClassPart == "\\r" || token[index].ClassPart == ",")
            {
                //index++;
                Console.WriteLine($"array init \\r condition {token[index].ClassPart}");
                return true;
            }
            else
            {
                if (token[index].ClassPart == "{")
                {
                    Console.WriteLine($"array init open curly condition {token[index].ClassPart}");
                    index++;
                    if (AR_DATA())
                    {
                        Console.WriteLine("AR DATA PARSED *********************************************");
                        if (token[index].ClassPart == "}")
                        {
                            index++;
                            return true;
                            //if (token[index].ClassPart == "\\r")
                            //{
                            //    index++;
                            //    return true;
                            //}
                        }
                    }

                }
            }
            return false;
        }

        bool ARR_TYPE()
        {

            if (token[index].ClassPart == "DT" || token[index].ClassPart == "ID")
            {
                index++;
                return true;
            }
            return false;
        }

        bool DIMENSION()
        {
            if (token[index].ClassPart == ",")
            {
                index++;
                if (DIMENSION())
                    return true;
            }
            else
            {
                if (token[index].ClassPart == "]")
                    return true;
            }
            return false;
        }

        bool LENGTH()
        {
            if (token[index].ClassPart == "ID" || token[index].ClassPart == "this" || token[index].ClassPart == "!" || token[index].ClassPart == "new" || token[index].ClassPart == "inc_dec" ||
               token[index].ClassPart == "(" || token[index].ClassPart == "int-const" || token[index].ClassPart == "float-const" || token[index].ClassPart == "char-const" ||
               token[index].ClassPart == "string-const" || token[index].ClassPart == "bool-const")
            {
                if (OE())
                    if (FUR_LEN())
                        return true;
            }
            else
            {
                if (token[index].ClassPart == "]")
                    return true;
            }
            return false;
        }

        bool FUR_LEN()
        {
            if (token[index].ClassPart == ",")
            {
                index++;
                if (LENGTH())
                    return true;
            }
            else
            {
                if (token[index].ClassPart == "]")
                    return true;
            }
            return false;
        }

        bool AR_DATA()
        {
            if (token[index].ClassPart == "ID" || token[index].ClassPart == "new" || token[index].ClassPart == "this" || token[index].ClassPart == "!" || token[index].ClassPart == "(" ||
                token[index].ClassPart == "inc_dec" || token[index].ClassPart == "int-const" || token[index].ClassPart == "float-const" || token[index].ClassPart == "char-const" ||
                token[index].ClassPart == "string-const" || token[index].ClassPart == "bool-const")
            {
                if (OE())
                    if (oneD())
                        return true;
            }
            else
            {
                if (token[index].ClassPart == "{")
                {
                    Console.WriteLine($"array data MD condition {token[index].ClassPart}");

                    if (MD())
                        return true;
                }

            }
            return false;
        }

        bool oneD()
        {
            if (token[index].ClassPart == ",")
            {
                Console.WriteLine($"One D first condition {token[index].ClassPart}");

                index++;
                if (OE())
                {
                    if (oneD())
                        return true;
                }
            }
            else
            {
                if (token[index].ClassPart == "=" || token[index].ClassPart == "}")
                    return true;
            }
            return false;
        }

        /*bool MD()
        {
            if (token[index].ClassPart == "{")
            {
                index++;
                if (OE())
                {
                    Console.WriteLine("1param*****************************");
                    if (token[index].ClassPart == ",")
                    {
                        index++;
                        Console.WriteLine($"MD after first OE {token[index].ClassPart}");
                        if (OE())
                            if (oneD())
                                if (token[index].ClassPart == "}")
                                {
                                    Console.WriteLine($"MD first }} close  {token[index].ClassPart}");

                                    index++;
                                    if (FUR_MD())
                                        return true;
                                }
                    }
                }
            }
            return false;
        }*/


        //MULTI DIMENSIONAL
        bool MD()
        {
            if (token[index].ClassPart == "{")
            {
                index++;
                if (MD())
                    if (token[index].ClassPart == "}")
                    {
                        index++;
                        if (MD())
                            return true;
                    }

            }
            if (token[index].ClassPart == ",")
            {
                index++;
                if (token[index].ClassPart == "{")
                {
                    index++;
                    if (MD())
                        if (token[index].ClassPart == "}")
                        {
                            index++;
                            if (MD())
                                return true;
                        }

                }
            }
            if (token[index].ClassPart == "ID" || token[index].ClassPart == "this" || token[index].ClassPart == "!" || token[index].ClassPart == "new" || token[index].ClassPart == "inc_dec" ||
                token[index].ClassPart == "(" || token[index].ClassPart == "int-const" || token[index].ClassPart == "float-const" || token[index].ClassPart == "char-const" ||
                token[index].ClassPart == "string-const" || token[index].ClassPart == "bool-const")
            {
                if (OE())
                    if (oneD())
                        return true;
            }
            if (token[index].ClassPart == "}")
            {
                return true;
            }
            return false;
        }

        bool FUR_MD()
        {
            if (token[index].ClassPart == ",")
            {
                index++;
                if (MD())
                    return true;
            }
            else
            {
                if (token[index].ClassPart == "}" || token[index].ClassPart == "=")
                    return true;
            }
            return false;
        }

        bool INDEX()
        {
            if (token[index].ClassPart == "ID" || token[index].ClassPart == "new" || token[index].ClassPart == "this" || token[index].ClassPart == "!" || token[index].ClassPart == "(" ||
                token[index].ClassPart == "inc_dec" || token[index].ClassPart == "int-const" || token[index].ClassPart == "float-const" || token[index].ClassPart == "char-const" ||
                token[index].ClassPart == "string-const" || token[index].ClassPart == "bool-const")
            {
                if (OE())
                {
                    if (FUR_INX())
                        return true;
                }
            }
            return false;
        }

        bool FUR_INX()
        {
            if (token[index].ClassPart == ",")
            {
                index++;
                if (INDEX())
                    return true;
            }
            else
            {
                if (token[index].ClassPart == "]")
                    return true;
            }
            return false;
        }

        // DECLARATION

        bool DEC()
        {
            Console.WriteLine($"DEC {token[index].ClassPart} {token[index].ValuePart}");
            if (token[index].ClassPart == "=")
            {
                index++;
                if (DEC_INIT())
                    return true;
            }
            else
            {
                if (token[index].ClassPart == "\\r" || token[index].ClassPart == ",")
                {
                    if (FUR_INIT())
                        return true;
                }
            }
            return false;
        }

        bool DEC_INIT()
        {
            int backTracking = index;
            if (token[index].ClassPart == "ID")
            {
                index++;
                if (Z())
                {
                    Console.WriteLine($"Z {token[index].ClassPart}");
                    if (DEC())
                        return true;
                    else
                    {
                        Console.WriteLine("DEC_INIT before backTRack");
                        index = backTracking;
                        if (token[index].ClassPart == "ID" || token[index].ClassPart == "new" || token[index].ClassPart == "this" || token[index].ClassPart == "!" || token[index].ClassPart == "(" ||
                        token[index].ClassPart == "inc_dec" || token[index].ClassPart == "int-const" || token[index].ClassPart == "float-const" || token[index].ClassPart == "char-const" ||
                        token[index].ClassPart == "string-const" || token[index].ClassPart == "bool-const")
                        {
                            if (OE())
                                if (FUR_INIT())
                                    return true;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("DEC_INIT");
                Console.WriteLine($"{token[index].ClassPart}");
                index = backTracking;
                if (token[index].ClassPart == "ID" || token[index].ClassPart == "new" || token[index].ClassPart == "this" || token[index].ClassPart == "!" || token[index].ClassPart == "(" ||
                token[index].ClassPart == "inc_dec" || token[index].ClassPart == "int-const" || token[index].ClassPart == "float-const" || token[index].ClassPart == "char-const" ||
                token[index].ClassPart == "string-const" || token[index].ClassPart == "bool-const")
                {
                    if (OE())
                        if (FUR_INIT())
                            return true;
                }
            }
            return false;
        }

        //bool DEC_INIT()
        //{
        //    int backTracking = index;
        //    if (token[index].ClassPart == "ID")
        //    {
        //        index++;
        //        if (Z())
        //        {
        //            if (DEC())
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                index = backTracking;
        //                if (token[index].ClassPart == "ID" || token[index].ClassPart == "new" || token[index].ClassPart == "this" || token[index].ClassPart == "!" || token[index].ClassPart == "(" ||
        //                token[index].ClassPart == "inc_dec" || token[index].ClassPart == "int-const" || token[index].ClassPart == "float-const" || token[index].ClassPart == "char-const" ||
        //                token[index].ClassPart == "string-const" || token[index].ClassPart == "bool-const")
        //                {
        //                    if (OE())
        //                    {
        //                        if (FUR_INIT())
        //                            return true;
        //                    }

        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("DEC_INIT");
        //        Console.WriteLine(token[index].ClassPart);
        //        index = backTracking;
        //        if (token[index].ClassPart == "ID" || token[index].ClassPart == "new" || token[index].ClassPart == "this" || token[index].ClassPart == "!" || token[index].ClassPart == "(" ||
        //        token[index].ClassPart == "inc_dec" || token[index].ClassPart == "int-const" || token[index].ClassPart == "float-const" || token[index].ClassPart == "char-const" ||
        //        token[index].ClassPart == "string-const" || token[index].ClassPart == "bool-const")
        //        {
        //            if (OE())
        //                if (FUR_INIT())
        //                    return true;
        //        }
        //    }
        //    return false;
        //}

        bool FUR_INIT()
        {
            if (token[index].ClassPart == "\\r")
            {
                index++;
                return true;
            }
            else
            {
                if (token[index].ClassPart == ",")
                {
                    index++;
                    if (token[index].ClassPart == "ID")
                    {
                        index++;
                        if (DEC())
                        {
                            //if (FUR_INIT())
                            return true;
                        }
                    }
                }
            }
            return false;

        }

        //CONSTANTS
        bool CONST()
        {
            if (token[index].ClassPart == "int-const" || token[index].ClassPart == "float-const" || token[index].ClassPart == "char-const" ||
                token[index].ClassPart == "string-const" || token[index].ClassPart == "bool-const")
            {
                index++;
                return true;
            }
            return false;
        }

        //VAR DECLARATION
        bool VAR_DEC()
        {
            Console.WriteLine("var author ID =");
            if (token[index].ClassPart == "var")
            {
                index++;
                if (token[index].ClassPart == "ID")
                {
                    index++;
                    if (token[index].ClassPart == "=")
                    {
                        index++;
                        if (VAR_DEC_0())
                            if (token[index].ClassPart == "\\r")
                            {
                                index++;
                                return true;
                            }
                    }
                }
            }
            return false;
        }
        bool VAR_DEC_0()
        {
            Console.WriteLine($"VAR_DEC_0 {token[index].ClassPart}");
            if (token[index].ClassPart == "ID" || token[index].ClassPart == "this" || token[index].ClassPart == "!" || token[index].ClassPart == "(" ||
                token[index].ClassPart == "inc_dec" || token[index].ClassPart == "int-const" || token[index].ClassPart == "float-const" || token[index].ClassPart == "char-const" ||
                token[index].ClassPart == "string-const" || token[index].ClassPart == "bool-const")
            {
                if (OE())
                    return true;
            }
            else
            {
                if (token[index].ClassPart == "new")
                {
                    index++;
                    if (VAR_DEC_1())
                        return true;
                }
            }
            return false;
        }

        bool VAR_DEC_1()
        {
            Console.WriteLine($"VAR_DEC_1 {token[index].ClassPart}");
            if (token[index].ClassPart == "ID")
            {
                index++;
                if (VAR_DEC_2())
                    return true;
            }
            else if (token[index].ClassPart == "DT")
            {
                index++;
                if (VAR_DEC_2())
                    return true;

            }
            else
            {
                if (token[index].ClassPart == "[" || token[index].ClassPart == "(")
                {
                    if (VAR_DEC_2())
                        return true;
                }
                else
                {
                    if (token[index].ClassPart == "{")
                    {
                        if (VAR_INIT())
                            return true;
                    }
                }
            }
            return false;
        }

        bool VAR_DEC_2()
        {
            if (token[index].ClassPart == "[")
            {
                index++;
                if (DIMENSION())
                    if (token[index].ClassPart == "]")
                    {
                        index++;
                        if (VAR_INIT())
                            return true;
                    }
            }
            else
            {
                if (token[index].ClassPart == "(")
                {
                    index++;
                    if (PL_PR())
                        if (token[index].ClassPart == ")")
                        {
                            index++;
                            return true;
                        }


                }
            }
            return false;
        }

        bool VAR_INIT()
        {
            Console.WriteLine($"VAR_INIT {token[index].ClassPart}");
            if (token[index].ClassPart == "{")
            {
                index++;
                if (VAR_INIT_1())
                    if (token[index].ClassPart == "}")
                    {
                        index++;
                        return true;
                    }
            }
            return false;
        }

        bool VAR_INIT_1()
        {
            Console.WriteLine($"VAR_INIT_1 {token[index].ClassPart}");
            if (token[index].ClassPart == "ID" || token[index].ClassPart == "this" || token[index].ClassPart == "!" || token[index].ClassPart == "(" || token[index].ClassPart == "new" ||
                token[index].ClassPart == "inc_dec" || token[index].ClassPart == "int-const" || token[index].ClassPart == "float-const" || token[index].ClassPart == "char-const" ||
                token[index].ClassPart == "string-const" || token[index].ClassPart == "bool-const" || token[index].ClassPart == "{")
            {
                if (AR_DATA())
                    if (ANYM_INIT())
                        return true;
            }
            return false;
        }

        bool ANYM_INIT()
        {
            Console.WriteLine($"ANYM_INIT {token[index].ClassPart}");
            if (token[index].ClassPart == "=")
            {
                index++;
                if (OE())
                    if (ANYM_INIT_2())
                        return true;
            }
            else
            {
                if (token[index].ClassPart == "}")
                {
                    return true;
                }

            }
            Console.WriteLine("anym_INIT RETURNING FALSE");
            return false;
        }

        bool ANYM_INIT_2()
        {
            Console.WriteLine($"ANYM_INIT_2 {token[index].ClassPart}");
            if (token[index].ClassPart == ",")
            {
                index++;
                if (token[index].ClassPart == "ID")
                {
                    index++;
                    if (ANYM_INIT())
                        return true;
                }
            }
            else
            {
                if (token[index].ClassPart == "}")
                {
                    return true;
                }

            }
            return false;
        }
        //bool VAR_DEC()
        //{
        //    if (token[index].ClassPart == "var")
        //    {
        //        index++;
        //        if (token[index].ClassPart == "ID")
        //        {
        //            index++;
        //            if (token[index].ClassPart == "=")
        //            {
        //                index++;
        //                if (VAR_DEC_0())
        //                    if (token[index].ClassPart == "\\r")
        //                    {
        //                        index++;
        //                        return true;
        //                    }
        //            }
        //        }
        //    }
        //    return false;
        //}
        //bool VAR_DEC_0()
        //{
        //    if (token[index].ClassPart == "ID" || token[index].ClassPart == "this" || token[index].ClassPart == "!" || token[index].ClassPart == "(" ||
        //        token[index].ClassPart == "inc_dec" || token[index].ClassPart == "int-const" || token[index].ClassPart == "float-const" || token[index].ClassPart == "char-const" ||
        //        token[index].ClassPart == "string-const" || token[index].ClassPart == "bool-const")
        //    {
        //        if (OE())
        //            return true;
        //    }
        //    else
        //    {
        //        if (token[index].ClassPart == "new")
        //        {
        //            index++;
        //            if (VAR_DEC_1())
        //                return true;
        //        }
        //    }
        //    return false;
        //}
        ////bool VAR_DEC_0()
        ////{
        ////    if (token[index].ClassPart == "ID" || token[index].ClassPart == "this" || token[index].ClassPart == "!" || token[index].ClassPart == "(" ||
        ////        token[index].ClassPart == "inc_dec" || token[index].ClassPart == "int-const" || token[index].ClassPart == "float-const" || token[index].ClassPart == "char-const" ||
        ////        token[index].ClassPart == "string-const" || token[index].ClassPart == "bool-const")
        ////    {
        ////        if (OE())
        ////            return true;
        ////    }
        ////    else
        ////    {
        ////        if (token[index].ClassPart == "new")
        ////        {
        ////            index++;
        ////            if (VAR_DEC_1())
        ////                return true;
        ////        }
        ////    }
        ////    return false;
        ////}

        //bool VAR_DEC_1()
        //{
        //    if (token[index].ClassPart == "ID")
        //    {
        //        index++;
        //        if (VAR_DEC_2())
        //            return true;
        //    }
        //    else if (token[index].ClassPart == "DT")
        //    {
        //        index++;
        //        if (VAR_DEC_2())
        //            return true;

        //    }
        //    else
        //    {
        //        if (token[index].ClassPart == "[" || token[index].ClassPart == "(")
        //        {
        //            if (VAR_DEC_2())
        //                return true;
        //        }
        //    }
        //    return false;
        //}

        //bool VAR_DEC_2()
        //{
        //    if (token[index].ClassPart == "[")
        //    {
        //        index++;
        //        if (DIMENSION())
        //            if (token[index].ClassPart == "]")
        //            {
        //                index++;
        //                if (VAR_INIT())
        //                    return true;
        //            }
        //    }
        //    else
        //    {
        //        if (token[index].ClassPart == "(")
        //        {
        //            index++;
        //            if (PL_PR())
        //                if (token[index].ClassPart == ")")
        //                {
        //                    index++;
        //                    return true;
        //                }


        //        }
        //    }
        //    return false;
        //}

        //bool VAR_INIT()
        //{
        //    if (token[index].ClassPart == "{")
        //    {
        //        index++;
        //        if (VAR_INIT_1())
        //            if (token[index].ClassPart == "}")
        //            {
        //                index++;
        //                return true;
        //            }
        //    }
        //    return false;
        //}

        //bool VAR_INIT_1()
        //{
        //    if (token[index].ClassPart == "ID" || token[index].ClassPart == "this" || token[index].ClassPart == "!" || token[index].ClassPart == "(" ||
        //        token[index].ClassPart == "inc_dec" || token[index].ClassPart == "int-const" || token[index].ClassPart == "float-const" || token[index].ClassPart == "char-const" ||
        //        token[index].ClassPart == "string-const" || token[index].ClassPart == "bool-const" || token[index].ClassPart == "{")
        //    {
        //        if (AR_DATA())
        //            if (ANYM_INIT())
        //                return true;
        //    }
        //    return false;
        //}

        //bool ANYM_INIT()
        //{
        //    if (token[index].ClassPart == "=")
        //    {
        //        index++;
        //        if (OE())
        //            if (ANYM_INIT_2())
        //                return true;
        //    }
        //    else
        //    {
        //        if (token[index].ClassPart == "}")
        //            return true;
        //    }
        //    return false;
        //}

        //bool ANYM_INIT_2()
        //{
        //    if (token[index].ClassPart == ",")
        //    {
        //        index++;
        //        if (ANYM_INIT())
        //            return true;
        //    }
        //    else
        //    {
        //        if (token[index].ClassPart == "}")
        //            return true;
        //    }
        //    return false;
        //}


        //CONSTANT DECLARATION

        bool CONST_DEC()
        {
            if (token[index].ClassPart == "const")
            {
                index++;
                if (token[index].ClassPart == "DT")
                {
                    index++;
                    if (token[index].ClassPart == "ID")
                    {
                        index++;
                        if (token[index].ClassPart == "=")
                        {
                            index++;
                            if (CONST_LIST())
                            {
                                if (token[index].ClassPart == "\\r")
                                {
                                    index++;
                                    return true;
                                }
                            }

                        }
                    }
                }

            }
            return false;
        }

        bool CONST_LIST()
        {
            if (token[index].ClassPart == "int-const" || token[index].ClassPart == "float-const" || token[index].ClassPart == "char-const" ||
                token[index].ClassPart == "string-const" || token[index].ClassPart == "bool-const")
            {
                index++;
                return true;
            }
            else
            {
                if (token[index].ClassPart == ",")
                {
                    index++;
                    if (token[index].ClassPart == "ID")
                    {
                        index++;
                        if (token[index].ClassPart == "=")
                        {
                            index++;
                            if (CONST_LIST())
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        // IF ELSE STATEMENT;
        bool IFELSE()
        {
            if (token[index].ClassPart == "if")
            {
                index++;
                if (token[index].ClassPart == "(")
                {
                    index++;
                    if (OE())
                    {
                        Console.WriteLine($"If cONDITION CROSS {token[index].ClassPart}");
                        if (token[index].ClassPart == ")")
                        {
                            index++;
                            if (token[index].ClassPart == ":")
                            {
                                index++;
                                if (token[index].ClassPart == "\\r")
                                {
                                    index++;
                                    Console.WriteLine($"if before Mst {token[index].ClassPart}");
                                    if (MST())
                                        if (STOP())
                                            if (OELSE())
                                                if (token[index].ClassPart == "end")
                                                {
                                                    index++;
                                                    if (token[index].ClassPart.Equals("\\r"))
                                                    {
                                                        index++;
                                                        return true;
                                                    }

                                                }
                                }
                            }
                        }
                    }

                }

            }
            return false;
        }

        bool OELSE()
        {
            if (token[index].ClassPart == "else")
            {
                index++;
                if (token[index].ClassPart == ":")
                {
                    index++;
                    if (token[index].ClassPart == "\\r")
                    {
                        index++;
                        if (MST())
                            if (STOP())
                                return true;
                    }
                }
            }
            else
            {
                if (token[index].ClassPart == "end")
                {
                    return true;
                }
            }
            return false;
        }

        //FOREACH STATEMENT
        bool FOREACH()
        {
            if (token[index].ClassPart == "foreach")
            {
                index++;
                if (token[index].ClassPart == "(")
                {
                    index++;
                    if (LOOP_DEC())
                        if (token[index].ClassPart == "ID")
                        {
                            index++;
                            if (token[index].ClassPart == "in")
                            {
                                index++;
                                if (token[index].ClassPart == "ID")
                                {
                                    index++;
                                    if (token[index].ClassPart == ")")
                                    {
                                        index++;
                                        if (token[index].ClassPart == ":")
                                        {
                                            index++;
                                            if (token[index].ClassPart == "\\r")
                                            {
                                                index++;
                                                if (MST())
                                                    if (STOP())
                                                        if (token[index].ClassPart == "end")
                                                        {
                                                            index++;
                                                            if (token[index].ClassPart == "\\r")
                                                            {
                                                                index++;
                                                                return true;
                                                            }
                                                        }
                                            }

                                        }
                                    }
                                }
                            }
                        }
                }
            }
            return false;
        }

        bool LOOP_DEC()
        {
            if (token[index].ClassPart == "DT")
            {
                index++;
                return true;
            }
            else
            {
                if (token[index].ClassPart == "var")
                {
                    index++;
                    return true;
                }
            }
            return false;
        }


        //WHILE STATEMENT
        bool WHILE_ST()
        {
            if (token[index].ClassPart == "while")
            {
                index++;
                if (token[index].ClassPart == "(")
                {
                    index++;
                    if (OE())
                    {
                        if (token[index].ClassPart == ")")
                        {
                            index++;
                            if (token[index].ClassPart == ":")
                            {
                                index++;
                                if (token[index].ClassPart == "\\r")
                                {
                                    index++;
                                    if (MST())
                                        if (STOP())
                                            if (token[index].ClassPart == "end")
                                            {
                                                index++;
                                                if (token[index].ClassPart == "\\r")
                                                {
                                                    index++;
                                                    return true;
                                                }
                                            }
                                }
                            }
                        }
                    }

                }
            }
            return false;
        }


        // SWITCH CASE STATEMENT
        bool SWITCH()
        {
            if (token[index].ClassPart == "switch")
            {
                index++;
                if (token[index].ClassPart == "(")
                {
                    index++;
                    if (OE())
                        if (token[index].ClassPart == ")")
                        {
                            index++;
                            if (token[index].ClassPart == ":")
                            {
                                index++;
                                if (token[index].ClassPart == "\\r")
                                {
                                    index++;
                                    if (SW_BODY())
                                        if (token[index].ClassPart == "end")
                                        {
                                            index++;
                                            if (token[index].ClassPart == "\\r")
                                            {
                                                index++;
                                                return true;
                                            }
                                        }
                                }
                            }
                        }
                }
            }
            return false;
        }

        bool SW_BODY()
        {
            if (token[index].ClassPart == "case" || token[index].ClassPart == "default")
            {
                if (CASE_ST())
                    if (DFLT_ST())
                        return true;
            }
            else
            {
                if (token[index].ClassPart == "end")
                    return true;
            }
            return false;
        }

        bool CASE_ST()
        {
            if (token[index].ClassPart == "case")
            {
                index++;
                if (OE())
                    if (token[index].ClassPart == ":")
                    {
                        index++;
                        if (token[index].ClassPart == "\\r")
                        {
                            index++;
                            if (SM_ST())
                            {
                                if (token[index].ClassPart == "end")
                                {
                                    index++;
                                    if (token[index].ClassPart.Equals("\\r"))
                                    {
                                        index++;
                                        if (CASE_ST())
                                            return true;
                                    }
                                }
                            }

                        }
                    }

            }
            else
            {
                if (token[index].ClassPart == "default")
                {
                    return true;
                }

            }
            return false;
        }

        bool DFLT_ST()
        {
            if (token[index].ClassPart == "default")
            {
                index++;
                if (token[index].ClassPart.Equals("\\r"))
                {
                    index++;
                    if (SM_ST())
                    {
                        Console.WriteLine($"&&&&&&&&&&&&&&&&&&&&&&&&&&&&& {token[index].Line}  {token[index].ClassPart}");
                        if (token[index].ClassPart == "end")
                        {
                            index++;
                            if (token[index].ClassPart == "\\r")
                            {
                                index++;
                                return true;
                            }
                        }
                    }

                }

            }
            return false;

        }

        bool SM_ST()
        {
            if (token[index].ClassPart == "if" || token[index].ClassPart == "while" || token[index].ClassPart == "foreach" || token[index].ClassPart == "switch" ||
                token[index].ClassPart == "label" || token[index].ClassPart == "goto" || token[index].ClassPart == "var" || token[index].ClassPart == "inc_dec" ||
                token[index].ClassPart == "void" || token[index].ClassPart == "DT" || token[index].ClassPart == "this" || token[index].ClassPart == "ID" ||
                token[index].ClassPart == "break" || token[index].ClassPart == "continue" || token[index].ClassPart == "return")
            {
                if (MST())
                    if (STOP())
                    {
                        return true;
                    }
            }
            return false;
        }

        bool STOP()
        {

            Console.WriteLine($"inside stop {token[index].ClassPart}");
            if (token[index].ClassPart == "break")
            {
                index++;
                if (token[index].ClassPart == "\\r")
                {
                    index++;
                    return true;
                }
            }
            else if (token[index].ClassPart == "continue")
            {
                index++;
                if (token[index].ClassPart == "\\r")
                {
                    index++;
                    return true;
                }
            }
            else if (token[index].ClassPart == "return")
            {
                index++;
                if (OE())
                {
                    if (token[index].ClassPart == "\\r")
                    {
                        index++;
                        return true;
                    }
                }

            }

            else
            {
                if (token[index].ClassPart == "end" || token[index].ClassPart == "else")
                    return true;
            }
            return false;
        }

        //LABEL DECLARATION
        bool LABEL_DEC()
        {
            if (token[index].ClassPart == "label")
            {
                index++;
                if (token[index].ClassPart == "ID")
                {
                    index++;
                    if (token[index].ClassPart == ":")
                    {
                        index++;
                        if (token[index].ClassPart == "\\r")
                        {
                            index++;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        //GOTO DECLARATION
        bool GOTO_DEC()
        {
            if (token[index].ClassPart == "goto")
            {
                index++;
                if (token[index].ClassPart == "ID")
                {
                    index++;
                    if (token[index].ClassPart == "\\r")
                    {
                        index++;
                        return true;
                    }

                }
            }
            return false;

        }

        bool print()
        {
            if (token[index].ClassPart.Equals("print"))
            {
                index++;
                if (token[index].ClassPart.Equals("("))
                {
                    index++;
                    if (token[index].ClassPart.Equals("ID"))
                    {
                        index++;
                        if (token[index].ClassPart.Equals(")"))
                        {
                            index++;
                            if (token[index].ClassPart.Equals("\\r"))
                            {
                                index++;
                                return true;
                            }
                            
                        }
                    }
                }
            }
            return false;
        }

        //SST
        bool SST()
        {
            if (token[index].ClassPart.Equals("print"))
            {
                if (print())
                    return true;
            }
                    
            if (token[index].ClassPart.Equals("if"))
            {
                if (IFELSE())
                    return true;
            }

            if (token[index].ClassPart.Equals("while"))
            {
                if (WHILE_ST())
                    return true;
            }

            if (token[index].ClassPart.Equals("foreach"))
            {
                if (FOREACH())
                    return true;
            }

            if (token[index].ClassPart.Equals("switch"))
            {
                if (SWITCH())
                    return true;
            }

            if (token[index].ClassPart.Equals("label"))
            {
                if (LABEL_DEC())
                    return true;
            }

            if (token[index].ClassPart.Equals("goto"))
            {
                if (GOTO_DEC())
                    return true;
            }

            if (token[index].ClassPart.Equals("var"))
            {
                if (VAR_DEC())
                {
                    Console.WriteLine("VAR RETURNING TRUE *****************************");
                    return true;
                }

            }
            if (token[index].ClassPart == "const")
            {
                if (CONST_DEC())
                    return true;
            }

            if (token[index].ClassPart.Equals("this"))
            {
                if (THIS_ST())
                    return true;
            }

            if (token[index].ClassPart.Equals("void")  || token[index].ClassPart.Equals("override")|| token[index].ClassPart.Equals("abstract") || token[index].ClassPart.Equals("virtual") || token[index].ClassPart.Equals("DT") || token[index].ClassPart.Equals("ID"))
            {
                if (ALLDEC4())
                    return true;
            }

            if (token[index].ClassPart.Equals("inc_dec"))
            {
                index++;
                if (token[index].ClassPart.Equals("ID"))
                {
                    index++;
                    if (X())
                    {
                        if (token[index].ClassPart.Equals("\\r"))
                        {
                            index++;
                            return true;
                        }
                    }
                }


            }
            return false;
        }

        bool MST()
        {
            Console.WriteLine($"COMING IN MAST WITH {token[index].ClassPart} {token[index].Line} ************************************* ");
            if (token[index].ClassPart == "if" || token[index].ClassPart == "while" || token[index].ClassPart == "foreach" || token[index].ClassPart == "switch" || token[index].ClassPart == "label" ||
                token[index].ClassPart == "goto" || token[index].ClassPart == "var" || token[index].ClassPart == "inc_dec" || token[index].ClassPart == "void" || token[index].ClassPart == "DT" || token[index].ClassPart == "abstract" ||
                token[index].ClassPart == "ID" || token[index].ClassPart == "this" || token[index].ClassPart == "const" || token[index].ClassPart == "virtual" || token[index].ClassPart=="override" || token[index].ClassPart == "print")
            {
                if (SST())
                {
                    if (MST())
                        return true;
                }

            }
            else
            {
                if (token[index].ClassPart == "end" || token[index].ClassPart == "return" || token[index].ClassPart == "else" || token[index].ClassPart == "break" || token[index].ClassPart == "continue")
                {
                    return true;
                }
                //return true;


            }
            return false;
        }
    }
}
