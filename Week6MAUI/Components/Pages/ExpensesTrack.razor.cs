using DataAccess.Services;
using DataModel.Model;

namespace Week6MAUI.Components.Pages
{
    public partial class ExpensesTrack
    {
        private List<Transaction> Expenses = new();
        private List<Category> Categories = new();
        private Transaction NewExpense = new();
        public decimal TotalExpenses => Expenses.Sum(e => e.Amount);
        public decimal TotalRemainingAmount =0;
        string currencyType = Preferences.Get("Currency_Type", "Rs");
        private bool IsAddExpensePopupVisible = false;
        protected override async Task OnInitializedAsync()
        {

            await getTransactions();
        }
        public async Task getTransactions()
        {
            Expenses = await transactionService.GetAll();
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
     }).OrderByDescending(x => x.Date)
     .ToList();
            TotalRemainingAmount = transactionDtos.Sum(x => x.IncomeAmount + x.DebtAmount - x.ExpensesAmount);
            Expenses = Expenses.Where(x => x.Type == "expense").ToList();
            Categories = await categoryService.GetAll();
            Categories = Categories.Where(x => x.Type == "expense").ToList();
        }
        private void OpenAddExpensePopup()
        {
            NewExpense = new Transaction
            {
                Date = DateTime.Now
            };
            IsAddExpensePopupVisible = true;
        }
        private void HighestSort()
        {
            Expenses = Expenses.OrderByDescending(x => x.Amount).ToList();
        }
        private void LowestSort()
        {
            Expenses = Expenses.OrderBy(x => x.Amount).ToList();
        }

        private void CloseAddExpensePopup()
        {
            IsAddExpensePopupVisible = false;
        }

        private async Task SaveExpense()
        {
            if (TotalRemainingAmount< NewExpense.Amount)
            {
                await App.Current.MainPage.DisplayAlert("Oops",
               "Expenses is greater than remaining amount", "OK");
            }
            NewExpense.Type = "expense";
            Expenses.Add(NewExpense);
            await transactionService.Add(NewExpense);
            CloseAddExpensePopup();
            await getTransactions();
        }
    }
}