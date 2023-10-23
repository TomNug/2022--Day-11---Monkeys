class Program
{
    public static void Main(string[] args)
    {
        string path = @"C:\Users\Tom\Documents\ASPNET Projects\2022- Day 11 - Monkeys\2022- Day 11 - Monkeys\data_test.txt";

        string[] instructions = System.IO.File.ReadAllLines(path);

        foreach(string instruction in instructions)
        {
            Console.WriteLine(instruction);
        }
    }
}