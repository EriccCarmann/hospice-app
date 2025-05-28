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

    public PatientDemographicsViewModel DemographicsViewModel { get; }
    public HealthIssuesViewModel HealthIssuesViewModel { get; }

    public IRelayCommand NextViewCommand { get; }
    public IRelayCommand PreviousViewCommand { get; }
    
    public InputUserDataViewModel(IStrapiService strapiService)
    {
        _strapiService = strapiService;
        
        // Create child ViewModels
        DemographicsViewModel = new PatientDemographicsViewModel();
        HealthIssuesViewModel = new HealthIssuesViewModel(_strapiService);
        
        IsDemographicsVisible = true;
        IsHealthIssuesVisible = false;
        NextViewCommand = new RelayCommand(OnNextViewCommand);
        PreviousViewCommand = new RelayCommand(OnPrevious);
    }

    private void OnNextViewCommand()
    {
        if (IsDemographicsVisible)
        {
            IsDemographicsVisible = false;
            IsHealthIssuesVisible = true;
        }
        else if (IsHealthIssuesVisible)
        {
            // TODO: Handle next view transition
            IsHealthIssuesVisible = false;
        }
    }

    private void OnPrevious()
    {
        IsDemographicsVisible = true;
        IsHealthIssuesVisible = false;
        PreviousViewCommandVisibility = false;
    }
}