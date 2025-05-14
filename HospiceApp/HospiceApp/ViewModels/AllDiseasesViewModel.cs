using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using HospiceApp.Models;
using HospiceApp.Services.Abstract;

namespace HospiceApp.ViewModels;

public partial class AllDiseasesViewModel : ObservableObject
{
    public ObservableCollection<Disease> Diseases { get; } = new ObservableCollection<Disease>();
    
    private readonly IStrapiService _strapiService;
    
    public AllDiseasesViewModel(IStrapiService strapiService)
    {
        _strapiService = strapiService;
        GetIllnesses();
    }
    
    private async Task GetIllnesses()
    {
        var diseasesList = await _strapiService.GetDiseasesAsync();
        foreach (var disease in diseasesList)
        {
            Diseases.Add(disease);
        }
    }
}