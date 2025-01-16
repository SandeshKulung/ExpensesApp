using DataAccess.Services;
using DataModel.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.JSInterop;

namespace Week6MAUI.Components.Pages
{
    public partial class Dashboard
    {
        private decimal TotalRemainingAmount { get; set; }
        decimal totalIncomeAmount = 0;
        decimal totalDebtAmount = 0;
        decimal totalExpensesAmount = 0;
        decimal RemainingDebt = 0;
        private DateTime StartDate { get; set; } = DateTime.Today.AddDays(-30);
        private DateTime EndDate { get; set; } = DateTime.Today;
        string currencyType = Preferences.Get("Currency_Type", "Rs");
        private List<Transaction> Expenses { get; set; } = new List<Transaction>();
        private List<TransactionDto> FilteredExpenses { get; set; } = new List<TransactionDto>();
        private string[] ChartLabels = Array.Empty<string>();
        private int[] ChartData = Array.Empty<int>();
        protected override async Task OnInitializedAsync()
        {
            Expenses = await transactionService.GetAll();

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
     }).OrderByDescending(x=>x.Date)
     .ToList();
            totalIncomeAmount = transactionDtos.Sum(dto => dto.IncomeAmount);
            totalDebtAmount = transactionDtos.Sum(dto => dto.DebtAmount);
            totalExpensesAmount = transactionDtos.Sum(dto => dto.ExpensesAmount);
            TotalRemainingAmount = totalIncomeAmount + totalDebtAmount - totalExpensesAmount;

            FilteredExpenses = transactionDtos.Take(5).ToList();


            var amount = Expenses.Where(x => x.Type == "debt" || x.Type == "cleardebt").ToList();

            var totaldebt = Expenses.Where(x => x.Type == "debt").Sum(e => e.Amount);
            var ClearedDebt = Expenses.Where(x => x.Type == "cleardebt").Sum(e => e.Amount);
            RemainingDebt = totaldebt - ClearedDebt;

            var groupedData = Expenses.Where(x=>x.Type=="expense")
            .GroupBy(t => t.CategoryId)
            .Select(g => new { Category = g.Key, TotalAmount = g.Sum(t => t.Amount) })
            .ToList();
            ChartLabels = groupedData.Select(g => g.Category).ToArray();
            ChartData = groupedData.Select(g => g.TotalAmount).ToArray();
        }
        private void NavigateToIncomeTrack()
        {
            Nav.NavigateTo("/incometrack");
        }
        private void NavigateToExpenseTrack()
        {
            Nav.NavigateTo("/expensestrack");
        }
        private void NavigateToDebtTrack()
        {
            Nav.NavigateTo("/debttrack");
        }
        private void NavigateToReportTrack()
        {
            Nav.NavigateTo("/Report");
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("drawExpenseChart", ChartLabels, ChartData);
            }
        }
    }
}