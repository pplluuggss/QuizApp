using System;
using System.Linq;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace QuizApp
{
    public partial class CategoryPage : ContentPage
    {
        public CategoryPage()
        {
            InitializeComponent();
        }
        private async void OnTopCategoryTapped(object sender, EventArgs e)
        {
            try
            {
                if (sender is Frame frame)
                {
                    var tg = frame.GestureRecognizers?.FirstOrDefault() as TapGestureRecognizer;
                    var param = tg?.CommandParameter?.ToString() ?? string.Empty;
                    if (string.IsNullOrEmpty(param)) return;
                    await Navigation.PushAsync(new SubCategoryPage(param));
                }
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync("Ошибка", "Не удалось открыть подкатегории: " + ex.Message, "ОК");
            }
        }

        private async void OnCategoryClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            string category = button.CommandParameter?.ToString() ?? string.Empty;

            if (string.IsNullOrEmpty(category))
            {
                await DisplayAlertAsync("Ошибка", "Категория не выбрана.", "ОК");
                return;
            }

            await Navigation.PushAsync(new QuizPage(category));
        }

        private async void OnCategoryTapped(object sender, EventArgs e)
        {
            try
            {
                if (sender is Frame frame)
                {
                    var tg = frame.GestureRecognizers?.FirstOrDefault() as TapGestureRecognizer;
                    var param = tg?.CommandParameter?.ToString() ?? string.Empty;
                    if (string.IsNullOrEmpty(param)) return;
                    await Navigation.PushAsync(new SubCategoryPage(param));
                }
            }
            catch (Exception ex)
            {
                // Предотвращаем вылет
                await DisplayAlertAsync("Ошибка", "Не удалось открыть категорию: " + ex.Message, "ОК");
            }
        }

        // Метод для обновления состояния галочек и счёта при загрузке
        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.BackgroundColor = Application.Current.UserAppTheme == AppTheme.Dark ? (Color)Application.Current.Resources["Gray950"] : (Color)Application.Current.Resources["Gray100"];
        }

        // Кнопка назад
        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
