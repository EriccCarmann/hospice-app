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
    public readonly List<DiseaseName> DiseaseNames  = new List<DiseaseName>();
    
    [ObservableProperty]
    private ObservableCollection<DiseaseName> _filteredList;

    [ObservableProperty]
    private DiseaseName _selectedItem;
    
    
    
    
    
    

    [ObservableProperty]
    public int _cursorPosition;
    

    
    private readonly IStrapiService _strapiService;
    
     public ObservableCollection<Disease> Diseases { get; } = new ();
    
    [ObservableProperty] private string _name;
     public IAsyncRelayCommand SearchCommand { get; }
    
    public SearchDiseasesViewModel(IStrapiService strapiService)
    {
        _strapiService = strapiService;

        GetNames();
        
        SearchCommand = new AsyncRelayCommand(SearchDiseases);
        //TextChangedCommand = new AsyncRelayCommand<string>(OnTextChangedAsync);
    }
    
    [RelayCommand]
    private void TextChanged(string text)
    {
        FilterList(text);
    }
    private void FilterList(string filter)
    {
        SelectedItem = null;
        FilteredList.Clear();

        FilteredList.AddRange(DiseaseNames.Where(t => t.Name.Contains(filter, StringComparison.CurrentCultureIgnoreCase)));
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
    
    private async Task GetNames()
    {
        var diseases = await _strapiService.GetDiseasesAsync();
        
        foreach (var disease in diseases)
        {
            DiseaseNames.Add(new DiseaseName{Name = disease.Name});
        }
        
        FilteredList = new(DiseaseNames);
        SelectedItem = null;
    }
}