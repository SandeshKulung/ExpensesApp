using DataAccess.Services;
using DataModel.Model;

namespace Week6MAUI.Components.Pages
{
    public partial class Report
    {
        private DateTime StartDate { get; set; } = DateTime.Today.AddDays(-30);
        private DateTime EndDate { get; set; } = DateTime.Today;
        private string type { get; set; } = "all";
        private string label { get; set; } = "all";
        private decimal TotalExpense { get; set; }
        private List<Category> Categories = new();
        private List<Transaction> Expenses { get; set; } = new List<Transaction>();
        private List<TransactionDto> FilteredExpenses { get; set; } = new List<TransactionDto>();
        decimal totalIncomeAmount = 0;
        decimal totalDebtAmount = 0;
        decimal totalExpensesAmount = 0;

        string currencyType = Preferences.Get("Currency_Type", "Rs");
        protected override async Task OnInitializedAsync()
        {
            Expenses = await transactionService.GetAll();
            Categories = await categoryService.GetAll();
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            List<TransactionDto> transactionDtos = Expenses
     .Where(e => e.Date >= StartDate && e.Date <= EndDate.AddDays(1))
     .Select(e => new TransactionDto
     {
         TransactionId = e.TransactionId,
         CategoryId = e.CategoryId,
         Date = e.Date,
         Note = e.Note,
         Type = e.Type,
         // Map amount based on Type
         IncomeAmount = e.Type == "income" ? e.Amount : 0,
         DebtAmount = e.Type == "debt" ? e.Amount : 0,
         ExpensesAmount = e.Type == "expense" ? e.Amount : 0
     })
     .ToList();
            if (type != "all") { 
                transactionDtos = transactionDtos.Where(x => x.Type == type).ToList();
            }
            if (label!="all")
            {
                transactionDtos = transactionDtos.Where(x => x.CategoryId == label).ToList();
            }
            FilteredExpenses = transactionDtos.ToList();
            totalIncomeAmount = transactionDtos.Sum(dto => dto.IncomeAmount);
            totalDebtAmount = transactionDtos.Sum(dto => dto.DebtAmount);
            totalExpensesAmount = transactionDtos.Sum(dto => dto.ExpensesAmount);
            TotalExpense = totalIncomeAmount + totalDebtAmount - totalExpensesAmount;
        }

        private void ClearFilter()
        {
            StartDate = DateTime.Today.AddDays(-30);
            EndDate = DateTime.Today;
            ApplyFilter();
        }

        private void ExportPdf()
        {
            FilteredExpenses = FilteredExpenses.OrderBy(x => x.Date).ToList();
        }

        private void ExportCsv()
        {
            FilteredExpenses = FilteredExpenses.OrderByDescending(x => x.Date).ToList();
        }

        private void ViewSummaryChart()
        {
            // Navigate to or display summary chart
        }
    }
}