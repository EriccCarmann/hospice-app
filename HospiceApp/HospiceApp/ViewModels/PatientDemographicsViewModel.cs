using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospiceApp.ViewModels;

namespace HospiceApp.ViewModels;

public partial class PatientDemographicsViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _patientDemographicsViewVisibility;
    
    
    
    
    
    private readonly InputUserDataViewModel _parentViewModel;

    [ObservableProperty] private string _fullName;
    [ObservableProperty] private DateTime _dateOfBirth = DateTime.Now;
    [ObservableProperty] private string _fullAdress;
    [ObservableProperty] private string _phoneNumber;
    [ObservableProperty] private string _secondaryPhoneNumber;
    [ObservableProperty] private string _primaryInsurance;
    
    public IRelayCommand ContinueCommand { get; }

    public PatientDemographicsViewModel()//InputUserDataViewModel parentViewModel
    {
       // _parentViewModel = parentViewModel;
        ContinueCommand = new RelayCommand(OnContinue);
    }

    private void OnContinue()
    {
        // TODO: Validate form data
       // _parentViewModel.ShowHealthIssuesCommand.Execute(null);
    }
}