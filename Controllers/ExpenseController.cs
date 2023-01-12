using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseTreckerApi.Models;
using ExpenseTreckerApi.Data;

namespace ExpenseTreckerApi.Controllers
{
	[Route("/api/[controller]")]
	[ApiController]
	public class ExpenseController : ControllerBase
	{
		private readonly ExpenseTreckerApiContext _expenseContext;
        private readonly IncomeTreckerApiContext _incomeContext;

        public ExpenseController(IncomeTreckerApiContext income_context, ExpenseTreckerApiContext expense_context)
		{
            _incomeContext = income_context;
			_expenseContext = expense_context;

        }

        // GET: api/Expense
        [HttpGet]
        [NonAction]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpense()
		{
			return await _expenseContext.Expense.Include("ExpenseCategory").ToListAsync();
		}

        // GET: api/Income
        [HttpGet]
        [NonAction]
        public async Task<ActionResult<IEnumerable<Incomes>>> GetIncome()
        {
            return await _incomeContext.Income.Include("IncomeCategory").ToListAsync();
        }

        // GET: api/Expense/{id}
        [HttpGet("{id}")]
        [NonAction]
        public async Task<ActionResult<Expense>> GetExpense(int id)
		{
			var expense = await _expenseContext.Expense.Include("ExpenseCategory").FirstOrDefaultAsync(x => x.Id == id);

			if (expense == null)
			{
				return NotFound();
			}

			return expense;
		}

        // GET: api/Income/{id}
        [HttpGet("{id}")]
        [NonAction]
        public async Task<ActionResult<Incomes>> GetIncome(int id)
        {
            var income = await _incomeContext.Income.Include("IncomeCategory").FirstOrDefaultAsync(x => x.Id == id);

            if (income == null)
            {
                return NotFound();
            }

            return income;
        }

        // PUT: api/Expense/{id}
        [HttpPut("{id}")]
        [NonAction]
        public async Task<IActionResult> PutExpense(int id, Expense expense)
		{
			if (id != expense.Id)
			{
				return BadRequest();
			}

			_expenseContext.Entry(expense).State = EntityState.Modified;

			try
			{
				await _expenseContext.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ExpenseExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

        // PUT: api/Income/{id}
        [HttpPut("{id}")]
        [NonAction]
        public async Task<IActionResult> PutIncome(int id, Incomes incomes)
        {
            if (id != incomes.Id)
            {
                return BadRequest();
            }

            _incomeContext.Entry(incomes).State = EntityState.Modified;

            try
            {
                await _incomeContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncomeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Expense
        [HttpPost]
        [NonAction]
        public async Task<ActionResult<Expense>> PostExpense(Expense expense)
		{
            expense.ExpenseCategory = await _expenseContext.ExpenseCategory.Where(x => x.Id == expense.ExpenseCategoryId).FirstOrDefaultAsync();
			_expenseContext.Expense.Add(expense);
			await _expenseContext.SaveChangesAsync();

			return CreatedAtAction("GetExpense", new { id = expense.Id }, expense);
		}


        // POST: api/Income
        [HttpPost]
        [NonAction]
        public async Task<ActionResult<Incomes>> PostIncome(Incomes income)
        {
            income.IncomesCategory = await _incomeContext.IncomeCategory.Where(x => x.Id == income.IncomesCategoryId).FirstOrDefaultAsync();
            _incomeContext.Income.Add(income);
            await _incomeContext.SaveChangesAsync();

            return CreatedAtAction("GetIncome", new { id = income.Id }, income);
        }

        // DELETE: api/Expense/{id}
        [HttpDelete("{id}")]
        [NonAction]
        public async Task<IActionResult> DeleteExpense(int id)
		{
			var expense = await _expenseContext.Expense.FindAsync(id);
			if (expense == null)
			{
				return NotFound();
			}

			_expenseContext.Expense.Remove(expense);
			await _expenseContext.SaveChangesAsync();

			return NoContent();
		}

        // DELETE: api/Income/{id}
        [HttpDelete("{id}")]
        [NonAction]
        public async Task<IActionResult> DeleteIncome(int id)
        {
            var income = await _incomeContext.Income.FindAsync(id);
            if (income == null)
            {
                return NotFound();
            }

            _incomeContext.Income.Remove(income);
            await _incomeContext.SaveChangesAsync();

            return NoContent();
        }

        private bool ExpenseExists(int id)
		{
			return _expenseContext.Expense.Any(e => e.Id == id);
		}

        private bool IncomeExists(int id)
        {
            return _incomeContext.Income.Any(e => e.Id == id);
        }
    }
}
