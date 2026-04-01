using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace QuizApp;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
        UpdateThemeText();
    }

    private void UpdateThemeText()
    {
        var theme = Preferences.Get("UserTheme", "Light");
        ThemeBtn.Text = theme == "Dark" ? "Тёмная" : "Светлая";
    }

    private void OnThemeToggle(object sender, EventArgs e)
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
        UpdateThemeText();
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
    private void OnResetClicked(object sender, EventArgs e)
    {
        Preferences.Set("UserTheme", "Light");
        Application.Current.UserAppTheme = AppTheme.Light;
        UpdateThemeText();
    }
}
