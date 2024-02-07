using Microsoft.EntityFrameworkCore;
using Shop.App.Models;
using Shop.App.Services;

CategoryService categoryService = new CategoryService();
ProductService productService = new ProductService();

ShowMenu();
int.TryParse(Console.ReadLine(), out int request);

int ProductChoose = 0;
int CategoryChoose = 0;


while (request != 0)
{
    switch (request)
    {
        case 1:
            ProductMenu();
            ProductChoose = int.Parse(Console.ReadLine());

            while (ProductChoose != 0)
            {
                switch (ProductChoose)
                {
                    case 1:
                        await productService.CreateProductAsync();
                        break;
                    case 2:
                        await productService.RemoveProductAsync();
                        break;
                    case 3:
                        await productService.UpdateProductAsync();
                        break;
                    case 4:
                        await productService.GetAllProductAsync();
                        break;
                    case 5:
                        await productService.GetByIdProductAsync();
                        break;

                    default:
                        Console.WriteLine("Duzgun deyer daxil edin!");
                        break;
                }

                ProductMenu();
                ProductChoose = int.Parse(Console.ReadLine());
            }
            break;
        case 2:
            CategoryMenu();
            CategoryChoose = int.Parse(Console.ReadLine());

            while (CategoryChoose != 0)
            {
                switch (CategoryChoose)
                {
                    case 1:
                        await categoryService.CreateCategoryAsync();
                        break;
                    case 2:
                        await categoryService.RemoveCategoryAsync();
                        break;
                    case 3:
                        await categoryService.UpdatedCategoryAsync();
                        break;
                    case 4:
                        await categoryService.GetAllCategoryAsync();
                        break;
                    case 5:
                        await categoryService.GetByIdCategoryAsync();
                        break;

                    default:
                        Console.WriteLine("Duzgun deyer daxil edin!");
                        break;
                }

                CategoryMenu();
                CategoryChoose = int.Parse(Console.ReadLine());
            }
            break;
    }
}

void ShowMenu()
{
    Console.WriteLine("Add a process");
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("1.Product");
    Console.WriteLine("2.Category");
    Console.WriteLine("0.Close");
    Console.ForegroundColor = ConsoleColor.White;
}
void ProductMenu()
{
    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.WriteLine("1.Create Product");
    Console.WriteLine("2.Remove Product");
    Console.WriteLine("3.Update Product");
    Console.WriteLine("4.GetAll Product");
    Console.WriteLine("5.GetById Product");
    Console.WriteLine("0.Close");
    Console.ForegroundColor = ConsoleColor.White;
}
void CategoryMenu()
{
    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.WriteLine("1.Create Category");
    Console.WriteLine("2.Remove Category");
    Console.WriteLine("3.Update Category");
    Console.WriteLine("4.GetAll Category");
    Console.WriteLine("5.GetById Category");
    Console.WriteLine("0.Close");
    Console.ForegroundColor = ConsoleColor.White;
}