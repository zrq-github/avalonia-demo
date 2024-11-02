using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.MusicStore.Models;
using ReactiveUI;

namespace Avalonia.MusicStore.ViewModels;

public class MainViewModel : ViewModelBase
{
    private string _greeting = "Welcome to Avalonia!";
    
#pragma warning disable CA1822 // Mark members as static
    public string Greeting
    {
        get => _greeting;
        set => this.RaiseAndSetIfChanged(ref _greeting, value);
    }
#pragma warning restore CA1822 // Mark members as static

    public int t_Count { get; set; }
    
    public MainViewModel()
    {
        BuyMusicCommand = ReactiveCommand.Create(() =>
        {
            Greeting = (t_Count++).ToString();
        });
    }

    public ICommand BuyMusicCommand { get; }
    public ICollection<Album> Albums { get; }
}