﻿using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Microsoft.Win32;
using WordsCount.Commands;
using WordsCount.Services;
using WordsCount.Models;
using System.Windows;

namespace WordsCount.ViewModels
{
    internal class TextRequestsViewModel : INotifyPropertyChanged
    {
        private RelayCommand _logOutCommand;
        private RelayCommand _myTextRequestsCommand;
        private RelayCommand _openFileCommand;
        private RelayCommand _exitCommand;
    
        public RelayCommand LogOutCommand => _logOutCommand ?? (_logOutCommand = new RelayCommand(LogOut));

        public RelayCommand MyTextRequestsCommand => _myTextRequestsCommand ?? (_myTextRequestsCommand = new RelayCommand(OpenTextRequests));

        public RelayCommand OpenFileCommand => _openFileCommand ?? (_openFileCommand = new RelayCommand(OpenFile));

        public RelayCommand ExitCommand => _exitCommand ?? (_exitCommand = new RelayCommand(obj => OnRequestClose(true)));

        private string _filePath = "Path to file...";
        public string FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                OnPropertyChanged();
            }
        }

        private string _fileText;
        public string FileText
        {
            get => _fileText;
            set
            {
                _fileText = value;
                OnPropertyChanged();
            }
        }

        private int _symbolsAmount;
        public int SymbolsAmount
        {
            get => _symbolsAmount;
            set
            {
                _symbolsAmount = value;
                OnPropertyChanged();
            }
        }

        private int _wordsAmount;
        public int WordsAmount
        {
            get => _wordsAmount;
            set
            {
                _wordsAmount = value;
                OnPropertyChanged();
            }
        }

        private int _linesAmount;
        public int LinesAmount
        {
            get => _linesAmount;
            set
            {
                _linesAmount = value;
                OnPropertyChanged();
            }
        }

        private void LogOut(object obj)
        {
            OnRequestClose(false);

            Logger.Log($"User {StationManager.CurrentUser?.UserName} logged out");
            StationManager.CurrentUser = null;
            SerializeManager.RemoveFile(StationManager.UserFilePath);

            var loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
        }

        private void OpenFile(object obj)
        {
            // Method to give user acces to files on his computer
            var openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                // Validation of selected text format
                var extension = Path.GetExtension(openFileDialog.FileName);

                if (extension != ".txt")
                {
                    MessageBox.Show("Wrong file format! You can download only .txt files");
                    return;
                }
                
                // Read filename and text from file
                FilePath = openFileDialog.FileName;
                FileText = File.ReadAllText(FilePath);

                // Perform text analyzing
                HandeRequestText();
            }
        }

        private void HandeRequestText()
        {
            // Analyze text
            var textAnalyzer = new TextAnalyzer(FileText);
            var textRequest = new TextRequest(FilePath, textAnalyzer);

            // Fill results
            SymbolsAmount = textAnalyzer.CountSymbols();
            WordsAmount = textAnalyzer.CountWords();
            LinesAmount = textAnalyzer.CountLines();
            
            Logger.Log($"User {StationManager.CurrentUser?.UserName} analyzed file {FilePath}");
            StationManager.CurrentUser?.TextRequests.Add(textRequest);
            
            OnRequestShowResults();
        }
            
        private void OpenTextRequests(object obj)
        {
            OnRequestClose(false);
<<<<<<< HEAD

=======
            
            Logger.Log($"User {StationManager.CurrentUser?.UserName} oppened requests history");
>>>>>>> lab2
            var requestsHistoryWindow = new RequestsHistoryWindow();
            requestsHistoryWindow.ShowDialog();
        }

        internal event FillResultsHandler RequestShowResults;
        public delegate void FillResultsHandler();

        protected virtual void OnRequestShowResults()
        {
            RequestShowResults?.Invoke();
        }

        internal event FillTextHandler RequestFillText;
        public delegate void FillTextHandler(string text);

        protected virtual void OnRequestFillText(string text)
        {
            RequestFillText?.Invoke(text);
        }

        internal event FillPathHandler RequestFillPath;
        public delegate void FillPathHandler(string path);

        protected virtual void OnRequestFillPath(string path)
        {
            RequestFillPath?.Invoke(path);
        }

        internal event CloseHandler RequestClose;
        public delegate void CloseHandler(bool isQuitApp);

        protected virtual void OnRequestClose(bool isquitapp)
        {
            RequestClose?.Invoke(isquitapp);
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
