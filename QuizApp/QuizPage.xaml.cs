using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Essentials;
using System.Threading;
using System.Diagnostics;
using QuizApp.Models;
using QuizApp.Services;
using Microsoft.Maui.Storage;

namespace QuizApp;

public partial class QuizPage : ContentPage
{
    private List<Question> _questions; // Список вопросов для выбранной темы
    private int _currentIndex = 0;      // Номер текущего вопроса
    private int _score = 0;             // Счетчик очков
    private int _timePerQuestion = 15;  // секунды на вопрос
    private int _timeLeft;
    private CancellationTokenSource _timerCts;
    private int _lives = 3;
    private bool _fiftyUsed = false;
    private bool _skipUsed = false;
    private bool _isInputLocked = false;
    // Старница вопросов
    public QuizPage(string category)
    {
        InitializeComponent();
        Category = category;
        LoadQuestions(category);
        if (_questions == null || _questions.Count == 0)
        {
            DisplayAlertAsync("Нет вопросов", "В выбранной категории нет вопросов.", "ОК");
            return;
        }
        DisplayQuestion();
    }
    // Выбор цвета в зависимости от темы
    private Color ResolveColor(string lightResourceKey, string darkResourceKey)
    {
        try
        {
            var appTheme = Application.Current?.UserAppTheme ?? AppTheme.Light;
            if (appTheme == AppTheme.Dark)
            {
                if (Application.Current.Resources.TryGetValue(darkResourceKey, out var darkVal) && darkVal is Color dc)
                    return dc;
            }
            else
            {
                if (Application.Current.Resources.TryGetValue(lightResourceKey, out var lightVal) && lightVal is Color lc)
                    return lc;
            }
        }
        catch
        {
            // Игнорируем и вернём дефолт
        }

        return Colors.Transparent;
    }

    public string Category { get; set; }
    // Загружаем все вопросы из сервиса и фильтруем по выбранной категории
    private void LoadQuestions(string category)
    {
        var allQuestions = QuizService.GetQuestions();
        _questions = allQuestions.Where(q => q.Category == category).ToList();
    }

    private void DisplayQuestion()
    {
        _ = DisplayQuestionAsync();
    }

