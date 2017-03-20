namespace compiler
{
    class scanner
    {
        //tokenizes file by calling methods
        public void scan(string fileName)
        {
            string fileContents=getFile(fileName);
            System.Collections.Generic.List<string> strings =toStrings(fileContents);
            printStrings(strings);
            System.Collections.Generic.List<string[]> tokens = tokenizer(strings);
            printTokens(tokens);
        }
        //converts file to string
        public string getFile(string fileName)
        {
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
        public System.Collections.Generic.List<string> toStrings(string fileContents)
        {
            System.Collections.Generic.List<string> tokens= new System.Collections.Generic.List<string>();
            string currentToken = "";
            for(int i=0; i<fileContents.Length;i++)
            {
                //check if white space
                if(char.IsWhiteSpace(fileContents[i])){
                    if (!(string.IsNullOrEmpty(currentToken)))
                    {
                        tokens.Add(currentToken);
                        currentToken = "";
                    }
                }
                //check for letters or digits
                else if(char.IsLetter(fileContents[i])|| char.IsNumber(fileContents[i]))
                {
                    currentToken += fileContents[i];
                }
                //check for special characters
                else if (fileContents[i] == ';' || fileContents[i] == '(' || fileContents[i] == ')' || fileContents[i] == '{' || fileContents[i] == '}' || fileContents[i] == '+' || fileContents[i] == '/' || fileContents[i] == '*' || fileContents[i] == '-' || fileContents[i] == '=' || fileContents[i] == '!')
                {
                    if (!(string.IsNullOrEmpty(currentToken)))
                    {
                        tokens.Add(currentToken);
                        currentToken = System.Char.ToString(fileContents[i]);
                        tokens.Add(currentToken);
                        currentToken = "";
                    }
                    else
                    {
                        currentToken = System.Char.ToString(fileContents[i]);
                        tokens.Add(currentToken);
                        currentToken = "";
                    }
                }
            }
            tokens.Add("EOF");
            return tokens;
        }
        public void printStrings(System.Collections.Generic.List<string> strings)
        {
            for(int i = 0; i < strings.Count; i++)
            {
                System.Console.WriteLine(strings[i]);
            }
        }
        //prints tokens
        public void printTokens(System.Collections.Generic.List<string[]> tokens)
        {
            for (int i = 0; i < tokens.Count; i++)
            {
                System.Console.WriteLine("token- type:"+tokens[i][0]+" value:"+tokens[i][1]);
            }
        }
        //converts to tokens
        public System.Collections.Generic.List<string[]> tokenizer(System.Collections.Generic.List<string> strings)
        {
            System.Collections.Generic.List<string[]> tokens = new System.Collections.Generic.List<string[]>();
            for (int i = 0; i < strings.Count; i++)
            {   
                switch (strings[i])
                {
                    case ";":
                        tokens.Add(new string[] { "semicolon", strings[i] });
                        break;
                    case ",":
                        tokens.Add(new string[] { "comma", strings[i] });
                        break;
                    case "+":
                        tokens.Add(new string[] { "plus", strings[i] });
                        break;
                    case "-":
                        tokens.Add(new string[] { "minus", strings[i] });
                        break;
                    case "*":
                        tokens.Add(new string[] { "multiply", strings[i] });
                        break;
                    case "/":
                        tokens.Add(new string[] { "divide", strings[i] });
                        break;
                    case "(":
                        tokens.Add(new string[] { "lParenthesis", strings[i] });
                        break;
                    case ")":
                        tokens.Add(new string[] { "rParenthesis", strings[i] });
                        break;
                    case "=":
                        tokens.Add(new string[] { "equal", strings[i] });
                        break;
                    case "{":
                        tokens.Add(new string[] { "lBrace", strings[i] });
                        break;
                    case "}":
                        tokens.Add(new string[] { "rBrace", strings[i] });
                        break;
                    case "[":
                        tokens.Add(new string[] { "lBracket", strings[i] });
                        break;
                    case "]":
                        tokens.Add(new string[] { "rBracket", strings[i] });
                        break;
                    case "if":
                        tokens.Add(new string[] { "if", strings[i] });
                        break;
                    case "else":
                        tokens.Add(new string[] { "else", strings[i] });
                        break;
                    case "read":
                        tokens.Add(new string[] { "read", strings[i] });
                        break;
                    case "write":
                        tokens.Add(new string[] { "write", strings[i] });
                        break;
                    case "!":
                        tokens.Add(new string[] { "not", strings[i] });
                        break;
                    case "return":
                        tokens.Add(new string[] { "return", strings[i] });
                        break;
                    case "<":
                        tokens.Add(new string[] { "lessThan", strings[i] });
                        break;
                    case ">":
                        tokens.Add(new string[] { "greaterThan", strings[i] });
                        break;
                    case "while":
                        tokens.Add(new string[] { "while", strings[i] });
                        break;
                    case "EOF":
                        tokens.Add(new string[] { "EOF", strings[i] });
                        break;
                    default:
                        //checks if it is a variable
                        if (char.IsLetter(strings[i][0]))
                        {
                            tokens.Add(new string[] { "id", strings[i] });
                        }
                        //checks if it is a value
                        else if (char.IsNumber(strings[i][0]))
                        {
                            tokens.Add(new string[] { "number", strings[i] });
                        }
                        break;
                }
            }
            return tokens;
        }

    }
}
