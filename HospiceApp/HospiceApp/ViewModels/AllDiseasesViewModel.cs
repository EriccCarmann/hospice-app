using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospiceApp.Models;
using HospiceApp.Services.Abstract;
using HospiceApp.Views;

namespace HospiceApp.ViewModels;

public partial class AllDiseasesViewModel : ObservableObject
{
    public ObservableCollection<Disease> Diseases { get; } = new ObservableCollection<Disease>();
    
    private readonly IStrapiService _strapiService;
    private readonly IPopupService _popupService;
    
    public IRelayCommand EditCommand { get; set; } 
    public AllDiseasesViewModel(IStrapiService strapiService, IPopupService popupService)
    {
        _strapiService = strapiService;
        _popupService = popupService;
        
        GetIllnesses();
        EditCommand = new RelayCommand(Test);
    }

    private void Test()
    {
        _popupService.ShowPopup<AddOrEditDiseasePopupViewModel>();
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