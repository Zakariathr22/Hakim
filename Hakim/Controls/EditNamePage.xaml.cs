using Hakim.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace Hakim.Controls;

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
