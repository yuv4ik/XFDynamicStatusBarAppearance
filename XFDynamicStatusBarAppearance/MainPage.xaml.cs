using System;
using System.Timers;
using Xamarin.Forms;
using XFDynamicStatusBarAppearance.Services;

namespace XFDynamicStatusBarAppearance
{
    public partial class MainPage : ContentPage
    {
        readonly Timer timer;
        int tickNum;

        public MainPage()
        {
            InitializeComponent();
            timer = new Timer(TimeSpan.FromSeconds(2).TotalMilliseconds);
            timer.Elapsed += Timer_Elapsed;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            timer.Start();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            timer.Stop();
        }

        void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var currentNavPage = (Application.Current.MainPage as NavigationPage);
                var statusBarStyleManager = DependencyService.Get<IStatusBarStyleManager>();
                if (++tickNum % 2 == 0)
                {
                    currentNavPage.BarBackgroundColor = Color.DarkCyan;
                    statusBarStyleManager.SetDarkTheme();
                }
                else
                {
                    currentNavPage.BarBackgroundColor = Color.LightGreen;
                    statusBarStyleManager.SetLightTheme();
                }
            });
        }
    }
}
