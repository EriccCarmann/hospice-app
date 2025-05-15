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
    
    public IRelayCommand<Disease> EditCommand { get; set; } 
    public IAsyncRelayCommand AddDiseaseCommand { get; set; } 
    public AllDiseasesViewModel(IStrapiService strapiService, IPopupService popupService)
    {
        _strapiService = strapiService;
        _popupService = popupService;
        
        GetIllnesses();
        EditCommand = new RelayCommand<Disease>(ShowDiseasePopup);
        AddDiseaseCommand = new AsyncRelayCommand(AddDisease);
    }

    private async Task AddDisease()
    {
        await _strapiService.AddDiseaseAsync(new Disease()
        {
            Name = "Test",
            Description = "Test",
            ICDCode = "Test",
            IsHospiceEligible = true
        });
        Diseases.Clear();
        GetIllnesses();
    }
    
    private void ShowDiseasePopup(Disease disease)
    {
        _popupService.ShowPopup<AddOrEditDiseasePopupViewModel>(vm =>
        {
            vm.Name = disease.Name;
            vm.ICDCode = disease.ICDCode;
            vm.Description = disease.Description;
            vm.IsHospiceEligible = disease.IsHospiceEligible;
        });
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