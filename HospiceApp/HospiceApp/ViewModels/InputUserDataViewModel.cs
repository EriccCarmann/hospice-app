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

    public IRelayCommand NextViewCommand { get; }
    public IRelayCommand PreviousViewCommand { get; }

    public InputUserDataViewModel(IStrapiService strapiService)
    {
        _strapiService = strapiService;
        
        NextViewCommand = new RelayCommand(OnNext);
        PreviousViewCommand = new RelayCommand(OnPrevious);
    }

    private void OnNext()
    {
        IsDemographicsVisible = false;
        IsHealthIssuesVisible = true;
        PreviousViewCommandVisibility = true;
    }

    private void OnPrevious()
    {
        IsDemographicsVisible = true;
        IsHealthIssuesVisible = false;
        PreviousViewCommandVisibility = false;
    }
}

// public partial class InputUserDataViewModel : ObservableObject
// {
//     private readonly IStrapiService _strapiService;
//
//     [ObservableProperty]
//     private bool _isDemographicsVisible = true;
//
//     [ObservableProperty]
//     private bool _isHealthIssuesVisible = false;
//     
//     [ObservableProperty]
//     private bool _previousViewCommandVisibility = false;
//     
//     public IRelayCommand NextViewCommand { get; }
//     public IRelayCommand PreviousViewCommand { get; }
//
//     public InputUserDataViewModel(IStrapiService strapiService)
//     {
//         _strapiService = strapiService;
//         
//         NextViewCommand = new RelayCommand(OnNextViewCommand);
//         PreviousViewCommand = new RelayCommand(OnPreviousViewCommand);
//         
//         IsDemographicsVisible = true;
//         IsHealthIssuesVisible = false;
//     }
//
//     private void OnPreviousViewCommand()
//     {
//         IsDemographicsVisible = true;
//         IsHealthIssuesVisible = false;
//         PreviousViewCommandVisibility = false;
//     }
//     
//     private void OnNextViewCommand()
//     {
//         IsDemographicsVisible = false;
//         IsHealthIssuesVisible = true;
//         PreviousViewCommandVisibility = true;
//         
//         if (IsDemographicsVisible)
//         {
//             IsDemographicsVisible = false;
//             IsHealthIssuesVisible = true;
//             PreviousViewCommandVisibility = true;
//         }
//         else if (IsHealthIssuesVisible)
//         {
//             // TODO: Handle next view transition
//             IsHealthIssuesVisible = false;
//         }
//     }
// }