    private async System.Threading.Tasks.Task DisplayQuestionAsync()
    {
        var q = _questions[_currentIndex];
        QuestionLabel.Text = q.Text;
        // Попытка загрузить картинку
        if (!string.IsNullOrEmpty(q.ImageFile))
        {
            bool loaded = false;
            try
            {
                var tryNames = new List<string>();
                // если имя содержит расширение — сначала пробуем его как есть
                if (System.IO.Path.HasExtension(q.ImageFile))
                {
                    tryNames.Add(q.ImageFile);
                }
                else
                {
                    // пробуем несколько популярных расширений
                    tryNames.Add(q.ImageFile + ".png");
                    tryNames.Add(q.ImageFile + ".jpg");
                    tryNames.Add(q.ImageFile + ".jpeg");
                    tryNames.Add(q.ImageFile + ".webp");
                }

                foreach (var name in tryNames)
                {
                    try
                    {
                        using var stream = await FileSystem.OpenAppPackageFileAsync(name);
                        if (stream != null)
                        {
                            QuestionImage.Source = Microsoft.Maui.Controls.ImageSource.FromFile(name);
                            QuestionImage.IsVisible = true;
                            ImagePlaceholder.IsVisible = false;
                            loaded = true;
                            break;
                        }
                    }
                    catch
                    {
                        // если не найден — пробуем следующий вариант
                    }
                }

                if (!loaded)
                {
                    QuestionImage.Source = null;
                    QuestionImage.IsVisible = false;
                    ImagePlaceholder.Text = "Картинка недоступна";
                    ImagePlaceholder.IsVisible = true;
                }
            }
            catch
            {
                QuestionImage.Source = null;
                QuestionImage.IsVisible = false;
                ImagePlaceholder.Text = "Картинка недоступна";
                ImagePlaceholder.IsVisible = true;
            }
        }
        else
        {
            QuestionImage.Source = null;
            QuestionImage.IsVisible = false;
            ImagePlaceholder.Text = "Без изображения";
            ImagePlaceholder.IsVisible = true;
        }

        // Обновляем прогресс
        ProgressLabel.Text = $"{_currentIndex + 1}/{_questions.Count}";
        // Обновляем счёт
        ScoreLabel.Text = $"Очки: {_score}";

        var buttons = new[] { Btn0, Btn1, Btn2, Btn3 };
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i < q.Options.Count)
            {
                buttons[i].Text = q.Options[i];
                // Используем цвета из ресурсов в зависимости от текущей темы
                buttons[i].BackgroundColor = ResolveColor("Gray100", "Gray950");
                buttons[i].TextColor = ResolveColor("Gray900", "White");
                buttons[i].IsVisible = true;
                buttons[i].IsEnabled = true;
            }
            else
            {
                buttons[i].IsVisible = false;
            }
        }

        NextBtn.IsEnabled = false;
        NextBtn.IsVisible = false;

        // Обновим индикатор жизней и подсказок
        UpdateHeartsUI();
        UpdateHintsUI();

        // Перезапускаем таймер для вопроса
        CancelTimer();
        StartTimer();
    }
    // Простая анимация: масштаб и возврат
    private async System.Threading.Tasks.Task AnimateButtonAsync(Button btn, bool correct)
    {
        
        await btn.ScaleTo(1.05, 80);
        if (correct)
        {
            await btn.ScaleTo(1.0, 80);
        }
        else
        {
            await btn.ScaleTo(1.0, 80);
        }
    }
    // Возврат в меню
    private async void OnBackToMenuClicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }

    private void OnInputBlockerTapped(object sender, EventArgs e)
    {
       
    }

    private void OnAnswerClicked(object sender, EventArgs e)
    {
        if (_isInputLocked) return;

        var clickedBtn = (Button)sender;
        var q = _questions[_currentIndex];

        var buttons = new[] { Btn0, Btn1, Btn2, Btn3 };

        int clickedIndex = Array.IndexOf(buttons, clickedBtn);
        if (clickedIndex < 0 || clickedIndex >= q.Options.Count)
        {
            clickedIndex = -1;
            for (int i = 0; i < Math.Min(buttons.Length, q.Options.Count); i++)
            {
                if (buttons[i] != null && buttons[i].Text == clickedBtn.Text)
                {
                    clickedIndex = i;
                    break;
                }
            }
            if (clickedIndex < 0)
            {
                NextBtn.IsVisible = true;
                NextBtn.IsEnabled = false;
                return;
            }
        }

        int correctIndex = q.CorrectAnswerIndex;

        // Правильный ответ — зелёный фон, неправильный — красный. 
        for (int i = 0; i < buttons.Length; i++)
        {
            if (!buttons[i].IsVisible) continue;
            buttons[i].IsEnabled = false;
            if (i == correctIndex)
            {
                buttons[i].BackgroundColor = (Color)Application.Current.Resources[Application.Current.UserAppTheme == AppTheme.Dark ? "CorrectAnswerBgDark" : "CorrectAnswerBgLight"];
                buttons[i].TextColor = ResolveColor("Gray900", "White");
            }
            else if (i == clickedIndex && clickedIndex != correctIndex)
            {
                buttons[i].BackgroundColor = (Color)Application.Current.Resources[Application.Current.UserAppTheme == AppTheme.Dark ? "IncorrectAnswerBgDark" : "IncorrectAnswerBgLight"];
                buttons[i].TextColor = ResolveColor("Gray900", "White");
            }
        }

        // Останавливаем таймер при ответе
        CancelTimer();

        if (clickedIndex == correctIndex)
        {
            _score++;
            try { Vibration.Default.Vibrate(TimeSpan.FromMilliseconds(50)); } catch { }
        }
        else
        {
            try { Vibration.Default.Vibrate(TimeSpan.FromMilliseconds(30)); } catch { }
            // снимаем жизнь при неправильном ответе
            if (_lives > 0) _lives--;
            UpdateHeartsUI();
            UpdateHintsUI();
            // если жизни кончились — проигрыш
            if (_lives <= 0)
            {
                _isInputLocked = true;
                if (InputBlocker != null) { InputBlocker.IsVisible = true; InputBlocker.InputTransparent = false; }
                _ = Dispatcher.Dispatch(async () =>
                {
                    var a = await DisplayActionSheet("Вы проиграли", "В меню", "Начать заново", "");
                    if (a == "В меню") await Navigation.PopToRootAsync();
                    else if (a == "Начать заново")
                    {
                        _currentIndex = 0; _score = 0; _lives = 3; _fiftyUsed = false; _skipUsed = false; _isInputLocked = false; if (InputBlocker != null) { InputBlocker.IsVisible = false; InputBlocker.InputTransparent = true; } DisplayQuestion(); UpdateHintsUI();
                    }
                });
            }
        }
        // Показываем кнопку "Следующий" только если смогли определить выбранный индекс
        NextBtn.IsVisible = true;
        NextBtn.IsEnabled = true;

        // Обновляем счёт на экране
        ScoreLabel.Text = $"Очки: {_score}";
        _ = AnimateButtonAsync(clickedBtn, clickedIndex == correctIndex);
    }

    private void OnFiftyClicked(object sender, EventArgs e)
    {
        if (_isInputLocked) return;
        if (_fiftyUsed) return;
        _fiftyUsed = true;
        var q = _questions[_currentIndex];
        int correctIndex = q.CorrectAnswerIndex;
        var buttons = new[] { Btn0, Btn1, Btn2, Btn3 };
        var rnd = new Random();

        var wrongIndexes = Enumerable.Range(0, q.Options.Count).Where(i => i != correctIndex).ToList();
        int toDisable = Math.Min(2, wrongIndexes.Count);
        var disabled = wrongIndexes.OrderBy(i => rnd.Next()).Take(toDisable).ToList();
        foreach (var idx in disabled)
        {
            if (idx < buttons.Length)
            {
                var b = buttons[idx];
                b.IsEnabled = false;
                b.BackgroundColor = ResolveColor("Gray200", "Gray600");
                b.TextColor = ResolveColor("Gray600", "Gray400");
            }
        }

        UpdateHintsUI();
        var animButtons = new[] { Btn0, Btn1, Btn2, Btn3 };
        foreach (var b in animButtons.Where(x => x.IsVisible))
        {
            _ = b.ScaleTo(1.02, 80).ContinueWith(_ => b.ScaleTo(1.0, 80));
        }
    }

    private void OnSkipClicked(object sender, EventArgs e)
    {
        if (_isInputLocked) return;
        // Пропустить вопрос — засчитать как правильный ответ, не снимать жизни
        if (_skipUsed) return;
        _score++;
        // Использование пропуска также закрывает подсказку "пропустить" для этого раунда
        _skipUsed = true;
        UpdateHintsUI();
        CancelTimer();
        _currentIndex++;
        if (_currentIndex < _questions.Count) DisplayQuestion();
        else OnNextClicked(this, EventArgs.Empty);
    }

    private void UpdateHeartsUI()
    {
        HeartsLayout.Children.Clear();
        for (int i = 0; i < 3; i++)
        {
            var lbl = new Label { Text = i < _lives ? "❤" : "♡", TextColor = (Color)Application.Current.Resources["HeartColor"], FontSize = 18 };
            HeartsLayout.Children.Add(lbl);
        }
    }

    private void UpdateHintsUI()
    {
        FiftyButton.IsEnabled = !_fiftyUsed; 
        SkipButton.IsEnabled = !_skipUsed;
    }

    private async void OnNextClicked(object sender, EventArgs e)
    {
        if (_isInputLocked) return;
        _currentIndex++;
        CancelTimer();
        if (_currentIndex < _questions.Count)
        {
            DisplayQuestion();
            return;
        }

        // Конец викторины — показать результаты и вариант действий
        string result = $"Вы набрали {_score} из {_questions.Count}";
        var action = await DisplayActionSheet("Викторина завершена", "В меню", "Начать заново", result);

        // Отметим категорию как пройденную только если пользователь ответил правильно на все вопросы
        bool completed = (_score == _questions.Count);
        Preferences.Set($"Completed_{Category}", completed);

        // Сохраняем количество правильных ответов для этой категории
        Preferences.Set($"Score_{Category}_Correct", _score);

        if (action == "В меню")
        {
            await Navigation.PopToRootAsync();
            return;
        }
        else if (action == "Начать заново")
        {
            _currentIndex = 0;
            _score = 0;
            _lives = 3;
            _fiftyUsed = false;
            _skipUsed = false;
            DisplayQuestion();
            UpdateHintsUI();
            return;
        }
    }

    private void StartTimer()
    {
        _timeLeft = _timePerQuestion;
        TimerProgress.Progress = 1;
        TimerLabel.Text = $"{_timeLeft}s";

        _timerCts = new CancellationTokenSource();
        var cts = _timerCts;

        var sw = Stopwatch.StartNew();
        var interval = TimeSpan.FromMilliseconds(50);

        Device.StartTimer(interval, () =>
        {
            if (cts.IsCancellationRequested)
            {
                sw.Stop();
                return false;
            }

            double elapsed = sw.Elapsed.TotalSeconds;
            double remaining = _timePerQuestion - elapsed;

            if (remaining < 0) remaining = 0;

            Dispatcher.Dispatch(() =>
            {
                TimerProgress.Progress = Math.Max(0, remaining / _timePerQuestion);
                TimerLabel.Text = $"{Math.Ceiling(remaining)}s";
            });

            if (elapsed >= _timePerQuestion)
            {
                sw.Stop();
                Dispatcher.Dispatch(() => OnTimeExpired());
                return false;
            }

            return true; 
        });
    }

    private void CancelTimer()
    {
        try
        {
            _timerCts?.Cancel();
            _timerCts?.Dispose();
            _timerCts = null;
        }
        catch { }
    }

    private void OnTimeExpired()
    {
        // Время вышло — блокируем варианты и подсвечиваем правильный
        var buttons = new[] { Btn0, Btn1, Btn2, Btn3 };
        int correctIndex = _questions[_currentIndex].CorrectAnswerIndex;
        for (int i = 0; i < buttons.Length; i++)
        {
            if (!buttons[i].IsVisible) continue;
            buttons[i].IsEnabled = false;
            if (i == correctIndex)
            {
                buttons[i].BackgroundColor = (Color)Application.Current.Resources[Application.Current.UserAppTheme == AppTheme.Dark ? "CorrectAnswerBgDark" : "CorrectAnswerBgLight"];
                buttons[i].TextColor = ResolveColor("Gray900", "White");
            }
        }

        NextBtn.IsVisible = true;
        NextBtn.IsEnabled = true;
        // при истечении времени снимаем жизнь
        if (_lives > 0) _lives--;
        UpdateHeartsUI();
        UpdateHintsUI();
        // Если жизни закончились, завершаем викторину
        if (_lives <= 0)
        {
            // Показать результат сразу
                _isInputLocked = true;
                if (InputBlocker != null) { InputBlocker.IsVisible = true; InputBlocker.InputTransparent = false; }
                _ = Dispatcher.Dispatch(async () =>
                {
                    string result = $"Вы набрали {_score} из {_questions.Count}";
                    var action = await DisplayActionSheet("Игра окончена", "В меню", "Начать заново", result);
                    if (action == "В меню") await Navigation.PopToRootAsync();
                    else if (action == "Начать заново")
                    {
                        _currentIndex = 0; _score = 0; _lives = 3; _fiftyUsed = false; _skipUsed = false; _isInputLocked = false; if (InputBlocker != null) { InputBlocker.IsVisible = false; InputBlocker.InputTransparent = true; } DisplayQuestion(); UpdateHintsUI();
                    }
                });
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        CancelTimer();
    }
}
