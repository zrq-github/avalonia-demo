using CommunityToolkit.Mvvm.ComponentModel;

namespace Avalonia.CrossPlatform.MusicStore.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private string _greeting = "Welcome to Avalonia!";
}