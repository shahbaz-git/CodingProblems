namespace ATMDemo
{
    /// <summary>
    /// Program class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main method.
        /// </summary>
        /// <param name="args">The args.</param>
        public static void Main(string[] args)
        {
            PrintDenominations();
        }

        /// <summary>
        /// Print denominations.
        /// </summary>
        public static void PrintDenominations()
        {
            Console.WriteLine("Please enter amount in multiples of 10, 50, 100.");

            string valueFromConsole = Console.ReadLine();
            _ = int.TryParse(valueFromConsole, out int amount);

            if (amount != 0 && amount % 10 != 0 && amount % 50 != 0 && amount % 100 != 0)
            {
                PrintDenominations();
            }

            CalculateDenominations(amount);

            Console.ReadLine();
        }

        /// <summary>
        /// Calculates the possible combinations for each payout.
        /// </summary>
        /// <param name="amount"></param>
        private static void CalculateDenominations(int amount)
        {
            int[] denominations = new int[] { 100, 50, 10 };
            for (int i = 0; i < denominations.Length; i++)
            {
                IList<int> numberList = new List<int>();

                for (int j = i; j < denominations.Length; j++)
                {
                    int totalAmount = numberList.Sum();

                    if (denominations[i] > amount)
                    {
                        Console.WriteLine($"{denominations[j]} x 0");
                        break;
                    }
                    else if (j == denominations.Length - 1)
                    {
                        int remainingAmount = amount - totalAmount;
                        int quotient = remainingAmount / denominations[j];
                        Console.WriteLine($"{denominations[j]} x {quotient}");
                        break;
                    }
                    else
                    {
                        numberList.Add(denominations[j]);
                        totalAmount = numberList.Sum();
                        if (totalAmount == amount)
                        {
                            Console.WriteLine($"{denominations[j]} x {numberList.Count(x => x == denominations[j])}");
                            break;
                        }
                    }
                    Console.Write($"{denominations[j]} x 1 + ");
                }

                Console.WriteLine();
            }
        }
    }
}