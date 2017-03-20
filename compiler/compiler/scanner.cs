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
                //checks if it is a variable name or namespace
                if (char.IsLetter(strings[i][0]))
                {
                    if(strings[i]=="void"|| strings[i] == "int")//fdsafsadfsadfsadfsafsafdsfsafffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff
                    tokens.Add(new string[] { "variable", strings[i] });
                }
                //checks if it is a value
                if (char.IsNumber(strings[i][0]))
                {
                    tokens.Add(new string[] { "value", strings[i] });
                }
                //check if it is an operator
                else if (strings[i][0]==';'||strings[i][0]=='('||strings[i][0]==')'||strings[i][0]=='{'||strings[i][0]=='}'||strings[i][0]=='+'||strings[i][0]=='/'||strings[i][0]=='*'||strings[i][0]=='-'||strings[i][0]=='='||strings[i][0]=='!')
                {
                    tokens.Add(new string[] { "operator", strings[i] });
                }

            }
            return tokens;
        }

    }
}
