using System.Collections.ObjectModel;
using ReactiveUI;

namespace Avalonia.MusicStore.ViewModels;

public class MusicStoreViewModel: ViewModelBase
{
    private string? _searchText;
    private bool _isBusy;
    private AlbumViewModel? _selectedAlbum;

    public MusicStoreViewModel()
    {
        // 数据模拟
        SearchResults.Add(new AlbumViewModel());
        SearchResults.Add(new AlbumViewModel());
        SearchResults.Add(new AlbumViewModel());
    }
    
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
}