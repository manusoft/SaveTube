using SaveTube.Services;
using SaveTube.ViewModels;

namespace SaveTube;
public partial class App : Application
{
    public new static App Current => (App)Application.Current;
    public IServiceProvider Services { get; }

    public static Window appWindow;
    public static Frame rootFrame;
    public static nint hwnd;

    public App()
    {
        Services = ConfigureServices();
        this.InitializeComponent();
    }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        // Services
        services.AddSingleton<YoutubeService>();

        // ViewModels
        services.AddSingleton<MainViewModel>();

        // Views
        services.AddSingleton<MainPage>();

        // Configuration
        // services.Configure<LocalSettingsOptions>((nameof(LocalSettingsOptions));

        return services.BuildServiceProvider();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        appWindow = new Window();
        hwnd = WindowNative.GetWindowHandle(appWindow);
        appWindow.Content = rootFrame = new Frame();
        appWindow.ExtendsContentIntoTitleBar = false;
        appWindow.Title = "Laksya Solutions - Vetlife";
        appWindow.SetWindowSize(1300, 800);
        appWindow.CenterOnScreen();        
        appWindow.SetIcon(@"Assets\youtube.ico");
        appWindow.SetTitleBarBackgroundColors(Colors.BlanchedAlmond);      
        rootFrame.Navigate(typeof(MainPage));
        appWindow.Activate();
    }

}
