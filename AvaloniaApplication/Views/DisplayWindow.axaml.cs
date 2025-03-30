using Avalonia.Controls;
using AvaloniaApplication.ViewModels;

namespace AvaloniaApplication;

public partial class DisplayWindow : Window
{
    public DisplayWindow()
    {
        InitializeComponent();
        this.DataContext = new DisplayWindowViewModel();
    }
}