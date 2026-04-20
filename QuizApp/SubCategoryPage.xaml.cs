using System;
using Microsoft.Maui.Controls;

namespace QuizApp
{
    public partial class SubCategoryPage : ContentPage
    {
        private string _topCategory;

        public SubCategoryPage(string topCategory)
        {
            InitializeComponent();
            _topCategory = topCategory;
            SetupSubcategories();
        }

        private void SetupSubcategories()
        {
            TitleLabel.Text = _topCategory switch
            {
                "Kino" => "Кино: выберите подкатегорию",
                "Priroda" => "Природа: выберите подкатегорию",
                "Geografiya" => "География: выберите подкатегорию",
                _ => "Выберите подкатегорию"
            };

            if (_topCategory == "Kino")
            {
                TopLeftEmoji.Text = "🎬"; TopLeftLabel.Text = "Фильмы"; ((TapGestureRecognizer)TopLeftFrame.GestureRecognizers[0]).CommandParameter = "Movies"; TopLeftFrame.IsVisible = true;
                TopRightEmoji.Text = "📺"; TopRightLabel.Text = "Сериалы"; ((TapGestureRecognizer)TopRightFrame.GestureRecognizers[0]).CommandParameter = "Series"; TopRightFrame.IsVisible = true;
                BottomLeftEmoji.Text = "🐻"; BottomLeftLabel.Text = "Мультфильмы"; ((TapGestureRecognizer)BottomLeftFrame.GestureRecognizers[0]).CommandParameter = "Cartoons"; BottomLeftFrame.IsVisible = true;
                BottomRightEmoji.Text = "🎞️"; BottomRightLabel.Text = "Документальные"; ((TapGestureRecognizer)BottomRightFrame.GestureRecognizers[0]).CommandParameter = "Documentaries"; BottomRightFrame.IsVisible = true;
            }
            else if (_topCategory == "Priroda")
            {
                TopLeftEmoji.Text = "🐾"; TopLeftLabel.Text = "Животные"; ((TapGestureRecognizer)TopLeftFrame.GestureRecognizers[0]).CommandParameter = "Animals"; TopLeftFrame.IsVisible = true;
                TopRightEmoji.Text = "🌱"; TopRightLabel.Text = "Растения"; ((TapGestureRecognizer)TopRightFrame.GestureRecognizers[0]).CommandParameter = "Nature_Plants"; TopRightFrame.IsVisible = true;
                BottomLeftEmoji.Text = "🌍"; BottomLeftLabel.Text = "Экология"; ((TapGestureRecognizer)BottomLeftFrame.GestureRecognizers[0]).CommandParameter = "Nature_Ecology"; BottomLeftFrame.IsVisible = true;
                BottomRightEmoji.Text = "🕊️"; BottomRightLabel.Text = "Птицы"; ((TapGestureRecognizer)BottomRightFrame.GestureRecognizers[0]).CommandParameter = "Nature_Birds"; BottomRightFrame.IsVisible = true;
            }
            else if (_topCategory == "Geografiya")
            {
                TopLeftEmoji.Text = "🗺️"; TopLeftLabel.Text = "Страны"; ((TapGestureRecognizer)TopLeftFrame.GestureRecognizers[0]).CommandParameter = "Geo_Countries"; TopLeftFrame.IsVisible = true;
                TopRightEmoji.Text = "🏔️"; TopRightLabel.Text = "Рельеф"; ((TapGestureRecognizer)TopRightFrame.GestureRecognizers[0]).CommandParameter = "Geo_Relief"; TopRightFrame.IsVisible = true;
                BottomLeftEmoji.Text = "🌊"; BottomLeftLabel.Text = "Океаны"; ((TapGestureRecognizer)BottomLeftFrame.GestureRecognizers[0]).CommandParameter = "Geo_Oceans"; BottomLeftFrame.IsVisible = true;
                BottomRightEmoji.Text = "☁️"; BottomRightLabel.Text = "Климат"; ((TapGestureRecognizer)BottomRightFrame.GestureRecognizers[0]).CommandParameter = "Geo_Climate"; BottomRightFrame.IsVisible = true;
            }
            else if (_topCategory == "Istoriya")
            {
                TitleLabel.Text = "История: выберите подкатегорию";
                TopLeftEmoji.Text = "🏺"; TopLeftLabel.Text = "Древняя"; ((TapGestureRecognizer)TopLeftFrame.GestureRecognizers[0]).CommandParameter = "History_Ancient"; TopLeftFrame.IsVisible = true;
                TopRightEmoji.Text = "🛡️"; TopRightLabel.Text = "Средневековье"; ((TapGestureRecognizer)TopRightFrame.GestureRecognizers[0]).CommandParameter = "History_Medieval"; TopRightFrame.IsVisible = true;
                BottomLeftEmoji.Text = "🕰️"; BottomLeftLabel.Text = "Новейшая"; ((TapGestureRecognizer)BottomLeftFrame.GestureRecognizers[0]).CommandParameter = "History_Modern"; BottomLeftFrame.IsVisible = true;
                BottomRightEmoji.Text = "⚔️"; BottomRightLabel.Text = "Мировые войны"; ((TapGestureRecognizer)BottomRightFrame.GestureRecognizers[0]).CommandParameter = "History_Wars"; BottomRightFrame.IsVisible = true;
            }
        }

        private async void OnSubCategoryTapped(object sender, EventArgs e)
        {
            try
            {
                if (sender is Frame frame)
                {
                    var tg = frame.GestureRecognizers?[0] as TapGestureRecognizer;
                    var param = tg?.CommandParameter?.ToString() ?? string.Empty;
                    if (string.IsNullOrEmpty(param)) return;
                    await Navigation.PushAsync(new QuizPage(param));
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "OK");
            }
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
