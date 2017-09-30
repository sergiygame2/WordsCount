using System;
using System.Windows;
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
            textRequestsViewModel.RequestFillResults += FillResults;
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

        private void FillResults(TextAnalyzer textAnalyzer)
        {
            SymbolsAmountValue.Content = textAnalyzer.CountSymbols();
            WordsAmountValue.Content = textAnalyzer.CountWords();
            LinesAmountValue.Content = textAnalyzer.CountLines();
        }
    }
}
