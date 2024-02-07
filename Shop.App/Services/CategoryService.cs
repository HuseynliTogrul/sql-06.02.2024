
using Microsoft.EntityFrameworkCore;
using Shop.App.Models;
using System;

namespace Shop.App.Services
{
    public class CategoryService
    {
        ShopContext shopContext = new ShopContext();
        public async Task CreateCategoryAsync()
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
                Console.WriteLine("--Already category");
                Console.WriteLine("Add Name");
                Name = Console.ReadLine();
            }

            Category category = new Category()
            {
                Name = Name
            };

            await shopContext.Categories.AddAsync(category);
            await shopContext.SaveChangesAsync();
            Console.WriteLine("Successful Created");
        }

        public async Task UpdatedCategoryAsync()
        {
            Console.WriteLine("Add Id");
            int.TryParse(Console.ReadLine(), out int Id);

            Category? category = await shopContext.Categories.Where(x => x.Id == Id).FirstOrDefaultAsync();

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

            while (await shopContext.Categories.AnyAsync(x => x.Name.Trim().ToLower() == Name.Trim().ToLower() && x.Id != Id))
            {
                Console.WriteLine("--Already category");
                Console.WriteLine("Add Name");
                Name = Console.ReadLine();
            }

            category.Name = Name;
            await shopContext.SaveChangesAsync();
            Console.WriteLine("Successful Updated");
        }

        public async Task RemoveCategoryAsync()
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
            Console.WriteLine("Successful Removed");
        }

        public async Task GetAllCategoryAsync()
        {
            //IEnumerable<Category> categories = await shopContext.Categories.Where(x => !x.IsDeleted)
            //        .AsNoTracking().ToListAsync();


            IQueryable<Category> query = shopContext.Categories.Where(x => !x.IsDeleted).AsNoTracking();

            IEnumerable<Category> categories = await query.ToListAsync();

            foreach (var item in categories)
            {
                Console.WriteLine($"Id:{item.Id} Name:{item.Name}");
            }
        }

        public async Task GetByIdCategoryAsync()
        {
            Console.WriteLine("Add Id");
            int.TryParse(Console.ReadLine(), out int Id);

            Category? category = await shopContext.Categories.Where(x => !x.IsDeleted && x.Id == Id)
                 .AsNoTracking().FirstOrDefaultAsync();

            while (category == null)
            {
                Console.WriteLine("Category not found");
                return;
            }

            Console.WriteLine($"Id:{category.Id} Name:{category.Name}");
        }
    }
}
