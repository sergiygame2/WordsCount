using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WordsCount.Data;
using WordsCount.Services;
using WordsCount.ViewModels;

namespace WordsCount
{
    /// <summary>
    /// Interaction logic for Requests.xaml
    /// </summary>
    public partial class TextRequestsWindow
    {
        public TextRequestsWindow()
        {
            WindowStyle = WindowStyle.None;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            UserNameLabel.Content = StationManager.CurrentUser.UserName;
            var textRequestsViewModel = new TextRequestsViewModel();
            textRequestsViewModel.RequestClose += Close;
            textRequestsViewModel.RequestFillPath += FillPath;
            textRequestsViewModel.RequestFillText += FillText;
            textRequestsViewModel.RequestShowResults += ShowResultsLabels;
            DataContext = textRequestsViewModel;
        }

        private void Close(bool isQuitApp)
        {
            if (!isQuitApp)
            {
                Close();
            }
            else
            {
                // Serialize user before exit, to update it's fields at next start of program
                SerializeManager.RemoveFile(StationManager.UserFilePath);
                SerializeManager.Serialize(StationManager.CurrentUser);

                MessageBox.Show("Salut!");
                Environment.Exit(0);
            }
        }

        private void FillPath(string path)
        {
            if (!String.IsNullOrEmpty(path))
            {
                PathLabel.Content = path;
            }
        }

        private void FillText(string text)
        {
            FileText.Text = !String.IsNullOrEmpty(text) ? text : "Text is empty";
        }

        private void ShowResultsLabels()
        {
            SymbolsAmount.Visibility = Visibility.Visible;
            SymbolsAmountValue.Visibility = Visibility.Visible;
            WordsAmount.Visibility = Visibility.Visible;
            WordsAmountValue.Visibility = Visibility.Visible;
            LinesAmount.Visibility = Visibility.Visible;
            LinesAmountValue.Visibility = Visibility.Visible;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }
    }
}
