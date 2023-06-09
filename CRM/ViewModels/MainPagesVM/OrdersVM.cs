using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DAL.Entitys.Database;
using DAL.Repositories.Implementations;

namespace CRM.ViewModels.MainPagesVM;

public class OrdersVM : ViewModelBase
{
    private OrdersRepository rep;
    private ProductRepository productRepository;
    private OrderStatusRepository orderStatusRepository;
    private CustomerRepository customerRepository;
    
    private ObservableCollection<Product> _products;

    public ObservableCollection<Product> Products
    {
        get { return _products;}
        set { SetProperty(ref _products, value); }
    }
    
    private ObservableCollection<OrderStatus> _orderStatuses;

    public ObservableCollection<OrderStatus> OrderStatuses
    {
        get { return _orderStatuses;}
        set { SetProperty(ref _orderStatuses, value); }
    }
    
    private ObservableCollection<Customer> _customers;

    public ObservableCollection<Customer> Customers
    {
        get { return _customers;}
        set { SetProperty(ref _customers, value); }
    }
    
    private ObservableCollection<Order> _data;

    public ObservableCollection<Order> Data
    {
        get { return _data;}
        set { SetProperty(ref _data, value); }
    }

    public OrderStatus _addOrderStatus;

    public OrderStatus AddOrderStatus
    {
        get { return _addOrderStatus; }
        set { SetProperty(ref _addOrderStatus, value); }
    }
    
    public Product _addProduct;

    public Product AddProduct
    {
        get { return _addProduct; }
        set { SetProperty(ref _addProduct, value); }
    }
    
    public Customer _addCustomer;

    public Customer AddCustomer
    {
        get { return _addCustomer; }
        set { SetProperty(ref _addCustomer, value); }
    }
    
    private Order _chosen;

    public Order Chosen
    {
        get { return _chosen;}
        set { SetProperty(ref _chosen, value); }
    }
    
    private Order _adding;

    public Order Adding
    {
        get { return _adding;}
        set { SetProperty(ref _adding, value); }
    }
    
    
    private Visibility _addVisible;

    public Visibility AddVisible
    {
        get { return _addVisible;}
        set { SetProperty(ref _addVisible, value); }
    }

    private Visibility _infoVisible;

    public Visibility InfoVisible
    {
        get { return _infoVisible;}
        set { SetProperty(ref _infoVisible, value); }
    }

    public OrdersVM()
    {
        Adding = new Order();
        rep = new OrdersRepository();
        productRepository = new ProductRepository();
        customerRepository = new CustomerRepository();
        orderStatusRepository = new OrderStatusRepository();
        Fill();
        AddVisible = Visibility.Collapsed;
        InfoVisible = Visibility.Collapsed;
    }

    public async Task Fill()
    {
        try
        {
            Data = new ObservableCollection<Order>(await rep.GetAll());
            Products = new ObservableCollection<Product>(await productRepository.GetAll());
            OrderStatuses = new ObservableCollection<OrderStatus>(await orderStatusRepository.GetAll());
            Customers = new ObservableCollection<Customer>(await customerRepository.GetAll());
        }
        catch (Exception e)
        {
            
        }
    }

    public void SelectionChanged(object sender, RoutedEventArgs args)
    {
        try
        {
            Chosen = (((DataGrid)sender).SelectedItem as Order);
        }
        catch (Exception e)
        {
            
        }
    }
    public void Add()
    {
        if (AddVisible == Visibility.Collapsed)
        {
            AddVisible = Visibility.Visible;
        }
        else
        {
            AddVisible = Visibility.Collapsed;
        }
    }
    
    public void Info()
    {
        if (InfoVisible == Visibility.Collapsed)
        {
            InfoVisible = Visibility.Visible;
        }
        else
        {
            InfoVisible = Visibility.Collapsed;
        }
    }

    public async void AddEntity()
    {
        if (AddCustomer == null||AddProduct == null||AddOrderStatus==null)
        {
            MessageBox.Show("Не все поля заполнены");
            return;
        }
        Adding.ProductId = AddProduct.Id;
        Adding.CustomerId = AddCustomer.Id;
        Adding.OrderStatusId = AddOrderStatus.Id;
        var res = await rep.Add(Adding);
        if (!res)
        {
            MessageBox.Show("Ошибка при добавлении, вероятно не все поля заполнены или имеют не валидный вид");
        }
        Adding = new Order();
        await Fill();
    }
    public async void UpdateEntity()
    {
        if (Chosen == null)
        {
            MessageBox.Show("Выберите объект");
            return;
        }

        if (AddCustomer == null||AddProduct == null||AddOrderStatus==null)
        {
            MessageBox.Show("Не все поля заполнены");
            return;
        }
        Adding.ProductId = AddProduct.Id;
        Adding.CustomerId = AddCustomer.Id;
        Adding.OrderStatusId = AddOrderStatus.Id;
        var res = await rep.Update(Chosen.Id,Chosen);
        if (!res)
        {
            MessageBox.Show("Ошибка при изменении, вероятно не все поля заполнены или имеют не валидный вид");
        }
        await Fill();
    }
    public async void DeleteEntity(object sender, RoutedEventArgs args)
    {
        if (Chosen==null)
        {
            return;
        }
        var res = await rep.Delete(Chosen.Id);
        if (!res)
        {
            MessageBox.Show("Ошибка при удалении");
        }
        await Fill();
    }
}