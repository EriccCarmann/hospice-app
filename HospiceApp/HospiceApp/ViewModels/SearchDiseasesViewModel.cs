using CommunityToolkit.Mvvm.ComponentModel;
using HospiceApp.Models;
using HospiceApp.Services.Abstract;

namespace HospiceApp.ViewModels;

public partial class SearchDiseasesViewModel : ObservableObject
{
    private readonly IStrapiService _strapiService;

    public SearchDiseasesViewModel(IStrapiService strapiService)
    {
        _strapiService = strapiService;
    }
    
    private async Task<List<Illness>> SearchDiseases(string name)
    {
        var result = await _strapiService.GetIllnessesByNameAsync(name);
        foreach (var illness in result)
        {
            
        }
        return result;
    }
}