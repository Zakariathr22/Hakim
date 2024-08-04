using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using Windows.UI;
using Hakim.View.Clients.AddPatient;
using Hakim.View.Clients.Patient;
using Hakim.ViewModel;
using System.Collections.ObjectModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Hakim.View.Clients
{
    public sealed partial class PatientPage : Page
    {
        ClientsViewModel viewModel;
        ScrollView scrollView1 = new ScrollView();
        Grid mainPanel = new Grid();
        ScrollView scrollView2 = new ScrollView();
        Grid InternalPanel = new Grid();
        Expander expander = new Expander();

        PatientInfoEditorControl patientInfoEditor;
        PatientDetailsDisplayControl patientDetailsDisplay;
        PatientRecordsControl patientRecords;

        public PatientPage()
        {
            this.InitializeComponent();
            Loaded += PatientPage_Loaded;
        }

        private void PatientPage_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = viewModel;

            viewModel.SelectedPatient.files = new ObservableCollection<Model.File>();
            viewModel.SelectedPatient.files.Add(new Model.File());
            viewModel.SelectedPatient.files.Add(new Model.File());
            viewModel.SelectedPatient.files.Add(new Model.File());
            viewModel.SelectedPatient.files.Add(new Model.File());

            viewModel.SelectedPatient.appointments = new ObservableCollection<Model.Appointment>();
            viewModel.SelectedPatient.appointments.Add(new Model.Appointment());
            viewModel.SelectedPatient.appointments.Add(new Model.Appointment());
            viewModel.SelectedPatient.appointments.Add(new Model.Appointment());
            viewModel.SelectedPatient.appointments.Add(new Model.Appointment());

            patientInfoEditor = new PatientInfoEditorControl(viewModel.SelectedPatient);
            patientDetailsDisplay = new PatientDetailsDisplayControl(viewModel.SelectedPatient);
            patientRecords = new PatientRecordsControl(viewModel.SelectedPatient);

            Grid.SetRow(scrollView1, 1);
            Grid.SetColumn(scrollView1, 0);
            PagePanel.Children.Add(scrollView1);

            mainPanel.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // Second row takes 2/3 of available space

            mainPanel.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            mainPanel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); // First column takes 1/3 of available space

            scrollView1.Content = mainPanel;

            Grid.SetRow(expander, 0);
            Grid.SetColumn(expander, 0);
            mainPanel.Children.Add(expander);

            expander.Header = patientInfoEditor;
            expander.Content = patientDetailsDisplay;
            expander.HorizontalAlignment = HorizontalAlignment.Stretch;

            patientDetailsDisplay.HorizontalAlignment = HorizontalAlignment.Stretch;
            patientInfoEditor.HorizontalAlignment = HorizontalAlignment.Stretch;
            patientRecords.HorizontalAlignment = HorizontalAlignment.Stretch;

            patientRecords.VerticalAlignment = VerticalAlignment.Top;

            Grid.SetRow(patientRecords, 1);
            Grid.SetColumn(patientRecords, 0);
            mainPanel.Children.Add(patientRecords);

            InternalPanel.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            InternalPanel.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

            scrollView1.VerticalScrollBarVisibility = ScrollingScrollBarVisibility.Auto;
            scrollView1.Padding = new Thickness(24, 0, 24, 24);
            expander.Margin = new Thickness(0, 24, 0, 0);
            patientInfoEditor.Margin = new Thickness(16, 16, 0, 16);
            patientDetailsDisplay.Margin = new Thickness(0);
            patientRecords.Margin = new Thickness(0);

            Page_SizeChanged(null, null);
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.ActualWidth >= 832 && mainPanel.RowDefinitions.Count == 2)
            {
                scrollView1.ScrollTo(0, 0);
                mainPanel.RowDefinitions.Clear();
                mainPanel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                mainPanel.ColumnDefinitions[0].Width = new GridLength(450);
                mainPanel.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                Grid.SetRow(patientRecords, 0);
                Grid.SetColumn(patientRecords, 1);

                mainPanel.Children.Clear();
                expander.Header = null;
                expander.Content = null;
                PagePanel.Children.RemoveAt(1);
                scrollView1.Content = null;

                Grid.SetRow(mainPanel, 1);
                PagePanel.Children.Add(mainPanel);

                Grid.SetRow(scrollView1, 0);
                Grid.SetColumn(scrollView1, 0);
                mainPanel.Children.Add(scrollView1);
                Grid.SetRow(scrollView2, 0);
                Grid.SetColumn(scrollView2, 1);
                mainPanel.Children.Add(scrollView2);

                scrollView1.Content = InternalPanel;
                Grid.SetRow(patientInfoEditor, 0);
                Grid.SetColumn(patientInfoEditor, 0);
                InternalPanel.Children.Add(patientInfoEditor);
                Grid.SetRow(patientDetailsDisplay, 1);
                Grid.SetColumn(patientDetailsDisplay, 0);
                InternalPanel.Children.Add(patientDetailsDisplay);
                scrollView2.Content = patientRecords;

                scrollView1.VerticalScrollBarVisibility = ScrollingScrollBarVisibility.Visible;
                scrollView1.Padding = new Thickness(12, 0, 16, 0);
                scrollView2.Padding = new Thickness(12, 0, 16, 0);
                patientInfoEditor.Margin = new Thickness(16, 24, 0, 0);
                patientDetailsDisplay.Margin = new Thickness(0, 16, 0, 24);
                patientRecords.Margin = new Thickness(0, 12, 0, 24);
            }
            else if (this.ActualWidth < 832 && mainPanel.ColumnDefinitions.Count == 2)
            {
                mainPanel.ColumnDefinitions.RemoveAt(1);
                mainPanel.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                mainPanel.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                mainPanel.RowDefinitions[0].Height = GridLength.Auto;
                Grid.SetRow(patientRecords, 1);
                Grid.SetColumn(patientRecords, 0);

                InternalPanel.Children.Clear();
                mainPanel.Children.Clear();
                PagePanel.Children.RemoveAt(1);
                scrollView1.Content = null;
                scrollView2.Content = null;
                scrollView1 = new ScrollView();

                Grid.SetColumn(scrollView1, 0);
                Grid.SetRow(scrollView1, 1);
                PagePanel.Children.Add(scrollView1);

                scrollView1.Content = mainPanel;

                Grid.SetColumn(patientRecords, 0);
                Grid.SetRow(patientRecords, 1);
                mainPanel.Children.Add(patientRecords);

                Grid.SetColumn(expander, 0);
                Grid.SetRow(expander, 0);
                mainPanel.Children.Add(expander);

                expander.Header = patientInfoEditor;
                expander.Content = patientDetailsDisplay;

                scrollView1.VerticalScrollBarVisibility = ScrollingScrollBarVisibility.Auto;
                scrollView1.Padding = new Thickness(24, 0, 24, 24);
                expander.Margin = new Thickness(0, 24, 0, 0);
                patientInfoEditor.Margin = new Thickness(16, 16, 0, 16);
                patientDetailsDisplay.Margin = new Thickness(0);
                patientRecords.Margin = new Thickness(0);
            } 
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is ClientsViewModel)
            {
                viewModel = e.Parameter as ClientsViewModel;
            }
            base.OnNavigatedTo(e);
        }
    }
}
