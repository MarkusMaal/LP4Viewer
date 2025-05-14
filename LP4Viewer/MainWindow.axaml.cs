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
    }

    private async void ImportLP4_Click(object? sender, RoutedEventArgs e)
    {
        // Get top level from the current control. Alternatively, you can use Window reference instead.
        var topLevel = TopLevel.GetTopLevel(this);

        // Start async operation to open the dialog.
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open LP4",
            AllowMultiple = false
        });

        if (files.Count < 1) return;
        // Open reading stream from the first file.
        var file = files[0];
        var actualFile = Uri.UnescapeDataString(file.Path.AbsolutePath);
        GlControl.ImportLP4(actualFile);
    }
}