using Hakim.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
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

namespace Hakim.View.Controls
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within PatientDetailsDisplay Frame.
    /// </summary>
    public sealed partial class EditNamePage : Page
    {
        ContentDialog dialog;
        SettingsViewModel settingsViewModel;
        public EditNamePage()
        {
            this.InitializeComponent();
        }

        public EditNamePage(ContentDialog dialog, SettingsViewModel settingsViewModel)
        {
            this.InitializeComponent();
            this.dialog = dialog;
            this.DataContext = settingsViewModel;
            this.settingsViewModel = settingsViewModel;
            //this.settingsViewModel.NewFirstName = settingsViewModel.User.FirstName;
            //this.settingsViewModel.NewLastName = settingsViewModel.User.LastName;
            if (settingsViewModel.User.Rank == "Dr.")
                rankComboBox.SelectedIndex = 0;
            else rankComboBox.SelectedIndex = 1;
        }

        private void rankComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (rankComboBox.SelectedIndex == 0)
                settingsViewModel.User.Rank = "Dr.";
            else settingsViewModel.User.Rank = "Prof.";
            settingsViewModel.RankChangedCommand.Execute(null);
        }
    }
}
