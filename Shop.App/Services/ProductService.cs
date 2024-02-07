
using Microsoft.EntityFrameworkCore;
using Shop.App.Models;

namespace Shop.App.Services
{
    public class ProductService
    {
        ShopContext shopContext = new ShopContext();
        CategoryService categoryService = new CategoryService();

        public async Task CreateProductAsync()
        {
            Console.WriteLine("Add Name:");
            string? Name = Console.ReadLine();

            Console.WriteLine("Add Price:");
            int.TryParse(Console.ReadLine(), out int Price);

            Console.WriteLine("Add CategoryId:");
            int.TryParse(Console.ReadLine(), out int CategoryId);


            while (string.IsNullOrWhiteSpace(Name))
            {
                Console.WriteLine("Add Valid Name:");
                Name = Console.ReadLine();
            }

            while (!await shopContext.Categories.AnyAsync(x => x.Id == CategoryId))
            {
                Console.WriteLine("Category not Valid");
                int.TryParse(Console.ReadLine(), out CategoryId);
            }

            //await categoryService.GetAllCategoryAsync();


            Product product = new Product()
            {
                Name = Name,
                Price = Price,
                CategoryId = CategoryId
            };

            await shopContext.Products.AddAsync(product);
            await shopContext.SaveChangesAsync();
            Console.WriteLine("Successful Created");
        }

        public async Task UpdateProductAsync()
        {
            Console.WriteLine("Add Id:");
            int.TryParse(Console.ReadLine(), out int Id);

            Product? product = await shopContext.Products.Where(x => !x.IsDeleted && x.Id == Id).FirstOrDefaultAsync();

            Console.WriteLine("Add Name:");
            string? Name = Console.ReadLine();

            Console.WriteLine("Add Price:");
            int.TryParse(Console.ReadLine(), out int Price);

            Console.WriteLine("Add CategoryId:");
            int.TryParse(Console.ReadLine(), out int CategoryId);

            await categoryService.GetAllCategoryAsync();

            if (product == null)
            {
                Console.WriteLine("Product not found");
                return;
            }

            while (string.IsNullOrWhiteSpace(Name))
            {
                Console.WriteLine("Add Valid Name:");
                Name = Console.ReadLine();
            }

            while (!await shopContext.Categories.AnyAsync(x => x.Id == CategoryId))
            {
                Console.WriteLine("Category not Valid");
                int.TryParse(Console.ReadLine(), out CategoryId);
            }

            product.Name = Name;
            product.Price = Price;
            product.CategoryId = CategoryId;
            await shopContext.SaveChangesAsync();
            Console.WriteLine("Successful Updated");
        }

        public async Task RemoveProductAsync()
        {
            Console.WriteLine("Add Id:");
            int.TryParse(Console.ReadLine(), out int Id);

            Product? product = await shopContext.Products.Where(x => !x.IsDeleted && x.Id == Id).FirstOrDefaultAsync();

            if (product == null)
            {
                Console.WriteLine("Product not found");
                return;
            }

            product.IsDeleted = true;
            await shopContext.SaveChangesAsync();
            Console.WriteLine("Successful Removed");
        }

        public async Task GetAllProductAsync()
        {
            IEnumerable<Product> products = await shopContext.Products.Where(x => !x.IsDeleted)
                .Include(x => x.Category).AsNoTracking().ToListAsync();

            foreach (Product product in products)
            {
                Console.WriteLine($"Id:{product.Id} Name:{product.Name} Price:{product.Price} Category:{product.Category.Name} ");
            }
        }

        public async Task GetByIdProductAsync()
        {
            Console.WriteLine("Add Id:");
            int.TryParse(Console.ReadLine(),out int Id);
            Product? product = await shopContext.Products.Where(x => !x.IsDeleted && x.Id==Id)
                 .Include(x => x.Category).AsNoTracking().FirstOrDefaultAsync();

            if(product == null)
            {
                Console.WriteLine("Product not found");
                return;
            }

            Console.WriteLine($"Id:{product.Id} Name:{product.Name} Price:{product.Price} Category:{product.Category.Name} ");
        }
    }
}

