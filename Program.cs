string? input = "";
List<string> items = new List<string> {"milk", "eggs", "bread"};
List<int> quantities = new List<int>{2, 12, 2};

while (input != "6")
{
    //Console.Clear();
    Console.WriteLine("--- Shopping List ---");
    Console.WriteLine("1. Add Item");
    Console.WriteLine("2. Show list");
    Console.WriteLine("3. Remove Item");
    Console.WriteLine("4. Totals");
    Console.WriteLine("5. Search");
    Console.WriteLine("6. Quit");
    Console.Write("Choose: ");

    input = Console.ReadLine();

    switch (input)
    {
        case "1":
            Console.Write("Item: ");
            string item = Console.ReadLine() ?? "";
            if (item == "")
            {
                Console.WriteLine("No Item added");
                break;
            }
            Console.Write("Quantity: ");
            int quantity = int.Parse(Console.ReadLine() ?? "0");
            items.Add(item);
            quantities.Add(quantity);
            Console.WriteLine($"Added {quantity} x {item}");
            break;
        case "2":
            if (items.Count == 0)
            {
                Console.WriteLine("Your list is empty");
                break;
            }
            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {quantities[i]} x {items[i]}");
            }
            break;
        case "3":
            Console.Write("Number to remove: ");
            int number = int.Parse(Console.ReadLine() ?? "");
            if (number < 1 || number > items.Count)
            {
                Console.WriteLine("Invalid item number");
                break;
            }
            Console.WriteLine($"Removed {quantities[number - 1]} x {items[number - 1]}");
            items.RemoveAt(number - 1);
            quantities.RemoveAt(number - 1);
            break;
        case "4":
            Console.WriteLine($"Items: {items.Count}, total quantity: {quantities.Sum()}");
            break;
        case "5":
            Console.Write("Search for: ");
            string search = Console.ReadLine() ?? "";
            string? found = items.FirstOrDefault(x => x.Contains(search));
            if (found == null)
            {
                Console.WriteLine("Not Found");
                break;
            }
            int index = items.IndexOf(found);
            Console.WriteLine($"Found: {quantities[index]} x {found}");
            break;
        case "6":
            Console.WriteLine("Bye!");
            break;
        default:
            Console.WriteLine("Invalid choice");
            break;
    }
}