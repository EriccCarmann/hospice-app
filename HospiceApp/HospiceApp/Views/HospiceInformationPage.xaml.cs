using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospiceApp.ViewModels;

namespace HospiceApp.Views;

public partial class HospiceInformationPage : ContentPage
{
    public HospiceInformationPage(HospiceInformationViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}