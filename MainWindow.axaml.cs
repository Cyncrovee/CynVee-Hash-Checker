using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace CynVee_Hash_Checker;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
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
}