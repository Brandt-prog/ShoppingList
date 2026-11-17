string? input = "";
List<ShoppingItem> shoppingItems = new List<ShoppingItem>();

LoadItems(shoppingItems);

while (input != "6")
{
    PrintMenu();
    input = Console.ReadLine();

    switch (input)
    {
        case "1":
            AddItem(shoppingItems);
            break;
        case "2":
            ShowList(shoppingItems);
            break;
        case "3":
            RemoveItem(shoppingItems);
            break;
        case "4":
            ShowTotals(shoppingItems);
            break;
        case "5":
            Search(shoppingItems);
            break;
        case "6":
            Console.WriteLine("Bye!");
            SaveItems(shoppingItems);
            break;
        default:
            Console.WriteLine("Invalid choice");
            break;
    }
}

void SaveItems(List<ShoppingItem> it)
{
    File.WriteAllText("shopping.txt", "");
    foreach (ShoppingItem item in it)
    {
        File.AppendAllText("shopping.txt", $"{item.Name};{item.Quantity};{item.Price}\n");
    }
}

void LoadItems(List<ShoppingItem> it)
{
    try
    {
        string[] lines = File.ReadAllLines("shopping.txt");
        foreach (string line in lines)
        {
            string[] parts = line.Split(';');
            if (parts.Length == 3 && int.TryParse(parts[1], out int parsedQuantity) && double.TryParse(parts[2], out double parsedPrice))
            {
                it.Add(new ShoppingItem(parts[0], parsedQuantity, parsedPrice));
            }
            else
            {
                Console.WriteLine($"Skipped: {line}");
            }
        }
    }
    catch (FileNotFoundException)
    {
        Console.WriteLine("No items found");
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

void AddItem(List<ShoppingItem> it)
{
    Console.Write("Item: ");
    string item = Console.ReadLine() ?? "";
    if (item == "")
    {
        Console.WriteLine("No Item added");
        return;
    }
    Console.Write("Quantity: ");
    int quantity;
    string quantityText = Console.ReadLine() ?? "0";
    if (int.TryParse(quantityText, out int parsedQuantity))
    {
        quantity = parsedQuantity;
    }
    else
    {
        Console.WriteLine("Not a valid number");
        return;
    }
    Console.Write("Price: ");
    double price;
    string priceText = Console.ReadLine() ?? "";
    if (double.TryParse(priceText, out double parsedPrice))
    {
        price = parsedPrice;
    }
    else
    {
        Console.WriteLine("Not a valid number");
        return;
    }
    it.Add(new ShoppingItem(item, quantity, price));
    Console.WriteLine(it[it.Count - 1].Describe());
}

void ShowList(List<ShoppingItem> it)
{
    if (it.Count == 0)
    {
        Console.WriteLine("Your list is empty");
        return;
    }
    for (int i = 0; i < it.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {it[i].Describe()}");
    }
}

void RemoveItem(List<ShoppingItem> it)
{
    Console.Write("Number to remove: ");
    int number;
    string numberText = Console.ReadLine() ?? "";
    if (int.TryParse(numberText, out int parsedNumber))
    {
        number = parsedNumber;
    }
    else
    {
        Console.WriteLine("Not a valid number");
        return;
    }
    if (number < 1 || number > it.Count)
    {
        Console.WriteLine("Invalid item number");
        return;
    }
    Console.WriteLine($"Removed: {it[number - 1].Describe()}");
    it.RemoveAt(number - 1);
}

void ShowTotals(List<ShoppingItem> it)
{
    Console.WriteLine($"Items: {it.Count}, total quantity: {it.Sum(item => item.Quantity)}, total price: {it.Sum(item => item.Total)} kr");
}

void Search(List<ShoppingItem> it)
{
    Console.Write("Search for: ");
    string search = Console.ReadLine() ?? "";
    if (search == "")
    {
        Console.WriteLine("Nothing to search for");
        return;
    }
    ShoppingItem? found = it.FirstOrDefault(x => x.Name.Contains(search));
    if (found == null)
    {
        Console.WriteLine("Not Found");
        return;
    }
    Console.WriteLine($"Found: {found.Describe()}");
}

class ShoppingItem
{
    public string Name { get; set; } = "";

    private int quantity;
    public int Quantity
    {
        get { return quantity; }
        set
        {
            if (value < 1) quantity = 1;
            else { quantity = value; }
        }
    }

    public double Price { get; set; }

    public double Total => Price * Quantity;

    public ShoppingItem(string name, int quantity, double price)
    {
        Name = name;
        Quantity = quantity;
        Price = price;
    }

    public string Describe()
    {
        return $"{Quantity} x {Name} = {Total} kr";
    }
}