using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using static System.Console;

namespace comp609lecture3
{
    public class MainAppConsole
    {
        public ObservableCollection<Store> _stores { get; set; }
        public readonly Database _database;

        public MainAppConsole()
        {
            _stores = new ObservableCollection<Store>();
            _database = new Database();
            ReadDB(); // Fill the collection
        }

        public void CalculateGovernmentTaxFor30Days()
        {
            double totalTax = 0;

            // Calculate tax for all stores
            foreach (var store in _stores)
            {
                double tax = store.Weight * 0.2;
                totalTax += tax;
            }
            Console.WriteLine($"30-day government tax: ${totalTax * 30}");
        }

        public void CalculateFarmDailyProfit()
        {
            double totalProfit = 0;

            // Calculate profit for each item in the store collection
            foreach (var item in _stores)
            {
                if (item is Cow cow)
                {
                    double income = cow.Milk * 9.4;
                    double tax = cow.Weight * 0.2;
                    double profit = income - cow.Cost - tax;
                    totalProfit += profit;
                }
                else if (item is Sheep sheep)
                {
                    double income = sheep.Wool * 6.2;
                    double tax = sheep.Weight * 0.2;
                    double profit = income - sheep.Cost - tax;
                    totalProfit += profit;
                }
            }

            Console.WriteLine($"Total farm daily profit: ${totalProfit}");
        }

        public void CalculateAverageWeightOfLivestocks()
        {
            double totalWeight = 0;
            int totalLivestockCount = 0;

            // Calculate total weight and count of all livestock
            foreach (var item in _stores)
            {
                totalWeight += item.Weight;
                totalLivestockCount++;
            }

            // Check to avoid division by zero
            if (totalLivestockCount == 0)
            {
                Console.WriteLine("No livestock available to calculate average weight.");
                return;
            }

            double averageWeight = totalWeight / totalLivestockCount;
            Console.WriteLine($"Average weight of all livestock: {averageWeight}");
        }

        public void CalculateAverageDailyProfitPerCow()
        {
            double totalProfit = 0;
            int cowCount = 0;

            foreach (var item in _stores)
            {
                if (item is Cow cow)
                {
                    double income = cow.Milk * 9.4;
                    double tax = cow.Weight * 0.2;
                    double profit = income - cow.Cost - tax;
                    totalProfit += profit;
                    cowCount++;
                }
            }

            if (cowCount == 0)
            {
                Console.WriteLine("No cows available to calculate average daily profit.");
                return;
            }

            double averageProfit = totalProfit / cowCount;
            Console.WriteLine($"Average daily profit per cow: ${averageProfit}");
        }

        public void CalculateAverageDailyProfitPerSheep()
        {
            double totalProfit = 0;
            int sheepCount = 0;

            foreach (var item in _stores)
            {
                if (item is Sheep sheep)
                {
                    double income = sheep.Wool * 6.2;
                    double tax = sheep.Weight * 0.2;
                    double profit = income - sheep.Cost - tax;
                    totalProfit += profit;
                    sheepCount++;
                }
            }

            if (sheepCount == 0)
            {
                Console.WriteLine("No sheep available to calculate average daily profit.");
                return;
            }

            double averageProfit = totalProfit / sheepCount;
            Console.WriteLine($"Average daily profit per sheep: ${averageProfit}");
        }

        public void GetCurrentDailyProfitOfAllSheep()
        {
            double totalProfit = 0;

            foreach (var item in _stores)
            {
                if (item is Sheep sheep)
                {
                    double income = sheep.Wool * 6.2;
                    double tax = sheep.Weight * 0.2;
                    double profit = income - sheep.Cost - tax;
                    totalProfit += profit;
                }
            }

            Console.WriteLine($"Current daily profit of all sheep: ${totalProfit}");
        }

        public void GetCurrentDailyProfitOfAllCows()
        {
            double totalProfit = 0;

            foreach (var item in _stores)
            {
                if (item is Cow cow)
                {
                    double income = cow.Milk * 9.4;
                    double tax = cow.Weight * 0.2;
                    double profit = income - cow.Cost - tax;
                    totalProfit += profit;
                }
            }

            Console.WriteLine($"Current daily profit of all cows: ${totalProfit}");
        }

