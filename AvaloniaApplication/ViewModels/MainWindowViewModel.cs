using AvaloniaApplication.Queries;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaApplication.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly TableUserQueries _queries;

        [ObservableProperty]
        private string login = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        public MainWindowViewModel()
        {
            TablesInitializer.Initialize();

            _queries = new TableUserQueries();

            // Tests Only
            Login = "admin";
            Password = "admin1!";
        }

        [RelayCommand]
        private void UserRegister()
        {
            _queries.RegisterUser(Login, Password);    
        }

        [RelayCommand]
        private void UserLogin()
        {
            if (_queries.AuthenticateUser(Login, Password))
                OpenMainWindow();
        }

        private void OpenMainWindow()
        {
            var mainWindow = new DisplayWindow();
            mainWindow.DataContext = new DisplayWindowViewModel();
            mainWindow.Show();
        }
    }
}
