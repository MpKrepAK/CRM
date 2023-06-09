using System.Windows.Controls;
using DAL.Entitys.Database;
using DAL.Repositories.Implementations;

namespace CRM.CustomControls;

public partial class ChatMessage : UserControl
{
    public ChatMessage()
    {
        chatRepository = new ChatRepository();
        InitializeComponent();
    }

    private ChatRepository chatRepository;
    public UserChat Message { get; set; }
    
}