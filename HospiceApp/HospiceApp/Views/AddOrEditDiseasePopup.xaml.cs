using CommunityToolkit.Maui.Views;
using HospiceApp.ViewModels;

namespace HospiceApp.Views;

public partial class AddOrEditDiseasePopup : Popup
{
    public AddOrEditDiseasePopup(AddOrEditDiseasePopupViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}