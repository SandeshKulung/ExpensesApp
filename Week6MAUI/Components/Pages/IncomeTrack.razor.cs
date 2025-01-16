using DataAccess.Services;
using DataModel.Model;

namespace Week6MAUI.Components.Pages
{
    public partial class IncomeTrack
    {

        private List<Transaction> Expenses = new();
        private List<Category> Categories = new();
        private Transaction NewExpense = new();
        public decimal TotalExpenses => Expenses.Sum(e => e.Amount);
        private bool IsAddExpensePopupVisible = false;
        protected override async Task OnInitializedAsync()
        {
            Expenses = await transactionService.GetAll();
            Expenses = Expenses.Where(x => x.Type == "income").ToList();
            Categories = await categoryService.GetAll();
            Categories = Categories.Where(x => x.Type == "income").ToList();
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
            NewExpense.Type = "income";
            Expenses.Add(NewExpense);
            await transactionService.Add(NewExpense);
            CloseAddExpensePopup();
        }
    }
}