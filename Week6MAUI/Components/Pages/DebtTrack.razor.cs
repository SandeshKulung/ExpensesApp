using DataAccess.Services;
using DataModel.Model;

namespace Week6MAUI.Components.Pages
{
    public partial class DebtTrack
    {

        private List<Transaction> Expenses = new();
        private Transaction NewExpense = new();
        public decimal TotalExpenses => Expenses.Sum(e => e.Amount);
        private bool IsAddExpensePopupVisible = false;
        string currencyType = Preferences.Get("Currency_Type", "Rs");
        protected override async Task OnInitializedAsync()
        {
            Expenses = await transactionService.GetAll();
            Expenses = Expenses.Where(x => x.Type == "debt").ToList();
        }
        private void OpenAddExpensePopup()
        {
            NewExpense = new Transaction
            {
                Date = DateTime.Now
            };
            IsAddExpensePopupVisible = true;
        }

        private void CloseAddExpensePopup()
        {
            IsAddExpensePopupVisible = false;
        }

        private async Task SaveExpense()
        {
            NewExpense.Type = "debt";
            Expenses.Add(NewExpense);
            await transactionService.Add(NewExpense);
            CloseAddExpensePopup();
        }
    }
}