﻿using System.Windows;
using System.Windows.Controls;

namespace Alu_Prog_9.Pages.Store_Pages.News
{
    /// <summary>
    /// Логика взаимодействия для Store_News_Update_History_Page.xaml
    /// </summary>
    public partial class Store_News_Update_History_Page : Page
    {
        public Store_News_Update_History_Page()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //                                                                                          / <--Это край области /    / <--Расстояние от края
            TextBox_Text.Text =
            "      V.0.1.1.3.Beta( 23.05.2022г. )\n" +
            " - Оптимизация:\n" +
            "   • Оптимизирована загрузка данных при запуске Al-Store(загрузка ускорилась на ~1 сек.)\n" +
            " - Функции:\n" +
            "   • Добавлена возможность отправлять авто-отчёт об ошибках\n" +
            "   • Добавлена возможность настроить загрузку данных при запуске Al-Store\n" +
            "   • Добавлен автономный сбор отчёта о возникающих ошибках программы\n" +
            " - Страницы:\n" +
            "   • Добавлена страница новостей \"Что нового\"\n" +
            "   • Добавлена страница настроек \"Загрузка данных\"\n" +
            "   • Добавлена страница настроек \"Ошибки\"\n" +
            "_______________________________________________________________________________________________________________\n" +
            "      V.0.1.1.11.Patch.Beta( 20.05.2022г. )\n" +
            " - Баги:\n" +
            "   • Исправлен баг, не отображающий программы при первом запуске после обновления Al-Store\n" +
            "   • Исправлен баг, не корректно отображавший число сторонних программ\n" +
            " - Оптимизация:\n" +
            "   • Добавлено удаление предыдущих страниц после перехода на новую вкладку программы\n" +
            " - Функции:\n" +
            "   • Уведомлениие об авто-авторзации перенесено в правый нижний угол экрана, и отображается\n" +
            "    в другом формате\n" +
            "_______________________________________________________________________________________________________________\n" +
            "      V.0.1.1.1.Beta( 19.05.2022г. )\n" +
            " - База данных:\n" +
            "   • Переделана система загрузки информации о приложениях. Теперь данные загружаются сразу\n" +
            "    при запуске, а не при нажатии на приложение\n" +
            " - Функции:\n" +
            "   • Добавлена анимация загрузки при запуске Al-Store\n" +
            "_______________________________________________________________________________________________________________\n" +
            "      V.0.1.0.92.Patch.Beta( @.@.2022г. )\n" +
            "_______________________________________________________________________________________________________________\n" +
            "      V.0.1.0.91.Patch.Beta( @.@.2022г. )\n" +
            "_______________________________________________________________________________________________________________\n" +
            "      V.0.1.0.9.Beta( @.@.2022г. )\n";
        }
    }
}
