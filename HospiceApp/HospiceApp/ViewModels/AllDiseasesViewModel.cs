using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using HospiceApp.Models;
using HospiceApp.Services.Abstract;

namespace HospiceApp.ViewModels;

public partial class AllDiseasesViewModel : ObservableObject
{
    public ObservableCollection<Illness> Illnesses { get; } = new ObservableCollection<Illness>();
    
    private readonly IStrapiService _strapiService;
    
    public AllDiseasesViewModel(IStrapiService strapiService)
    {
        _strapiService = strapiService;
        GetIllnesses();
    }
    
    private async Task GetIllnesses()
    {
        var illnessesList = await _strapiService.GetIllnessesAsync();
        foreach (var illness in illnessesList)
        {
            Illnesses.Add(illness);
        }
    }
}