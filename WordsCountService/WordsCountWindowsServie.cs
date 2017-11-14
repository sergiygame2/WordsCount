using System;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceProcess;
using AppServices.Services;

namespace WordsCount.Service
{
    public class WordsCountWindowsService : ServiceBase
    {
        internal const string CurrentServiceName = "WordsCountService";
        internal const string CurrentServiceDisplayName = "Words Count Service";
        internal const string CurrentServiceSource = "WordsCountServiceSource";
        internal const string CurrentServiceLogName = "WordsCountServiceLogName";
        internal const string CurrentServiceDescription = "Words Count for learning purposes.";
        private ServiceHost _serviceHost = null;
        private EventLog _serviceEventLog;
        private void InitializeComponent()
        {
            _serviceEventLog = new EventLog(); 
            ((System.ComponentModel.ISupportInitialize)(_serviceEventLog)).BeginInit();
            ServiceName = CurrentServiceName;
            ((System.ComponentModel.ISupportInitialize)(_serviceEventLog)).EndInit();
        }
        public WordsCountWindowsService()
        {
            ServiceName = CurrentServiceName;
            InitializeComponent();
            try
            {
                if (!EventLog.SourceExists(CurrentServiceSource))
                    EventLog.CreateEventSource(CurrentServiceSource, CurrentServiceLogName);
                _serviceEventLog.Source = CurrentServiceSource;
                _serviceEventLog.Log = CurrentServiceLogName;
                _serviceEventLog.ModifyOverflowPolicy(OverflowAction.OverwriteAsNeeded, 0);
                _serviceEventLog.MaximumKilobytes = 8192;
            }
            catch (Exception ex)
            {
                Logger.Log("Failed To Initialize Log", ex);
            }
            try
            {
                AppDomain.CurrentDomain.UnhandledException += UnhandledException;
                try
                {
                    _serviceEventLog.WriteEntry("Initialization");
                }
                catch (Exception ex)
                {
                    Logger.Log("Initialization error", ex);
                }
            }
            catch (Exception ex)
            {
                _serviceEventLog.WriteEntry(
                    $"Initialization{ex.Message}{Environment.NewLine}ex.StackTrace = {ex.StackTrace}{Environment.NewLine}ex.InnerException.Message = {ex.InnerException?.Message ?? "null"}",
                    EventLogEntryType.Error);
                Logger.Log("Initialization", ex);
            }
        }

        protected override void OnStart(string[] args)
        {
            Logger.Log("OnStart");
            RequestAdditionalTime(120 * 1000);

            try
            {
                _serviceHost?.Close();
            }
            catch
            {
                // ignored
            }
            try
            {
                _serviceHost = new ServiceHost(typeof(WordsCountService));
                _serviceHost.Open();
            }
            catch (Exception ex)
            {
                _serviceEventLog.WriteEntry($"Opening The Host: {ex.Message}", EventLogEntryType.Error);
                Logger.Log("OnStart", ex);
                throw;
            }
            Logger.Log("Service Started");
        }

        protected override void OnStop()
        {
            Logger.Log("OnStop");
            RequestAdditionalTime(120 * 1000);
            try
            {
                _serviceHost.Close();
            }
            catch (Exception ex)
            {
                _serviceEventLog.WriteEntry($"Trying To Stop The Host Listener{ex.Message}",
                    EventLogEntryType.Error);
                Logger.Log("Trying To Stop The Host Listener", ex);
            }
            _serviceEventLog.WriteEntry("Service Stopped", EventLogEntryType.Information);
            Logger.Log("Service Stopped");
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            var ex = (Exception)args.ExceptionObject;
            _serviceEventLog.WriteEntry(string.Format("UnhandledException {0} ex.Message = {1}{0} ex.StackTrace = {2}", Environment.NewLine, ex.Message, ex.StackTrace),
                EventLogEntryType.Error);

            Logger.Log("UnhandledException", ex);
        }
    }
}
