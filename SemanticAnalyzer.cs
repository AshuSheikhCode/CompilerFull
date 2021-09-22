using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Compiler_Construction
{
    class SemanticAnalyzer
    {

        string outputPath = @"C:\Users\Admin\Desktop\CompilerFinal\maintable.txt";
        static List<MainTable> mainTable = new List<MainTable>();
        public ArrayList functionTable = new ArrayList();
        public List<ArrayList> classLinks = new List<ArrayList>();
        Stack scopeStack = new Stack();
        int scope = 0;
        int link = -1;

        
        public void print_MT()
        {
            
            File.WriteAllText(outputPath, String.Empty);
            File.AppendAllText(outputPath, $"{"Name"}    {"Class Type"}    {"Access Modifier"}      {"Category"}      {"Parent"}    {"Link"} \n");
        }
        public void print_CT()
        {
            File.WriteAllText(outputPath, String.Empty);
            File.AppendAllText(outputPath, $"{"Name"}    {"Type"}    {"Access Modifier"}   {"Type Modifier"} \n");
        }

        public bool insert_MT(string name, string ctype, string accessModifier, string category, string parent)
        {
            bool isParent = false;
            foreach (MainTable row in mainTable)
            {
                if (row.name == name)
                {
                    File.AppendAllText(outputPath, "Redeclaration of class \n");
                    classLinks.RemoveAt(this.link);
                    this.link--;
                    return false;
                }
            }
            if (parent != "none")
            {
                foreach (MainTable row in mainTable)
                {
                    if (parent == row.name)
                    {
                        isParent = true;
                        if (row.category == "final")
                        {
                            classLinks.RemoveAt(this.link);
                            this.link--;
                            File.AppendAllText(outputPath, "Final class cant be inherited \n");
                            //Console.WriteLine("Final class cant be inherited");
                            return false;
                        }
                    }
                }
                if (!isParent)
                {
                    File.AppendAllText(outputPath, "Parent class is not declared \n");
                    //Console.WriteLine("PARENT CLASS IS NOT DECLARED^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
                    classLinks.RemoveAt(this.link);
                    this.link--;
                    return false;
                }
            }
            mainTable.Add(new MainTable(name, ctype, accessModifier, category, parent, this.link));
            File.AppendAllText(outputPath, $"{name}    {ctype}         {accessModifier}       {category}      {parent}    {link}     \n");
            return true;
        }

        public bool insert_FT(string name, string type, string scope)
        {
            foreach (FunctionTable row in functionTable)
            {
                if (row.name == name)
                {
                    if (row.scope == scope)
                    {
                        File.AppendAllText(outputPath, "Redeclaration of Identifier \n");
                        return false;
                    }
                }
            }
            functionTable.Add(new FunctionTable(name, type, scope));
            File.AppendAllText(outputPath, $"Function Table \n");
            File.AppendAllText(outputPath, $"{"Name"}    {"Type"}   {"Scope"} \n");
            File.AppendAllText(outputPath, $"{name}    {type}   {scope} \n");
            return true;
        }


        public bool insert_CT(string name, string type, string acessModifier, string typeModifier, ArrayList a)
        {
            foreach (ClassTable row in a)
            {
                if (row.name == name)
                {
                    if (row.type == type)
                    {
                        File.AppendAllText(outputPath, "Redeclaration of Function \n");
                        return false;
                    }
                        
                    
                }
            }
            classLinks[this.link].Add(new ClassTable(name, type, acessModifier, typeModifier));
            File.AppendAllText(outputPath, $"Class Table \n");
            File.AppendAllText(outputPath, $"{"Name"}    {"Type"}   {"AccessModifier"}   {"TypeModifier"} \n");
            File.AppendAllText(outputPath, $"{name}    {type}   {acessModifier}     {typeModifier} \n");
            return true;
        }

        public void create_CT()
        {
            link++;
            ArrayList c = new ArrayList();
            classLinks.Add(c);
        }

        string lookup_MT(string name, out string category, out string parents)
        {
            foreach (MainTable main in mainTable)
            {
                if (main.name == name)
                {
                    category = main.category;
                    parents = main.parent;
                    return "class";
                }
            }
            category = "";
            parents = "";
            return "class not declared";
        }

        string lookupAttr_CT(int link, string name, out string AM, out string TM)
        {
            foreach (ClassTable classT in classLinks[link])
            {
                if (classT.name == name)
                {
                    AM = classT.acessModifier;
                    TM = classT.typeModifier;
                    return classT.type;
                }
            }
            AM = "";
            TM = "";
            return "variable not declared";
        }

        public void createScope()
        {
            scope++;
            scopeStack.Push(scope);
        }
        public void destroyScope()
        {
            scopeStack.Pop();
        }
    }

    class MainTable
    {
        public string name, ctype, accessModifier, category, parent;
        int link;
        public MainTable(string name, string ctype, string accessModifier, string category, string parent, int link)
        {
            this.name = name;
            this.ctype = ctype;
            this.accessModifier = accessModifier;
            this.category = category;
            this.parent = parent;
            this.link = link;
        }
    }

    class FunctionTable
    {
        public string name, type, scope;
        public FunctionTable(string name, string type, string scope)
        {
            this.name = name;
            this.type = type;
            this.scope = scope;
        }
    }

    class ClassTable
    {
        public string name, type, acessModifier, typeModifier;
        public ClassTable(string name, string type, string acessModifier, string typeModifier)
        {
            this.name = name;
            this.type = type;
            this.acessModifier = acessModifier;
            this.typeModifier = typeModifier;
        }
    }
}
