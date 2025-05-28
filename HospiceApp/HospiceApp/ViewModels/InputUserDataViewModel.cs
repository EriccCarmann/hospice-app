using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospiceApp.Services.Abstract;
using HospiceApp.Views;

namespace HospiceApp.ViewModels;

public partial class InputUserDataViewModel : ObservableObject
{
    private readonly IStrapiService _strapiService;
    
    [ObservableProperty] private bool _isDemographicsVisible = true;
    [ObservableProperty] private bool _isHealthIssuesVisible = false;
    [ObservableProperty] private bool _previousViewCommandVisibility = false;

    public PatientDemographicsViewModel PatientDemographicsContext { get; }
    public HealthIssuesViewModel HealthIssuesContext { get; set; }

    public IRelayCommand NextViewCommand { get; }
    public IRelayCommand PreviousViewCommand { get; }
    
    public InputUserDataViewModel(IStrapiService strapiService)
    {
        _strapiService = strapiService;
        
        // Create child ViewModels
        PatientDemographicsContext = new PatientDemographicsViewModel();
        HealthIssuesContext = new HealthIssuesViewModel(_strapiService);

        PatientDemographicsContext.IsDemographicsVisible = true;
        HealthIssuesContext.IsHealthIssuesVisible = false;
        
        NextViewCommand = new RelayCommand(OnNextViewCommand);
        PreviousViewCommand = new RelayCommand(OnPrevious);
    }

    private void OnNextViewCommand()
    {
        if (PatientDemographicsContext.IsDemographicsVisible)
        {
            PatientDemographicsContext.IsDemographicsVisible = false;
            HealthIssuesContext.IsHealthIssuesVisible = true;
            PreviousViewCommandVisibility = true;
        }
        else if (HealthIssuesContext.IsHealthIssuesVisible)
        {
            // TODO: Handle next view transition
            HealthIssuesContext.IsHealthIssuesVisible = false;
        }
    }

    private void OnPrevious()
    {
        PatientDemographicsContext.IsDemographicsVisible = true;
        HealthIssuesContext.IsHealthIssuesVisible = false;
        PreviousViewCommandVisibility = false;
    }
}