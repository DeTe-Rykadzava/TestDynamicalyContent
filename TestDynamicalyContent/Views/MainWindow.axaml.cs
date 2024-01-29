using System;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using TestDynamicalyContent.ViewModels;

namespace TestDynamicalyContent.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        this.WhenActivated(d => d(ViewModel!.UpdateContext.RegisterHandler(Handler)));
    }

    private async Task Handler(InteractionContext<string, Unit> obj)
    {
        try
        {
            var target = AvaloniaRuntimeXamlLoader.Parse<ContentControl>(obj.Input);
    
            var panel = this.FindControl<Panel>("ContentPanel");
            
            panel?.Children.Clear();
            panel?.Children.Add(target);
            obj.SetOutput(Unit.Default);
        
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error");
            Console.ResetColor();
            Console.WriteLine(e);
            obj.SetOutput(Unit.Default);
        }
    }
}