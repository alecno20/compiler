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
        public void printParseTree(treeNode ast, int indents)
        {
            for(int i = 0;i <= indents; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine("Node Type:" + ast.nodeType);
            for (int i = 0; i <= indents; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine(" Line Number:" + ast.lineNumber);
            for (int i = 0; i <= indents; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine(" name:" + ast.name);
            if (ast.C1 != null)
            {
                indents += 1;
                for (int i = 0; i <= indents; i++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine("C1");
                printParseTree(ast.C1,indents);
            }
            if (ast.C2 != null)
            {
                indents += 1;
                for (int i = 0; i <= indents; i++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine("C2");
                printParseTree(ast.C2, indents);
            }
            if (ast.C3 != null)
            {
                indents += 1;
                for (int i = 0; i <= indents; i++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine("C3");
                printParseTree(ast.C3, indents);
            }
            if (ast.C4 != null)
            {
                indents += 1;
                for (int i = 0; i <= indents; i++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine("C4");
                printParseTree(ast.C4, indents);
            }
            if (ast.C5 != null)
            {
                indents += 1;
                for (int i = 0; i <= indents; i++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine("C5");
                printParseTree(ast.C5, indents);
            }
            if (ast.C6 != null)
            {
                indents += 1;
                for (int i = 0; i <= indents; i++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine("C6");
                printParseTree(ast.C6, indents);
            }
            if (ast.C7 != null)
            {
                indents += 1;
                for (int i = 0; i <= indents; i++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine("C7");
                printParseTree(ast.C7, indents);
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
        public treeNode parseTerminal(List<token> tokens, ref int position, string value)
        {
            if (tokens[position].type == value)
            {
                treeNode terminal = new treeNode(value, tokens[position].line, value);
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
                printParseTree(program, 0);
                error(" error in parse branch with origin on line" + tokens[position].line);
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
            varDeclaration = new treeNode("varDeclaration", tokens[position].line, "varDeclaration", parseTypeSpecifier(tokens, ref position), parseId(tokens, ref position), parseTerminal(tokens, ref position, "semicolon"));
            //checks if declaration is an array
            if (varDeclaration.C1 == null || varDeclaration.C2 == null || varDeclaration.C3 == null)
            {
                position = initialPosition;
                varDeclaration = new treeNode("varDeclaration", tokens[position].line, "varDeclaration", parseTypeSpecifier(tokens, ref position), parseId(tokens, ref position), parseTerminal(tokens, ref position,"lBracket"), parseNumber(tokens, ref position), parseTerminal(tokens, ref position, "rBracket"), parseTerminal(tokens, ref position,"semicolon"));
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
            typeSpecifier = new treeNode("typeSpecifier", tokens[position].line, "typeSpecifier", parseTerminal(tokens, ref position,"int"));
            //checks for correct parse
            if (typeSpecifier.C1 == null)
            {
                position = initialPosition;
                typeSpecifier = new treeNode("typeSpecifier", tokens[position].line, "typeSpecifier", parseTerminal(tokens, ref position,"void"));
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
            funDeclaration = new treeNode("funDeclaration", tokens[position].line, "funDeclaration", parseTypeSpecifier(tokens, ref position), parseId(tokens, ref position), parseTerminal(tokens, ref position, "lParenthesis"));
            if (funDeclaration.C1 != null & funDeclaration.C2 != null & funDeclaration.C3 != null)
            {
                funDeclaration.C4 = parseParams(tokens, ref position);
                funDeclaration.C5 = parseTerminal(tokens, ref position, "rParenthesis");
                funDeclaration.C6 = parseCompoundStmt(tokens, ref position);
            }
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
                param = new treeNode("params", tokens[position].line, "params", parseTerminal(tokens, ref position,"void"));
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
            paramsList = new treeNode("paramsList", tokens[position].line, "paramsList", parseParam(tokens, ref position),parseTerminal(tokens, ref position,"comma"));
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
            param = new treeNode("param", tokens[position].line, "param", parseTypeSpecifier(tokens, ref position),parseId(tokens, ref position),parseTerminal(tokens, ref position,"lBracket"), parseTerminal(tokens, ref position,"rBracket"));
            //checks for correct parse
            if (param.C1 == null || param.C2 == null|| param.C3 == null || param.C4 == null)
            {
                position = initialPosition;
                param = new treeNode("param", tokens[position].line, "param", parseTypeSpecifier(tokens, ref position), parseId(tokens, ref position));
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
            compoundStmt = new treeNode("compoundStmt", tokens[position].line, "compoundStmt", parseTerminal(tokens, ref position, "lBrace"));
            if (compoundStmt.C1 != null)
            {
                compoundStmt.C2 = parseLocalDeclaration(tokens, ref position);
                compoundStmt.C3 = parseStatementList(tokens, ref position);
                compoundStmt.C4 = parseTerminal(tokens, ref position, "rBrace");
            }
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
            localDeclaration = new treeNode("localDeclaration", tokens[position].line, "localDeclaration",parseVarDeclaration(tokens, ref position));
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
            statementList = new treeNode("statementList", tokens[position].line, "statementList", parseStatement(tokens, ref position));
            if (statementList.C1 != null)
            {
                statementList.C2 = parseStatementList(tokens, ref position);
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
            statement = new treeNode("statement", tokens[position].line, "statement", parseExpressionStmt(tokens, ref position));
            if (statement.C1 == null)
            { 
                position = initialPosition;
                statement = new treeNode("statement", tokens[position].line, "statement", parseCompoundStmt(tokens, ref position));
                if (statement.C1 == null)
                {
                    position = initialPosition;
                    statement = new treeNode("statement", tokens[position].line, "statement", parseSelectionStmt(tokens, ref position));
                    if (statement.C1 == null)
                    {
                        position = initialPosition;
                        statement = new treeNode("statement", tokens[position].line, "statement", parseIterationStmt(tokens, ref position));
                        if (statement.C1 == null)
                        {
                            position = initialPosition;
                            statement = new treeNode("statement", tokens[position].line, "statement", parseReturnStmt(tokens, ref position));
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
        //parses a SelectionStmt
        //............................................................................
        public treeNode parseSelectionStmt(List<token> tokens, ref int position)
        {
            int initialPosition = position;
            treeNode selectionStmt = null;
            selectionStmt = new treeNode("selectStmt", tokens[position].line, "selectStmt", parseTerminal(tokens, ref position, "if"), parseTerminal(tokens, ref position, "lParenthesis"));
            if(selectionStmt.C1!=null& selectionStmt.C2 != null)
            {
                selectionStmt.C3 = parseExpression(tokens, ref position);
                selectionStmt.C4 = parseTerminal(tokens, ref position, "rParenthesis");
            }
            if (selectionStmt.C3 != null & selectionStmt.C4 != null)
            {
                selectionStmt.C5 = parseStatement(tokens, ref position);
                selectionStmt.C6 = parseTerminal(tokens, ref position, "else");
            }
            if (selectionStmt.C5 != null & selectionStmt.C6 != null)
            {
                selectionStmt.C7 = parseStatement(tokens, ref position);
            }
            //checks for correct parse
            if (selectionStmt.C1 == null || selectionStmt.C2 == null || selectionStmt.C3 == null || selectionStmt.C4 == null || selectionStmt.C5 == null || selectionStmt.C6 == null || selectionStmt.C7 == null)
            {
                position = initialPosition;
                selectionStmt = new treeNode("selectStmt", tokens[position].line, "selectStmt", parseTerminal(tokens, ref position, "if"), parseTerminal(tokens, ref position, "lParenthesis"));
                if (selectionStmt.C1 != null & selectionStmt.C2 != null)
                {
                    selectionStmt.C3 = parseExpression(tokens, ref position);
                    selectionStmt.C4 = parseTerminal(tokens, ref position, "rParenthesis");
                }
                if (selectionStmt.C3 != null & selectionStmt.C4 != null)
                {
                    selectionStmt.C5=parseStatement(tokens, ref position);
                }
                if (selectionStmt.C1 == null || selectionStmt.C2 == null || selectionStmt.C3 == null || selectionStmt.C4 == null || selectionStmt.C5 == null)
                {
                    position = initialPosition;
                    return null;
                }
            }
            return selectionStmt;
        }

        //............................................................................
        //parses a IterationStmt
        //............................................................................
        public treeNode parseIterationStmt(List<token> tokens, ref int position)
        {
            int initialPosition = position;
            treeNode iterationStmt = null;
            iterationStmt = new treeNode("iterationStmt", tokens[position].line, "iterationStmt", parseTerminal(tokens, ref position, "while"), parseTerminal(tokens, ref position, "lParenthesis"));
            if (iterationStmt.C1 != null & iterationStmt.C2 != null)
            {
                iterationStmt.C3 = parseExpression(tokens, ref position);
                iterationStmt.C4 = parseTerminal(tokens, ref position, "rParenthesis");
            }
            if (iterationStmt.C3 != null & iterationStmt.C4 != null)
            {
                iterationStmt.C5 = parseStatement(tokens, ref position);
            }
            //checks for correct parse
            if (iterationStmt.C1 == null || iterationStmt.C2 == null || iterationStmt.C3 == null || iterationStmt.C4 == null || iterationStmt.C5 == null)
            {
                position = initialPosition;
                return null;
            }
            return iterationStmt;
        }

        //............................................................................
        //parses a returnStmt
        //............................................................................
        public treeNode parseReturnStmt(List<token> tokens, ref int position)
        {
            int initialPosition = position;
            treeNode returnStmt = null;
            returnStmt = new treeNode("returnStmt", tokens[position].line, "returnStmt", parseTerminal(tokens, ref position,"return"), parseExpression(tokens, ref position), parseTerminal(tokens, ref position,"semicolon"));
            //checks for correct parse
            if (returnStmt.C1 == null || returnStmt.C2 == null || returnStmt.C3 == null)
            {
                position = initialPosition;
                returnStmt = new treeNode("returnStmt", tokens[position].line, "returnStmt", parseTerminal(tokens, ref position,"return"), parseTerminal(tokens, ref position,"semicolon"));
                //checks for correct parse
                if (returnStmt.C1 == null || returnStmt.C2 == null)
                {
                    position = initialPosition;
                    return null;
                }
            }
            return returnStmt;
        }


        //............................................................................
        //parses an expressionStmt
        //............................................................................
        public treeNode parseExpressionStmt(List<token> tokens, ref int position)
        {
            int initialPosition = position;
            treeNode expressionStmt = null;
            expressionStmt = new treeNode("expressionStmt", tokens[position].line, "expressionStmt", parseExpression(tokens, ref position),parseTerminal(tokens, ref position,"semicolon"));
            //checks for correct parse
            if (expressionStmt.C1 == null || expressionStmt.C2 == null)
            {
                position = initialPosition;
                expressionStmt = new treeNode("expressionStmt", tokens[position].line, "expressionStmt", parseTerminal(tokens, ref position,"semicolon"));
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
            expression = new treeNode("expression", tokens[position].line, "expression", parseVar(tokens, ref position), parseTerminal(tokens, ref position, "equal"));
            if (expression.C1 != null & expression.C2 != null)
            {
                expression.C3 = parseExpression(tokens, ref position);
            }
            if (expression.C1 == null || expression.C2 == null||expression.C3==null)
            {
                position = initialPosition;
                expression = new treeNode("expression", tokens[position].line, "expression", parseSimpleExpression(tokens, ref position));
                //checks for correct parse
                if (expression.C1 == null)
                {
                    position = initialPosition;
                    return null;
                }
            }
            return expression;
        }

        //............................................................................
        //parses a var
        //............................................................................
        public treeNode parseVar(List<token> tokens, ref int position)
        {
            int initialPosition = position;
            treeNode var= null;
            var = new treeNode("var", tokens[position].line, "var", parseId(tokens, ref position), parseTerminal(tokens, ref position, "lBracket"));
               if (var.C1 != null & var.C2 != null)
            {
                var.C3 = parseExpression(tokens, ref position);
                var.C4 = parseTerminal(tokens, ref position, "rBracket");
            } 
               
            //checks for correct parse
            if (var.C1 == null || var.C2 == null || var.C3 == null || var.C4 == null)
            {
                position = initialPosition;
                var = new treeNode("var", tokens[position].line, "var", parseId(tokens, ref position));
                if (var.C1 == null)
                {
                    position = initialPosition;
                    return null;
                }
            }
            return var;
        }

        //............................................................................
        //parses a simpleExpression
        //............................................................................
        public treeNode parseSimpleExpression(List<token> tokens, ref int position)
        {
            int initialPosition = position;
            treeNode simpleExpression = null;
            simpleExpression = new treeNode("simpleExpression", tokens[position].line, "simpleExpression", parseAdditiveExpression(tokens, ref position), parseRelop(tokens, ref position));
            if (simpleExpression.C1 != null & simpleExpression.C2 != null)
            {
                simpleExpression.C3=parseAdditiveExpression(tokens, ref position);
            }
            //checks for correct parse
            if (simpleExpression.C1 == null || simpleExpression.C2 == null || simpleExpression.C3 == null)
            {
                position = initialPosition;
                simpleExpression = new treeNode("simpleExpression", tokens[position].line, "simpleExpression", parseAdditiveExpression(tokens, ref position));
                if (simpleExpression.C1 == null)
                {
                    position = initialPosition;
                    return null;
                }
            }
            return simpleExpression;
        }


        //............................................................................
        //parses an additiveExpression
        //............................................................................
        public treeNode parseAdditiveExpression(List<token> tokens, ref int position)
        {
            int initialPosition = position;
            treeNode additiveExpression = null;
            additiveExpression = new treeNode("additiveExpression", tokens[position].line, "additiveExpression", parseTerm(tokens, ref position), parseAddop(tokens, ref position));//problem
            if (additiveExpression.C1!=null&additiveExpression.C2!=null)
            {
                additiveExpression.C3 = parseAdditiveExpression(tokens, ref position);
            }
            //checks for correct parse
            if (additiveExpression.C1 == null || additiveExpression.C2 == null || additiveExpression.C3 == null)
            {
                position = initialPosition;
                additiveExpression = new treeNode("additiveExpression", tokens[position].line, "additiveExpression", parseTerm(tokens, ref position));
                if (additiveExpression.C1 == null)
                {
                    position = initialPosition;
                    return null;
                }
            }
            return additiveExpression;
        }

        //............................................................................
        //parses an addop
        //............................................................................
        public treeNode parseAddop(List<token> tokens, ref int position)
        {
            int initialPosition = position;
            treeNode addopExpression = null;
            addopExpression = new treeNode("addopExpression", tokens[position].line, "addopExpression", parseTerminal(tokens, ref position,"plus"));
            //checks for correct parse
            if (addopExpression.C1 == null)
            {
                position = initialPosition;
                addopExpression = new treeNode("addopExpression", tokens[position].line, "addopExpression", parseTerminal(tokens, ref position,"minus"));
                if (addopExpression.C1 == null)
                {
                    position = initialPosition;
                    return null;
                }
            }
            return addopExpression;
        }

        //............................................................................
        //parses a term
        //............................................................................
        public treeNode parseTerm(List<token> tokens, ref int position)
        {
            int initialPosition = position;
            treeNode term = null;
            term = new treeNode("term", tokens[position].line, "term", parseFactor(tokens, ref position), parseMulop(tokens, ref position));//problem
            if (term.C1 != null & term.C2 != null)
            {
                term.C3 = parseTerm(tokens, ref position);
            }
            //checks for correct parse
            if (term.C1 == null || term.C2 == null || term.C3 == null)
            {
                position = initialPosition;
                term = new treeNode("term", tokens[position].line, "term", parseFactor(tokens, ref position));
                if (term.C1 == null)
                {
                    position = initialPosition;
                    return null;
                }
            }
            return term;
        }

        //............................................................................
        //parses a factor
        //............................................................................
        public treeNode parseFactor(List<token> tokens, ref int position)
        {
            int initialPosition = position;
            treeNode factor = null;
            factor = new treeNode("factor", tokens[position].line, "factor", parseTerminal(tokens, ref position, "lParenthesis"));
            if(factor.C1!=null)
            {
                factor.C2=parseExpression(tokens, ref position);
                factor.C3=parseTerminal(tokens, ref position, "rParenthesis");//problem
            }
            //checks for correct parse
            if (factor.C1 == null || factor.C2 == null || factor.C3 == null)
            {
                position = initialPosition;
                factor = new treeNode("factor", tokens[position].line, "factor", parseCall(tokens, ref position));
                if (factor.C1 == null)
                {
                    position = initialPosition;
                    factor = new treeNode("factor", tokens[position].line, "factor", parseVar(tokens, ref position));
                    if (factor.C1 == null)
                    {
                        position = initialPosition;
                        factor = new treeNode("factor", tokens[position].line, "factor", parseNumber(tokens, ref position));
                        if (factor.C1 == null)
                        {
                            position = initialPosition;
                            return null;
                        }
                    }
                }
            }
            return factor;
        }

        //............................................................................
        //parses a relop
        //............................................................................
        public treeNode parseRelop(List<token> tokens, ref int position)
        {
            int initialPosition = position;
            treeNode relop = null;
            relop = new treeNode("relop", tokens[position].line, "relop", parseTerminal(tokens, ref position,"greaterThan"),parseTerminal(tokens, ref position,"equal"));
            //checks for correct parse
            if (relop.C1 == null || relop.C2 == null)
            {
                position = initialPosition;
                relop = new treeNode("relop", tokens[position].line, "relop", parseTerminal(tokens, ref position,"lessThan"), parseTerminal(tokens, ref position,"equal"));
                if (relop.C1 == null || relop.C2 == null)
                {
                    position = initialPosition;
                    relop = new treeNode("relop", tokens[position].line, "relop", parseTerminal(tokens, ref position,"equal"), parseTerminal(tokens, ref position,"equal"));
                    if (relop.C1 == null || relop.C2 == null)
                    {
                        position = initialPosition;
                        relop = new treeNode("relop", tokens[position].line, "relop", parseTerminal(tokens, ref position,"not"), parseTerminal(tokens, ref position,"equal"));
                        if (relop.C1 == null || relop.C2 == null)
                        {
                            position = initialPosition;
                            relop = new treeNode("relop", tokens[position].line, "relop", parseTerminal(tokens, ref position,"greaterThan"));
                            if (relop.C1 == null)
                            {
                                position = initialPosition;
                                relop = new treeNode("relop", tokens[position].line, "relop", parseTerminal(tokens, ref position,"lessThan"));
                                if (relop.C1 == null)
                                {
                                    position = initialPosition;
                                    return null;
                                }
                            }
                        }
                    }
                }
            }
            return relop;
        }

        //............................................................................
        //parses a call
        //............................................................................
        public treeNode parseCall(List<token> tokens, ref int position)
        {
            int initialPosition = position;
            treeNode call = null;
            call = new treeNode("call", tokens[position].line, "call", parseId(tokens, ref position), parseTerminal(tokens, ref position, "lParenthesis"));
            if(call.C1!=null& call.C2 != null)
            {
                call.C3=parseArgs(tokens, ref position);
                call.C4=parseTerminal(tokens, ref position, "rParenthesis");
            }
            //checks for correct parse
            if (call.C1 == null || call.C2 == null || call.C4 == null)
            {
                position = initialPosition;
                return null;
            }
            return call;
        }

        //............................................................................
        //parses an args
        //............................................................................
        public treeNode parseArgs(List<token> tokens, ref int position)
        {
            int initialPosition = position;
            treeNode args = null;
            args = new treeNode("args", tokens[position].line, "args", parseArgList(tokens, ref position));
            //checks for correct parse
            if (args.C1 == null)
            {
                position = initialPosition;
                return null;
            }
            return args;
        }

        //............................................................................
        //parses an argsList
        //............................................................................
        public treeNode parseArgList(List<token> tokens, ref int position)
        {
            int initialPosition = position;
            treeNode argList = null;
            argList = new treeNode("argList", tokens[position].line, "argList", parseExpression(tokens, ref position), parseTerminal(tokens, ref position,"comma"));
            if (argList.C1 != null & argList.C2 != null)
            {
                argList.C3 = parseArgList(tokens, ref position);
            }
            //checks for correct parse
            if (argList.C1 == null || argList.C2 == null || argList.C3 == null)
            {
                position = initialPosition;
                argList = new treeNode("argList", tokens[position].line, "argList", parseExpression(tokens, ref position));
                if (argList.C1 == null)
                {
                    position = initialPosition;
                    return null;
                }
            }
            return argList;
        }

        //............................................................................
        //parses an mulop
        //............................................................................
        public treeNode parseMulop(List<token> tokens, ref int position)
        {
            int initialPosition = position;
            treeNode mulop = null;
            mulop = new treeNode("mulop", tokens[position].line, "mulop", parseTerminal(tokens, ref position,"multiply"));
            //checks for correct parse
            if (mulop.C1 == null)
            {
                position = initialPosition;
                mulop = new treeNode("funDeclaration", tokens[position].line, "funDeclaration", parseTerminal(tokens, ref position, "divide"));
                if (mulop.C1 == null)
                {
                    position = initialPosition;
                    return null;
                }
            }
            return mulop;
        }










    }
}
