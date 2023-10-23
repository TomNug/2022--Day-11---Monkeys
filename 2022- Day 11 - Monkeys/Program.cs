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
    // Reads the string and returns the integer
    // E.g. "Monkey 0:" -> 0
    public static int ReadNum(string instruction)
    {
        int spaceIndex = instruction.IndexOf(' ');
        string monkeyNum = instruction.Substring(spaceIndex + 1, instruction.Length - spaceIndex - 2);
        return Convert.ToInt32(monkeyNum);
    }
    // Reads the string and returns queue of integers
    // E.g. "Starting items: 54, 65, 75, 74" -> {54, 65, 75, 74}
    public static Queue<int> ReadItems(string instruction)
    {
        Queue<int> items = new Queue<int>();

        instruction = instruction.Replace("  Starting items: ", "");
        string[] nums = instruction.Split(", ");
        foreach (string num in nums)
        {
            items.Enqueue(Convert.ToInt32(num));
        }
        return items;
    }
    // Reads the string operation from the string
    // E.g. "Operation: new = old + 6" -> "new = old + 6"
    public static string ReadOperation(string instruction)
    {
        return instruction.Replace("  Operation: ", "");
    }
    // Reads the integer divisor from the string
    // E.g. "  Test: divisible by 19" -> 19
    public static int ReadDivisibleBy(string instruction)
    {
        instruction = instruction.Replace("  Test: divisible by ", "");
        return Convert.ToInt32(instruction);
    }
    // Reads the integer monkey target from the string
    // E.g. "    If true: throw to monkey 2" -> 2
    public static int ReadMonkeyIfTrue(string instruction)
    {
        instruction = instruction.Replace("    If true: throw to monkey ", "");
        return Convert.ToInt32(instruction);
    }
    // Reads the integer monkey target from the string
    // E.g. "    If false: throw to monkey 0" -> 0
    public static int ReadMonkeyIfFalse(string instruction)
    {
        instruction = instruction.Replace("    If false: throw to monkey ", "");
        return Convert.ToInt32(instruction);
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
            //Console.WriteLine(monkeyNum);
            Queue<int> items = ReadItems(instructions[monkeyIndex + 1]);
            string operation = ReadOperation(instructions[monkeyIndex + 2]);
            Console.WriteLine(operation);
            int divisibleBy = ReadDivisibleBy(instructions[monkeyIndex + 3]);
            Console.WriteLine(divisibleBy);
            int monkeyIfTrue = ReadMonkeyIfTrue(instructions[monkeyIndex + 4]);
            Console.WriteLine(monkeyIfTrue);
            int monkeyIfFalse = ReadMonkeyIfFalse(instructions[monkeyIndex + 5]);
            Console.WriteLine(monkeyIfFalse);
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