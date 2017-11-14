using System;
using System.Windows;
using System.Windows.Input;
using AppServices.Services;
using FontAwesome.WPF;
using WordsCount.ViewModels;

namespace WordsCount
{
    /// <summary>
    /// Interaction logic for Requests.xaml
    /// </summary>
    public partial class TextRequestsWindow
    {
        private ImageAwesome _loader;

        private readonly FontAwesomeIcon[] _batteryIcons =
        {
            FontAwesomeIcon.BatteryEmpty, FontAwesomeIcon.Battery1,
            FontAwesomeIcon.Battery2, FontAwesomeIcon.Battery3,
            FontAwesomeIcon.BatteryFull
        };

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
            textRequestsViewModel.RequestProgressBar += UpdateProgressBar;
            textRequestsViewModel.RequestVisibilityChange += (x) => Visibility = x;
            DataContext = textRequestsViewModel;
        }

        private void UpdateProgressBar(bool isShow, int step = 0)
        {
            if(isShow)
            {
                if (_loader == null)
                {
                    // Initializing ImageAwesome instance only once
                    _loader = new ImageAwesome();
                    TextAnalyzerGrid.Children.Add(_loader);
                    _loader.Width = 400;
                    _loader.Height = 75;
                }

                // Change icon state
                _loader.Icon = _batteryIcons[step];
                 
                IsEnabled = false;
            }
            else
            {
                // Close progress bar
                TextAnalyzerGrid.Children.Remove(_loader);
                _loader = null;
                IsEnabled = true;
            }
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
                Logger.Log($"User {StationManager.CurrentUser.UserName} closed program wuthout log out");

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

        // Method to show results label when they are available
        private void ShowResultsLabels(bool visible = true)
        {
            SymbolsAmount.Visibility = visible ? Visibility.Visible : Visibility.Hidden;
            SymbolsAmountValue.Visibility = visible ? Visibility.Visible : Visibility.Hidden;
            WordsAmount.Visibility = visible ? Visibility.Visible : Visibility.Hidden;
            WordsAmountValue.Visibility = visible ? Visibility.Visible : Visibility.Hidden;
            LinesAmount.Visibility = visible ? Visibility.Visible : Visibility.Hidden;
            LinesAmountValue.Visibility = visible ? Visibility.Visible : Visibility.Hidden;
        }

        // Allow to move form
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }
    }
}
