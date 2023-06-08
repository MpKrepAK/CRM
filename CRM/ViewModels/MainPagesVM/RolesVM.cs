using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DAL.Entitys.Database;
using DAL.Repositories.Implementations;

namespace CRM.ViewModels.MainPagesVM;

public class RolesVM : ViewModelBase
{
    private RoleRepository rep;
    
    private ObservableCollection<Role> _data;

    public ObservableCollection<Role> Data
    {
        get { return _data;}
        set { SetProperty(ref _data, value); }
    }
    
    private Role _chosen;

    public Role Chosen
    {
        get { return _chosen;}
        set { SetProperty(ref _chosen, value); }
    }
    
    private Role _adding;

    public Role Adding
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

    public RolesVM()
    {
        Adding = new Role();
        rep = new RoleRepository();
        Fill();
        AddVisible = Visibility.Collapsed;
        InfoVisible = Visibility.Collapsed;
    }

    public async Task Fill()
    {
        try
        {
            Data = new ObservableCollection<Role>(await rep.GetAll());
        }
        catch (Exception e)
        {
            
        }
    }

    public void SelectionChanged(object sender, RoutedEventArgs args)
    {
        try
        {
            Chosen = (((DataGrid)sender).SelectedItem as Role);
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
        Adding = new Role();
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