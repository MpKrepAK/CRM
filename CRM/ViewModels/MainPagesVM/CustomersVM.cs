using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DAL.Entitys.Database;
using DAL.Repositories.Implementations;

namespace CRM.ViewModels.MainPagesVM;

public class CustomersVM : ViewModelBase
{
    private ObservableCollection<Customer> _customers;

    public ObservableCollection<Customer> Customers
    {
        get { return _customers;}
        set { SetProperty(ref _customers, value); }
    }

    public CustomersVM()
    {
        Fill();

    }

    public async Task Fill()
    {
        var rep = new CustomerRepository();
        Customers = new ObservableCollection<Customer>(await rep.GetAll());
    }
}