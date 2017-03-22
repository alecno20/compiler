using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Console.WriteLine(tokens[position].type+ "int"+tokens[position+1].type + "id"+ tokens[position+2].type + "lBracket"+ tokens[position+3].type + "number"+ tokens[position+4].type + "rBracket"+ tokens[position+5].type + "semicolon");

namespace compiler
{
    class parser
    {
        //............................................................................
        //prints parse tree
        //............................................................................
        public void printParseTree(treeNode ast)
        {
            Console.WriteLine("type:" + ast.nodeType + " name:" + ast.name + " line:" + ast.lineNumber);
            if (ast.C1 != null)
            {
                Console.WriteLine("C1");
                printParseTree(ast.C1);
            }
            if (ast.C2 != null)
            {
                Console.WriteLine("C1");
                printParseTree(ast.C2);
            }
            if (ast.C3 != null)
            {
                Console.WriteLine("C1");
                printParseTree(ast.C3);
            }

        }

        //............................................................................
        //prints an error
        //............................................................................
        public void error(string error)
        {
            System.Console.WriteLine("error");
            System.Console.WriteLine(error);
            System.Console.WriteLine("press any key to terminate program");
            System.Console.ReadKey();
            Environment.Exit(0);
        }
        //............................................................................
        //begins to parse an entire program
        //............................................................................
        public treeNode parseProgram(List<token> tokens)
        {
            int position = 0;
            //parses a program
            treeNode program = new treeNode("program", 0, "program", parseDeclarationList(tokens, ref position), null, null);
            //checks for end of file token
            if (tokens[position].type == "EOF")
            {
                System.Console.WriteLine("parse complete");
            }
            else
            {
                printParseTree(program);
                error("wanted eof, got: "+tokens[position].type+" error on line"+ tokens[position].line);
            }
            return program;
        }
        //............................................................................
        //parses a declaration list
        //............................................................................
        public treeNode parseDeclarationList(List<token> tokens, ref int position)
        {

            //parses a declaration list
            treeNode declarationList = new treeNode("declarationList", 0, "declarationList", parseDeclaration(tokens, ref position), parseDeclarationListPrime(tokens, ref position), null);
            return declarationList;
        }
        //............................................................................
        //parses a declaration list prime
        //............................................................................
        public treeNode parseDeclarationListPrime(List<token> tokens, ref int position)
        {
            try
            {
                //checks the current token
                if (tokens[position].type == "int" || tokens[position].type == "void")
                {
                    treeNode declarationListPrime = new treeNode("declarationListprime", tokens[position].line, "declarationListprime", parseDeclaration(tokens, ref position), parseDeclarationListPrime(tokens, ref position), null);
                    return declarationListPrime;
                }
                else
                {
                    return null;
                }
            }
            catch(System.ArgumentOutOfRangeException)
            {
                return null;
            }
        }

        //............................................................................
        //parses a declaration
        //............................................................................
        public treeNode parseDeclaration(List<token> tokens, ref int position)
        {
            treeNode declaration = null;
            //parses a declaration
            //checks if declaration is a function
            declaration = new treeNode("declaration", tokens[position].line, "declaration", parseFunDeclaration(tokens, ref position), null, null);
            //checks if declaration is a variable
            if (declaration.C1 == null)
            {
                declaration = new treeNode("declaration", tokens[position].line, "declaration", parseVarDeclaration(tokens, ref position), null, null);
            }
            if (declaration.C1 == null)
            {
                return null;
            }
            return declaration;
        }

        //............................................................................
        //parses a varDeclaration
        //............................................................................
        public treeNode parseVarDeclaration(List<token> tokens, ref int position)
        {
            treeNode varDeclaration = null;
            //parses a varDeclaration
            try
            {
                //checks if varDeclaration is an int
                if ((tokens[position].type == "int" || tokens[position].type == "void") & tokens[position+1].type == "id" & tokens[position+2].type == "semicolon")
                {
                    varDeclaration = new treeNode("intDeclaration", tokens[position].line, tokens[position+1].value, null, null, null);
                    position += 3;
                }
                //checks if varDeclaration is an array
                else if ((tokens[position].type == "int" || tokens[position].type == "void") & tokens[position+1].type == "id" & tokens[position+2].type == "lBracket" & tokens[position+3].type == "number" & tokens[position+4].type == "rBracket" & tokens[position+5].type == "semicolon")
                {
                    varDeclaration = new treeNode("arrayDeclaration", tokens[position].line, tokens[position+1].value, null, null, null);
                    position += 6;
                }
                else
                {
                    return null;
                }
            }
            catch (System.ArgumentOutOfRangeException)
            {
                return null;
            }
            return varDeclaration;
        }

        //............................................................................
        //parses a funDeclaration
        //............................................................................
        public treeNode parseFunDeclaration(List<token> tokens, ref int position)
        {
            int initialposition = position;
            treeNode funDeclaration = null;
            //parses a funDeclaration
             try
             {
                 //checks if funDeclaration
                 if ((tokens[position].type == "int" || tokens[position].type == "void") & tokens[position + 1].type == "id" & tokens[position + 2].type == "lParenthesis")
                 {
                     funDeclaration = new treeNode("funDeclaration", tokens[position].line, tokens[position+1].value,null, null, null);
                     position += 3;
                     funDeclaration.C1 = parseParams(tokens, ref position);
                     
                     //funDeclaration.C2 = parseCompoundStmt(tokens, ref position);

                 }
                 else
                 {
                     position = initialposition;
                     return null;
                 }
             }
             catch (System.ArgumentOutOfRangeException)
             {
                 position = initialposition;
                 return null;
             }
             return funDeclaration;
        }
        //............................................................................
        //parses a params
        //............................................................................
        public treeNode parseParams(List<token> tokens, ref int position)
        {
            int initialposition = position;
            treeNode funDeclaration = null;
            //parses a funDeclaration
            try
            {
                //checks if params
                if (tokens[position].type == "void")
                {
                    funDeclaration = new treeNode("params", tokens[0].line, "void", null, null, null);
                    position += 1;
                }
                else
                {
                    position = initialposition;
                    return null;
                }
            }
            catch (System.ArgumentOutOfRangeException)
            {
                position = initialposition;
                return null;
            }
            return funDeclaration;
        }
    }
}
