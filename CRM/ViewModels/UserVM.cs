﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DAL.Entitys.Database;
using DAL.Repositories.Implementations;

namespace CRM.ViewModels;

public class UserVM : ViewModelBase
{
    private CustomerRepository rep;
    private ChatRepository chatRepository;
    private ObservableCollection<UserChat> _chat;

    public ObservableCollection<UserChat> Chat
    {
        get { return _chat;}
        set { SetProperty(ref _chat, value); }
    }
    
    private ObservableCollection<Customer> _data;

    public ObservableCollection<Customer> Data
    {
        get { return _data; }
        set { SetProperty(ref _data, value); }
    }

    private Visibility _v1;

    public Visibility V1
    {
        get { return _v1; }
        set { SetProperty(ref _v1, value); }
    }
    
    private Visibility _v2;

    public Visibility V2
    {
        get { return _v2; }
        set { SetProperty(ref _v2, value); }
    }
    
    private Visibility _v3;

    public Visibility V3
    {
        get { return _v3; }
        set { SetProperty(ref _v3, value); }
    }
    
    private Visibility _v4;

    public Visibility V4
    {
        get { return _v4; }
        set { SetProperty(ref _v4, value); }
    }

    private Customer _find;

    public Customer Find
    {
        get { return _find; }
        set { SetProperty(ref _find, value); }
    }

    private string _message;

    public string Message
    {
        get { return _message;}
        set { SetProperty(ref _message, value); }

    }

    private Customer _chosen;

    public Customer Chosen
    {
        get { return _chosen; }
        set { SetProperty(ref _chosen, value); }
    }
    public UserVM()
    {
        Adding = new Order();
        rep = new CustomerRepository();
        chatRepository = new ChatRepository();
        productRepository = new ProductRepository();
        orderStatusRepository = new OrderStatusRepository();
        ordersRepository = new OrdersRepository();
        Find = new Customer();
        V1 = Visibility.Collapsed;
        V2 = Visibility.Collapsed;
        V3 = Visibility.Collapsed;
        V4 = Visibility.Collapsed;
        AddVisible = Visibility.Collapsed;
        InfoVisible = Visibility.Collapsed;
        Fill();
        Update();
    }

    private OrdersRepository ordersRepository;
    
    private async Task Fill()
    {
        Orders = new ObservableCollection<Order>(await ordersRepository.GetAll());
        Data = new ObservableCollection<Customer>(await rep.GetAll());
        Products = new ObservableCollection<Product>(await productRepository.GetAll());
        OrderStatuses = new ObservableCollection<OrderStatus>(await orderStatusRepository.GetAll());
        
    }

    public async Task Update()
    {
        while (true)
        {
            await Task.Delay(1000);
            var list = await chatRepository.GetAll();
            list.Reverse();
            Chat = new ObservableCollection<UserChat>(list);
        }
    }
    
    public async void Send()
    {
        if (string.IsNullOrEmpty(Message))
        {
            MessageBox.Show("dfgdfg");
            return;
        }

        var mes = new UserChat();
        mes.DateTime = DateTime.Now;
        mes.Message = Message;
        mes.SenderId = 1;
        await chatRepository.Add(mes);
        Message = "";
        //mes.SenderId = ApplicationData.User.Id;
    }
    
    public void V1OpenClose()
    {
        if (V1 == Visibility.Collapsed)
        {
            V1 = Visibility.Visible;
        }
        else
        {
            V1 = Visibility.Collapsed;
        }
    }
    
    public void V2OpenClose()
    {
        if (V2 == Visibility.Collapsed)
        {
            V2 = Visibility.Visible;
        }
        else
        {
            V2 = Visibility.Collapsed;
        }
    }
    
    public void V3OpenClose()
    {
        if (V3 == Visibility.Collapsed)
        {
            V3 = Visibility.Visible;
        }
        else
        {
            V3 = Visibility.Collapsed;
        }
    }
    
    public void V4OpenClose()
    {
        if (V4 == Visibility.Collapsed)
        {
            V4 = Visibility.Visible;
        }
        else
        {
            V4 = Visibility.Collapsed;
        }
    }

