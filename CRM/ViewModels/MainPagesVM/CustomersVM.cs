using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DAL.Entitys.Database;
using DAL.Repositories.Implementations;

namespace CRM.ViewModels.MainPagesVM;

public class CustomersVM : ViewModelBase
{
    private CustomerRepository rep;
    
    private ObservableCollection<Customer> _data;

    public ObservableCollection<Customer> Data
    {
        get { return _data;}
        set { SetProperty(ref _data, value); }
    }
    
    private Customer _chosen;

    public Customer Chosen
    {
        get { return _chosen;}
        set { SetProperty(ref _chosen, value); }
    }
    
    private Customer _adding;

    public Customer Adding
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

    public CustomersVM()
    {
        Adding = new Customer();
        rep = new CustomerRepository();
        Fill();
        AddVisible = Visibility.Collapsed;
        InfoVisible = Visibility.Collapsed;
    }

    public async Task Fill()
    {
        try
        {
            Data = new ObservableCollection<Customer>(await rep.GetAll());
        }
        catch (Exception e)
        {
            
        }
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
        var res = await rep.Add(Adding);
        if (!res)
        {
            MessageBox.Show("Ошибка при добавлении, вероятно не все поля заполнены или имеют не валидный вид");
        }
        Adding = new Customer();
        await Fill();
    }
    public async void UpdateEntity()
    {
        if (Chosen == null)
        {
            MessageBox.Show("Выберите объект");
            return;
        }
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