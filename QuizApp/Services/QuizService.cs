using System.Collections.Generic;
using QuizApp.Models;

namespace QuizApp.Services
{
    public static class QuizService
    {
        public static List<Question> GetQuestions()
        {
            var list = new List<Question>
            {
                // Фильмы
                new Question{ Category="Movies", Text="Кто сыграл роль Джека Доусона в фильме 'Титаник' (1997)?", Options=new List<string>{"Брэд Питт","Леонардо ДиКаприо","Мэтт Дэймон","Том Круз"}, CorrectAnswerIndex=1},
                new Question{ Category="Movies", Text="Какой режиссёр снял трилогию 'Властелин колец'?", Options=new List<string>{"Джеймс Кэмерон","Питер Джексон","Стивен Спилберг","Кристофер Нолан"}, CorrectAnswerIndex=1},
                new Question{ Category="Movies", Text="Какой фильм стал первой частью серии 'Звёздные войны'?", Options=new List<string>{"Эпизод IV: Новая надежда","Эпизод I: Скрытая угроза","Эпизод VII: Пробуждение силы","Эпизод V: Империя наносит ответный удар"}, CorrectAnswerIndex=0},
                new Question{ Category="Movies", Text="Кто исполнил роль Джокера в фильме 'Тёмный рыцарь' (2008)?", Options=new List<string>{"Хит Леджер","Джек Николсон","Хоакин Феникс","Джаред Лето"}, CorrectAnswerIndex=0},
                new Question{ Category="Movies", Text="Какой фильм получил Оскар за 'Лучший фильм' в 2020 году?", Options=new List<string>{"1917","Паразиты","Зеленая книга","Форд против Феррари"}, CorrectAnswerIndex=1},
                new Question{ Category="Movies", Text="В каком фильме звучит фраза 'Я вернусь' (I'll be back)?", Options=new List<string>{"Терминатор","Робокоп","Матрица","Хищник"}, CorrectAnswerIndex=0},
                new Question{ Category="Movies", Text="Кто режиссёр фильма 'Челюсти'?", Options=new List<string>{"Альфред Хичкок","Фрэнсис Форда Копполы","Стивен Спилберг","Ридли Скотт"}, CorrectAnswerIndex=2},
                new Question{ Category="Movies", Text="Какой фильм является основой для франшизы 'Марвел'?", Options=new List<string>{"Железный человек","Человек-паук","Тор","Капитан Америка"}, CorrectAnswerIndex=0},
                new Question{ Category="Movies", Text="Какой фильм рассказывает историю о выживании на необитаемом острове с Томом Хэнксом?", Options=new List<string>{"Выживший","Изгой","КастAway","127 часов"}, CorrectAnswerIndex=1},
                new Question{ Category="Movies", Text="Кто сыграл главную роль в фильме 'Форрест Гамп'?", Options=new List<string>{"Том Хэнкс","Роберт Де Ниро","Дензел Вашингтон","Николь Кидман"}, CorrectAnswerIndex=0},

                // Сериалы
                new Question{ Category="Series", Text="Как называется сериал о семье Старков и борьбе за Железный трон?", Options=new List<string>{"Игра престолов","Во все тяжкие","Остаться в живых","Под куполом"}, CorrectAnswerIndex=0},
                new Question{ Category="Series", Text="В каком сериале главный герой — преподаватель химии, ставший производителем метамфетамина?", Options=new List<string>{"Во все тяжкие","Лучше звоните Солу","Нарко","Мыслить как преступник"}, CorrectAnswerIndex=0},
                new Question{ Category="Series", Text="Как называется сериал про шесть друзей в Нью-Йорке?", Options=new List<string>{"Друзья","Сайнфелд","Как я встретил вашу маму","Уил и Грейс"}, CorrectAnswerIndex=0},
                new Question{ Category="Series", Text="Кто является главным детективом в сериале 'Шерлок' с Бенедиктом Камбербэтчем?", Options=new List<string>{"Доктор Ватсон","Шерлок Холмс","Майкрофт Холмс","Реджинальд"}, CorrectAnswerIndex=1},
                new Question{ Category="Series", Text="Какой сериал рассказывает о жизни медицинских работников в вымышленном госпитале 'Сиэтл Грейс'?", Options=new List<string>{"Анатомия страсти","Холм одного дерева","Доктор Хаус","Клиника"}, CorrectAnswerIndex=0},
                new Question{ Category="Series", Text="В сериале 'Очень странные дела' как зовут девочку с психокинетическими способностями?", Options=new List<string>{"Макс","Одиннадцать","Элли","Нэнси"}, CorrectAnswerIndex=1},
                new Question{ Category="Series", Text="Какой сериал основан на преступлениях Уолтера Уайта?", Options=new List<string>{"Во все тяжкие","Сыны анархии","Подпольная империя","Нарко"}, CorrectAnswerIndex=0},
                new Question{ Category="Series", Text="Какой сериал известен своим сюжетом об альтернативных реальностях и тёмных тайнах в маленьком городе?", Options=new List<string>{"Твин Пикс","Очень странные дела","Тьма","Остаться в живых"}, CorrectAnswerIndex=1},
                new Question{ Category="Series", Text="Как называется сериал о похождениях командного состава космического корабля 'Орегон' (пример)?", Options=new List<string>{"Звёздный путь","Вавилон 5","Доктор Кто","Темный ангел"}, CorrectAnswerIndex=0},
                new Question{ Category="Series", Text="Кто создал сериал 'Во все тяжкие'?", Options=new List<string>{"Винс Гиллиган","Шонда Раймс","Дэвид Шор","Винсент Джон"}, CorrectAnswerIndex=0},

                // Мультфильмы
                new Question{ Category="Cartoons", Text="Как зовут главного героя мультфильма 'Король Лев'?", Options=new List<string>{"Муфаса","Симба","Нала","Тимон"}, CorrectAnswerIndex=1},
                new Question{ Category="Cartoons", Text="Кто живёт на дне океана в ананасе?", Options=new List<string>{"Патрик","Спанч Боб","Сэнди","Сквидвард"}, CorrectAnswerIndex=1},
                new Question{ Category="Cartoons", Text="Как зовут говорящего медвежонка в 'Винни-Пухе'?", Options=new List<string>{"Пух","Тигра","Кролик","Пятачок"}, CorrectAnswerIndex=0},
                new Question{ Category="Cartoons", Text="Кто является главным героем мультфильмов о Микки Маусе?", Options=new List<string>{"Гуфи","Дональд","Микки Маус","Плуто"}, CorrectAnswerIndex=2},
                new Question{ Category="Cartoons", Text="Какой мультфильм рассказывает о приключениях льва Симбы?", Options=new List<string>{"Аладдин","Король Лев","Мулан","Бэмби"}, CorrectAnswerIndex=1},
                new Question{ Category="Cartoons", Text="Как зовут подружку Спанч Боба?", Options=new List<string>{"Сэнди","Нэнси","Сьюзи","Пэта"}, CorrectAnswerIndex=0},
                new Question{ Category="Cartoons", Text="Кто является главным антагонистом в мультфильме 'Русалочка'?", Options=new List<string>{"Тритон","Урсула","Себастьян","Флаундер"}, CorrectAnswerIndex=1},
                new Question{ Category="Cartoons", Text="Какой персонаж часто произносит 'Милый мой' и является другом Винни-Пуха?", Options=new List<string>{"Пятачок","Кенга","Иа","Кристофер Робин"}, CorrectAnswerIndex=2},
                new Question{ Category="Cartoons", Text="Какой мультсериал рассказывает о приключениях семьи Симпсонов?", Options=new List<string>{"Гриффины","Футурама","Симпсоны","Рик и Морти"}, CorrectAnswerIndex=2},
                new Question{ Category="Cartoons", Text="Кто является создателем мультфильмов про Микки Мауса?", Options=new List<string>{"Уолт Дисней","Хаяо Миядзаки","Геннадий Сокольский","Мэтт Стоун"}, CorrectAnswerIndex=0},

                // Животные
                new Question{ Category="Animals", Text="Какое животное самое быстрое на суше?", Options=new List<string>{"Лев","Гепард","Заяц","Тигр"}, CorrectAnswerIndex=1},
                new Question{ Category="Animals", Text="Какое животное самое высокое в мире?", Options=new List<string>{"Слон","Жираф","Верблюд","Лось"}, CorrectAnswerIndex=1},
                new Question{ Category="Animals", Text="Какое млекопитающее умеет летать?", Options=new List<string>{"Летучая мышь","Белка","Летучая белка","Коала"}, CorrectAnswerIndex=0},
                new Question{ Category="Animals", Text="Какое животное известно как 'царь джунглей'?", Options=new List<string>{"Тигр","Лев","Медведь","Гепард"}, CorrectAnswerIndex=1},
                new Question{ Category="Animals", Text="Какое животное обладает панцирем и медленно передвигается?", Options=new List<string>{"Черепаха","Крокодил","Ящерица","Змей"}, CorrectAnswerIndex=0},
                new Question{ Category="Animals", Text="Кто является самым большим животным на Земле?", Options=new List<string>{"Слон","Синий кит","Голубой касатик","Кит-убийца"}, CorrectAnswerIndex=1},
                new Question{ Category="Animals", Text="Какой зверь откладывает яйца?", Options=new List<string>{"Ехидна","Кенгуру","Опоссум","Белка"}, CorrectAnswerIndex=0},
                new Question{ Category="Animals", Text="Какое существо мигрирует на большие расстояния каждые год?", Options=new List<string>{"Слон","Синяя птица","Стая гусей","Морская черепаха"}, CorrectAnswerIndex=2},
                new Question{ Category="Animals", Text="Какое животное известно своей способностью изменять цвет кожи?", Options=new List<string>{"Хамелеон","Игуана","Лягушка","Ящерица"}, CorrectAnswerIndex=0},
                new Question{ Category="Animals", Text="Какое животное имеет длинную шею и питается листьями на высоких деревьях?", Options=new List<string>{"Жираф","Слон","Лама","Коала"}, CorrectAnswerIndex=0},

                // Растения
                new Question{ Category="Nature_Plants", Text="Как называется процесс, при котором растения превращают свет в энергию?", Options=new List<string>{"Фотосинтез","Дыхание","Транспирация","Хемосинтез"}, CorrectAnswerIndex=0},
                new Question{ Category="Nature_Plants", Text="Какое растение считается самым высоким деревом на Земле?", Options=new List<string>{"Секвойя","Дуб","Красное дерево","Бамбук"}, CorrectAnswerIndex=0},
                new Question{ Category="Nature_Plants", Text="Какое растение даёт нам хлеб и макароны (зёрна)?", Options=new List<string>{"Пшеница","Рис","Овёс","Кукуруза"}, CorrectAnswerIndex=0},
                new Question{ Category="Nature_Plants", Text="Как называется покрытие семени, в котором находится зародыш растения?", Options=new List<string>{"Плод","Корень","Оболочка семени","Лист"}, CorrectAnswerIndex=2},
                new Question{ Category="Nature_Plants", Text="Какое растение используется для производства бумаги?", Options=new List<string>{"Ель","Сосна","Тростник","Ель и сосна"}, CorrectAnswerIndex=3},
                new Question{ Category="Nature_Plants", Text="Какой орган у растения отвечает за фотосинтез?", Options=new List<string>{"Корень","Стебель","Лист","Цветок"}, CorrectAnswerIndex=2},
                new Question{ Category="Nature_Plants", Text="Какой тип растений имеет семена, заключённые в плод?", Options=new List<string>{"Споровые","Покрытосеменные","Голоземенные","Мохообразные"}, CorrectAnswerIndex=1},
                new Question{ Category="Nature_Plants", Text="Какой компонент необходим растению для фотосинтеза кроме света?", Options=new List<string>{"Азот","Кислород","Вода","Метан"}, CorrectAnswerIndex=2},
                new Question{ Category="Nature_Plants", Text="Как называется цикличное изменение года, влияющее на рост растений?", Options=new List<string>{"Фазы луны","Сезоны","Тектоника","Прилив"}, CorrectAnswerIndex=1},
                new Question{ Category="Nature_Plants", Text="Какой процесс помогает растениям переносить воду от корней к листьям?", Options=new List<string>{"Корневая активность","Транспирация","Осмос","Фотосинтез"}, CorrectAnswerIndex=1},

                // Экология
                new Question{ Category="Nature_Ecology", Text="Как называется разрушение лесов человеком?", Options=new List<string>{"Оперение","Вырубка","Засуха","Залив"}, CorrectAnswerIndex=1},
                new Question{ Category="Nature_Ecology", Text="Что такое биоразнообразие?", Options=new List<string>{"Видовое богатство","Качество почвы","Климат","Типы гор"}, CorrectAnswerIndex=0},
                new Question{ Category="Nature_Ecology", Text="Какой газ в основном вызывает парниковый эффект?", Options=new List<string>{"Кислород","Углекислый газ","Азот","Неон"}, CorrectAnswerIndex=1},
                new Question{ Category="Nature_Ecology", Text="Что такое переработка отходов?", Options=new List<string>{"Сжигание мусора","Повторное использование материалов","Захоронение","Плавление"}, CorrectAnswerIndex=1},
                new Question{ Category="Nature_Ecology", Text="Как называют способность экосистемы восстанавливаться после повреждений?", Options=new List<string>{"Устойчивость","Чувствительность","Адаптация","Пластичность"}, CorrectAnswerIndex=0},
                new Question{ Category="Nature_Ecology", Text="Какая деятельность сокращает озоновый слой?", Options=new List<string>{"Использование хлорфторуглеродов","Посадка деревьев","Рециклинг","Очистка воды"}, CorrectAnswerIndex=0},
                new Question{ Category="Nature_Ecology", Text="Что является источником загрязнения воды?", Options=new List<string>{"Промышленные сбросы","Чистая энергия","Растения","Птицы"}, CorrectAnswerIndex=0},
                new Question{ Category="Nature_Ecology", Text="Какой показатель часто используют для оценки качества воздуха?", Options=new List<string>{"pH","Индекс качества воздуха (AQI)","Температура","Скорость ветра"}, CorrectAnswerIndex=1},
                new Question{ Category="Nature_Ecology", Text="Что такое устойчивое развитие?", Options=new List<string>{"Развитие без учёта будущих поколений","Развитие с учётом ресурсов будущих поколений","Только экономический рост","Максимальное потребление"}, CorrectAnswerIndex=1},
                new Question{ Category="Nature_Ecology", Text="Какой вид энергии считается возобновляемым?", Options=new List<string>{"Нефть","Уголь","Солнечная энергия","Природный газ"}, CorrectAnswerIndex=2},

                // Страны
                new Question{ Category="Geo_Countries", Text="Какая страна самая большая по площади?", Options=new List<string>{"Канада","Россия","Китай","США"}, CorrectAnswerIndex=1},
                new Question{ Category="Geo_Countries", Text="Какова столица Франции?", Options=new List<string>{"Париж","Марсель","Лион","Ницца"}, CorrectAnswerIndex=0},
                new Question{ Category="Geo_Countries", Text="В какой стране находится река Нил?", Options=new List<string>{"Индия","Египет","Бразилия","Австралия"}, CorrectAnswerIndex=1},
                new Question{ Category="Geo_Countries", Text="Какая страна известна как 'Земля восходящего солнца'?", Options=new List<string>{"Китай","Корея","Япония","Вьетнам"}, CorrectAnswerIndex=2},
                new Question{ Category="Geo_Countries", Text="Какой континент включает в себя Бразилию и Аргентину?", Options=new List<string>{"Африка","Южная Америка","Европа","Северная Америка"}, CorrectAnswerIndex=1},
                new Question{ Category="Geo_Countries", Text="Какой город является столицей Японии?", Options=new List<string>{"Осака","Токио","Киото","Хиросима"}, CorrectAnswerIndex=1},
                new Question{ Category="Geo_Countries", Text="В какой стране находится гора Эверест?", Options=new List<string>{"Пакистан/Китай","Индия/Непал","Непал/Китай","Тибет/Пакистан"}, CorrectAnswerIndex=2},
                new Question{ Category="Geo_Countries", Text="Какая страна состоит из множества островов и имеет столицу Сукре?", Options=new List<string>{"Эквадор","Боливия","Перу","Чили"}, CorrectAnswerIndex=1},
                new Question{ Category="Geo_Countries", Text="Какая страна известна своей системой каналов и Амстердамом?", Options=new List<string>{"Германия","Нидерланды","Бельгия","Дания"}, CorrectAnswerIndex=1},
                new Question{ Category="Geo_Countries", Text="Какой океан омывает западный берег Африки?", Options=new List<string>{"Индийский океан","Атлантический океан","Тихий океан","Северный Ледовитый океан"}, CorrectAnswerIndex=1},

                // Рельеф
                new Question{ Category="Geo_Relief", Text="Как называется самая высокая гора на Земле?", Options=new List<string>{"К2","Эверест","Килиманджаро","Монблан"}, CorrectAnswerIndex=1},
                new Question{ Category="Geo_Relief", Text="Как называется длиннейшая река в мире?", Options=new List<string>{"Амазонка","Нил","Янцзы","Миссисипи"}, CorrectAnswerIndex=0},
                new Question{ Category="Geo_Relief", Text="Что такое плоскогорье?", Options=new List<string>{"Низменность","Равнина на большой высоте","Долина","Остров"}, CorrectAnswerIndex=1},
                new Question{ Category="Geo_Relief", Text="Какой тип рельефа образуется вулканами?", Options=new List<string>{"Дельта","Кратер и конус","Равнина","Каньон"}, CorrectAnswerIndex=1},
                new Question{ Category="Geo_Relief", Text="Как называется длинная узкая впадина между горами?", Options=new List<string>{"Долина","Плато","Равнина","Фьорд"}, CorrectAnswerIndex=0},
                new Question{ Category="Geo_Relief", Text="Что такое эрозия?", Options=new List<string>{"Процесс образования гор","Разрушение и вынос пород водой и ветром","Осадконакопление","Извержение"}, CorrectAnswerIndex=1},
                new Question{ Category="Geo_Relief", Text="Как называется часть суши, окружённая со всех сторон водой?", Options=new List<string>{"Полуостров","Остров","Материк","Залив"}, CorrectAnswerIndex=1},
                new Question{ Category="Geo_Relief", Text="Какой термин обозначает глубокое узкое морское углубление, заполненное морской водой?", Options=new List<string>{"Фьорд","Каньон","Лагуна","Залив"}, CorrectAnswerIndex=0},
                new Question{ Category="Geo_Relief", Text="Как называется грандиозная система гор, простирающаяся через западную часть Южной Америки?", Options=new List<string>{"Андская система","Гималаи","Скалистые горы","Альпы"}, CorrectAnswerIndex=0},
                new Question{ Category="Geo_Relief", Text="Что такое дюна?", Options=new List<string>{"Кусок леса","Песчаный холм, образованный ветром","Скальный утёс","Оазис"}, CorrectAnswerIndex=1},

                // Океаны
                new Question{ Category="Geo_Oceans", Text="Какой океан является самым большим по площади?", Options=new List<string>{"Атлантический","Индийский","Тихий","Северный ледовитый"}, CorrectAnswerIndex=2},
                new Question{ Category="Geo_Oceans", Text="Как называется самая глубокая точка Мирового океана (в Марианской впадине)?", Options=new List<string>{"Кермадек","Челленджер Дип","Тренч Мариана","Гибралтарский пролив"}, CorrectAnswerIndex=1},
                new Question{ Category="Geo_Oceans", Text="Какой океан омывает берега Австралии с запада?", Options=new List<string>{"Тихий","Индийский","Атлантический","Северный ледовитый"}, CorrectAnswerIndex=1},
                new Question{ Category="Geo_Oceans", Text="Как называется явление, когда поверхность океана временно поднимается и опускается из-за приливов?", Options=new List<string>{"Цунами","Прилив и отлив","Шторм","Циркуляция"}, CorrectAnswerIndex=1},
                new Question{ Category="Geo_Oceans", Text="Какой морской поток тёплый и влияет на климат северо-западной Европы?", Options=new List<string>{"Калифорнийское течение","Гольфстрим","Лабрадорское течение","Бенгальское течение"}, CorrectAnswerIndex=1},
                new Question{ Category="Geo_Oceans", Text="Что такое коралловый риф?", Options=new List<string>{"Подводная гора","Сооружение из вулканических пород","Экосистема, созданная кораллами","Тип водорослей"}, CorrectAnswerIndex=2},
                new Question{ Category="Geo_Oceans", Text="Как называется процесс изменения солёности и температуры вод океана, влияющий на климат?", Options=new List<string>{"Тектоника","Океанская циркуляция","Эрозия","Инсоляция"}, CorrectAnswerIndex=1},
                new Question{ Category="Geo_Oceans", Text="Что вызывает образование волн на поверхности океана?", Options=new List<string>{"Двигатели кораблей","Ветер","Подводные вулканы","Соседи"}, CorrectAnswerIndex=1},
                new Question{ Category="Geo_Oceans", Text="Как называется зона океана с повышенной продуктивностью и большим количеством рыб?", Options=new List<string>{"Мёртвые зоны","Пелагическая зона","Прибрежная экосистема","Зона апвеллинга"}, CorrectAnswerIndex=3},
                new Question{ Category="Geo_Oceans", Text="Какой процесс приводит к повышению уровня моря?", Options=new List<string>{"Уменьшение осадков","Таяние ледников","Рост кораллов","Испарение"}, CorrectAnswerIndex=1}
                ,
                // Документальные
                new Question{ Category="Documentaries", Text="Какой фильм получил приз в Венеции как лучший документальный, хотя его сюжет о полете СССР на Луну в 1938 году — выдумка?" , Options=new List<string>{ "Космос как предчувствие", "Укрощение огня", "Первые на Луне", "Бумажный солдат"}, CorrectAnswerIndex=2},
                new Question{ Category="Documentaries", Text="Что такое документальный фильм?", Options=new List<string>{"Фильм с вымышленным сюжетом","Нефикшн фильм о реальных событиях","Рекламный ролик","Музыкальный клип"}, CorrectAnswerIndex=1},
                new Question{ Category="Documentaries", Text="Какой документальный фильм исследует природу Амазонки?", Options=new List<string>{"Амазонка: жизнь","Тайны джунглей","Реки мира","Лесной мир"}, CorrectAnswerIndex=0},
                new Question{ Category="Documentaries", Text="Какой тип материалов часто используется в документальных фильмах?", Options=new List<string>{"Интервью","Музыка","Художественные сцены","Анимация"}, CorrectAnswerIndex=0},
                new Question{ Category="Documentaries", Text="Какая цель документалистики?", Options=new List<string>{"Развлечь зрителя","Документировать и информировать","Создать игровой сюжет","Продать продукт"}, CorrectAnswerIndex=1},
                new Question{ Category="Documentaries", Text="Какой элемент НЕ является обязательным в документальном фильме?", Options=new List<string>{"Факты","Интервью","Полностью вымышленный сюжет","Архивные кадры"}, CorrectAnswerIndex=2},
                new Question{ Category="Documentaries", Text="Какой формат часто применяется для научных документальных фильмов?", Options=new List<string>{"Обучающий","Мюзикл","Романтика","Фэнтези"}, CorrectAnswerIndex=0},
                new Question{ Category="Documentaries", Text="Какая платформа известна за показ документальных фильмов?", Options=new List<string>{"Рутуб","Ютуб","Твич","Стим"}, CorrectAnswerIndex=0},
                new Question{ Category="Documentaries", Text="Какой жанр документалки рассказывает о жизни конкретного человека?", Options=new List<string>{"Биография","Фантастика","Хоррор","Комедия"}, CorrectAnswerIndex=0},
                new Question{ Category="Documentaries", Text="Какой подход характерен для документальных расследований?", Options=new List<string>{"Инсценировка событий","Журналистское исследование","Полный вымысел","Анимированное повествование"}, CorrectAnswerIndex=1},

                // Птицы
                new Question{ Category="Nature_Birds", Text="Какая птица известна своими длинными перелётами и возвращением в одно и то же место?", Options=new List<string>{"Голубь","Ласточка","Соловей","Стриж"}, CorrectAnswerIndex=1},
                new Question{ Category="Nature_Birds", Text="Как называется способность птиц ориентироваться на большие расстояния?", Options=new List<string>{"Навигация","Фотосинтез","Эхолокация","Транспирация"}, CorrectAnswerIndex=0},
                new Question{ Category="Nature_Birds", Text="Какая птица не умеет летать, но хорошо плавает?", Options=new List<string>{"Пингвин","Страус","Голубь","Сова"}, CorrectAnswerIndex=0},
                new Question{ Category="Nature_Birds", Text="Какой орган помогает птицам издавать песни?", Options=new List<string>{"Сердце","Клюв","Сиринкс","Лёгкие"}, CorrectAnswerIndex=2},
                new Question{ Category="Nature_Birds", Text="Какая птица самая крупная по массе?", Options=new List<string>{"Страус","Альбатрос","Пеликан","Гуси"}, CorrectAnswerIndex=0},
                new Question{ Category="Nature_Birds", Text="Какая птица известна способностью имитировать звуки и голос человека?", Options=new List<string>{"Канарейка","Попугай","Ворона","Синица"}, CorrectAnswerIndex=1},
                new Question{ Category="Nature_Birds", Text="Как называется период, когда птицы откладывают яйца?", Options=new List<string>{"Миграция","Гнездование","Кладка","Парация"}, CorrectAnswerIndex=1},
                new Question{ Category="Nature_Birds", Text="Какая птица признана символом мира?", Options=new List<string>{"Голубь","Ястреб","Воробей","Утка"}, CorrectAnswerIndex=0},
                new Question{ Category="Nature_Birds", Text="Что помогает птицам держаться в воздухе?", Options=new List<string>{"Крылья и воздуховедение","Лёгкие и кости","Только мышцы","Хвост и лапы"}, CorrectAnswerIndex=0},
                new Question{ Category="Nature_Birds", Text="Какая птица имеет острый клюв для охоты?", Options=new List<string>{"Ястреб","Голубь","Утка","Курица"}, CorrectAnswerIndex=0},

                // Климат
                new Question{ Category="Geo_Climate", Text="Как называется среднее состояние погоды в регионе в течение длительного периода?", Options=new List<string>{"Погода","Климат","Сезон","Атмосфера"}, CorrectAnswerIndex=1},
                new Question{ Category="Geo_Climate", Text="Что такое глобальное потепление?", Options=new List<string>{"Локальное похолодание","Повышение средней температуры планеты","Увеличение осадков","Извержение вулканов"}, CorrectAnswerIndex=1},
                new Question{ Category="Geo_Climate", Text="Какой газ усиливает парниковый эффект?", Options=new List<string>{"Кислород","Азот","Углекислый газ","Гелий"}, CorrectAnswerIndex=2},
                new Question{ Category="Geo_Climate", Text="Какая система измеряет погодные условия и помогает прогнозировать климат?", Options=new List<string>{"Метеорология","Астрономия","Геология","Биология"}, CorrectAnswerIndex=0},
                new Question{ Category="Geo_Climate", Text="Какой фактор влияет на климат региона?", Options=new List<string>{"Растительность","Рельеф","Океанические течения","Все перечисленное"}, CorrectAnswerIndex=3},
                new Question{ Category="Geo_Climate", Text="Что может стать следствием изменения климата?", Options=new List<string>{"Повышение уровня моря","Стабильность экосистем","Уменьшение ураганов","Снижение температуры всей планеты"}, CorrectAnswerIndex=0},
                new Question{ Category="Geo_Climate", Text="Какой термин описывает циклические изменения климата Земли на большие промежутки времени?", Options=new List<string>{"Сезоны","Ледниковые и межледниковые периоды","День и ночь","Приливы"}, CorrectAnswerIndex=1},
                new Question{ Category="Geo_Climate", Text="Что такое климатическая зона?", Options=new List<string>{"Регион с похожими климатическими условиями","Городская зона","Тип почвы","Рельеф"}, CorrectAnswerIndex=0},
                new Question{ Category="Geo_Climate", Text="Какой фактор способствует смягчению климата побережья?", Options=new List<string>{"Океанические течения","Горы","Пустыни","Реки"}, CorrectAnswerIndex=0},
                new Question{ Category="Geo_Climate", Text="Какая деятельность человека увеличивает выбросы парниковых газов?", Options=new List<string>{"Энергетика на ископаемом топливе","Посадка лесов","Использование возобновляемых источников","Рециклинг"}, CorrectAnswerIndex=0},

                // Древняя история
                new Question{ Category="History_Ancient", Text="Где находилась цивилизация Древнего Египта?", Options=new List<string>{"В долине реки Нил","В долине реки Инд","В долине реки Янцзы","В долине реки Амазонки"}, CorrectAnswerIndex=0},
                new Question{ Category="History_Ancient", Text="Как назывался правитель Древнего Египта?", Options=new List<string>{"Император","Фараон","Царь","Князь"}, CorrectAnswerIndex=1},
                new Question{ Category="History_Ancient", Text="Какой древний город знаменит своим маяком и библиотекой?", Options=new List<string>{"Александрия","Рим","Афины","Карфаген"}, CorrectAnswerIndex=0},
                new Question{ Category="History_Ancient", Text="К какому народу принадлежит миф о Троянской войне?", Options=new List<string>{"Римляне","Греки","Египтяне","Персы"}, CorrectAnswerIndex=1},
                new Question{ Category="History_Ancient", Text="Какой древний человек считается одним из первых философов в Греции?", Options=new List<string>{"Сократ","Фалес","Аристотель","Платон"}, CorrectAnswerIndex=1},
                new Question{ Category="History_Ancient", Text="Что строили древние римляне для перемещения воды в города?", Options=new List<string>{"Акведуки","Каналы","Дамбы","Мосты"}, CorrectAnswerIndex=0},
                new Question{ Category="History_Ancient", Text="Какая культура использовала клинопись?", Options=new List<string>{"Шумеры","Ацтеки","Китайцы","Индусы"}, CorrectAnswerIndex=0},
                new Question{ Category="History_Ancient", Text="Кто был завоевателем, создавшим империю, простирающуюся от Европы до Индии в IV в. до н.э.?", Options=new List<string>{"Юлий Цезарь","Александр Македонский","Чингисхан","Наполеон"}, CorrectAnswerIndex=1},
                new Question{ Category="History_Ancient", Text="Какой материал использовали для письма в Древнем Египте?", Options=new List<string>{"Папирус","Бумага","Пластик","Кожа"}, CorrectAnswerIndex=0},
                new Question{ Category="History_Ancient", Text="Какая древняя цивилизация строила пирамиды?", Options=new List<string>{"Междуречье","Древний Египет","Индская","Ма́я"}, CorrectAnswerIndex=1},

                // Средневековье
                new Question{ Category="History_Medieval", Text="Какой период принято называть Средневековьем?", Options=new List<string>{"IX–XV века","V–XV века","I–V века","XVI–XVIII века"}, CorrectAnswerIndex=1},
                new Question{ Category="History_Medieval", Text="Что было характерно для феодальной системы?", Options=new List<string>{"Равенство всех","Крепостное право и вассалитет","Демократия","Индустриализация"}, CorrectAnswerIndex=1},
                new Question{ Category="History_Medieval", Text="Как назывались защитные сооружения для аристократии в Средневековье?", Options=new List<string>{"Ратуши","Крепости и замки","Пагоды","Дворцы"}, CorrectAnswerIndex=1},
                new Question{ Category="History_Medieval", Text="Как назывался торговый путь, связывавший Китай и Европу в средние века?", Options=new List<string>{"Шелковый путь","Путь моряков","Каменный путь","Торговая тропа"}, CorrectAnswerIndex=0},
                new Question{ Category="History_Medieval", Text="Какая религия была доминирующей в Европе в Средние века?", Options=new List<string>{"Буддизм","Ислам","Христианство","Индуизм"}, CorrectAnswerIndex=2},
                new Question{ Category="History_Medieval", Text="Что такое крестовые походы?", Options=new List<string>{"Торговые миссии","Военные походы европейцев в Святую землю","Семейные праздники","Научные экспедиции"}, CorrectAnswerIndex=1},
                new Question{ Category="History_Medieval", Text="Кто был вассалом?", Options=new List<string>{"Свободный крестьянин","Воин, принявший феодальную присягу лорду","Купец","Король"}, CorrectAnswerIndex=1},
                new Question{ Category="History_Medieval", Text="Какой язык был основным в церковной службе в средневековой Европе?", Options=new List<string>{"Греческий","Латинский","Староанглийский","Французский"}, CorrectAnswerIndex=1},
                new Question{ Category="History_Medieval", Text="Что помогло распространению знаний в позднем Средневековье?", Options=new List<string>{"Печатный станок","Телеграф","Интернет","Автомобиль"}, CorrectAnswerIndex=0},
                new Question{ Category="History_Medieval", Text="Как назывался главный правитель в феодальной структуре?", Options=new List<string>{"Король","Мэр","Депутат","Президент"}, CorrectAnswerIndex=0},

                // Новейшая история
                new Question{ Category="History_Modern", Text="В каком веке произошла промышленная революция?", Options=new List<string>{"XVIII–XIX века","XVI век","XX век","XVII век"}, CorrectAnswerIndex=0},
                new Question{ Category="History_Modern", Text="Какой конфликт называют Первой мировой войной?", Options=new List<string>{"1914–1918 гг.","1939–1945 гг.","1804–1815 гг.","1900–1905 гг."}, CorrectAnswerIndex=0},
                new Question{ Category="History_Modern", Text="Какой год считается годом падения Берлинской стены?", Options=new List<string>{"1989","1991","1985","1979"}, CorrectAnswerIndex=0},
                new Question{ Category="History_Modern", Text="Кто был лидером СССР в конце 1980-х годов, начавшим реформы перестройки?", Options=new List<string>{"Ленин","Горбачёв","Сталин","Хрущёв"}, CorrectAnswerIndex=1},
                new Question{ Category="History_Modern", Text="Какое событие считается формальным началом Второй мировой войны?", Options=new List<string>{"Атака на Пёрл-Харбор","Вторжение в Польшу 1939","Битва при Сталинграде","День D"}, CorrectAnswerIndex=1},
                new Question{ Category="History_Modern", Text="Какой процесс сопровождал распад колониальных империй в XX веке?", Options=new List<string>{"Индустриализация","Деколонизация","Колонизация","Миграция"}, CorrectAnswerIndex=1},
                new Question{ Category="History_Modern", Text="Какая организация была создана после Второй мировой войны для поддержания мира?", Options=new List<string>{"Лига наций","ООН","НАТО","Варшавский договор"}, CorrectAnswerIndex=1},
                new Question{ Category="History_Modern", Text="Какой год отмечает начало информационной эры с распространением персональных компьютеров?", Options=new List<string>{"1960","1970","1980","1990"}, CorrectAnswerIndex=2},
                new Question{ Category="History_Modern", Text="Что обычно называют холодной войной?", Options=new List<string>{"Военный конфликт между США и Японией","Геополитическое противостояние СССР и США","Экономический кризис","Климатическое явление"}, CorrectAnswerIndex=1},
                new Question{ Category="History_Modern", Text="Какой процесс привёл к глобальной интеграции экономики в конце XX века?", Options=new List<string>{"Автаркия","Глобализация","Изоляция","Коллективизация"}, CorrectAnswerIndex=1},

                // Мировые войны
                new Question{ Category="History_Wars", Text="Когда началась Первая мировая война?", Options=new List<string>{"1914","1939","1918","1945"}, CorrectAnswerIndex=0},
                new Question{ Category="History_Wars", Text="Какой город был целью атомной бомбардировки в 1945 году?", Options=new List<string>{"Токио","Хиросима","Нагасаки","Осака"}, CorrectAnswerIndex=1},
                new Question{ Category="History_Wars", Text="Какая страна была на стороне Осей во Второй мировой войне?", Options=new List<string>{"США","Великобритания","Япония","Канада"}, CorrectAnswerIndex=2},
                new Question{ Category="History_Wars", Text="Какой договор завершил Первую мировую войну для Германии?", Options=new List<string>{"Мир в Версале","Парижский договор","Рапалльский договор","Сан-Франциский договор"}, CorrectAnswerIndex=0},
                new Question{ Category="History_Wars", Text="Как назывался план восстановления Европы после Второй мировой войны?", Options=new List<string>{"План Маршалла","План Рузвельта","План Трумэна","План Шумана"}, CorrectAnswerIndex=0},
                new Question{ Category="History_Wars", Text="Какая битва считается поворотной в Тихоокеанском театре во Второй мировой войне?", Options=new List<string>{"При Мидуэе","При Сталинграде","При Курске","При Эль-Аламейне"}, CorrectAnswerIndex=0},
                new Question{ Category="History_Wars", Text="Какой год завершил Вторую мировую войну?", Options=new List<string>{"1944","1945","1946","1947"}, CorrectAnswerIndex=1},
                new Question{ Category="History_Wars", Text="Какая организация была создана для предотвращения будущих мировых войн?", Options=new List<string>{"Лига Наций","ООН","НАТО","ЕС"}, CorrectAnswerIndex=1},
                new Question{ Category="History_Wars", Text="Что стало одной из причин начала Первой мировой войны?", Options=new List<string>{"Колониальные споры","Эпидемии","Авиакатастрофы","Стихийные бедствия"}, CorrectAnswerIndex=0},
                new Question{ Category="History_Wars", Text="Какая военная технология впервые широко использована в Первой мировой войне?", Options=new List<string>{"Ракеты","Химическое оружие","Ядерное оружие","Кибероружие"}, CorrectAnswerIndex=1}
            };

            var counters = new Dictionary<string, int>();
            foreach (var q in list)
            {
                if (string.IsNullOrEmpty(q.ImageFile))
                {
                    if (!counters.ContainsKey(q.Category)) counters[q.Category] = 1;
                    var idx = counters[q.Category];
                    // Формируем имя файла на основе категории и порядкового номера (используем .jpg)
                    // Добавляем суффикс 'a' в конец имени, чтобы соответствовать переименованным ресурсам
                    q.ImageFile = $"{q.Category.ToLower()}_{idx}a.jpg";
                    counters[q.Category] = idx + 1;
                }
            }

            return list;
        }
    }
}
