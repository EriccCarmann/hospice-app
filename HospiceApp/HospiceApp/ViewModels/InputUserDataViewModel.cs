using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospiceApp.Services.Abstract;
using zoft.MauiExtensions.Core.Extensions;

namespace HospiceApp.ViewModels;

public partial class InputUserDataViewModel : ObservableObject
{
    [ObservableProperty] private static bool _isDemographicsVisible = true;
    [ObservableProperty] private static bool _isHealthIssuesVisible = false;
    [ObservableProperty] private static bool _previousViewCommandVisibility = false;
    [ObservableProperty] private static bool _nextViewCommandVisibility = true;

    private readonly IStrapiService _strapiService;
    public PatientDemographicsViewModel PatientDemographicsContext { get; }
    public HealthIssuesViewModel HealthIssuesContext { get; set; }
    public SymptomBurdenModelView SymptomBurdenContext { get; set; }
    public MultiStepFormResultViewModel MultiStepFormResultContext { get; set; }
    public IRelayCommand NextViewCommand { get; }
    public IRelayCommand PreviousViewCommand { get; }
    
    public InputUserDataViewModel(IStrapiService strapiService)
    {
        _strapiService = strapiService;

        PatientDemographicsContext = new PatientDemographicsViewModel();
        HealthIssuesContext = new HealthIssuesViewModel(_strapiService);
        SymptomBurdenContext = new SymptomBurdenModelView();
        MultiStepFormResultContext = new MultiStepFormResultViewModel();
        
        PatientDemographicsContext.IsDemographicsVisible = true;
        HealthIssuesContext.IsHealthIssuesVisible = false;
        SymptomBurdenContext.IsSymptomBurdenVisible = false;
        MultiStepFormResultContext.IsMultiStepFormResultVisible = false;
        
        NextViewCommand = new RelayCommand(OnNextViewCommand);
        PreviousViewCommand = new RelayCommand(OnPrevious);

        NextViewCommandVisibility = true;
        PreviousViewCommandVisibility = false;
    }

    private void OnNextViewCommand()
    {
        if (PatientDemographicsContext.IsDemographicsVisible)
        {
            PatientDemographicsContext.IsDemographicsVisible = false;
            HealthIssuesContext.IsHealthIssuesVisible = true;
            SymptomBurdenContext.IsSymptomBurdenVisible = false;

            AddDemographicsDataToResult();
            
            PreviousViewCommandVisibility = true;
        }
        else if (HealthIssuesContext.IsHealthIssuesVisible)
        {
            if (HealthIssuesContext.selectedDiseaseName.IsNullOrEmpty())
            {
                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

                string text = "Pick a disease";
                ToastDuration duration = ToastDuration.Short;
                double fontSize = 14;

                var toast = Toast.Make(text, duration, fontSize);

                toast.Show(cancellationTokenSource.Token);
                return;
            }
            
            HealthIssuesContext.IsHealthIssuesVisible = false;
            SymptomBurdenContext.IsSymptomBurdenVisible = true;
            MultiStepFormResultContext.IsMultiStepFormResultVisible = false;

            AddDiseaseToResult();
        }
        else if (SymptomBurdenContext.IsSymptomBurdenVisible)
        {
            PreviousViewCommandVisibility = false;
            NextViewCommandVisibility = false;
            SymptomBurdenContext.IsSymptomBurdenVisible = false;
            MultiStepFormResultContext.IsMultiStepFormResultVisible = true;
            
            AddSymptomsToResult();
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

    private void AddDemographicsDataToResult()
    {
        var demographics = PatientDemographicsContext.GetDemographicsInfo();
        MultiStepFormResultContext.SetDemographicsInfo(demographics);
    }

    private void AddDiseaseToResult()
    {
        var diseaseName = HealthIssuesContext.selectedDiseaseName;
        MultiStepFormResultContext.SetDiseaseInfo(diseaseName);
    }

    private void AddSymptomsToResult()
    {
        var symptoms = SymptomBurdenContext.GetSymptoms();
        MultiStepFormResultContext.SetSymptomsInfo(symptoms);
    }
}