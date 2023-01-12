using ExpenseTreckerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTreckerApi.Data
{
	public class IncomeTreckerApiContext : DbContext
	{
		public IncomeTreckerApiContext(DbContextOptions<ExpenseTreckerApiContext> options)
			: base(options)
		{
			//Database.EnsureDeleted();
			Database.EnsureCreated();

			if (!IncomeCategory.Any())
			{

                IncomeCategory Salary = new IncomeCategory { Name = "Зарплата" };
                IncomeCategory Bonus = new IncomeCategory { Name = "Бонуси" };

                Incomes wage = new Incomes
                {
                    Name = "Місячна зарплата",
                    IncomesCategory = Salary,
                    MoneyIncomes = 10000,
                    DescriptionIncomes = "Місячна зарплата на роботі"
                };

                Incomes bonusSalary = new Incomes
                {
                    Name = "Бонус до зарплати",
                    IncomesCategory = Bonus,
                    MoneyIncomes = 2000,
                    DescriptionIncomes = "Бонус до зарплати"
                };

                Incomes harm = new Incomes
                {
                    Name = "Бонус за переробіток",
                    IncomesCategory = Bonus,
                    MoneyIncomes = 3000,
                    DescriptionIncomes = "Бонус за переробіток з шкідливими речовинами"
                };



                IncomeCategory.AddRange(Salary, Bonus);
                Income.AddRange(wage, bonusSalary, harm);
                SaveChanges();

            }
		}
        public DbSet<Incomes> Income { get; set; } = default!;
        public DbSet<IncomeCategory> IncomeCategory { get; set; } = default!;
    }
}
