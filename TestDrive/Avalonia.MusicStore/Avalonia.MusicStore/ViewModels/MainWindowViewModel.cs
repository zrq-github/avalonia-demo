using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Windows.Input;
using ReactiveUI;

namespace Avalonia.MusicStore.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        ShowDialog = new Interaction<MusicStoreViewModel, AlbumViewModel?>();

        BuyMusicCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var store = new MusicStoreViewModel();
            var result = await ShowDialog.Handle(store);
            if (result != null) Albums.Add(result);
        });
    }
#pragma warning disable CA1822 // Mark members as static
    public string Greeting => "Welcome to Avalonia!";
#pragma warning restore CA1822 // Mark members as static

    public ObservableCollection<AlbumViewModel> Albums { get; } = new();
    public ICommand BuyMusicCommand { get; }

    public Interaction<MusicStoreViewModel, AlbumViewModel?> ShowDialog { get; }
}