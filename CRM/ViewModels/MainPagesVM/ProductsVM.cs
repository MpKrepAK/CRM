using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DAL.Entitys.Database;
using DAL.Repositories.Implementations;

namespace CRM.ViewModels.MainPagesVM;

public class ProductsVM : ViewModelBase
{
    private ProductRepository rep;
    private ProductTypeRepository productTypeRepository;
    private ProviderRepository providerRepository;
    
    private ObservableCollection<Provider> _providers;

    public ObservableCollection<Provider> Providers
    {
        get { return _providers;}
        set { SetProperty(ref _providers, value); }
    }
    
    private ObservableCollection<ProductType> _productTypes;

    public ObservableCollection<ProductType> ProductTypes
    {
        get { return _productTypes;}
        set { SetProperty(ref _productTypes, value); }
    }
    
    private ObservableCollection<Product> _data;

    public ObservableCollection<Product> Data
    {
        get { return _data;}
        set { SetProperty(ref _data, value); }
    }

    public ProductType _addProductType;

    public ProductType AddProductType
    {
        get { return _addProductType; }
        set { SetProperty(ref _addProductType, value); }
    }
    
    public Provider _addProvider;

    public Provider AddProvider
    {
        get { return _addProvider; }
        set { SetProperty(ref _addProvider, value); }
    }
    
    private Product _chosen;

    public Product Chosen
    {
        get { return _chosen;}
        set { SetProperty(ref _chosen, value); }
    }
    
    private Product _adding;

    public Product Adding
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

    public ProductsVM()
    {
        Adding = new Product();
        rep = new ProductRepository();
        productTypeRepository = new ProductTypeRepository();
        providerRepository = new ProviderRepository();
        Fill();
        AddVisible = Visibility.Collapsed;
        InfoVisible = Visibility.Collapsed;
    }

    public async Task Fill()
    {
        try
        {
            Data = new ObservableCollection<Product>(await rep.GetAll());
            Providers = new ObservableCollection<Provider>(await providerRepository.GetAll());
            ProductTypes = new ObservableCollection<ProductType>(await productTypeRepository.GetAll());
        }
        catch (Exception e)
        {
            
        }
    }

    public void SelectionChanged(object sender, RoutedEventArgs args)
    {
        try
        {
            Chosen = (((DataGrid)sender).SelectedItem as Product);
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
        if (AddProvider == null||AddProductType == null)
        {
            MessageBox.Show("Не все поля заполнены");
            return;
        }
        Adding.ProviderId = AddProvider.Id;
        Adding.ProductTypeId = AddProductType.Id;
        var res = await rep.Add(Adding);
        if (!res)
        {
            MessageBox.Show("Ошибка при добавлении, вероятно не все поля заполнены или имеют не валидный вид");
        }
        Adding = new Product();
        await Fill();
    }
    public async void UpdateEntity()
    {
        if (Chosen == null)
        {
            MessageBox.Show("Выберите объект");
            return;
        }

        if (AddProvider == null||AddProductType == null)
        {
            MessageBox.Show("Не все поля заполнены");
            return;
        }
        
        Adding.ProviderId = AddProvider.Id;
        Adding.ProductTypeId = AddProductType.Id;
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