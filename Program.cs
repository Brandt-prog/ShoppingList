string? input = "";

List<ShoppingItem> shoppingItems = new List<ShoppingItem>
{
    new ShoppingItem("milk", 2, 12.5),
    new ShoppingItem("eggs", 1, 30),
    new ShoppingItem("bread", 1, 25)
};

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