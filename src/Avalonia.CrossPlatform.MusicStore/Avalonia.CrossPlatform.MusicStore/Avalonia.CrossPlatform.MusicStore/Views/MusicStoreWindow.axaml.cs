using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.CrossPlatform.MusicStore.ViewModels;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace Avalonia.CrossPlatform.MusicStore.Views;

public partial class MusicStoreWindow : ReactiveWindow<MusicStoreViewModel>
{
    public MusicStoreWindow()
    {
        InitializeComponent();
        
        this.WhenActivated(action => action(ViewModel!.BuyMusicCommand.Subscribe(Close)));
    }
}