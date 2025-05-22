using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospiceApp.Services.Abstract;
using HospiceApp.Views;

namespace HospiceApp.ViewModels;

public partial class InputUserDataViewModel : ObservableObject
{
    private readonly IStrapiService _strapiService;

    [ObservableProperty]
    private bool _isDemographicsVisible = true;

    [ObservableProperty]
    private bool _isHealthIssuesVisible = false;
    
    public IRelayCommand NextViewCommand { get; }

    public InputUserDataViewModel(IStrapiService strapiService)
    {
        _strapiService = strapiService;
        
        NextViewCommand = new RelayCommand(OnNextViewCommand);
        
        IsHealthIssuesVisible = false;
        IsDemographicsVisible = true;

    }

    private void OnNextViewCommand()
    {
        IsDemographicsVisible = false;
        IsHealthIssuesVisible = true;
        
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
}