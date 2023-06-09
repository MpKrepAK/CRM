using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using DAL.Entitys.Database;
using DAL.Repositories.Implementations;

namespace CRM.ViewModels.MainPagesVM;

public class ChatsVM : ViewModelBase
{
    private ChatRepository rep;
    
    private ObservableCollection<UserChat> _data;

    public ObservableCollection<UserChat> Data
    {
        get { return _data;}
        set { SetProperty(ref _data, value); }
    }

    private string _message;

    public string Message
    {
        get { return _message;}
        set { SetProperty(ref _message, value); }
    }
    
    public ChatsVM()
    {
        rep = new ChatRepository();
        Update();
    }

    public async Task Fill()
    {
        Data = new ObservableCollection<UserChat>(await rep.GetAll());
    }

    public async Task Update()
    {
        while (true)
        {
            await Task.Delay(1000);
            var list = await rep.GetAll();
            list.Reverse();
            Data = new ObservableCollection<UserChat>(list);
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
        await rep.Add(mes);
        Message = "";
        //mes.SenderId = ApplicationData.User.Id;
    }
}