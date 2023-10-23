class Monkey
{
    private int inspectionCount;
    private Queue<int> itemQueue;
    private string operation;
    private int testDivisbleBy;
    private int targetIfTrue;
    private int targetIfFalse;

    public Monkey(Queue<int> items, string newOperation, int div, int newTargetIfTrue, int newTargetIfFalse)
    {
        inspectionCount = 0;
        itemQueue = items;
        operation = newOperation;
        testDivisbleBy = div;
        targetIfTrue = newTargetIfTrue;
        targetIfFalse = newTargetIfFalse;
    }
}


class Program
{
    public static int ReadNum(string instruction)
    {
        int spaceIndex = instruction.IndexOf(' ');
        string monkeyNum = instruction.Substring(spaceIndex + 1, instruction.Length - spaceIndex - 2);
        return Convert.ToInt32(monkeyNum);
    }
    public static List<Monkey> ParseMonkeys(string[] instructions)
    {
        List<Monkey> monkeys = new List<Monkey>();

        // Each monkey is 6 lines
        // a line between each monkey
        int numMonkeys = (instructions.Length + 1) / 7;
        
        for (int i = 0; i < numMonkeys; i++)
        {
            int monkeyIndex = 7 * i; // 6 lines plus a space per monkey
            int monkeyNum = ReadNum(instructions[monkeyIndex]);
            Console.WriteLine(monkeyNum);
        }
        
        return monkeys;
    }
    public static void Main(string[] args)
    {
        string path = @"C:\Users\Tom\Documents\ASPNET Projects\2022- Day 11 - Monkeys\2022- Day 11 - Monkeys\data_test.txt";

        string[] instructions = System.IO.File.ReadAllLines(path);

        List<Monkey> monkeys = ParseMonkeys(instructions);
    }
}