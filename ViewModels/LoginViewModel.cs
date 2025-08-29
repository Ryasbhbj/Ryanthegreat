using System.Windows.Input;
using YourMauiApp.Services;


namespace YourMauiApp.ViewModels;


public class LoginViewModel(AuthService auth) : BaseViewModel
{
private readonly AuthService _auth = auth;


private string _email = string.Empty;
public string Email { get => _email; set => SetProperty(ref _email, value); }


private string _password = string.Empty;
public string Password { get => _password; set => SetProperty(ref _password, value); }


private bool _isBusy;
public bool IsBusy { get => _isBusy; set => SetProperty(ref _isBusy, value); }


public ICommand LoginCommand => new Command(async () => await LoginAsync());
public ICommand GoogleCommand => new Command(async () => await GoogleAsync());
public ICommand ForgotCommand => new Command(async () => await Shell.Current.GoToAsync(nameof(Views.ForgotPasswordPage)));


private async Task LoginAsync()
{
if (IsBusy) return; IsBusy = true;
var ok = await _auth.LoginAsync(Email, Password);
IsBusy = false;
if (ok) await Shell.Current.GoToAsync("//DashboardPage");
else await Application.Current.MainPage.DisplayAlert("Login failed", "Check your email/password.", "OK");
}


private async Task GoogleAsync()
{
if (IsBusy) return; IsBusy = true;
var ok = await _auth.GoogleSignInAsync();
IsBusy = false;
if (ok) await Shell.Current.GoToAsync("//DashboardPage");
else await Application.Current.MainPage.DisplayAlert("Google Sign-In", "Could not authenticate.", "OK");
}
}