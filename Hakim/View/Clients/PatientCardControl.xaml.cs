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
using static System.Net.Mime.MediaTypeNames;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Hakim.View.Clients
{
    public sealed partial class PatientCardControl : UserControl
    {
        public PatientCardControl()
        {
            this.InitializeComponent();
        }

        private void ShowMenu()
        {
            FlyoutShowOptions myOption = new FlyoutShowOptions();
            myOption.ShowMode = FlyoutShowMode.TransientWithDismissOnPointerMoveAway;
            CommandBarFlyout.ShowAt(this, myOption);
        }

        private void CustomButton_ContextRequested(UIElement sender, ContextRequestedEventArgs args)
        {
            ShowMenu();
        }
    }
}
