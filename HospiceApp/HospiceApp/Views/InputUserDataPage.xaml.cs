using HospiceApp.ViewModels;

namespace HospiceApp.Views;

public partial class InputUserDataPage : ContentPage
{
    public InputUserDataPage(InputUserDataViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}