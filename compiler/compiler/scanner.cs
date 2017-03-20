namespace compiler
{
    class scanner
    {
        //tokenizes file
        public void scan(string fileName)
        {
            string fileContents=getFile(fileName);
            System.Collections.Generic.List<string> strings =toStrings(fileContents);
            printStrings(strings);

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

    }
}
