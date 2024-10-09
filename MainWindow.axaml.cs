using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
    public string filePath = null;

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
            filePath = file.First().Path.PathAndQuery;
            Console.WriteLine(filePath);
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
        var sha1String = calculateSHA1();
        var sha256String = calculateSHA256();
        var sha512String = calculateSHA512();
        var md5String = calculateMD5();
    }

    public string calculateSHA1()
    {
        using (var sha1 = SHA1.Create())
        {
            using (var stream = File.OpenRead(filePath))
            {
                var hash = sha1.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", String.Empty).ToLowerInvariant();
            }
        }
    }
    public string calculateSHA256()
    {
        using (var sha256 = SHA256.Create())
        {
            using (var stream = File.OpenRead(filePath))
            {
                var hash = sha256.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", String.Empty).ToLowerInvariant();
            }
        }
    }
    public string calculateSHA512()
    {
        using (var sha512 = SHA512.Create())
        {
            using (var stream = File.OpenRead(filePath))
            {
                var hash = sha512.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", String.Empty).ToLowerInvariant();
            }
        }
    }
    public string calculateMD5()
    {
        using (var md5 = MD5.Create())
        {
            using (var stream = File.OpenRead(filePath))
            {
                var hash = md5.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", String.Empty).ToLowerInvariant();
            }
        }
    }
}