        public void CalculateEstimatedDailyProfit(string livestockType, int quantity)
        {
            double averageProfitPerAnimal = 0;

            if (livestockType.ToLower() == "cow")
            {
                CalculateAverageDailyProfitPerCow(); // Print average profit per cow
                averageProfitPerAnimal = _stores.OfType<Cow>().Average(cow => cow.Milk * 9.4 - cow.Cost - cow.Weight * 0.2);
            }
            else if (livestockType.ToLower() == "sheep")
            {
                CalculateAverageDailyProfitPerSheep(); // Print average profit per sheep
                averageProfitPerAnimal = _stores.OfType<Sheep>().Average(sheep => sheep.Wool * 6.2 - sheep.Cost - sheep.Weight * 0.2);
            }
            else
            {
                WriteLine("Invalid livestock type.");
                return;
            }

            double estimatedDailyProfit = averageProfitPerAnimal * quantity;
            WriteLine($"Buying {quantity} {livestockType}s would bring in estimated daily profit of {estimatedDailyProfit.ToString("0.00")}");
        }

        public void QueryFarmLivestock()
        {
            Write("Enter livestock type (Cow/Sheep): ");
            string livestockTypeInput = ReadLine().ToLower();
            string livestockType = "";
            if (livestockTypeInput == "cow" || livestockTypeInput == "sheep")
            {
                livestockType = livestockTypeInput;
            }
            else
            {
                WriteLine("Invalid input.");
                return;
            }

            Write("Enter livestock colour (All/black/red/white): ");
            string livestockColorInput = ReadLine().ToLower();
            string livestockColor = "";
            if (livestockColorInput == "all" || livestockColorInput == "black" || livestockColorInput == "red" || livestockColorInput == "white")
            {
                livestockColor = livestockColorInput;
            }
            else
            {
                WriteLine("Invalid input.");
                return;
            }

            IEnumerable<Store> livestock = _stores;

            if (livestockType == "cow")
            {
                livestock = _stores.OfType<Cow>();
            }
            else if (livestockType == "sheep")
            {
                livestock = _stores.OfType<Sheep>();
            }

            if (livestockColor != "all")
            {
                livestock = livestock.Where(animal => animal.Colour.ToLower() == livestockColor);
            }

            double totalLivestock = _stores.Count();
            double totalSelectedLivestock = livestock.Count();
            double percentageSelected = (totalSelectedLivestock / totalLivestock) * 100;

            double totalWeight = livestock.Sum(animal => animal.Weight);
            double totalTax = livestock.Sum(animal => animal.Weight * 0.2);
            double totalProfit = livestock.Sum(animal => livestockType == "cow" ? ((Cow)animal).Milk * 9.4 - animal.Cost - animal.Weight * 0.2 : ((Sheep)animal).Wool * 6.2 - animal.Cost - animal.Weight * 0.2);
            double totalProduce = livestock.Sum(animal => livestockType == "cow" ? ((Cow)animal).Milk : ((Sheep)animal).Wool);

            WriteLine($"Number of livestock ({livestockType} in {livestockColor} colour): {totalSelectedLivestock}");
            WriteLine($"Percentage of selected livestock from all: {percentageSelected}%");
            WriteLine($"Daily tax of selected livestock: ${totalTax}");
            WriteLine($"Profit: ${totalProfit}");
            WriteLine($"Average weight: {totalWeight / totalSelectedLivestock} kg");
            WriteLine($"Produce amount: {totalProduce} kg");
        }

