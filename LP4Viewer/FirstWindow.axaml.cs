using System;
using System.Collections.Generic;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using LP4Viewer;

namespace Simple3DRender;

public partial class FirstWindow : Window
{
    public FirstWindow()
    {
        InitializeComponent();
    }

    private void DisplayButton_OnClick(object? sender, RoutedEventArgs e)
    {
        Program.Args[0] = LocationBox.Text!;
        if (!File.Exists(Program.Args[0])) return;
        var mw = new MainWindow();
        mw.Show();
    }

    private void Control_OnLoaded(object? sender, RoutedEventArgs e)
    {
        LocationBox.Text = Program.Args[0];
    }

    private static FilePickerFileType LP4Type { get; } = new("LP4 model files")
    {
        Patterns = ["*.lp4"],
    };
    
    private async void ImportLP4_Click(object? sender, RoutedEventArgs e)
    {
        // Get top level from the current control. Alternatively, you can use Window reference instead.
        var topLevel = TopLevel.GetTopLevel(this);

        // Start async operation to open the dialog.
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open LP4",
            FileTypeFilter = [LP4Type],
            AllowMultiple = false
        });

        if (files.Count < 1) return;
        // Open reading stream from the first file.
        var file = files[0];
        var actualFile = Uri.UnescapeDataString(file.Path.AbsolutePath);
        LocationBox.Text = actualFile;
    }

    private void ForceLoad_OnIsCheckedChanged(object? sender, RoutedEventArgs e)
    {
        Program.ForceLoad = ForceLoad.IsChecked!.Value;
    }
}