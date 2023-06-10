using System.Windows;
using System.Windows.Controls;
using CRM.Views.Pages;
using DAL.Entitys.DTO;

namespace CRM.ViewModels;

public class LoginVM : ViewModelBase
{
    private LoginDTO _loginData;
    public LoginDTO LoginData
    {
        get { return _loginData;}
        set { SetProperty(ref _loginData, value); }
    }

    public void Registration()
    {
        //MainWindowVM.Instance.CurrentPage = null;
    }
    public void Login(object sender, RoutedEventArgs args)
    {
        //MainWindowVM.Instance.CurrentPage = new MainPage();
        MainWindowVM.Instance.CurrentPage = new UserPage();
    }
}