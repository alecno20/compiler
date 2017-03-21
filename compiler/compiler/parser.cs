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
            System.Console.WriteLine(error);
        }
        //............................................................................
        //parses an entire program
        //............................................................................
        public void parseProgram(List<treeNode> syntaxTree, List<token> tokens)
        {
            //parses a program
            syntaxTree.Add(new treeNode("program", 0, "program"));

            //parses a function
            parseFunction(syntaxTree, tokens);

            //checks for end of file token
            if (tokens[0].type == "EOF")
            {
                syntaxTree.Add(new treeNode(tokens[0].type, tokens[0].line, tokens[0].value));
                tokens.RemoveAt(0);
                if (tokens.Count == 0)
                {
                    System.Console.WriteLine("parse complete");
                }
                else
                {
                    error("expected EOF character, except found more characters");
                }
            }

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
        public List<treeNode> parseFunction(List<treeNode> syntaxTree, List<token> tokens)
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
            //checks for right bracket
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
        }
    }
}
