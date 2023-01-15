namespace SaveTube.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    string title = string.Empty;

    [ObservableProperty]
    bool isBusy = false;

    [ObservableProperty]
    bool isAnalyze = false;

    [ObservableProperty]
    bool isError = false;

    [ObservableProperty]
    string errorText = string.Empty;
}
