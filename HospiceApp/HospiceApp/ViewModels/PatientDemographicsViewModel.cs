using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace HospiceApp.ViewModels;

public partial class PatientDemographicsViewModel : ObservableObject
{
    [ObservableProperty] private string _fullName;
    [ObservableProperty] private DateTime _dateOfBirth = DateTime.Now;
    [ObservableProperty] private string _fullAdress;
    [ObservableProperty] private string _phoneNumber;
    [ObservableProperty] private string _secondaryPhoneNumber;
    [ObservableProperty] private string _primaryInsurance;
    
    public IRelayCommand ContinueCommand { get; }

    public PatientDemographicsViewModel()
    {
        ContinueCommand = new RelayCommand(OnContinue);
    }

    private void OnContinue()
    {
        // TODO: Implement continue logic
    }
}