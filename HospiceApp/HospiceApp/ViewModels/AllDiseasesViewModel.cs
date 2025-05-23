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
    public IAsyncRelayCommand<Disease> DeleteCommand { get; set; } 
    public AllDiseasesViewModel(IStrapiService strapiService, IPopupService popupService)
    {
        _strapiService = strapiService;
        _popupService = popupService;
        
        GetDiseases();
        
        EditCommand = new RelayCommand<Disease>(EditDisease);
        AddDiseaseCommand = new AsyncRelayCommand(AddDisease);
        DeleteCommand = new AsyncRelayCommand<Disease>(DeleteDisease);
    }

    private async Task AddDisease()
    {
        var newDisease = new Disease();
        
        _popupService.ShowPopup<AddOrEditDiseasePopupViewModel>(vm =>
        {
            vm.SaveAction = async () =>
            {
                newDisease = new Disease()
                {
                    Name = vm.Name ?? string.Empty,
                    Description = vm.Description ?? string.Empty,
                    ICDCode = vm.ICDCode ?? string.Empty,
                    IsHospiceEligible = vm.IsHospiceEligible
                };
                
                await _strapiService.AddDiseaseAsync(newDisease);
                
                Diseases.Add(newDisease);
            };
        });
    }

    private async Task DeleteDisease(Disease disease)
    {
        await _strapiService.DeleteDiseaseAsync(disease.Name);
        Diseases.Remove(disease);
    }
    
    private void EditDisease(Disease disease)
    {
        _popupService.ShowPopup<AddOrEditDiseasePopupViewModel>(vm =>
        {
            vm.Name = disease.Name;
            vm.ICDCode = disease.ICDCode;
            vm.Description = disease.Description;
            vm.IsHospiceEligible = disease.IsHospiceEligible;
            vm.SaveAction = async () =>
            {
                var newDisease = new Disease()
                {
                    Name = vm.Name ?? string.Empty,
                    Description = vm.Description ?? string.Empty,
                    ICDCode = vm.ICDCode ?? string.Empty,
                    IsHospiceEligible = vm.IsHospiceEligible
                };
                
                await _strapiService.UpdateDiseaseAsync(disease.Name, newDisease);
                
                Diseases.Remove(disease);
                Diseases.Add(newDisease);
            };
        });
        
        
    }
    
    private async Task GetDiseases()
    {
        var diseasesList = await _strapiService.GetDiseasesAsync();
        foreach (var disease in diseasesList)
        {
            Diseases.Add(disease);
        }
    }
}