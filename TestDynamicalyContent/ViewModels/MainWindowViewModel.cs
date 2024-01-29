using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;
using ReactiveUI;

namespace TestDynamicalyContent.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private string _viewString = "";
    public string ViewString
    {
        get => _viewString;
        set => this.RaiseAndSetIfChanged(ref _viewString, value);
    }

    public Interaction<string, Unit> UpdateContext { get; }

    public ICommand SetNewContent { get; }

    public MainWindowViewModel()
    {
        UpdateContext = new Interaction<string, Unit>();
        var canExecuteSetNewContent = this
            .WhenAnyValue(x => x.ViewString, (content)=> !string.IsNullOrWhiteSpace(content))
            .DistinctUntilChanged();
        SetNewContent = ReactiveCommand.CreateFromTask(async () =>
        {
            await UpdateContext.Handle(ViewString);
        }, canExecuteSetNewContent);
    }
}