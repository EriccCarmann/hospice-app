using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospiceApp.Models;
using HospiceApp.Services.Abstract;
using zoft.MauiExtensions.Core.Extensions;

namespace HospiceApp.ViewModels;

public partial class DiseaseName : ObservableObject
{
    [ObservableProperty]
    public string _name;
}

public partial class SearchDiseasesViewModel : ObservableObject
{
    public readonly List<DiseaseName> DiseaseNames = new();
    
    [ObservableProperty]
    private ObservableCollection<DiseaseName> _filteredList = new();

    [ObservableProperty]
    private DiseaseName _selectedItem;
    
    [ObservableProperty] private string _name;
    
    [ObservableProperty]
    public int _cursorPosition;
    
    private readonly IStrapiService _strapiService;
    
     public ObservableCollection<Disease> Diseases { get; } = new ();
     
     public IAsyncRelayCommand SearchCommand { get; }
    
    public SearchDiseasesViewModel(IStrapiService strapiService)
    {
        _strapiService = strapiService;
        
        SearchCommand = new AsyncRelayCommand(SearchDiseases);
    }
    
    [RelayCommand]
    private async Task TextChanged(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            FilteredList.Clear();
            return;
        }

        await FilterList(text);
    }
    
    private async Task FilterList(string filter)
    {
        await GetNames(filter);
        
        SelectedItem = null;
        FilteredList.Clear();
        DiseaseNames.Clear();
    
        var diseases = await getDiseases(filter);

        foreach (var disease in diseases)
        {
            DiseaseNames.Add(new DiseaseName { Name = disease.Name });
        }

        var matches = DiseaseNames
            .Where(t => !string.IsNullOrWhiteSpace(t.Name) &&
                        t.Name.Contains(filter, StringComparison.CurrentCultureIgnoreCase));

        foreach (var match in matches)
            FilteredList.Add(match);
    }
    
    private async Task SearchDiseases()
    {
        Diseases.Clear();
    
        var diseases = await _strapiService.GetDiseasesByNameAsync(Name);
        
        foreach (var disease in diseases)
        {
            Diseases.Add(disease);
        }
    }
    
    private async Task<List<Disease>> getDiseases(string filter)
    {
        return await _strapiService.GetDiseasesByNameAsync(filter);
    }

    private async Task GetNames(string filter)
    {
        var diseases = await _strapiService.GetDiseasesByNameAsync(filter);
        
        foreach (var disease in diseases)
        {
            if (!DiseaseNames.Any(d => d.Name == disease.Name))
            {
                DiseaseNames.Add(new DiseaseName { Name = disease.Name });
            }
        }
        
        FilteredList = new(DiseaseNames);
        SelectedItem = null;
    }
}