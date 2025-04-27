using Hakim.Views.Patients.AddPatient;
using Hakim.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using System;

namespace Hakim.Views.Patients;

public sealed partial class AddPatientPage : Page
{
    public ContentDialog dialog;
    public ClientsViewModel viewModel;
    public AddPatientPage()
    {
        this.InitializeComponent();
        NavigateWithSlideTransition(typeof(PatientGeneralInfoPage));
    }

    public AddPatientPage(ContentDialog dialog, ClientsViewModel viewModel)
    {
        this.InitializeComponent();
        this.dialog = dialog;
        this.viewModel = viewModel;
        NavigateWithSlideTransition(typeof(PatientGeneralInfoPage));
    }

    public void NavigateWithSlideTransition(Type pageType)
    {
        var slideNavigationTransitionEffect = SlideNavigationTransitionEffect.FromRight;
        ContentFrame.Navigate(pageType, this, new SlideNavigationTransitionInfo() { Effect = slideNavigationTransitionEffect });
    }
}
