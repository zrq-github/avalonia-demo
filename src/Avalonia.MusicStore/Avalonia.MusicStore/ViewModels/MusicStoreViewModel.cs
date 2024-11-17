using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using Avalonia.MusicStore.Models;
using ReactiveUI;

namespace Avalonia.MusicStore.ViewModels;

public class MusicStoreViewModel : ViewModelBase
{
    private CancellationTokenSource? _cancellationTokenSource;
    private bool _isBusy;
    private string? _searchText;
    private AlbumViewModel? _selectedAlbum;

    public MusicStoreViewModel()
    {
        this.WhenAnyValue(x => x.SearchText)
            .Throttle(TimeSpan.FromMilliseconds(400))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(DoSearch!);

        BuyMusicCommand = ReactiveCommand.Create(() => { return SelectedAlbum; });
    }

    public ReactiveCommand<Unit, AlbumViewModel?> BuyMusicCommand { get; }
    public ObservableCollection<AlbumViewModel> SearchResults { get; } = new();

    public AlbumViewModel? SelectedAlbum
    {
        get => _selectedAlbum;
        set => this.RaiseAndSetIfChanged(ref _selectedAlbum, value);
    }

    /// <summary>
    /// 搜过字符串
    /// </summary>
    public string? SearchText
    {
        get => _searchText;
        set => this.RaiseAndSetIfChanged(ref _searchText, value);
    }

    /// <summary>
    /// 是否正在查询
    /// </summary>
    public bool IsBusy
    {
        get => _isBusy;
        set => this.RaiseAndSetIfChanged(ref _isBusy, value);
    }

    private async void DoSearch(string s)
    {
        // 每次搜索的时候取消令牌
        // 因此，如果仍有正在加载专辑封面的现有请求，它将被取消。
        // 同样，由于 _cancellationTokenSource 可能会被另一个线程异步替换
        // 因此您必须使用存储为局部变量的副本进行操作。
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = _cancellationTokenSource.Token;

        IsBusy = true;
        SearchResults.Clear();

        if (!string.IsNullOrWhiteSpace(s))
        {
            var albums = await Album.SearchAsync(s);

            foreach (var album in albums)
            {
                var vm = new AlbumViewModel(album);
                SearchResults.Add(vm);
            }

            if (!cancellationToken.IsCancellationRequested) LoadCovers(cancellationToken);
        }

        IsBusy = false;
    }

    private async void LoadCovers(CancellationToken cancellationToken)
    {
        foreach (var album in SearchResults.ToList())
        {
            await album.LoadCover();

            if (cancellationToken.IsCancellationRequested) return;
        }
    }
}