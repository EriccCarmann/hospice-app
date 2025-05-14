using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospiceApp.ViewModels;

namespace HospiceApp.Views;

public partial class SearchDiseasesPage : ContentPage
{
    public SearchDiseasesPage(SearchDiseasesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}