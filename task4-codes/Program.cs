namespace comp609lecture3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MainAppConsole app = new();
            app.Print();
            app.CalculateGovernmentTaxFor30Days();
            app.CalculateFarmDailyProfit();
            app.CalculateAverageWeightOfLivestocks();
            app.CalculateAverageDailyProfitPerCow();
            app.CalculateAverageDailyProfitPerSheep();
            app.GetCurrentDailyProfitOfAllSheep();
            app.GetCurrentDailyProfitOfAllCows();
            // Ask user for livestock type

            Console.Write("Enter the type of livestock (cow/sheep): ");
            string livestockType = Console.ReadLine().ToLower();

            // Ask user for quantity
            Console.Write("Enter the quantity: ");
            int quantity;
            if (!int.TryParse(Console.ReadLine(), out quantity))
            {
                Console.WriteLine("Invalid quantity. Please enter a valid integer.");
                return;
            }

            // Call the method with user input
            app.CalculateEstimatedDailyProfit(livestockType, quantity);
            app.QueryFarmLivestock();

            Console.WriteLine("=== Insert Record in the Database===");
            app.InsertRecord();
            Console.WriteLine("=== Delete a row in Database ===");
            app.DeleteRecord();
            Console.WriteLine("=== UPdate a row in database ===");
            app.UpdateRecord();

        }
    }
}
