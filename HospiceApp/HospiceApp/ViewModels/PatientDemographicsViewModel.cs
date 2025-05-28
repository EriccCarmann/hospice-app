using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospiceApp.ViewModels;

namespace HospiceApp.ViewModels;

public partial class PatientDemographicsViewModel : ObservableObject
{
    [ObservableProperty] public bool isDemographicsVisible;
    [ObservableProperty] private string _fullName;
    [ObservableProperty] private DateTime _dateOfBirth = DateTime.Now;
    [ObservableProperty] private string _fullAdress;
    [ObservableProperty] private string _phoneNumber;
    [ObservableProperty] private string _secondaryPhoneNumber;
    [ObservableProperty] private string _primaryInsurance;
  
    public PatientDemographicsViewModel()
    {
        
    }
}