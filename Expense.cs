using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTreckerApi.Models
{
	public class Expense
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Please enter a value")]
		public string? Name { get; set; }

		[Required, MinLength(2, ErrorMessage = "Minimum length is 2")]
		public string? DescriptionExpense { get; set; }

		[Required]
		[Range(0.01, double.MaxValue, ErrorMessage = "Please enter a value")]
		[Column(TypeName = "decimal(8, 2)")]
		public decimal MoneyExpense { get; set; }

		public int? ExpenseCategoryId { get; set; }

		public ExpenseCategory? ExpenseCategory { get; set; }

    }
}
