using System.Windows.Input;
using YourMauiApp.Services;


namespace YourMauiApp.ViewModels;


public class ForgotPasswordViewModel(ApiClient api) : BaseViewModel
{
private readonly ApiClient _api = api;


private string _email = string.Empty;
public string Email { get => _email; set => SetProperty(ref _email, value); }


public ICommand SubmitCommand => new Command(async () => await SubmitAsync());


private async Task SubmitAsync()
{
// Mirrors forgot_password.php lookup and redirect to reset
var ok = await _api.PostJsonAsync<bool>("api/forgot-password", new { email = Email });
if (ok)
await Application.Current.MainPage.DisplayAlert("Check your email", "We sent a reset link if your account exists.", "OK");
else
await Application.Current.MainPage.DisplayAlert("Notice", "If the email exists, you will receive a link shortly.", "OK");
}
}