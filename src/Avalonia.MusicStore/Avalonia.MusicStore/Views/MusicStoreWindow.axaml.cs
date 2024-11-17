using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.MusicStore.ViewModels;
using Avalonia.ReactiveUI;

namespace Avalonia.MusicStore.Views;

public partial class MusicStoreWindow : ReactiveWindow<MainWindowViewModel>
{
    public MusicStoreWindow()
    {
        InitializeComponent();
    }
}