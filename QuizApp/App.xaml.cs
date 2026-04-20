using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Storage;

namespace QuizApp
{
    public partial class App : Application
    {
        private string _startupCrashText;

        public App()
        {
            InitializeComponent();
            try
            {
                var path = System.IO.Path.Combine(FileSystem.AppDataDirectory, "crash.log");
                if (System.IO.File.Exists(path))
                {
                    _startupCrashText = System.IO.File.ReadAllText(path);
                    try { System.IO.File.Delete(path); } catch { }
                }
            }
            catch { }
            try
            {
                var userTheme = Preferences.Get("UserTheme", "Light");
                if (userTheme == "Dark")
                    Application.Current.UserAppTheme = AppTheme.Dark;
                else
                    Application.Current.UserAppTheme = AppTheme.Light;
            }
            catch
            {
            }
            try
            {
                Preferences.Remove("Completed_Animals");
                Preferences.Remove("Completed_Movies");
                Preferences.Remove("Completed_Cartoons");
                Preferences.Remove("Completed_Series");
                Preferences.Set("Score_Animals_Correct", 0);
                Preferences.Set("Score_Movies_Correct", 0);
                Preferences.Set("Score_Cartoons_Correct", 0);
                Preferences.Set("Score_Series_Correct", 0);
            }
            catch
            {
            }
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = new Window(new NavigationPage(new MainPage()));

            if (!string.IsNullOrEmpty(_startupCrashText))
            {
                var text = _startupCrashText.Length > 2000 ? _startupCrashText.Substring(0, 2000) : _startupCrashText;
                window.Page.Dispatcher.Dispatch(async () =>
                {
                    try
                    {
                        await window.Page.DisplayAlert("Предыдущее падение", text, "OK");
                    }
                    catch { }
                });
            }

            return window;
        }
    }
}