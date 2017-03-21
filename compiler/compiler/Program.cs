namespace compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Collections.Generic.List<token> tokens= new System.Collections.Generic.List<token>();
            scanner scanBoy = new scanner();
            tokens= scanBoy.scan("sampleEmpty.cm");
            parser parseBoy = new parser();
            treeNode ast= parseBoy.parseProgram(tokens);
            System.Console.ReadKey();
        }
    }
}
