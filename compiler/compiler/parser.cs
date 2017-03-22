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
                Console.WriteLine("C2");
                printParseTree(ast.C2);
            }
            if (ast.C3 != null)
            {
                Console.WriteLine("C3");
                printParseTree(ast.C3);
            }
            if (ast.C4 != null)
            {
                Console.WriteLine("C4");
                printParseTree(ast.C4);
            }
            if (ast.C5 != null)
            {
                Console.WriteLine("C5");
                printParseTree(ast.C5);
            }
            if (ast.C6 != null)
            {
                Console.WriteLine("C6");
                printParseTree(ast.C6);
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
        //terminals
        //}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}
        public treeNode parseSemicolon(List<token> tokens, ref int position)
        {
            if(tokens[position].type=="semicolon")
            {
                treeNode terminal= new treeNode("semicolon", tokens[position].line, "semicolon");
                position += 1;
                return terminal;
            }
            else
            {
                return null;
            }
        }
        public treeNode parseLBracket(List<token> tokens, ref int position)
        {
            if (tokens[position].type == "lBracket")
            {
                treeNode terminal = new treeNode("lBracket", tokens[position].line, "lBracket");
                position += 1;
                return terminal;
            }
            else
            {
                return null;
            }
        }
        public treeNode parseRBracket(List<token> tokens, ref int position)
        {
            if (tokens[position].type == "rBracket")
            {
                treeNode terminal = new treeNode("rBracket", tokens[position].line, "rBracket");
                position += 1;
                return terminal;
            }
            else
            {
                return null;
            }
        }
        public treeNode parseId(List<token> tokens, ref int position)
        {
            if (tokens[position].type == "id")
            {
                treeNode terminal = new treeNode("id", tokens[position].line, tokens[position].value);
                position += 1;
                return terminal;
            }
            else
            {
                return null;
            }
        }
        public treeNode parseNumber(List<token> tokens, ref int position)
        {
            if (tokens[position].type == "number")
            {
                treeNode terminal = new treeNode("number", tokens[position].line, tokens[position].value);
                position += 1;
                return terminal;
            }
            else
            {
                return null;
            }
        }
        public treeNode parseInt(List<token> tokens, ref int position)
        {
            if (tokens[position].type == "int")
            {
                treeNode terminal = new treeNode("int", tokens[position].line, "int");
                position += 1;
                return terminal;
            }
            else
            {
                return null;
            }
        }
        public treeNode parseVoid(List<token> tokens, ref int position)
        {
            if (tokens[position].type == "void")
            {
                treeNode terminal = new treeNode("void", tokens[position].line, "void");
                position += 1;
                return terminal;
            }
            else
            {
                return null;
            }
        }
        public treeNode parseLParenthesis(List<token> tokens, ref int position)
        {
            if (tokens[position].type == "lParenthesis")
            {
                treeNode terminal = new treeNode("lParenthesis", tokens[position].line, "lParenthesis");
                position += 1;
                return terminal;
            }
            else
            {
                return null;
            }
        }
        public treeNode parseRParenthesis(List<token> tokens, ref int position)
        {
            if (tokens[position].type == "rParenthesis")
            {
                treeNode terminal = new treeNode("rParenthesis", tokens[position].line, "rParenthesis");
                position += 1;
                return terminal;
            }
            else
            {
                return null;
            }
        }
        public treeNode parseComma(List<token> tokens, ref int position)
        {
            if (tokens[position].type == "comma")
            {
                treeNode terminal = new treeNode("comma", tokens[position].line, "comma");
                position += 1;
                return terminal;
            }
            else
            {
                return null;
            }
        }
        public treeNode parseRBrace(List<token> tokens, ref int position)
        {
            if (tokens[position].type == "rBrace")
            {
                treeNode terminal = new treeNode("rBrace", tokens[position].line, "rBrace");
                position += 1;
                return terminal;
            }
            else
            {
                return null;
            }
        }
        public treeNode parseLBrace(List<token> tokens, ref int position)
        {
            if (tokens[position].type == "lBrace")
            {
                treeNode terminal = new treeNode("lBrace", tokens[position].line, "lBrace");
                position += 1;
                return terminal;
            }
            else
            {
                return null;
            }
        }



        //}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}

        //............................................................................
        //begins to parse an entire program
        //............................................................................
        public treeNode parseProgram(List<token> tokens)
        {
            int position = 0;
            //parses a program
            treeNode program = new treeNode("program", 0, "program", parseDeclarationList(tokens, ref position));
            
            //checks for end of file token
            if (tokens[position].type == "EOF")
            {
                System.Console.WriteLine("parse complete");
            }
            else
            {
                printParseTree(program);
                error("wanted eof, got: " + tokens[position].type + " error on line" + tokens[position].line);
            }
            return program;
        }
        //............................................................................
        //parses a declaration list
        //............................................................................
        public treeNode parseDeclarationList(List<token> tokens, ref int position)
        {
            int initialPosition = position;
            //parses a declaration list
            treeNode declarationList = new treeNode("declarationList", 0, "declarationList", parseDeclaration(tokens, ref position));
            if (declarationList.C1 != null)
            {
                declarationList.C2 = parseDeclarationList(tokens, ref position);
            }
            if (declarationList.C1 == null)
            {
                position = initialPosition;
                return null;
            }
            return declarationList;
        }

        //............................................................................
        //parses a declaration
        //............................................................................
        public treeNode parseDeclaration(List<token> tokens, ref int position)
        {
            int initialPosition = position;
            treeNode declaration = null;
            //parses a declaration
            
            //parses if declaration is a function
            declaration = new treeNode("declaration", tokens[position].line, "declaration", parseFunDeclaration(tokens, ref position));
            
            //checks if declaration is a variable
            if (declaration.C1 == null)
            {
                position = initialPosition;
                declaration = new treeNode("declaration", tokens[position].line, "declaration", parseVarDeclaration(tokens, ref position));
                //checks for successful parse
                if (declaration.C1 == null)
                {
                    position = initialPosition;
                    return null;
                }
            }
            return declaration;
        }



        //............................................................................
        //parses a varDeclaration
        //............................................................................
        public treeNode parseVarDeclaration(List<token> tokens, ref int position)
        {
            int initialPosition = position;
            treeNode varDeclaration = null;
            //parses a declaration
            //checks if declaration is an int or void
            varDeclaration = new treeNode("varDeclaration", tokens[position].line, "varDeclaration", parseTypeSpecifier(tokens, ref position), parseId(tokens, ref position), parseSemicolon(tokens, ref position));
            //checks if declaration is an array
            if (varDeclaration.C1 == null || varDeclaration.C2 == null || varDeclaration.C3 == null)
            {
                position = initialPosition;
                varDeclaration = new treeNode("varDeclaration", tokens[position].line, "varDeclaration", parseTypeSpecifier(tokens, ref position), parseId(tokens, ref position), parseLBracket(tokens, ref position), parseNumber(tokens, ref position), parseRBracket(tokens, ref position), parseSemicolon(tokens, ref position));
                if (varDeclaration.C1 == null || varDeclaration.C2 == null || varDeclaration.C3 == null || varDeclaration.C4 == null || varDeclaration.C5 == null || varDeclaration.C6 == null)
                {
                    position = initialPosition;
                    return null;
                }
            }
            return varDeclaration;
        }




        //............................................................................
        //parses a typeSpecifier
        //............................................................................
        public treeNode parseTypeSpecifier(List<token> tokens, ref int position)
        {
            int initialPosition = position;
            treeNode typeSpecifier = null;
            typeSpecifier = new treeNode("typeSpecifier", tokens[position].line, "typeSpecifier", parseInt(tokens, ref position));
            //checks for correct parse
            if (typeSpecifier.C1 == null)
            {
                position = initialPosition;
                typeSpecifier = new treeNode("typeSpecifier", tokens[position].line, "typeSpecifier", parseVoid(tokens, ref position));
                if (typeSpecifier.C1 == null)
                {
                    position = initialPosition;
                    return null;
                }
            }
            return typeSpecifier;
        }


        //............................................................................
        //parses a funDeclaration
        //............................................................................
        public treeNode parseFunDeclaration(List<token> tokens, ref int position)
        {
             int initialPosition = position;
             treeNode funDeclaration = null;
             funDeclaration = new treeNode("funDeclaration", tokens[position].line, "funDeclaration", parseTypeSpecifier(tokens, ref position),parseId(tokens, ref position),parseLParenthesis(tokens, ref position),parseParams(tokens, ref position),parseRParenthesis(tokens,ref position), parseCompoundStmt(tokens, ref position));
             //checks for correct parse
             if (funDeclaration.C1 == null || funDeclaration.C2 == null || funDeclaration.C3 == null || funDeclaration.C4 == null || funDeclaration.C5 == null|| funDeclaration.C6 == null)
             {
                 position = initialPosition;
                 return null;
             }
             return funDeclaration;
        }


        //............................................................................
        //parses params
        //............................................................................
        public treeNode parseParams(List<token> tokens, ref int position)
        {
            int initialPosition = position;
            treeNode param = null;
            param = new treeNode("params", tokens[position].line, "params", parseParamsList(tokens, ref position));
            //checks for correct parse
            if (param.C1 == null)
            {
                position = initialPosition;
                param = new treeNode("params", tokens[position].line, "params", parseVoid(tokens, ref position));
                if (param.C1 == null)
                {
                    position = initialPosition;
                    return null;
                }
            }
            return param;
        }

        //............................................................................
        //parses a paramsList
        //............................................................................
        public treeNode parseParamsList(List<token> tokens, ref int position)
        {
            int initialPosition = position;
            treeNode paramsList = null;
            paramsList = new treeNode("funDeclaration", tokens[position].line, "funDeclaration", parseParam(tokens, ref position),parseComma(tokens, ref position));
            //checks for correct parse
            if (paramsList.C1!=null& paramsList.C2 != null)
            {
                paramsList.C3 = parseParamsList(tokens, ref position);
            }
            if (paramsList.C1 == null)
            {
                position = initialPosition;
                return null;
            }
            return paramsList;
        }

        //............................................................................
        //parses a param
        //............................................................................
        public treeNode parseParam(List<token> tokens, ref int position)
        {
            int initialPosition = position;
            treeNode param = null;
            param = new treeNode("funDeclaration", tokens[position].line, "funDeclaration", parseTypeSpecifier(tokens, ref position),parseId(tokens, ref position),parseLBracket(tokens, ref position), parseRBracket(tokens, ref position));
            //checks for correct parse
            if (param.C1 == null || param.C2 == null|| param.C3 == null || param.C4 == null)
            {
                position = initialPosition;
                param = new treeNode("funDeclaration", tokens[position].line, "funDeclaration", parseTypeSpecifier(tokens, ref position), parseId(tokens, ref position));
                if (param.C1 == null || param.C2 == null)
                {
                    position = initialPosition;
                    return null;
                }
            }
            return param;
        }

        //............................................................................
        //parses a compoundStmt
        //............................................................................
        public treeNode parseCompoundStmt(List<token> tokens, ref int position)
        {
            int initialPosition = position;
            treeNode compoundStmt = null;
            compoundStmt= new treeNode("funDeclaration", tokens[position].line, "funDeclaration", parseLBrace(tokens, ref position), parseLocalDeclaration(tokens, ref position), parseStatementList(tokens, ref position), parseRBrace(tokens, ref position));
            //checks for correct parse
            if (compoundStmt.C1 == null || compoundStmt.C4 == null)
            {
                position = initialPosition;
                return null;
            }
            return compoundStmt;
        }

        //............................................................................
        //parses a localDecleration
        //............................................................................
        public treeNode parseLocalDeclaration(List<token> tokens, ref int position)
        {
            int initialPosition = position;
            treeNode localDeclaration = null;
            localDeclaration = new treeNode("localDeclaration", tokens[position].line, "localDeclaration",parseVarDeclaration(tokens, ref position);
            if (localDeclaration.C1 != null)
            {
                localDeclaration.C2 = parseLocalDeclaration(tokens, ref position);
            }
            if (localDeclaration.C1== null)
            {
                position = initialPosition;
                return null;
            }
            return localDeclaration;
        }

        //............................................................................
        //parses a statementList
        //............................................................................
        public treeNode parseStatementList(List<token> tokens, ref int position)
        {
            int initialPosition = position;
            treeNode statementList = null;
            statementList = new treeNode("localDeclaration", tokens[position].line, "localDeclaration", parseStatement(tokens, ref position);
            if (statementList.C1 != null)
            {
                statementList.C2 = parseLocalDeclaration(tokens, ref position);
            }
            if (statementList.C1 == null)
            {
                position = initialPosition;
                return null;
            }
            return statementList;
        }

        //............................................................................
        //parses a statement
        //............................................................................
        public treeNode parseStatement(List<token> tokens, ref int position)
        {
            int initialPosition = position;
            treeNode statement = null;
            statement = new treeNode("localDeclaration", tokens[position].line, "localDeclaration",parseExpressionStmt(tokens, ref position);
            if (statement.C1 == null)
            { 
                position = initialPosition;
                statement = new treeNode("localDeclaration", tokens[position].line, "localDeclaration", parseCompoundStmt(tokens, ref position);
                if (statement.C1 == null)
                {
                    position = initialPosition;
                    statement = new treeNode("localDeclaration", tokens[position].line, "localDeclaration", parseSelectionStmt(tokens, ref position);
                    if (statement.C1 == null)
                    {
                        position = initialPosition;
                        statement = new treeNode("localDeclaration", tokens[position].line, "localDeclaration", parseIterationStmt(tokens, ref position);
                        if (statement.C1 == null)
                        {
                            position = initialPosition;
                            statement = new treeNode("localDeclaration", tokens[position].line, "localDeclaration", parseReturnStatement(tokens, ref position);
                            if (statement.C1 == null)
                            {
                                position = initialPosition;
                                return null;
                            }
                        }
                    }
                }
            }
            return statement;
        }

        //............................................................................
        //parses an expressionStmt
        //............................................................................
        public treeNode parseExpressionStmt(List<token> tokens, ref int position)
        {
            int initialPosition = position;
            treeNode expressionStmt = null;
            expressionStmt = new treeNode("funDeclaration", tokens[position].line, "funDeclaration", parseExpression(tokens, ref position),parseSemicolon(tokens, ref position));
            //checks for correct parse
            if (expressionStmt.C1 == null || expressionStmt.C2 == null)
            {
                expressionStmt = new treeNode("funDeclaration", tokens[position].line, "funDeclaration", parseSemicolon(tokens, ref position));
                if (expressionStmt.C1 == null)
                {

                    position = initialPosition;
                    return null;
                }
            }
            return expressionStmt;
        }

        //............................................................................
        //parses an expression
        //............................................................................
        public treeNode parseExpression(List<token> tokens, ref int position)
        {
            int initialPosition = position;
            treeNode expression = null;
            expression = new treeNode("funDeclaration", tokens[position].line, "funDeclaration", parseVar(tokens, ref position), parseEqual(tokens, ref position));
            if (expressionStmt.C1 != null & expressionStmt.C2 != null)
            {
                expressionStmt = new treeNode("funDeclaration", tokens[position].line, "funDeclaration", parseSemicolon(tokens, ref position));
                if (expressionStmt.C1 == null)
                {

                    position = initialPosition;
                    return null;
                }
            }
            //checks for correct parse
            if (expressionStmt.C1 == null || expressionStmt.C2 == null)
            {
                expressionStmt = new treeNode("funDeclaration", tokens[position].line, "funDeclaration", parseSemicolon(tokens, ref position));
                if (expressionStmt.C1 == null)
                {

                    position = initialPosition;
                    return null;
                }
            }
            return expressionStmt;
        }


















    }
}
