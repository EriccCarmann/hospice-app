using HospiceApp.Services.Implementation;
using HospiceApp.ViewModels;

namespace HospiceApp.Views;

public partial class HealthIssuesView : ContentView
{
    public HealthIssuesView()
    {
        InitializeComponent();
        BindingContext = new HealthIssuesViewModel(new StrapiService());
    }
}