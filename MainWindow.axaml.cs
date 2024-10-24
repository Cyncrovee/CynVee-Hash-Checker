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
    public string filePath = null;

    public string sha1String = null;
    public string sha256String = null;
    public string sha512String = null;
    public string md5String = null;

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
            filePath = file.First().Path.LocalPath;
            filePathBlock.Text = "Currently Selected File: " + filePath;
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

    private async void sha1CopyButton_Click(object sender, RoutedEventArgs e)
    {
        if (sha1String != null & Clipboard != null)
        {
            await Clipboard.SetTextAsync(sha1String);
        }
    }
    private async void sha256CopyButton_Click(object sender, RoutedEventArgs e)
    {
        if (sha256String != null & Clipboard != null)
        {
            await Clipboard.SetTextAsync(sha256String);
        }
    }
    private async void sha512CopyButton_Click(object sender, RoutedEventArgs e)
    {
        if (sha512String != null & Clipboard != null)
        {
            await Clipboard.SetTextAsync(sha512String);
        }
    }
    private async void md5CopyButton_Click(object sender, RoutedEventArgs e)
    {
        if (md5String != null & Clipboard != null)
        {
            await Clipboard.SetTextAsync(md5String);
        }
    }

    private void calculateButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            sha1String = calculateSHA1();
            sha256String = calculateSHA256();
            sha512String = calculateSHA512();
            md5String = calculateMD5();

            if (sha1TextBox.Text != null)
            {
                bool sha1Result = sha1TextBox.Text.Equals(sha1String);
                if (sha1Result == true)
                {
                    sha1ResultBlock.Text = "Result: True- Hashes match! Value: " + sha1String;
                    sha1ResultBlock.Foreground = Brushes.LightGreen;
                }
                else
                {
                    sha1ResultBlock.Text = "Result: False- Hashes dont match! Actual value: " + sha1String;
                    sha1ResultBlock.Foreground = Brushes.Crimson;
                }
            }
            if (sha256TextBox.Text != null)
            {
                bool sha256Result = sha256TextBox.Text.Equals(sha256String);
                if (sha256Result == true)
                {
                    sha256ResultBlock.Text = "Result: True- Hashes match! Value: " + sha256String;
                    sha256ResultBlock.Foreground = Brushes.LightGreen;
                }
                else
                {
                    sha256ResultBlock.Text = "Result: False- Hashes dont match! Actual value: " + sha256String;
                    sha256ResultBlock.Foreground = Brushes.Crimson;
                }
            }
            if (sha512TextBox.Text != null)
            {
                bool sha512Result = sha512TextBox.Text.Equals(sha512String);
                if (sha512Result == true)
                {
                    sha512ResultBlock.Text = "Result: True- Hashes match! Value: " + sha512String;
                    sha512ResultBlock.Foreground = Brushes.LightGreen;
                }
                else
                {
                    sha512ResultBlock.Text = "Result: False- Hashes dont match! Actual value: " + sha512String;
                    sha512ResultBlock.Foreground = Brushes.Crimson;
                }
            }
            if (md5TextBox.Text != null)
            {
                bool md5Result = md5TextBox.Text.Equals(md5String);
                if (md5Result == true)
                {
                    md5ResultBlock.Text = "Result: True- Hashes match! Value: " + md5String;
                    md5ResultBlock.Foreground = Brushes.LightGreen;
                }
                else
                {
                    md5ResultBlock.Text = "Result: False- Hashes dont match! Actual value: " + md5String;
                    md5ResultBlock.Foreground = Brushes.Crimson;
                }
            }
        }
        catch
        {
            Console.WriteLine("Failed to calculate!");
        }
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
    /*public string calculateSHA384()
    {
        using (var sha384 = SHA384.Create())
        {
            using (var stream = File.OpenRead(filePath))
            {
                var hash = sha384.ComputeHash(stream)
                return BitConverter.ToString(hash).Replace("-", String.Empty).ToLowerInvariant();
            }
        }
    }*/
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