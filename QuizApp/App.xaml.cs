using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Storage;

namespace QuizApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            // Применяем сохранённую тему пользователя (если есть)
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
                // Игнорируем ошибки при чтении настроек
            }
            // При старте приложения очищаем метки о пройденных категориях
            try
            {
                Preferences.Remove("Completed_Animals");
                Preferences.Remove("Completed_Movies");
                Preferences.Remove("Completed_Cartoons");
                Preferences.Remove("Completed_Series");
                // Сбрасываем сохранённые счёты правильных ответов
                Preferences.Set("Score_Animals_Correct", 0);
                Preferences.Set("Score_Movies_Correct", 0);
                Preferences.Set("Score_Cartoons_Correct", 0);
                Preferences.Set("Score_Series_Correct", 0);
            }
            catch
            {
                // Игнорируем ошибки при очистке настроек
            }
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(new MainPage()));
        }
    }
}