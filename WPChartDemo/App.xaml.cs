using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WPChartDemo.Properties;

namespace WPChartDemo
{
    public partial class App
    {
        public PhoneApplicationFrame RootFrame { get; private set; }
        public static ModelViewModel ViewModel { get; [UsedImplicitly] private set; }
        private const string DbConnectionString = "Data Source=isostore:/Model.sdf";

        public App()
        {
            UnhandledException += Application_UnhandledException;
            InitializeComponent();
            InitializePhoneApplication();

            if (!Debugger.IsAttached)
            {
                return;
            }
            Current.Host.Settings.EnableFrameRateCounter = true;
            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;

            using (var db = new ModelContext(DbConnectionString))
            {
                if (db.DatabaseExists() == false)
                {
                    db.CreateDatabase();

                    // TODO This is sample data... use Model context the way you like
                    db.Items.InsertOnSubmit(new Model { Title = "Vegitables", Value = 50 });
                    db.Items.InsertOnSubmit(new Model { Title = "Fruits", Value = 40 });
                    db.Items.InsertOnSubmit(new Model { Title = "Other items", Value = 10 });

                    db.SubmitChanges();
                }
            }

            ViewModel = new ModelViewModel(DbConnectionString);
            ViewModel.LoadCollectionsFromDatabase();
        }

        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
        }

        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
        }

        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
        }

        private void Application_Closing(object sender, ClosingEventArgs e)
        {
        }

        private static void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }
        }

        private static void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }
        }
        #region Phone application initialization

        private bool _phoneApplicationInitialized;

        private void InitializePhoneApplication()
        {
            if (_phoneApplicationInitialized)
            {
                return;
            }

            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;
            _phoneApplicationInitialized = true;
        }

        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            if (RootVisual != RootFrame)
            {
                RootVisual = RootFrame;
            }

            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion
    }
}