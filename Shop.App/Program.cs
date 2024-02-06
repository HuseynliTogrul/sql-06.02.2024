using Microsoft.EntityFrameworkCore;
using Shop.App.Models;

ShopContext shopContext = new ShopContext();

ShohMenu();
int.TryParse(Console.ReadLine(), out int request);

while (request != 0)
{
    switch (request)
    {
        case 1:
            await CreateCategoryAsync();
            Console.WriteLine("Successful Created");
            break;
        case 2:
            await RemoveCategoryAsync();
            Console.WriteLine("Successful Removed");
            break;
        case 3:
            await UpdatedCategoryAsync();
            Console.WriteLine("Successful Updated");
            break;
        case 4:
            await GetAllCategoryAsync();
            Console.WriteLine("Showed");
            break;
        case 5:
            await GetCategoryAsync();
            Console.WriteLine("Found");
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

async Task CreateCategoryAsync()
{
    Console.WriteLine("Add Name");
    string Name = Console.ReadLine();

    while (string.IsNullOrWhiteSpace(Name))
    {
        Console.WriteLine("Add Name");
        Name = Console.ReadLine();
    }

    while (await shopContext.Categories.AnyAsync(x => x.Name.Trim().ToLower() == Name.Trim().ToLower()))
    {
        Console.WriteLine("Already category");
        Name = Console.ReadLine();
    }

    Category category = new Category()
    {
        Name = Name
    };

    await shopContext.Categories.AddAsync(category);
    await shopContext.SaveChangesAsync();
}

async Task UpdatedCategoryAsync()
{
    Console.WriteLine("Add Id");
    int.TryParse(Console.ReadLine(),out int Id);

    Category? category = await shopContext.Categories.Where(x=>x.Id==Id).FirstOrDefaultAsync();

    while (category == null)
    {
        Console.WriteLine("Category not found");
        return;
    }

    Console.WriteLine("Add Name");
    string? Name = Console.ReadLine();

    while (string.IsNullOrWhiteSpace(Name))
    {
        Console.WriteLine("Add Name");
        Name = Console.ReadLine();
    }

    while (await shopContext.Categories.AnyAsync(x => x.Name.Trim().ToLower() == Name.Trim().ToLower() && x.Id!=Id))
    {
        Console.WriteLine("Already category");
        Name = Console.ReadLine();
    }

    category.Name = Name;
    await shopContext.SaveChangesAsync();
}

async Task RemoveCategoryAsync()
{
    Console.WriteLine("Add Id");
    int.TryParse(Console.ReadLine(), out int Id);

    Category? category = await shopContext.Categories.Where(x => x.Id == Id).FirstOrDefaultAsync();

    while (category == null)
    {
        Console.WriteLine("Category not found");
        return;
    }

    category.IsDeleted = true;
    await shopContext.SaveChangesAsync();
}

async Task GetAllCategoryAsync()
{
    IQueryable<Category> query = shopContext.Categories.Where(x=>x.IsDeleted).AsNoTracking();

    IEnumerable<Category> categories = await query.ToListAsync();

    foreach (var item in categories)
    {
        Console.WriteLine($"Id:{item.Id} Name:{item.Name}");
    }
}

async Task GetCategoryAsync()
{
    Console.WriteLine("Add Id");
    int.TryParse(Console.ReadLine(), out int Id);

    IQueryable<Category> query = shopContext.Categories.Where(x => x.IsDeleted && x.Id==Id).AsNoTracking();

    Category? category = await query.FirstOrDefaultAsync();

    Console.WriteLine($"Id:{category.Id} Name:{category.Name}");
}