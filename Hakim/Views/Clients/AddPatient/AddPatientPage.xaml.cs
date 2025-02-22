using Hakim.Model;
using Hakim.View.Clients.AddPatient;
using Hakim.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Hakim.View.Clients
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within PatientDetailsDisplay Frame.
    /// </summary>
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
}
