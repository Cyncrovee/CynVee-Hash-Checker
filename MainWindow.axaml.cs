using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Platform.Storage;
using Color = Avalonia.Media.Color;

namespace CynVee_Hash_Checker;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private string _filePath;

    private string _sha1String;
    private string _sha256String;
    private string _sha384String;
    private string _sha512String;
    private string _md5String;

    private async void SelectFileButton_Click(object sender, RoutedEventArgs e)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        var file = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open Text File",
            AllowMultiple = false
        });

        if (file.Count >= 1)
        {
            _filePath = file.First().Path.LocalPath;
            FilePathBlock.Text = "Currently Selected File: " + _filePath;
        }
    }
    
    private void ClearAllButton_Click(object? sender, RoutedEventArgs e)
    {
        Sha1TextBox.Clear();
        Sha256TextBox.Clear();
        Sha384TextBox.Clear();
        Sha512TextBox.Clear();
        Md5TextBox.Clear();
    }
    
    private void sha1ClearButton_Click(object? sender, RoutedEventArgs e)
    {
        Sha1TextBox.Clear();
    }
    private void sha256ClearButton_Click(object? sender, RoutedEventArgs e)
    {
        Sha256TextBox.Clear();
    }
    private void sha384ClearButton_Click(object? sender, RoutedEventArgs e)
    {
        Sha384TextBox.Clear();
    }
    private void sha512ClearButton_Click(object? sender, RoutedEventArgs e)
    {
        Sha512TextBox.Clear();
    }
    private void md5ClearButton_Click(object? sender, RoutedEventArgs e)
    {
        Md5TextBox.Clear();
    }

    private void sha1PasteButton_Click(object sender, RoutedEventArgs e)
    {
        Sha1TextBox.Paste();
    }
    private void sha256PasteButton_Click(object sender, RoutedEventArgs e)
    {
        Sha256TextBox.Paste();
    }
    private void sha384PasteButton_Click(object sender, RoutedEventArgs e)
    {
        Sha384TextBox.Paste();
    }
    private void sha512PasteButton_Click(object sender, RoutedEventArgs e)
    {
        Sha512TextBox.Paste();
    }
    private void md5PasteButton_Click(object sender, RoutedEventArgs e)
    {
        Md5TextBox.Paste();
    }

    private async void sha1CopyButton_Click(object sender, RoutedEventArgs e)
    {
        if (_sha1String != null & Clipboard != null)
        {
            await Clipboard.SetTextAsync(_sha1String);
        }
    }
    private async void sha256CopyButton_Click(object sender, RoutedEventArgs e)
    {
        if (_sha256String != null & Clipboard != null)
        {
            await Clipboard.SetTextAsync(_sha256String);
        }
    }
    private async void sha384CopyButton_Click(object sender, RoutedEventArgs e)
    {
        if (_sha384String != null & Clipboard != null)
        {
            await Clipboard.SetTextAsync(_sha384String);
        }
    }
    private async void sha512CopyButton_Click(object sender, RoutedEventArgs e)
    {
        if (_sha512String != null & Clipboard != null)
        {
            await Clipboard.SetTextAsync(_sha512String);
        }
    }
    private async void md5CopyButton_Click(object sender, RoutedEventArgs e)
    {
        if (_md5String != null & Clipboard != null)
        {
            await Clipboard.SetTextAsync(_md5String);
        }
    }

    private void CalculateButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            _sha1String = CalculateSha1();
            _sha256String = CalculateSha256();
            _sha512String = CalculateSha512();
            _sha384String = CalculateSha384();
            _md5String = CalculateMd5();

            if (Sha1TextBox.Text != null)
            {
                bool sha1Result = Sha1TextBox.Text.Equals(_sha1String);
                if (sha1Result == true)
                {
                    Sha1ResultBlock.Text = "Result: True- Hashes match! Value: " + _sha1String;
                    Sha1ResultBlock.Foreground = Brushes.LightGreen;
                }
                else
                {
                    Sha1ResultBlock.Text = "Result: False- Hashes dont match! Actual value: " + _sha1String;
                    Sha1ResultBlock.Foreground = Brushes.Crimson;
                }
            }
            if (Sha256TextBox.Text != null)
            {
                bool sha256Result = Sha256TextBox.Text.Equals(_sha256String);
                if (sha256Result == true)
                {
                    Sha256ResultBlock.Text = "Result: True- Hashes match! Value: " + _sha256String;
                    Sha256ResultBlock.Foreground = Brushes.LightGreen;
                }
                else
                {
                    Sha256ResultBlock.Text = "Result: False- Hashes dont match! Actual value: " + _sha256String;
                    Sha256ResultBlock.Foreground = Brushes.Crimson;
                }
            }
            if (Sha384TextBox.Text != null)
            {
                bool sha384Result = Sha384TextBox.Text.Equals(_sha384String);
                if (sha384Result == true)
                {
                    Sha384ResultBlock.Text = "Result: True- Hashes match! Value: " + _sha384String;
                    Sha384ResultBlock.Foreground = Brushes.LightGreen;
                }
                else
                {
                    Sha384ResultBlock.Text = "Result: False- Hashes dont match! Actual value: " + _sha384String;
                    Sha384ResultBlock.Foreground = Brushes.Crimson;
                }
            }
            if (Sha512TextBox.Text != null)
            {
                bool sha512Result = Sha512TextBox.Text.Equals(_sha512String);
                if (sha512Result == true)
                {
                    Sha512ResultBlock.Text = "Result: True- Hashes match! Value: " + _sha512String;
                    Sha512ResultBlock.Foreground = Brushes.LightGreen;
                }
                else
                {
                    Sha512ResultBlock.Text = "Result: False- Hashes dont match! Actual value: " + _sha512String;
                    Sha512ResultBlock.Foreground = Brushes.Crimson;
                }
            }
            if (Md5TextBox.Text != null)
            {
                bool md5Result = Md5TextBox.Text.Equals(_md5String);
                if (md5Result == true)
                {
                    Md5ResultBlock.Text = "Result: True- Hashes match! Value: " + _md5String;
                    Md5ResultBlock.Foreground = Brushes.LightGreen;
                }
                else
                {
                    Md5ResultBlock.Text = "Result: False- Hashes dont match! Actual value: " + _md5String;
                    Md5ResultBlock.Foreground = Brushes.Crimson;
                }
            }
        }
        catch
        {
            Console.WriteLine("Failed to calculate!");
        }
    }
    public string CalculateSha1()
    {
        using (var sha1 = SHA1.Create())
        {
            using (var stream = File.OpenRead(_filePath))
            {
                var hash = sha1.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", String.Empty).ToLowerInvariant();
            }
        }
    }
    public string CalculateSha256()
    {
        using (var sha256 = SHA256.Create())
        {
            using (var stream = File.OpenRead(_filePath))
            {
                var hash = sha256.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", String.Empty).ToLowerInvariant();
            }
        }
    }
    public string CalculateSha384()
    {
        using (var sha384 = SHA384.Create())
        {
            using (var stream = File.OpenRead(_filePath))
            {
                var hash = sha384.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", String.Empty).ToLowerInvariant();
            }
        }
    }
    public string CalculateSha512()
    {
        using (var sha512 = SHA512.Create())
        {
            using (var stream = File.OpenRead(_filePath))
            {
                var hash = sha512.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", String.Empty).ToLowerInvariant();
            }
        }
    }
    public string CalculateMd5()
    {
        using (var md5 = MD5.Create())
        {
            using (var stream = File.OpenRead(_filePath))
            {
                var hash = md5.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", String.Empty).ToLowerInvariant();
            }
        }
    }
}