        public void DeleteRecord()
        {
            while (true)
            {
                Console.Write("Enter livestock id: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }

                var recordToDelete = _stores.FirstOrDefault(s => s.Id == id);
                if (recordToDelete == null)
                {
                    Console.WriteLine($"Non-existent livestock id: {id}");
                    continue;
                }

                int result = _database.DeleteItem(recordToDelete);
                if (result > 0)
                {
                    Console.WriteLine($"Record deleted: {recordToDelete}");
                    _stores.Remove(recordToDelete);
                }
                else
                {
                    Console.WriteLine("Failed to delete record from database.");
                }

                break; // Exit the loop after attempting to delete the record
            }
        }
        public void InsertRecord()
        {
            while (true)
            {
                Console.Write("Enter table name to insert into (cow/sheep): ");
                string tableName = Console.ReadLine().ToLower();

                Store newLivestock;
                if (tableName == "cow")
                {
                    newLivestock = new Cow();
                }
                else if (tableName == "sheep")
                {
                    newLivestock = new Sheep();
                }
                else
                {
                    Console.WriteLine("Invalid table name.");
                    continue; // Repeat the loop to ask for input again
                }

                // Prompt user to enter details of the new livestock (cost, weight, color, etc.)
                Console.Write("Enter cost: ");
                double cost = Util.ParseDouble(Console.ReadLine());
                if (cost == Util.BAD_DOUBLE)
                {
                    Console.WriteLine("Invalid input for cost.");
                    continue; // Repeat the loop to ask for input again
                }
                newLivestock.Cost = cost;

                Console.Write("Enter weight: ");
                double weight = Util.ParseDouble(Console.ReadLine());
                if (weight == Util.BAD_DOUBLE)
                {
                    Console.WriteLine("Invalid input for weight.");
                    continue; // Repeat the loop to ask for input again
                }
                newLivestock.Weight = weight;

                string colour;
                while (true)
                {
                    Console.Write("Enter livestock colour (black/red/white): ");
                    colour = Console.ReadLine().ToLower();
                    if (colour != "black" && colour != "red" && colour != "white")
                    {
                        Console.WriteLine("Invalid input for colour.");
                        continue; // Repeat the loop to ask for input again
                    }
                    break; // Exit the loop if colour is valid
                }
                newLivestock.Colour = colour;

                // If the livestock is a cow, ask for milk
                if (newLivestock is Cow cow)
                {
                    Console.Write("Enter milk: ");
                    double milk = Util.ParseDouble(Console.ReadLine());
                    if (milk == Util.BAD_DOUBLE)
                    {
                        Console.WriteLine("Invalid input for milk.");
                        continue; // Repeat the loop to ask for input again
                    }
                    cow.Milk = milk;
                }
                // If the livestock is a sheep, ask for wool
                else if (newLivestock is Sheep sheep)
                {
                    Console.Write("Enter wool: ");
                    double wool = Util.ParseDouble(Console.ReadLine());
                    if (wool == Util.BAD_DOUBLE)
                    {
                        Console.WriteLine("Invalid input for wool.");
                        continue; // Repeat the loop to ask for input again
                    }
                    sheep.Wool = wool;
                }

                // Add the new record to the database
                int result = _database.InsertItem(newLivestock);
                if (result > 0)
                {
                    Console.WriteLine($"Record added: {newLivestock}");
                    _stores.Add(newLivestock);
                }
                else
                {
                    Console.WriteLine("Failed to insert record into database.");
                }

                break; // Exit the loop after adding the record
            }
        }
        public void UpdateRecord()
        {
            while (true)
            {
                Console.Write("Enter livestock id: ");
                if (!int.TryParse(Console.ReadLine(), out int livestockId))
                {
                    Console.WriteLine("Invalid input for livestock id.");
                    continue; // Repeat the loop to ask for input again
                }

                // Find the record in the _stores collection
                Store livestock = _stores.FirstOrDefault(item => item.Id == livestockId);
                if (livestock == null)
                {
                    Console.WriteLine($"Non-existent livestock id: {livestockId}");
                    continue; // Repeat the loop to ask for input again
                }

                // Display current property values
                Console.WriteLine($"Current cost: {livestock.Cost}");
                Console.WriteLine($"Current weight: {livestock.Weight}");
                Console.WriteLine($"Current colour: {livestock.Colour}");

                // Prompt for and update the properties of the livestock object
                Console.Write("Enter new cost (leave blank to keep current): ");
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input) && double.TryParse(input, out double cost))
                {
                    livestock.Cost = cost;
                }

                Console.Write("Enter new weight (leave blank to keep current): ");
                input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input) && double.TryParse(input, out double weight))
                {
                    livestock.Weight = weight;
                }

                Console.Write("Enter new colour (black/red/white, leave blank to keep current): ");
                input = Console.ReadLine().ToLower();
                if (!string.IsNullOrWhiteSpace(input) && (input == "black" || input == "red" || input == "white"))
                {
                    livestock.Colour = input;
                }

                // If the livestock is a cow, ask for milk
                if (livestock is Cow cow)
                {
                    Console.WriteLine($"Current milk: {cow.Milk}");
                    Console.Write("Enter new milk (leave blank to keep current): ");
                    input = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(input) && double.TryParse(input, out double milk))
                    {
                        cow.Milk = milk;
                    }
                }
                // If the livestock is a sheep, ask for wool
                else if (livestock is Sheep sheep)
                {
                    Console.WriteLine($"Current wool: {sheep.Wool}");
                    Console.Write("Enter new wool (leave blank to keep current): ");
                    input = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(input) && double.TryParse(input, out double wool))
                    {
                        sheep.Wool = wool;
                    }
                }

                // Update the record in the database
                int result = _database.UpdateItem(livestock);
                if (result > 0)
                {
                    Console.WriteLine($"Record updated: {livestock.GetType().Name} {livestock.Id} {livestock.Cost} {livestock.Weight} {livestock.Colour}");
                    break; // Exit the loop if update is successful
                }
                else
                {
                    Console.WriteLine("Failed to update record in database.");
                }
            }
        }


        public void Print()
        {
            WriteLine("==Livestock List==");
            _stores.ToList().ForEach(item => WriteLine(item));
        }
        public void ReadDB()
        {
            _database.ReadItems().ForEach(x => _stores.Add(x));
        }
    }
}

