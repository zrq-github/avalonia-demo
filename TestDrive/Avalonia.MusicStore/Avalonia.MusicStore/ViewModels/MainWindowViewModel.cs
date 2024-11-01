using System.Windows.Input;
using ReactiveUI;

namespace Avalonia.MusicStore.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
#pragma warning disable CA1822 // Mark members as static
    public string Greeting => "Welcome to Avalonia!";
#pragma warning restore CA1822 // Mark members as static
    
    public ICommand BuyMusicCommand { get; }

    public MainWindowViewModel()
    {
        BuyMusicCommand = ReactiveCommand.Create(() =>
        {

        });
    }
}