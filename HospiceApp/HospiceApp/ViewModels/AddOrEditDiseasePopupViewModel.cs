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
    public RelayCommand CancelCommand { get; set; }

    public AddOrEditDiseasePopupViewModel(IPopupService popupService)
    {
        _popupService = popupService;
        CancelCommand = new RelayCommand(Cancel);
    }

    private void Cancel()
    {
        _popupService.ClosePopup();
    }
}