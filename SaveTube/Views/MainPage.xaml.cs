using SaveTube.ViewModels;

namespace SaveTube;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.InitializeComponent();
        this.DataContext = App.Current.Services.GetService<MainViewModel>();
    }

    public MainViewModel ViewModel => (MainViewModel)DataContext;

    
}
