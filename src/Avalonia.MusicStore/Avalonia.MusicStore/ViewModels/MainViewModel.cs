using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;
using Avalonia.MusicStore.Models;
using ReactiveUI;

namespace Avalonia.MusicStore.ViewModels;

public class MainViewModel : ViewModelBase
{
    private int _greeting =0;
    private string _error = "error";

    public string Error
    {
        get => _error;
        set => this.RaiseAndSetIfChanged(ref _error, value);
    }

    public MainViewModel()
    {
        ShowDialog = new Interaction<MusicStoreViewModel, AlbumViewModel?>();
        BuyMusicCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            try
            {
                var store = new MusicStoreViewModel();
                
                var result = await ShowDialog.Handle(store);
                Greeting++;
            }
            catch (Exception e)
            {
                Error = e.ToString();
                throw;
            }
        });
    }

    public ObservableCollection<AlbumViewModel> Albums { get; } = new();

    public Interaction<MusicStoreViewModel, AlbumViewModel?> ShowDialog { get; }

#pragma warning disable CA1822 // Mark members as static
    public int Greeting
    {
        get => _greeting;
        set => this.RaiseAndSetIfChanged(ref _greeting, value);
    }
#pragma warning restore CA1822 // Mark members as static


    public ICommand BuyMusicCommand { get; }

    private async void LoadAlbums()
    {
        var albums = (await Album.LoadCachedAsync()).Select(x => new AlbumViewModel(x));

        foreach (var album in albums) Albums.Add(album);

        foreach (var album in Albums.ToList()) await album.LoadCover();
    }
}