using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LoginTestAppMaui.Models;
using LoginTestAppMaui.Services.Abstract;

namespace HospiceApp.ViewModels;

public partial class MainViewModel : ObservableObject
{
    int count = 0;
    public ObservableCollection<Illness> Illnesses { get; } = new ObservableCollection<Illness>();

    
    private readonly IStrapiService _strapiService;
    
    [ObservableProperty] private string _counter;
    public IRelayCommand CounterCommand { get; set; }

    public MainViewModel(IStrapiService strapiService)
    {
        _strapiService = strapiService;
        CounterCommand = new RelayCommand(OnCounterCommand);

        getIllnesses();
    }

    private async Task getIllnesses()
    {
        var illnessesList = await _strapiService.GetIllnessesAsync();
        foreach (var illness in illnessesList)
        {
            Illnesses.Add(illness);
        }
    }
    
    private void OnCounterCommand()
    {
        count++;

        if (count == 1)
            Counter = $"Clicked {count} time";
        else
            Counter = $"Clicked {count} times";
    }
}