using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace HospiceApp.ViewModels;

public partial class AddOrEditDiseasePopupViewModel : ObservableObject
{
    private readonly IPopupService _popupService;
    [ObservableProperty] private string _name;
    [ObservableProperty] private string _iCDCode;
    [ObservableProperty] private string _description;
    [ObservableProperty] private bool _isHospiceEligible;
    
    public Action SaveAction { get; set; }    
    public IRelayCommand CancelCommand { get; set; }
    public IRelayCommand SaveCommand { get; }
    public AddOrEditDiseasePopupViewModel(IPopupService popupService)
    {
        _popupService = popupService;
        CancelCommand = new RelayCommand(Cancel);
        SaveCommand = new RelayCommand(Save);
    }

    private void Save()
    {
        SaveAction?.Invoke();
        _popupService.ClosePopup();
    }
    
    private void Cancel()
    {
        _popupService.ClosePopup();
    }
}