using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospiceApp.Models;
using HospiceApp.Services.Abstract;

namespace HospiceApp.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;

    [ObservableProperty] private string _illnessByName;

    public IRelayCommand AllDiseases { get; set; }
    public IRelayCommand SearchDisease { get; set; }
  
    public MainViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        AllDiseases = new RelayCommand(GoToAllDiseases);
        // GetIllnessByName("stage");
    }

    private void GoToAllDiseases()
    {
        _navigationService.GoToAllDiseases();
    }

}