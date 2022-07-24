using Alu_Prog_9.User_Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Alu_Prog_9
{
    /// <summary>
    /// Логика взаимодействия для Telegram_Chating_Window.xaml
    /// </summary>
    public partial class Telegram_Chating_Window : Window
    {
        private static TelegramBotClient client;
        private static int AdminId { get; set; } = 783874748;

        //private static string Comm_Ch_UI = Properties.Settings.Default.User_Identyty;
        private const string Comm_Ch_1 = "Connect";
        private const string Comm_Ch_2 = "Disconnect";

        public Telegram_Chating_Window()
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None; Main_Border.CornerRadius = new CornerRadius(20); AllowsTransparency = true;
        }

        internal void GetUpdates()
        {
            client = new TelegramBotClient("2137127676:AAFs9OSCoZuYOQ9QlP_wCc9G4xxBf-N2iV8");
            var me = client.GetMeAsync().Result;
            if (me != null && !string.IsNullOrEmpty(me.Username))
            {
                client.SendTextMessageAsync(AdminId, "Al-Bot(Chating)\nPC: /" + Properties.Settings.Default.User_Identyty + $"\nAc: {Properties.Settings.Default.User_Name} {Properties.Settings.Default.User_SurName}\nMsg: Готов к подключению");
                int offset = 0;
                while (true)
                {
                    try
                    {
                        var updates = client.GetUpdatesAsync(offset).Result;
                        if (updates != null && updates.Count() > 0)
                        {
                            foreach (var update in updates)
                            {
                                ProcessUpdate(update);
                                offset = update.Id + 1;
                            }
                        }
                    }
                    catch (Exception)
                    { }
                    Thread.Sleep(1000);
                }
            }
        }

        private void ProcessUpdate(Telegram.Bot.Types.Update update)
        {
            switch (update.Type)
            {
                case Telegram.Bot.Types.Enums.UpdateType.Message:
                    var msg = update.Message.Text;
                    switch (msg)
                    {
                        //case Comm_Ch_UI:
                        //    Show();
                        //    break;
                        default:
                            break;
                    }

                    Telegram_Messages_UC telegram_Messages_UC = new Telegram_Messages_UC(msg, true);
                    Messages_Range.Children.Add(telegram_Messages_UC);
                    break;
                default:
                    break;
            }
        }

        private IReplyMarkup GetButtons_Chating_Commands()
        {
            return new ReplyKeyboardMarkup("")
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{new KeyboardButton(Comm_Ch_1), },
                    new List<KeyboardButton>{new KeyboardButton(Comm_Ch_2)}
                }
            };
        }

        private void Close_But_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch { }
        }

        private void Send_Msg_But_Click(object sender, RoutedEventArgs e)
        {
            if (Send_Msg_Text.Text != "")
            {
                Telegram_Messages_UC telegram_Messages_UC = new Telegram_Messages_UC(Send_Msg_Text.Text, false);
                Messages_Range.Children.Add(telegram_Messages_UC);
                client.SendTextMessageAsync(AdminId, "Al-Chat\nPC: /" + Properties.Settings.Default.User_Identyty + $"\nAc: {Properties.Settings.Default.User_Name} {Properties.Settings.Default.User_SurName}\nMsg: " + Send_Msg_Text.Text);
                Send_Msg_Text.Text = "";
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
