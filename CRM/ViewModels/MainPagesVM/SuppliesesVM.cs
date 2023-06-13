using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DAL.Entitys.Database;
using DAL.Repositories.Implementations;

namespace CRM.ViewModels.MainPagesVM;

public class SuppliesesVM : ViewModelBase
{
    private SuppliesRepository rep;
    private ProductRepository productRepository;
    
    private ObservableCollection<Supplies> _data;

    public ObservableCollection<Supplies> Data
    {
        get { return _data;}
        set { SetProperty(ref _data, value); }
    }
    
    private ObservableCollection<Product> _products;

    public ObservableCollection<Product> Products
    {
        get { return _products;}
        set { SetProperty(ref _products, value); }
    }
    
    private Product _addProduct;

    public Product AddProduct
    {
        get { return _addProduct;}
        set { SetProperty(ref _addProduct, value); }
    }
    
    private Supplies _chosen;

    public Supplies Chosen
    {
        get { return _chosen;}
        set { SetProperty(ref _chosen, value); }
    }
    
    private Supplies _adding;

    public Supplies Adding
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

    public SuppliesesVM()
    {
        Adding = new Supplies();
        rep = new SuppliesRepository();
        productRepository = new ProductRepository();
        Fill();
        AddVisible = Visibility.Collapsed;
        InfoVisible = Visibility.Collapsed;
    }

    public async Task Fill()
    {
        try
        {
            Data = new ObservableCollection<Supplies>(await rep.GetAll());
            Products = new ObservableCollection<Product>(await productRepository.GetAll());
        }
        catch (Exception e)
        {
            
        }
    }

    public void SelectionChanged(object sender, RoutedEventArgs args)
    {
        try
        {
            Chosen = (((DataGrid)sender).SelectedItem as Supplies);
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
        Adding.ProductId = AddProduct.Id;
        var res = await rep.Add(Adding);
        if (!res)
        {
            MessageBox.Show("Ошибка при добавлении, вероятно не все поля заполнены или имеют не валидный вид");
        }
        Adding = new Supplies();
        await Fill();
    }
    public async void UpdateEntity()
    {
        if (Chosen == null)
        {
            MessageBox.Show("Выберите объект");
            return;
        }
        Chosen.ProductId = AddProduct.Id;
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