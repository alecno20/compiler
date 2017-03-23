namespace compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Collections.Generic.List<token> tokens= new System.Collections.Generic.List<token>();
            scanner scanBoy = new scanner();
            if (args.Length == 0)
            {
                System.Console.WriteLine("usage is >compiler \"name of file\"");
                System.Console.WriteLine("for example");
                System.Console.WriteLine("compiler sample.cm");
                System.Environment.Exit(0);
            }
            tokens= scanBoy.scan(args[0]);
            parser parseBoy = new parser();
            treeNode ast= parseBoy.parseProgram(tokens);
            System.Console.WriteLine("ABSTRACT SYNTAX TREE:");
            parseBoy.printParseTree(ast, 0);
            System.Console.WriteLine("parse complete, press any key to exit");
            System.Console.ReadKey();
        }
    }
}
