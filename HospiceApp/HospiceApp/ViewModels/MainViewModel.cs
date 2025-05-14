using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using LoginTestAppMaui.Models;
using LoginTestAppMaui.Services.Abstract;

namespace HospiceApp.ViewModels;

public partial class MainViewModel : ObservableObject
{
    public ObservableCollection<Illness> Illnesses { get; } = new ObservableCollection<Illness>();

    [ObservableProperty] private string _illnessByName;
    
    private readonly IStrapiService _strapiService;
  
    public MainViewModel(IStrapiService strapiService)
    {
        _strapiService = strapiService;

        GetIllnesses();

        GetIllnessByName("stage");
    }

    private async Task GetIllnesses()
    {
        var illnessesList = await _strapiService.GetIllnessesAsync();
        foreach (var illness in illnessesList)
        {
            Illnesses.Add(illness);
        }
    }

    private async Task<List<Illness>> GetIllnessByName(string name)
    {
        var result = await _strapiService.GetIllnessesByNameAsync(name);
        foreach (var illness in result)
        {
            IllnessByName += illness.Name + " ";
        }
        return result;
    }
}