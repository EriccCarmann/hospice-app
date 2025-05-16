using HospiceApp.Services.Abstract;
using HospiceApp.Views;

namespace HospiceApp.Services.Implementation;

public class NavigationService : INavigationService
{
    public void GoToAllDiseases()
    {
        Shell.Current.GoToAsync($"{nameof(AllDiseasesPage)}");
    } 
    
    public void GoToSearchDiseases()
    {
        Shell.Current.GoToAsync($"{nameof(SearchDiseasesPage)}");
    }
}