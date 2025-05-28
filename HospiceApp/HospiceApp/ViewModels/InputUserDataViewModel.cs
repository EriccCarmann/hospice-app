using System.Collections;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospiceApp.Services.Abstract;
using HospiceApp.Views;

namespace HospiceApp.ViewModels;

public partial class InputUserDataViewModel : ObservableObject
{
    [ObservableProperty] private static bool _isDemographicsVisible = true;
    [ObservableProperty] private static bool _isHealthIssuesVisible = false;
    [ObservableProperty] private static bool _previousViewCommandVisibility = false;

    private readonly IStrapiService _strapiService;
    public PatientDemographicsViewModel PatientDemographicsContext { get; }
    public HealthIssuesViewModel HealthIssuesContext { get; set; }
    public SymptomBurdenModelView SymptomBurdenContext { get; set; }
    public IRelayCommand NextViewCommand { get; }
    public IRelayCommand PreviousViewCommand { get; }
    
    public InputUserDataViewModel(IStrapiService strapiService)
    {
        _strapiService = strapiService;

        PatientDemographicsContext = new PatientDemographicsViewModel();
        HealthIssuesContext = new HealthIssuesViewModel(_strapiService);
        SymptomBurdenContext = new SymptomBurdenModelView();
        
        PatientDemographicsContext.IsDemographicsVisible = true;
        HealthIssuesContext.IsHealthIssuesVisible = false;
        SymptomBurdenContext.IsSymptomBurdenVisible = false;
        
        NextViewCommand = new RelayCommand(OnNextViewCommand);
        PreviousViewCommand = new RelayCommand(OnPrevious);
    }

    private void OnNextViewCommand()
    {
        if (PatientDemographicsContext.IsDemographicsVisible)
        {
            PatientDemographicsContext.IsDemographicsVisible = false;
            HealthIssuesContext.IsHealthIssuesVisible = true;
            SymptomBurdenContext.IsSymptomBurdenVisible = false;
            
            PreviousViewCommandVisibility = true;
        }
        else if (HealthIssuesContext.IsHealthIssuesVisible)
        {
            HealthIssuesContext.IsHealthIssuesVisible = false;
            SymptomBurdenContext.IsSymptomBurdenVisible = true;
        }
    }

    private void OnPrevious()
    {
        if (HealthIssuesContext.IsHealthIssuesVisible)
        {
            PatientDemographicsContext.IsDemographicsVisible = true;
            PreviousViewCommandVisibility = false;
            HealthIssuesContext.IsHealthIssuesVisible = false;
        }
        else if (SymptomBurdenContext.IsSymptomBurdenVisible)
        {
            HealthIssuesContext.IsHealthIssuesVisible = true;
            SymptomBurdenContext.IsSymptomBurdenVisible = false;
        }
    }
}