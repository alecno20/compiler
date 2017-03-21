namespace compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            scanner scanBoy = new scanner();
            scanBoy.scan("sample1.cm");
            System.Console.ReadKey();
        }
    }
}
