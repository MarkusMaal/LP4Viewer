using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using Avalonia.Threading;
using TM2View;

namespace LP4Viewer;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Control_OnLoaded(object? sender, RoutedEventArgs e)
    {
        DispatcherTimer dpt = new DispatcherTimer();
        dpt.Interval = TimeSpan.FromMilliseconds(100);
        dpt.Tick += (_, _) =>
        {
            FPSLabel.Content = GlControl.GetInfo();
            MoreInfoLabel.Content = GlControl.GetInfo(true);
        };
        dpt.Start();
        foreach (var s in GlControl.GetVertices().Split("\n"))
        {
            Vertices.Items.Add(s);
        }

        GlControl.Focus();
    }
}