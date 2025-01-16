using DataAccess.Services;
using DataModel.Model;

namespace Week6MAUI.Components.Pages
{
    public partial class DebtTrack
    {

        private List<Transaction> Expenses = new();
        private Transaction NewExpense = new();
        public decimal TotalExpenses =0;
        public decimal ClearedDebt =0;
        public decimal RemainingDebt= 0;
        private bool IsAddExpensePopupVisible = false;
        string currencyType = Preferences.Get("Currency_Type", "Rs");
        protected override async Task OnInitializedAsync()
        {
            await GetAllDebt();
        }
        public async Task GetAllDebt()
        {
            Expenses = await transactionService.GetAll();
            //Expenses = Expenses.Where(x => x.Type == "debt" || x.Type == "cleardebt").ToList();
            Expenses = Expenses.Where(x => x.Type == "debt" || x.Type == "cleardebt").ToList();

            TotalExpenses = Expenses.Where(x => x.Type == "debt").Sum(e => e.Amount);
            ClearedDebt = Expenses.Where(x => x.Type == "cleardebt").Sum(e => e.Amount);
            RemainingDebt = TotalExpenses - ClearedDebt;
        }
        private void HighestSort()
        {
            Expenses = Expenses.OrderByDescending(x => x.Amount).ToList();
        }
        private void LowestSort()
        {
            Expenses = Expenses.OrderBy(x => x.Amount).ToList();
        }

        private void OpenAddExpensePopup()
        {
            NewExpense = new Transaction
            {
                Date = DateTime.Now,
                Type="debt"
            };
            IsAddExpensePopupVisible = true;
        }
        private void OpenClearDebtPopup()
        {
            NewExpense = new Transaction
            {
                Date = DateTime.Now,
                Type="cleardebt"
            };
            IsAddExpensePopupVisible = true;
        }

        private void CloseAddExpensePopup()
        {
            IsAddExpensePopupVisible = false;
        }

        private async Task SaveExpense()
        {
            Expenses.Add(NewExpense);
            if (NewExpense.Type!="debt"&& RemainingDebt < NewExpense.Amount)
            {
                await App.Current.MainPage.DisplayAlert("Oops",
               "Clear Debt Amount is greater than remaining amount", "OK");
                Expenses.Remove(NewExpense);
                return;
            }
            await transactionService.Add(NewExpense);
            CloseAddExpensePopup();
            await GetAllDebt();
        }
    }
}