using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospiceApp.Models;
using HospiceApp.Services.Abstract;

namespace HospiceApp.ViewModels;

public partial class SearchDiseasesViewModel : ObservableObject
{
    private readonly IStrapiService _strapiService;

    public ObservableCollection<Disease> Diseases { get; } = new ObservableCollection<Disease>();
    
    [ObservableProperty] private string _name;
    public IAsyncRelayCommand SearchCommand { get; }
    
    public SearchDiseasesViewModel(IStrapiService strapiService)
    {
        _strapiService = strapiService;
        SearchCommand = new AsyncRelayCommand(SearchDiseases);
    }

    private async Task SearchDiseases()
    {
        Diseases.Clear();

        var desieses = await _strapiService.GetDiseasesByNameAsync(Name);
        
        foreach (var desiese in desieses)
        {
            Diseases.Add(desiese);
        }
    }
}