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

        public ExpenseController(ExpenseTreckerApiContext expense_context)
		{
			_expenseContext = expense_context;

        }

        // GET: api/Expense
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpense()
		{
			return await _expenseContext.Expense.Include("ExpenseCategory").ToListAsync();
		}

        // GET: api/Expense/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpense(int id)
		{
			var expense = await _expenseContext.Expense.Include("ExpenseCategory").FirstOrDefaultAsync(x => x.Id == id);

			if (expense == null)
			{
				return NotFound();
			}

			return expense;
		}

        // PUT: api/Expense/{id}
        [HttpPut("{id}")]
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

        // POST: api/Expense
        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense(Expense expense)
		{
            expense.ExpenseCategory = await _expenseContext.ExpenseCategory.Where(x => x.Id == expense.ExpenseCategoryId).FirstOrDefaultAsync();
			_expenseContext.Expense.Add(expense);
			await _expenseContext.SaveChangesAsync();

			return CreatedAtAction("GetExpense", new { id = expense.Id }, expense);
		}

        // DELETE: api/Expense/{id}
        [HttpDelete("{id}")]
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

        private bool ExpenseExists(int id)
		{
			return _expenseContext.Expense.Any(e => e.Id == id);
		}

    }
}