    public async void Search()
    {
        List<Customer> list = await rep.GetAll();
        if (!String.IsNullOrEmpty(Find.FirstName))
        {
            list = list.Where(x => x.FirstName.ToLower().Contains(Find.FirstName)).ToList();
        }
        if (!String.IsNullOrEmpty(Find.MidleName))
        {
            list = list.Where(x => x.MidleName.ToLower().Contains(Find.MidleName)).ToList();
        }
        if (!String.IsNullOrEmpty(Find.LastName))
        {
            list = list.Where(x => x.LastName.ToLower().Contains(Find.LastName)).ToList();
        }
        if (!String.IsNullOrEmpty(Find.PhoneNumber))
        {
            list = list.Where(x => x.PhoneNumber.ToLower().Contains(Find.PhoneNumber)).ToList();
        }
        if (!String.IsNullOrEmpty(Find.VK))
        {
            list = list.Where(x => x.VK.ToLower().Contains(Find.VK)).ToList();
        }
        if (!String.IsNullOrEmpty(Find.Instagram))
        {
            list = list.Where(x => x.Instagram.ToLower().Contains(Find.Instagram)).ToList();
        }
        if (!String.IsNullOrEmpty(Find.Telegram))
        {
            list = list.Where(x => x.Telegram.ToLower().Contains(Find.Telegram)).ToList();
        }
        if (!String.IsNullOrEmpty(Find.Facebook))
        {
            list = list.Where(x => x.Facebook.ToLower().Contains(Find.Facebook)).ToList();
        }
        if (!String.IsNullOrEmpty(Find.Email))
        {
            list = list.Where(x => x.Email.ToLower().Contains(Find.Email)).ToList();
        }

        Data = new ObservableCollection<Customer>(list);
    }

    public async void Clear()
    {
        Find = new Customer();
    }

    public void SelectionChanged(object sender, RoutedEventArgs args)
    {
        try
        {
            Chosen = (((DataGrid)sender).SelectedItem as Customer);
        }
        catch (Exception e)
        {
            
        }
    }

    public async void Vk()
    {
        try
        {
            if (Chosen == null)
            {
                return;
            }
            if (String.IsNullOrEmpty(Chosen.VK))
            {
                MessageBox.Show("Ссылка не указана");
                return;
            }

            Process.Start(new ProcessStartInfo(Chosen.VK) { UseShellExecute = true });
        }
        catch (Exception e)
        {
        }
    }
    public async void Inst()
    {
        try
        {
            if (Chosen == null)
            {
                return;
            }
            if (String.IsNullOrEmpty(Chosen.Instagram))
            {
                MessageBox.Show("Ссылка не указана");
                return;
            }

            Process.Start(new ProcessStartInfo(Chosen.Instagram) { UseShellExecute = true });
        }
        catch (Exception e)
        {
            
        }
    }
    public async void Tel()
    {
        try
        {
            if (Chosen == null)
            {
                return;
            }
            if (String.IsNullOrEmpty(Chosen.Telegram))
            {
                MessageBox.Show("Ссылка не указана");
                return;
            }

            Process.Start(new ProcessStartInfo(Chosen.Telegram) { UseShellExecute = true });
        }
        catch (Exception e)
        {
        }
    }
    public async void Fb()
    {
        try
        {
            if (Chosen == null)
            {
                return;
            }
            if (String.IsNullOrEmpty(Chosen.Facebook))
            {
                MessageBox.Show("Ссылка не указана");
                return;
            }

            Process.Start(new ProcessStartInfo(Chosen.Facebook) { UseShellExecute = true });
        }
        catch (Exception e)
        {
        }
    }

    private ObservableCollection<Order> _orders;

    public ObservableCollection<Order> Orders
    {
        get { return _orders; }
        set { SetProperty(ref _orders, value); }
    }

    private Order _chosenOrder;

    public Order ChosenOrder
    {
        get { return _chosenOrder;}
        set { SetProperty(ref _chosenOrder, value); }

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
    
    public void SelectionChangedOrder(object sender, RoutedEventArgs args)
    {
        try
        {
            ChosenOrder = (((DataGrid)sender).SelectedItem as Order);
        }
        catch (Exception e)
        {
            
        }
    }
    
    
    private ProductRepository productRepository;
    private OrderStatusRepository orderStatusRepository;
    
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
    
    private Order _adding;

    public Order Adding
    {
        get { return _adding;}
        set { SetProperty(ref _adding, value); }
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
        var res = await ordersRepository.Add(Adding);
        if (!res)
        {
            MessageBox.Show("Ошибка при добавлении, вероятно не все поля заполнены или имеют не валидный вид");
        }
        Adding = new Order();
        await Fill();
    }
    public async void UpdateEntity()
    {
        if (ChosenOrder == null)
        {
            MessageBox.Show("Выберите объект");
            return;
        }

        if (AddCustomer == null||AddProduct == null||AddOrderStatus==null)
        {
            MessageBox.Show("Не все поля заполнены");
            return;
        }
        ChosenOrder.ProductId = AddProduct.Id;
        ChosenOrder.CustomerId = AddCustomer.Id;
        ChosenOrder.OrderStatusId = AddOrderStatus.Id;
        var res = await ordersRepository.Update(Chosen.Id, ChosenOrder);
        if (!res)
        {
            MessageBox.Show("Ошибка при изменении, вероятно не все поля заполнены или имеют не валидный вид");
        }
        await Fill();
    }
    public async void DeleteEntity(object sender, RoutedEventArgs args)
    {
        if (ChosenOrder==null)
        {
            return;
        }
        var res = await ordersRepository.Delete(ChosenOrder.Id);
        if (!res)
        {
            MessageBox.Show("Ошибка при удалении");
        }
        await Fill();
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
}