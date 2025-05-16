using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospiceApp.ViewModels;

namespace HospiceApp.Views;

public partial class AllDiseasesPage : ContentPage
{
    public AllDiseasesPage(AllDiseasesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}