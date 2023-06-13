using System;
using System.Linq;
using System.Management;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CRM.Views.Pages;
using DAL.Entitys.DTO;
using DAL.Repositories.Implementations;

namespace CRM.ViewModels;

public class LoginVM : ViewModelBase
{
    private LoginDTO _loginData;
    public LoginDTO LoginData
    {
        get { return _loginData;}
        set { SetProperty(ref _loginData, value); }
    }

    public LoginVM()
    {
        LoginData = new LoginDTO();
        Visibility = Visibility.Collapsed;
    }
    public void Login(object sender, RoutedEventArgs args)
    {
        MainWindowVM.Instance.CurrentPage = new MainPage();
        //MainWindowVM.Instance.CurrentPage = new UserPage();
        //Loging();
    }

    private async Task Loging()
    {
        
        if (String.IsNullOrEmpty(LoginData.Password) || String.IsNullOrEmpty(LoginData.UserName))
        {
            MessageBox.Show("Не все поля заполнены");
            return;
        }
        
        
        ManagementObjectSearcher theSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive WHERE InterfaceType='USB'");
        foreach (ManagementObject currentObject in theSearcher.Get())
        {
            ManagementObject theSerialNumberObjectQuery = new ManagementObject("Win32_PhysicalMedia.Tag='" + currentObject["DeviceID"] + "'");
            Console.WriteLine(theSerialNumberObjectQuery["SerialNumber"].ToString());
            //5B731DA9EC67
            //001CC0C83C18EBB0742E09B0
            if (theSerialNumberObjectQuery["SerialNumber"].ToString() == "001CC0C83C18EBB0742E09B0")
            {
                MainWindowVM.Instance.CurrentPage = new MainPage();
                return;
            }
        }
        
        
        var rep = new UserRepository();
        var users = await rep.GetAll();
        var user = users.FirstOrDefault(x=>x.Password == LoginData.Password && x.Login == LoginData.UserName);
        
        if (user == null)
        {
            MessageBox.Show("Пользователь не найден");
            return;
        }
        
        
        foreach (ManagementObject currentObject in theSearcher.Get())
        {
            ManagementObject theSerialNumberObjectQuery = new ManagementObject("Win32_PhysicalMedia.Tag='" + currentObject["DeviceID"] + "'");
            Console.WriteLine(theSerialNumberObjectQuery["SerialNumber"].ToString());
            
            if (theSerialNumberObjectQuery["SerialNumber"].ToString() == user.DiskName)
            {
                ApplicationData.User = user;
                MainWindowVM.Instance.CurrentPage = new UserPage();
                return;
            }
        }
        
        MessageBox.Show("Пользователь не найден");
        return;
    }

    public void Close()
    {
        Application.Current.Shutdown();
    }
    
    public void Info()
    {
        if (Visibility == Visibility.Collapsed)
        {
            Visibility = Visibility.Visible;
        }
        else
        {
            Visibility = Visibility.Collapsed;
        }
    }

    private Visibility _visibility;

    public Visibility Visibility
    {
        get { return _visibility; }
        set { SetProperty(ref _visibility, value); }
    }
}