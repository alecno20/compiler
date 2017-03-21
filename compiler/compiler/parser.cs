using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    class parser
    {
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
        public treeNode parseProgram( List<token> tokens)
        {
            //parses a program
            treeNode ast= new treeNode("program", 0, "program", null, null, null, null);
            //checks for more stuff 
            if (tokens.Count > 3)
            {
                //checks if sibling is a variable
                if (tokens[0].type == "int" & tokens[1].type == "id" & tokens[2].type == "semicolon")
                {
                    //ast = new treeNode("program", 0, "program", null, null, null, parseVariable(tokens));
                }
                //checks if sibling is an array
                else if (tokens[0].type == "int" & tokens[1].type == "id" & tokens[2].type == "lBracket")
                {
                    ast = new treeNode("program", 0, "program", null, null, null, parseArray(tokens));
                }
                //checks if sibling is a function
                else if ((tokens[0].type == "int" || tokens[0].type == "void") & tokens[1].type == "id" & tokens[2].type == "lParenthesis")
                {
                    //  treeNode program = new treeNode("program", 0, "program", null, null, null, parseFunction(tokens));
                }
            }
            //checks for end of file token
            if (tokens[0].type == "EOF")
            {
                tokens.RemoveAt(0);
                if (tokens.Count == 0)
                {
                    System.Console.WriteLine("parse complete");
                }
                else
                {
                    error("expected end of file, except found more characters");
                }
            }
            else
            {
                error("expected EOF character, except found more characters");
            }
            return ast;
        }
        //............................................................................
        //parses an array instantiation
        //............................................................................
        public treeNode parseArray(List<token> tokens)
        {
            treeNode array = new treeNode(null, 0, null, null, null, null, null);
            //check for array instantiation 
            if (tokens[0].type == "int" & tokens[1].type=="id"&tokens[2].type=="lBracket"&(tokens[3].type=="number"||tokens[3].type=="id")&tokens[4].type=="rBracket"&tokens[5].type=="semicolon")
            {
                array.nodeType = "array";
                array.lineNumber = tokens[0].line;
                array.sValue = tokens[1].value;
                tokens.RemoveAt(0);
            }
            //check for array parameter
            else if (tokens[0].type == "int" & tokens[1].type == "id" & tokens[2].type == "lBracket" & tokens[3].type == "rBracket" & (tokens[4].type == "comma"||tokens[4].type == "rParenthesis"))
            {
                array.nodeType = "array";
                array.lineNumber = tokens[0].line;
                array.sValue = tokens[1].value;
                //pops off stack
                for(int i=0;i<4;i++)
                {
                    tokens.RemoveAt(0);
                }
                //checks for another parameter
                if(tokens[0].type=="comma")
                {
                    tokens.RemoveAt(0);
                    //checks if sibling is a variable
                    if (tokens[0].type == "int" & tokens[1].type == "id" & tokens[2].type == "semicolon")
                    {
                        array.sibling=parseVariable(tokens);
                    }
                    //checks if sibling is an array
                    else if (tokens[0].type == "int" & tokens[1].type == "id" & tokens[2].type == "lBracket")
                    {
                        array.sibling=parseArray(tokens);
                    }
                }
                //otherwise pops and ends
                tokens.RemoveAt(0);
            }
            return array;
        }
        //............................................................................
        //parses a variable declaration
        //............................................................................
        /*public List<treeNode> parseDeclaration(List<treeNode> syntaxTree, List<token> tokens)
        {
            if ()
            {

            }
            return syntaxTree;
        }*/
        //............................................................................
        //parses a function call
        //............................................................................
        /*public List<treeNode> parseFunction(List<treeNode> syntaxTree, List<token> tokens)
        {
            //checks for return type
            if (tokens[0].type=="int"||tokens[0].type=="void")
            {
                syntaxTree.Add(new treeNode(tokens[0].type, tokens[0].line, tokens[0].value));
                tokens.RemoveAt(0);
            }
            else
            {
                error("expected return type, except found "+tokens[0].type+" on line "+tokens[0].line);
            }
            //checks for function name
            if (tokens[0].type == "id"|| tokens[0].type == "main")
            {
                syntaxTree.Add(new treeNode(tokens[0].type, tokens[0].line, tokens[0].value));
                tokens.RemoveAt(0);
            }
            else
            {
                error("expected function id type, except found " + tokens[0].type + " on line " + tokens[0].line);
            }
            //checks for left parenthesis
            if (tokens[0].type == "lParenthesis")
            {
                syntaxTree.Add(new treeNode(tokens[0].type, tokens[0].line, tokens[0].value));
                tokens.RemoveAt(0);
            }
            else
            {
                error("expected lParenthesis, except found " + tokens[0].type + " on line " + tokens[0].line);
            }
            //checks for parameters
            if (tokens[0].type == "void")
            {
                syntaxTree.Add(new treeNode(tokens[0].type, tokens[0].line, tokens[0].value));
                tokens.RemoveAt(0);
            }
            else
            {
                while (tokens[0].type == "int")
                { 
                    syntaxTree.Add(new treeNode(tokens[0].type, tokens[0].line, tokens[0].value));
                    tokens.RemoveAt(0);
                    //checks for parameter name
                    if (tokens[0].type == "id")
                    {
                        syntaxTree.Add(new treeNode(tokens[0].type, tokens[0].line, tokens[0].value));
                        tokens.RemoveAt(0);
                    }
                    else
                    {
                        error("expected id, except found " + tokens[0].type + "on line" + tokens[0].line);
                    }
                }
            }
            //checks for right parenthesis
            if (tokens[0].type == "rParenthesis")
            {
                syntaxTree.Add(new treeNode(tokens[0].type, tokens[0].line, tokens[0].value));
                tokens.RemoveAt(0);
            }
            else
            {
                error("expected rParenthesis, except found " + tokens[0].type + " on line " + tokens[0].line);
            }
            //checks for left bracket
            if (tokens[0].type == "lBrace")
            {
                syntaxTree.Add(new treeNode(tokens[0].type, tokens[0].line, tokens[0].value));
                tokens.RemoveAt(0);
            }
            else
            {
                error("expected lBrace, except found " + tokens[0].type + " on line " + tokens[0].line);
            }
            //check for stuff?
            //checks for right brace
            if (tokens[0].type == "rBrace")
            {
                syntaxTree.Add(new treeNode(tokens[0].type, tokens[0].line, tokens[0].value));
                tokens.RemoveAt(0);
            }
            else
            {
                error("expected rBrace, except found " + tokens[0].type + " on line " + tokens[0].line);
            }
            return syntaxTree;
        }*/
    }
}
