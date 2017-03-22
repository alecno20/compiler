namespace compiler
{
    class scanner
    {
        //tokenizes file by calling methods
        public System.Collections.Generic.List<token> scan(string fileName)
        {
            string fileContents=getFile(fileName);
            System.Collections.Generic.List<token> tokens =toStrings(fileContents);
            tokens = tokenizer(tokens);
            printTokens(tokens);
            return tokens;
        }

        //converts file to string
        public string getFile(string fileName)
        {
            System.Console.WriteLine("opening fie"+fileName);
            string file;
            if (System.IO.File.Exists(fileName))
            {
                file = System.IO.File.ReadAllText(fileName);
            }
            else
            {
                file = null;
                System.Console.WriteLine("error file does not exist");
                System.Console.ReadKey();
                System.Environment.Exit(1);
            }
            return file;
        }
        //converts string to individual strings
        public System.Collections.Generic.List<token> toStrings(string fileContents)
        {
            System.Collections.Generic.List<token> tokens= new System.Collections.Generic.List<token>();
            string currentToken = "";
            int line = 1;
            for(int i=0; i<fileContents.Length;i++)
            {
                //checks if newline character
                if (fileContents[i].Equals('\r'))
                {
                    line++;
                }
                //check if white space
                else if(char.IsWhiteSpace(fileContents[i])){
                    if (!(string.IsNullOrEmpty(currentToken)))
                    {
                        tokens.Add(new token(currentToken,line));
                        currentToken = "";
                    }
                }
                //check for letters or digits
                else if(char.IsLetter(fileContents[i])|| char.IsNumber(fileContents[i]))
                {
                    currentToken += fileContents[i];
                }
                //check for special characters
                else if (fileContents[i] == '[' || fileContents[i] == ']' || fileContents[i] == ';' || fileContents[i] == '(' || fileContents[i] == ')' || fileContents[i] == '{' || fileContents[i] == '}' || fileContents[i] == '+' || fileContents[i] == '/' || fileContents[i] == '*' || fileContents[i] == '-' || fileContents[i] == '=' || fileContents[i] == '!')
                {
                    if (!(string.IsNullOrEmpty(currentToken)))
                    {
                        tokens.Add(new token(currentToken, line));
                        currentToken = System.Char.ToString(fileContents[i]);
                        tokens.Add(new token(currentToken, line));
                        currentToken = "";
                    }
                    else
                    {
                        currentToken = System.Char.ToString(fileContents[i]);
                        tokens.Add(new token(currentToken, line));
                        currentToken = "";
                    }
                }
            }
            return tokens;
        }
        //prints the value of the tokens
        public void printValues(System.Collections.Generic.List<token> tokens)
        {
            for(int i = 0; i < tokens.Count; i++)
            {
                System.Console.WriteLine(tokens[i].value);
            }
        }
        //prints tokens
        public void printTokens(System.Collections.Generic.List<token> tokens)
        {
            for (int i = 0; i < tokens.Count; i++)
            {
                System.Console.WriteLine("token- type:"+tokens[i].type+" value:"+tokens[i].value+" line:"+tokens[i].line);
            }
        }
        //converts to tokens
        public System.Collections.Generic.List<token> tokenizer(System.Collections.Generic.List<token> tokens)
        {
            int i = 0;
            for (i = 0; i < tokens.Count; i++)
            {   
                switch (tokens[i].value)
                {
                    case "void":
                        tokens[i].type = "void";
                        break;
                    case "int":
                        tokens[i].type = "int";
                        break;
                    case ";":
                        tokens[i].type="semicolon";
                        break;
                    case ",":
                        tokens[i].type = "comma";
                        break;
                    case "+":
                        tokens[i].type = "plus";
                        break;
                    case "-":
                        tokens[i].type = "minus";
                        break;
                    case "*":
                        tokens[i].type = "multiply";
                        break;
                    case "/":
                        tokens[i].type = "divide";
                        break;
                    case "(":
                        tokens[i].type = "lParenthesis";
                        break;
                    case ")":
                        tokens[i].type = "rParenthesis";
                        break;
                    case "=":
                        tokens[i].type = "equal";
                        break;
                    case "{":
                        tokens[i].type = "lBrace";
                        break;
                    case "}":
                        tokens[i].type = "rBrace";
                        break;
                    case "[":
                        tokens[i].type = "lBracket";
                        break;
                    case "]":
                        tokens[i].type = "rBracket";
                        break;
                    case "if":
                        tokens[i].type = "if";
                        break;
                    case "else":
                        tokens[i].type = "else";
                        break;
                    case "read":
                        tokens[i].type = "read";
                        break;
                    case "write":
                        tokens[i].type = "write";
                        break;
                    case "!":
                        tokens[i].type = "not";
                        break;
                    case "return":
                        tokens[i].type = "return";
                        break;
                    case "<":
                        tokens[i].type = "lessThan";
                        break;
                    case ">":
                        tokens[i].type = "greaterThan";
                        break;
                    case "while":
                        tokens[i].type = "while";
                        break;
                    default:
                        //checks if it is a variable
                        if (char.IsLetter(tokens[i].value[0]))
                        {
                            tokens[i].type = "id";
                        }
                        //checks if it is a value
                        else if (char.IsNumber(tokens[i].value[0]))
                        {
                            tokens[i].type = "number";
                        }
                        
                        break;
                }
            }
            //adds EOF token
            tokens.Add(new token("EOF","EOF",tokens[i-1].line));
            return tokens;
        }

    }
}
