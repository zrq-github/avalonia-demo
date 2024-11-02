using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Windows.Input;
using Avalonia.CrossPlatform.MusicStore.Models;
using ReactiveUI;

namespace Avalonia.CrossPlatform.MusicStore.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        ShowDialog = new Interaction<MusicStoreViewModel, AlbumViewModel?>();

        BuyMusicCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var store = new MusicStoreViewModel();
            var result = await ShowDialog.Handle(store);
            if (result != null)
            {
                Albums.Add(result);
                await result.SaveToDiskAsync();
            }
        });
        
        RxApp.MainThreadScheduler.Schedule(LoadAlbums);
    }
#pragma warning disable CA1822 // Mark members as static
    public string Greeting => "Welcome to Avalonia!";
#pragma warning restore CA1822 // Mark members as static

    public ObservableCollection<AlbumViewModel> Albums { get; } = new();
    public ICommand BuyMusicCommand { get; }

    public Interaction<MusicStoreViewModel, AlbumViewModel?> ShowDialog { get; }

    private async void LoadAlbums()
    {
        var albums = (await Album.LoadCachedAsync()).Select(x => new AlbumViewModel(x));

        foreach (var album in albums) Albums.Add(album);

        foreach (var album in Albums.ToList()) await album.LoadCover();
    }
}