using System;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace GetStartedApp.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Double.TryParse(Celsius.Text, out double C))
        {
            var F = C * (9d / 5d) + 32;
            Fahrenheit.Text = F.ToString("0.0");
        }
        else
        {
            Celsius.Text = "0";
            Fahrenheit.Text = "0";
        }
        Console.WriteLine("Clicked");
        Debug.WriteLine($"Click! Celsius={this.Celsius.Text}");
        return;
        throw new System.NotImplementedException();
    }
}