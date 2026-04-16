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

            // Если есть файл crash.log от предыдущего запуска — прочитаем и удалим его, потом покажем в CreateWindow
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
            var window = new Window(new NavigationPage(new MainPage()));

            if (!string.IsNullOrEmpty(_startupCrashText))
            {
                // Показываем короткую часть лога, чтобы не перегружать UI
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