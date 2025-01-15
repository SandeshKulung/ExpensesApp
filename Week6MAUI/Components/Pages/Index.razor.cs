using Microsoft.AspNetCore.Components;

namespace Week6MAUI.Components.Pages
{ 
public partial class Index : ComponentBase
{
    protected override void OnInitialized()
    {
        Nav.NavigateTo("/login");
    }
}
}