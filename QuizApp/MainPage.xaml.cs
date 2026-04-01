using Microsoft.Maui.Storage;

namespace QuizApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnStartClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CategoryPage());
        }

        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }

        private void OnExitClicked(object sender, EventArgs e)
        {
            try
            {
                Preferences.Remove("Completed_Animals");
                Preferences.Remove("Completed_Movies");
                Preferences.Remove("Completed_Cartoons");
                Preferences.Remove("Completed_Series");
            }
            catch{}
            System.Environment.Exit(0);
        }
        // Переключаем между светлой и темной темами и сохраняем выбор
        private void OnToggleThemeClicked(object sender, EventArgs e)
        {
            var current = Application.Current.UserAppTheme;
            if (current == AppTheme.Dark)
            {
                Application.Current.UserAppTheme = AppTheme.Light;
                Preferences.Set("UserTheme", "Light");
            }
            else
            {
                Application.Current.UserAppTheme = AppTheme.Dark;
                Preferences.Set("UserTheme", "Dark");
            }
            UpdateThemeButtonText();
        }

        private void UpdateThemeButtonText()
        {
            var theme = Preferences.Get("UserTheme", "Light");
            if (theme == "Dark")
            {
                // Theme toggle removed from main menu
                // ThemeToggleButton.Text = "Тема: Тёмная";
            }
            else
            {
                // ThemeToggleButton.Text = "Тема: Светлая";
            }
        }
    }
}
