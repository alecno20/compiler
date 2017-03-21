namespace compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Collections.Generic.List<token> tokens= new System.Collections.Generic.List<token>();
            scanner scanBoy = new scanner();
            tokens= scanBoy.scan("sample.cm");
            System.Collections.Generic.List<treeNode> ast = new System.Collections.Generic.List<treeNode>();
            parser parseBoy = new parser();
            parseBoy.parseProgram(ast, tokens);
            System.Console.ReadKey();
        }
    }
}
