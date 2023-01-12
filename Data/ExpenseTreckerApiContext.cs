using ExpenseTreckerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTreckerApi.Data
{
	public class ExpenseTreckerApiContext : DbContext
	{
		public ExpenseTreckerApiContext(DbContextOptions<ExpenseTreckerApiContext> options)
			: base(options)
		{
			Database.EnsureCreated();

			if (!ExpenseCategory.Any())
			{
				ExpenseCategory Entertainment = new ExpenseCategory { Name = "Розваги" };
                ExpenseCategory Products = new ExpenseCategory { Name = "Продукти" };

                Expense slides = new Expense { 
					Name = "Амереканські горки", 
					ExpenseCategory = Entertainment, 
					MoneyExpense = 700, 
					DescriptionExpense = "Витрата на амереканські горки" };

                Expense milk = new Expense { 
					Name = "Молоко", 
					ExpenseCategory = Products, 
					MoneyExpense = 40, 
					DescriptionExpense = "Витрата на покупку молока" };

                Expense burger = new Expense { 
					Name = "Бургер", 
					ExpenseCategory = Products, 
					MoneyExpense = 80, 
					DescriptionExpense = "Витрата на покупку бургера" };

                Expense.AddRange(slides, milk, burger);
                ExpenseCategory.AddRange(Entertainment, Products);

			}
		}
		public DbSet<Expense> Expense { get; set; } = default!;
		public DbSet<ExpenseCategory> ExpenseCategory { get; set; } = default!;
	}
}
