string? input = "";
List<string> items = new List<string> { "milk", "eggs", "bread" };
List<int> quantities = new List<int> { 2, 12, 2 };

while (input != "6")
{
    PrintMenu();
    input = Console.ReadLine();

    switch (input)
    {
        case "1":
            AddItem(items, quantities);
            break;
        case "2":
            ShowList(items, quantities);
            break;
        case "3":
            RemoveItem(items, quantities);
            break;
        case "4":
            ShowTotals(items, quantities);
            break;
        case "5":
            Search(items, quantities);
            break;
        case "6":
            Console.WriteLine("Bye!");
            break;
        default:
            Console.WriteLine("Invalid choice");
            break;
    }
}

void PrintMenu()
{
    Console.WriteLine("--- Shopping List ---");
    Console.WriteLine("1. Add Item");
    Console.WriteLine("2. Show list");
    Console.WriteLine("3. Remove Item");
    Console.WriteLine("4. Totals");
    Console.WriteLine("5. Search");
    Console.WriteLine("6. Quit");
    Console.Write("Choose: ");
}

void AddItem(List<string> it, List<int> q)
{
    Console.Write("Item: ");
    string item = Console.ReadLine() ?? "";
    if (item == "")
    {
        Console.WriteLine("No Item added");
        return;
    }
    Console.Write("Quantity: ");
    int quantity = int.Parse(Console.ReadLine() ?? "0");
    it.Add(item);
    q.Add(quantity);
    Console.WriteLine($"Added {quantity} x {item}");
}

void ShowList(List<string> it, List<int> q)
{
    if (it.Count == 0)
    {
        Console.WriteLine("Your list is empty");
        return;
    }
    for (int i = 0; i < it.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {q[i]} x {it[i]}");
    }
}

void RemoveItem(List<string> it, List<int> q)
{
    Console.Write("Number to remove: ");
    int number = int.Parse(Console.ReadLine() ?? "");
    if (number < 1 || number > it.Count)
    {
        Console.WriteLine("Invalid item number");
        return;
    }
    Console.WriteLine($"Removed {q[number - 1]} x {it[number - 1]}");
    it.RemoveAt(number - 1);
    q.RemoveAt(number - 1);
}

void ShowTotals(List<string> it, List<int> q)
{
    Console.WriteLine($"Items: {it.Count}, total quantity: {q.Sum()}");
}

void Search(List<string> it, List<int> q)
{
    Console.Write("Search for: ");
    string search = Console.ReadLine() ?? "";
    if (search == "")
    {
        Console.WriteLine("Nothing to search for");
        return;
    }
    string? found = it.FirstOrDefault(x => x.Contains(search));
    if (found == null)
    {
        Console.WriteLine("Not Found");
        return;
    }
    int index = it.IndexOf(found);
    Console.WriteLine($"Found: {q[index]} x {found}");
}