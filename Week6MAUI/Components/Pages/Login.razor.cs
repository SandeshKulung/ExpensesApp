using DataAccess.Services;
using DataAccess.Services.Interface;
using DataModel.Model;

namespace Week6MAUI.Components.Pages
{
    public partial class Login
    {
        private string? ErrorMessage;
        public User Users { get; set; } = new();
        private async void HandleLogin()
        {
            if (UserService.Login(Users))
            {
                Nav.NavigateTo("/dashboard");
            }
            else
            {
                ErrorMessage = "Invalid username or password.";
            }
            //AddUserRecord();
            await AddDefaultCategory();
        }
        private async Task AddDefaultCategory()
        {
            var response = await categoryService.GetAll();
            if (!response.Any()) 
            { 
                List<Category> categoryList= new List<Category>();
                categoryList.Add(new Category
                {
                    Title = "Rent",
                    Type="expense"
                });
                categoryList.Add(new Category
                {
                    Title = "Grocery",
                    Type = "expense"
                });
                categoryList.Add(new Category
                {
                    Title = "Food",
                    Type = "expense"
                });
                categoryList.Add(new Category
                {
                    Title = "Salary",
                    Type="income"
                });
                categoryList.Add(new Category
                {
                    Title = "Bonus",
                    Type = "income"
                });
                categoryList.Add(new Category
                {
                    Title = "Budget",
                    Type = "income"
                });
                foreach (var category in categoryList)
                {
                    await categoryService.Add(category);
                }
            }

        }
        private async Task AddUserRecord()
        {
            User user = new User()
            {
                Username = "pritesh",
                Password = "pritesh",
                Currency_Type = "Rs"
            };

            var response = await useraccess.AddUser(user);


            if (response > 0)
            {
                this.StateHasChanged();
                await App.Current.MainPage.DisplayAlert("Record Saved",
                "Record Saved To Student Table", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Oops",
               "Something went wrong while adding record", "OK");
            }
            var getUser = await useraccess.GetAllUser();
        }
    }
}