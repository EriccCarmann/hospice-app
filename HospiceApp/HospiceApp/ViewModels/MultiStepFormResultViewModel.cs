using CommunityToolkit.Mvvm.ComponentModel;
using HospiceApp.Models;

namespace HospiceApp.ViewModels;

public partial class MultiStepFormResultViewModel : ObservableObject
{
    [ObservableProperty] public bool _isMultiStepFormResultVisible;
    
    [ObservableProperty] private string _fullName;
    [ObservableProperty] private string _dateOfBirth;
    [ObservableProperty] private string _fullAdress;
    [ObservableProperty] private string _phoneNumber;
    [ObservableProperty] private string _secondaryPhoneNumber;
    [ObservableProperty] private string _primaryInsurance;
    [ObservableProperty] private string _chosenDisease;
    
    [ObservableProperty] private double _painLevel;
    [ObservableProperty] private double _dyspneaLevel;
    [ObservableProperty] private double _nauseaLevel;
    [ObservableProperty] private double _fatigueLevel;
    [ObservableProperty] private double _anxietyLevel;
    [ObservableProperty] private double _confusionLevel;
    [ObservableProperty] private string _needsSymptomSupport;

    public void SetDemographicsInfo(Demographics demographics)
    {
        FullName = demographics.FullName;
        DateOfBirth = demographics.DateOfBirth.ToString();
        FullAdress = demographics.FullAdress;
        PhoneNumber = demographics.PhoneNumber;
        SecondaryPhoneNumber = demographics.SecondaryPhoneNumber;
        PrimaryInsurance = demographics.PrimaryInsurance;
    }

    public void SetDiseaseInfo(string name)
    {
        ChosenDisease = name;
    }

    public void SetSymptomsInfo(Symptoms symptoms)
    {
        PainLevel = symptoms.PainLevel;
        DyspneaLevel = symptoms.DyspneaLevel;
        NauseaLevel = symptoms.NauseaLevel;
        FatigueLevel = symptoms.FatigueLevel;
        AnxietyLevel = symptoms.AnxietyLevel;
        ConfusionLevel = symptoms.ConfusionLevel;
        NeedsSymptomSupport = symptoms.NeedsSymptomSupport;
    }
}