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
                Nav.NavigateTo("/home");
            }
            else
            {
                ErrorMessage = "Invalid username or password.";
            }
            //AddUserRecord();
        }
        private async void AddUserRecord()
        {
            User user = new User()
            {
                Username="pritesh",
                Password="pritesh",
                Currency_Type="Rs"
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
            var  getUser=await useraccess.GetAllUser();
        }
    }
}