using Shop.App.Models;

ShopContext shopContext = new ShopContext();

ShohMenu();
int.TryParse(Console.ReadLine(), out int request);

while (request != 0)
{
    switch (request)
    {
        case 1:
            await CreateCategory();
            Console.WriteLine("Successful");
            break;
        case 2:

            break;

        default:
            Console.WriteLine("Duzgun deyer daxil edin!");
            break;
    }

    ShohMenu();
    int.TryParse(Console.ReadLine(), out request);
}

void ShohMenu()
{
    Console.WriteLine("1.Create Category");
    Console.WriteLine("2.Remove Category");
    Console.WriteLine("3.Update Category");
    Console.WriteLine("4.GetAll Category");
    Console.WriteLine("5.GetById Category");

    Console.WriteLine("6.Create Product");
    Console.WriteLine("7.Remove Product");
    Console.WriteLine("8.Update Product");
    Console.WriteLine("9.GetAll Product");
    Console.WriteLine("10.GetById Product");

    Console.WriteLine("0.Close");
}

async Task CreateCategory()
{
    Console.WriteLine("Add Name");
    string Name = Console.ReadLine();

    while (string.IsNullOrWhiteSpace(Name))
    {
        Console.WriteLine("Add Name");
        Name = Console.ReadLine();
    }

    Category category = new Category()
    {
        Name = Name
    };

    await shopContext.Categories.AddAsync(category);
    await shopContext.SaveChangesAsync();
}

