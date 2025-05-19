using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospiceApp.Models;
using HospiceApp.Services.Abstract;
using zoft.MauiExtensions.Core.Extensions;

namespace HospiceApp.ViewModels;

public partial class SearchDiseasesViewModel : ObservableObject
{
    public List<Disease> DiseasesAll { get; } = new List<Disease>();//delete
    
    [ObservableProperty]
    private ObservableCollection<string> _filteredList = new ();

    [ObservableProperty]
    private string _selectedItem;

    [ObservableProperty]
    private int _cursorPosition;
    
    public IAsyncRelayCommand<string> TextChangedCommand { get; }
    
    private readonly IStrapiService _strapiService;

    public ObservableCollection<Disease> Diseases { get; } = new ();
    
    [ObservableProperty] private string _name;
    public IAsyncRelayCommand SearchCommand { get; }
    
    public SearchDiseasesViewModel(IStrapiService strapiService)
    {
        _strapiService = strapiService;

        MainThread.BeginInvokeOnMainThread(() =>
        {
            GetDiseases();
        });
        
        SearchCommand = new AsyncRelayCommand(SearchDiseases);
        TextChangedCommand = new AsyncRelayCommand<string>(OnTextChangedAsync);
    }

    private async Task OnTextChangedAsync(string text)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            FilteredList.Clear();
            SelectedItem = null;
        });

        if (string.IsNullOrWhiteSpace(text))
            return;

        // fetch matching diseases by name
        var matches = await _strapiService.GetDiseasesByNameAsync(text);

        // Update the filtered list on the main thread
        MainThread.BeginInvokeOnMainThread(() =>
        {
            foreach (var disease in matches)
            {
                FilteredList.Add(disease.Name);
            }
        });
    }

    // private async Task<List<string>> GetNames()
    // {
    //     var diseases = await _strapiService.GetDiseasesAsync();
    //     var stringList = new List<string>();
    //     
    //     foreach (var disease in diseases)
    //     {
    //         stringList.Add(disease.Name);;
    //     }
    //     
    //    return stringList;
    // }
    
    private async Task GetDiseases()
    {
        // Diseases.AddRange(await _strapiService.GetDiseasesAsync());
        //
        // foreach (var disease in Diseases)
        // {
        //     FilteredList.Add(disease.Name);
        // }
        
        // MainThread.BeginInvokeOnMainThread(() =>
        // {
        //     DiseasesAll.Clear();
        //     FilteredList.Clear();
        //     
        //     foreach (var disease in diseasesList)
        //     {
        //         DiseasesAll.Add(disease);
        //     }
        // });
    }
    
    private void FilterList(string filter)
    {
        SelectedItem = null;
        FilteredList.Clear();
        FilteredList.Add(filter);
        // FilteredList.AddRange(Teams.Where(t => t.Group.Contains(filter, StringComparison.CurrentCultureIgnoreCase) ||
        //                                        t.Country.Contains(filter, StringComparison.CurrentCultureIgnoreCase)));
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