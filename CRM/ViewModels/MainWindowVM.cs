using System.Threading.Tasks;
using System.Windows.Controls;
using CRM.Views.Pages;

namespace CRM.ViewModels;

public class MainWindowVM : ViewModelBase
{
    public static MainWindowVM Instance { get; set; }
    public MainWindowVM()
    {
        if (CurrentPage == null)
        {
            CurrentPage = new LoginPage();
        }

        Instance = this;
    }

    private Page _currentPage;

    public Page CurrentPage
    {
        get { return _currentPage; }
        set { SetProperty(ref _currentPage, value); }
    }

}