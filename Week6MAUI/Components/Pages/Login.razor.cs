using DataModel.Model;

namespace Week6MAUI.Components.Pages
{
    public partial class Login
    {
        private string? ErrorMessage;
        public User Users { get; set; } = new();
        private async void HandleLogin()
        {
            if (!UserService.Login(Users))
            {
                Nav.NavigateTo("/home");
            }
            else
            {
                ErrorMessage = "Invalid username or password.";
            }
        }
    }
}