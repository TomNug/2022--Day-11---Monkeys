using System.Text;

class Monkey
{
    private int inspectionCount;
    private Queue<long> itemQueue;
    private string op;
    private bool operandIsOwnVariable;
    private long operand;
    private int testDivisbleBy;
    private int targetIfTrue;
    private int targetIfFalse;
    private int lcm;
    // Reads a string, and determines the operator and operand
    // E.g. Operation: new = old * 19
    private void CalculateOpAndOperand(string newOperation)
    {
        newOperation = newOperation.Replace("new = old ", "");
        string[] operationAndOperand = newOperation.Split(" ");
        // Operator is now first character
        op = operationAndOperand[0];
        if (operationAndOperand[1] == "old")
        {
            operandIsOwnVariable = true;
        }
        else
        {
            operandIsOwnVariable = false;
            operand = Convert.ToInt32(operationAndOperand[1]);
        }
    }
    
    // Constructor
    public Monkey(Queue<long> items, string newOperation, int div, int newTargetIfTrue, int newTargetIfFalse)
    {
        inspectionCount = 0;
        itemQueue = items;
        CalculateOpAndOperand(newOperation);
        testDivisbleBy = div;
        targetIfTrue = newTargetIfTrue;
        targetIfFalse = newTargetIfFalse;
    }

    // Outputs the list of items
    public void Output()
    {
        StringBuilder sb = new StringBuilder();
        foreach (int item in itemQueue)
        {
            sb.Append(" ");
            sb.Append(item.ToString());
        }
        Console.WriteLine(String.Format("Items: {0}", sb.ToString()));
    }

    // Received the given item, adds it to queue
    private void ReceiveThrow(long item)
    {
        itemQueue.Enqueue(item);
    }

    // Returns inspection count
    public int GetInspectionCount()
    {
        return inspectionCount;
    }
    
    public int GetDivisibleFactor()
    {
        return testDivisbleBy;
    }

    public void SetLCM(int newLCM)
    {
        lcm = newLCM;
    }
    // Performs the operation on the given worry level
    private long PerformOperation(long worryLevel)
    {
        if (operandIsOwnVariable)
        {
            operand = worryLevel;
        }
        if (op == "*")
        {
            return worryLevel * operand;
        } else if (op == "/")
        {
            return worryLevel / operand;
        } else if (op == "+")
        {
            return worryLevel + operand;
        } else
        {
            return worryLevel - operand;
        }
    }

    // One monkey's whole turn
    public void TakeTurn(List<Monkey> monkeys)
    {
        // While queue has items
        while (itemQueue.Count > 0)
        {
            // Inspect item
            inspectionCount++;
            long currentItem = itemQueue.Dequeue();
            // Perform operation on item
            long worryLevel = PerformOperation(currentItem);
            // Divide worry by three
            worryLevel = worryLevel % (long)lcm; // 3;
            // Check divisibilit
            if (worryLevel%testDivisbleBy == 0)
            {
                // Throw true
                monkeys[targetIfTrue].ReceiveThrow(worryLevel);
            }
            else
            {
                // Throw false
                monkeys[targetIfFalse].ReceiveThrow(worryLevel);
            }
        }

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
    public static Queue<long> ReadItems(string instruction)
    {
        Queue<long> items = new Queue<long>();

        instruction = instruction.Replace("  Starting items: ", "");
        string[] nums = instruction.Split(", ");
        foreach (string num in nums)
        {
            items.Enqueue(long.Parse(num));
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

    // Formatted output of monkeys
    public static void OutputMonkeys(List<Monkey> monkeys)
    {
        for (int i = 0; i < monkeys.Count; i++)
        {
            Console.WriteLine("Monkey {0}", i);
            monkeys[i].Output();
        }
    }

    // Reads list of instructions
    // Parses details
    // Creates list of monkey objects using details
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
            Queue<long> items = ReadItems(instructions[monkeyIndex + 1]);
            string operation = ReadOperation(instructions[monkeyIndex + 2]);
            int divisibleBy = ReadDivisibleBy(instructions[monkeyIndex + 3]);
            int monkeyIfTrue = ReadMonkeyIfTrue(instructions[monkeyIndex + 4]);
            int monkeyIfFalse = ReadMonkeyIfFalse(instructions[monkeyIndex + 5]);

            Monkey newMonkey = new Monkey(items, operation, divisibleBy, monkeyIfTrue, monkeyIfFalse);
            monkeys.Add(newMonkey);
        }
        
        return monkeys;
    }

    public static void CalculateAndSetLCM(List<Monkey> monkeys)
    {
        int lcm = 1;
        foreach(Monkey monkey in monkeys)
        {
            lcm *= monkey.GetDivisibleFactor();
        }
        foreach (Monkey monkey in monkeys)
        {
            monkey.SetLCM(lcm);
        }
    }

    public static void Main(string[] args)
    {
        string pathTest = @"C:\Users\Tom\Documents\ASPNET Projects\2022- Day 11 - Monkeys\2022- Day 11 - Monkeys\data_test.txt";
        string pathFull = @"C:\Users\Tom\Documents\ASPNET Projects\2022- Day 11 - Monkeys\2022- Day 11 - Monkeys\data_full.txt";

        string[] instructions = System.IO.File.ReadAllLines(pathFull);

        List<Monkey> monkeys = ParseMonkeys(instructions);

        CalculateAndSetLCM(monkeys);

        for (int round = 1; round <= 10000; round++)
        {
            //Console.WriteLine(String.Format("\nAfter round {0}:", round));
            for (int i = 0; i < monkeys.Count(); i++)
            {
                monkeys[i].TakeTurn(monkeys);
                //Console.WriteLine(String.Format("Monkey {0} has a count of {1}", i, monkeys[i].GetInspectionCount()));
            }
            
            //OutputMonkeys(monkeys);
        }

        int inspectionFirstPlace = 0;
        int inspectionSecondPlace = 0;
        for (int i = 0; i < monkeys.Count(); i++)
        {
            int count = monkeys[i].GetInspectionCount();
            if (count > inspectionFirstPlace)
            {
                inspectionSecondPlace = inspectionFirstPlace;
                inspectionFirstPlace = count;
            } 
            else if (count > inspectionSecondPlace)
            {
                inspectionSecondPlace = count;
            }
            //Console.WriteLine(String.Format("End of round {0}", i + 1));

        }
        long monkeyBusiness = (long)inspectionFirstPlace * (long)inspectionSecondPlace;
        Console.WriteLine(String.Format("Total monkey business is: {0}", monkeyBusiness));

    }
}

// No errors, calculates the monkey business
// Appears to read in all data correctly
// Unsure of how to proceed and debug