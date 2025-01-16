using DataAccess.Services;
using DataModel.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Week6MAUI.Components.Pages
{
    public partial class Dashboard
    {
        private decimal TotalRemainingAmount { get; set; }
        decimal totalIncomeAmount = 0;
        decimal totalDebtAmount = 0;
        decimal totalExpensesAmount = 0;
        string currencyType = Preferences.Get("Currency_Type", "Rs");
        private List<Transaction> Expenses { get; set; } = new List<Transaction>();
        private List<TransactionDto> FilteredExpenses { get; set; } = new List<TransactionDto>();
        protected override async Task OnInitializedAsync()
        {
            Expenses = await transactionService.GetAll();

            ApplyFilter();
        }
        private void ApplyFilter()
        {
            List<TransactionDto> transactionDtos = Expenses
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
    }
}