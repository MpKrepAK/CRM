using System.Windows.Controls;
using CRM.Views.Pages.MainPages;

namespace CRM.ViewModels;

public class MainVM : ViewModelBase
{
    private Page _currentPage;

    public Page CurrentPage
    {
        get { return _currentPage; }
        set { SetProperty(ref _currentPage, value); }
    }

    public void ShowCustomers()
    {
        CurrentPage = new CustomersPage();
    }
    
    
    
}