﻿using System.Windows.Controls;
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

    public void ShowProvider()
    {
        CurrentPage = new ProvidersPage();
    }
    
    public void ShowUser()
    {
        CurrentPage = new UsersPage();
    }

    public void ShowProductType()
    {
        CurrentPage = new ProductTypesPage();
    }
    public void ShowProduct()
    {
        CurrentPage = new ProductsPage();
    }
    public void ShowOrder()
    {
        CurrentPage = new OrdersPage();
    }
    public void ShowWarehouse()
    {
        CurrentPage = new WarehousesPage();
    }
    public void ShowOrderStatus()
    {
        CurrentPage = new OrderStatusesPage();
    }
    public void ShowChat()
    {
        CurrentPage = new ChatsPage();
    }
}