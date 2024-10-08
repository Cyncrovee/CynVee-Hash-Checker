using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;

namespace CynVee_Hash_Checker;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    string filePath = null;
    private async void selectFileButton_Click(object sender, RoutedEventArgs e)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        var file = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open Text File",
            AllowMultiple = false
        });

        if (file.Count >= 1)
        {
            filePath = file.First().Path.ToString();
        }
    }

    private void sha1Button_Click(object sender, RoutedEventArgs e)
    {
        sha1TextBox.Paste();
    }
    private void sha256Button_Click(object sender, RoutedEventArgs e)
    {
        sha256TextBox.Paste();
    }
    private void sha512Button_Click(object sender, RoutedEventArgs e)
    {
        sha512TextBox.Paste();
    }
    private void md5Button_Click(object sender, RoutedEventArgs e)
    {
        md5TextBox.Paste();
    }

    private void calculateButton_Click(object sender, RoutedEventArgs e)
    {
        using (var md5 = MD5.Create())
        {
            using (var stream = File.OpenRead(filePath))
            {
                Console.WriteLine("temp");
            }
        }
    }
}