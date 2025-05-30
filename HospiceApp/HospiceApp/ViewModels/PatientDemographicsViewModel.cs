using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospiceApp.ViewModels;
using HospiceApp.Models;

namespace HospiceApp.ViewModels;

public partial class PatientDemographicsViewModel : ObservableObject
{
    [ObservableProperty] public bool _isDemographicsVisible;
    
    [ObservableProperty] private string _fullName;
    [ObservableProperty] private DateTime _dateOfBirth = DateTime.Today;
    [ObservableProperty] private string _fullAdress;
    [ObservableProperty] private string _phoneNumber;
    [ObservableProperty] private string _secondaryPhoneNumber;
    [ObservableProperty] private string _primaryInsurance;
    
    public Demographics GetDemographicsInfo()
    {
        return new Demographics
        {
            FullName = FullName,
            DateOfBirth = DateOnly.FromDateTime(DateOfBirth),
            FullAdress = FullAdress,
            PhoneNumber = PhoneNumber,
            SecondaryPhoneNumber = SecondaryPhoneNumber,
            PrimaryInsurance = PrimaryInsurance
        };
    }
}