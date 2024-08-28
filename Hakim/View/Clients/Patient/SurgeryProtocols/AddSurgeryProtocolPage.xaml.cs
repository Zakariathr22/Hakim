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

namespace Hakim.View.Clients.Patient.SurgeryProtocols
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddSurgeryProtocolPage : Page
    {
        ContentDialog dialog;
        public AddSurgeryProtocolPage(ContentDialog dialog)
        {
            this.InitializeComponent();
            this.dialog = dialog;
        }

        private void titleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (titleTextBox.Text != "")
                dialog.IsPrimaryButtonEnabled = true;
            else dialog.IsPrimaryButtonEnabled = false;
        }
    }
}