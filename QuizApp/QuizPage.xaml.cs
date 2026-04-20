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
using System.IO;

namespace QuizApp;

public partial class QuizPage : ContentPage
{
    private static readonly byte[] sampleImageBytes = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR4nGNgYAAAAAMAAWgmWQ0AAAAASUVORK5CYII=");

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

        try { _ = System.Threading.Tasks.Task.Run(() => EnsureSampleImages()); } catch { }
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
        try
        {
            ImagePlaceholder.Text = q.ImageFile ?? "(no image)";
            ImagePlaceholder.IsVisible = true;
            QuestionImage.IsVisible = false;
            Debug.WriteLine($"[QuizPage] Displaying question {_currentIndex + 1}/{_questions.Count}, imageFile='{q.ImageFile}'");
        }
        catch { }
        // Попытка загрузить картинку
        if (!string.IsNullOrEmpty(q.ImageFile))
        {
            bool loaded = false;
            try
            {
                var tryNames = new List<string>();
                var baseName = System.IO.Path.GetFileNameWithoutExtension(q.ImageFile);
                var originalExt = System.IO.Path.GetExtension(q.ImageFile);
                var commonExts = new[] { ".png", ".jpg", ".jpeg", ".webp" };
                if (!string.IsNullOrEmpty(originalExt))
                {
                    tryNames.Add(q.ImageFile);
                    foreach (var ext in commonExts)
                    {
                        var candidate = baseName + ext;
                        if (!tryNames.Contains(candidate, StringComparer.OrdinalIgnoreCase))
                            tryNames.Add(candidate);
                    }
                }
                else
                {
                    foreach (var ext in commonExts)
                        tryNames.Add(q.ImageFile + ext);
                }

                var expanded = tryNames.SelectMany(n => new[] { n, System.IO.Path.Combine("Images", n), System.IO.Path.Combine("Resources", "Images", n) }).ToList();
                Debug.WriteLine($"[QuizPage] Trying image names: {string.Join(",", expanded)}");

                foreach (var name in expanded)
                {
                    try
                    {
                        var fileNameOnly = System.IO.Path.GetFileName(name);
                        var localPath = Path.Combine(FileSystem.AppDataDirectory, "Images", fileNameOnly);
                        if (File.Exists(localPath))
                        {
                            Debug.WriteLine($"[QuizPage] Found local image: {localPath}");
                            QuestionImage.Source = Microsoft.Maui.Controls.ImageSource.FromFile(localPath);
                            QuestionImage.IsVisible = true;
                            ImagePlaceholder.Text = string.Empty;
                            ImagePlaceholder.IsVisible = false;
                            loaded = true;
                            break;
                        }
                        try
                        {
                            var bundledName = System.IO.Path.GetFileName(name);
                            Debug.WriteLine($"[QuizPage] Trying ImageSource.FromFile for bundled name: {bundledName}");
                            var bundledSource = Microsoft.Maui.Controls.ImageSource.FromFile(bundledName);
                        if (bundledSource != null)
                        {
                            QuestionImage.Source = bundledSource;
                            QuestionImage.IsVisible = true;
                            ImagePlaceholder.Text = string.Empty;
                            ImagePlaceholder.IsVisible = false;
                            loaded = true;
                            Debug.WriteLine($"[QuizPage] Loaded bundled image via FromFile: {bundledName}");
                            break;
                        }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"[QuizPage] FromFile attempt failed for {name}: {ex.Message}");
                        }


                        using var stream = await FileSystem.OpenAppPackageFileAsync(name);
                        if (stream != null)
                        {
                            var ms = new MemoryStream();
                            await stream.CopyToAsync(ms);
                            ms.Position = 0;
                            QuestionImage.Source = Microsoft.Maui.Controls.ImageSource.FromStream(() => ms);
                            QuestionImage.IsVisible = true;
                            ImagePlaceholder.Text = string.Empty;
                            ImagePlaceholder.IsVisible = false;
                            loaded = true;
                            break;
                        }
                    }
                    catch
                    {
                    }
                }

            }
            catch
            {
                QuestionImage.Source = GetPlaceholderImageSource();
                QuestionImage.IsVisible = true;
                ImagePlaceholder.Text = $"Ошибка при загрузке: {q.ImageFile}";
                ImagePlaceholder.IsVisible = true;
                Debug.WriteLine($"[QuizPage] Exception loading image for {q.ImageFile}");
            }
        }
        else
        {
            QuestionImage.Source = GetPlaceholderImageSource();
            QuestionImage.IsVisible = true;
            ImagePlaceholder.Text = "Без изображения для этого вопроса";
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

    private static ImageSource GetPlaceholderImageSource()
    {
        try
        {
            const string b64 = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR4nGNgYAAAAAMAAWgmWQ0AAAAASUVORK5CYII=";
            var bytes = Convert.FromBase64String(b64);
            return Microsoft.Maui.Controls.ImageSource.FromStream(() => new MemoryStream(bytes));
        }
        catch
        {
            return null;
        }
    }

    private void EnsureSampleImages()
    {
        try
        {
            var dir = Path.Combine(FileSystem.AppDataDirectory, "Images");
            Directory.CreateDirectory(dir);

            try
            {
                var all = QuizApp.Services.QuizService.GetQuestions();
                var counters = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                foreach (var q in all)
                {
                    var cat = (q.Category ?? "default").ToLower();
                    if (!counters.ContainsKey(cat)) counters[cat] = 0;
                    counters[cat] = counters[cat] + 1;
                }

                foreach (var kv in counters)
                {
                    var cat = kv.Key;
                    var count = kv.Value;
                    for (int i = 1; i <= count; i++)
                    {
                        var fileName = $"{cat}_{i}.jpg";
                        var path = Path.Combine(dir, fileName);
                        if (!File.Exists(path))
                        {
                            try { File.WriteAllBytes(path, sampleImageBytes); } catch { }
                        }
                    }
                }
            }
            catch
            {
                var sampleNames = new[] { "movies_1.jpg", "series_1.jpg", "cartoons_1.jpg", "animals_1.jpg", "nature_plants_1.jpg", "nature_ecology_1.jpg" };
                foreach (var name in sampleNames)
                {
                    var path = Path.Combine(dir, name);
                    if (!File.Exists(path))
                    {
                        try { File.WriteAllBytes(path, sampleImageBytes); } catch { }
                    }
                }
            }
        }
        catch { }
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
        // Пропустить вопрос 
        if (_skipUsed) return;
        _score++;
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

        // Конец викторины 
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
