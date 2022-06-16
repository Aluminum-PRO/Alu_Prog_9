using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Alu_Prog_9.Services
{
    internal class Telegram_Bot_Send_Activity
    {
        Errors_Saves_and_Sending errors_Saves_And_Sending = new Errors_Saves_and_Sending();
        private static TelegramBotClient client = new TelegramBotClient("2137127676:AAFs9OSCoZuYOQ9QlP_wCc9G4xxBf-N2iV8");
        private static int AdminId { get; set; } = 783874748;
        private string Msg;

        private void Send(string _Msg)
        {
            try { client.SendTextMessageAsync(AdminId, _Msg); }
            catch (Exception ex)
            {
                errors_Saves_And_Sending.Recording_Errors(ex);
            }
        }

        private void Send_File(string _Msg, string path)
        {
            try 
            {
                var stream = new FileStream(path, FileMode.Open);
                client.SendDocumentAsync(AdminId, new Telegram.Bot.Types.InputFiles.InputOnlineFile(stream, "Errors Log"), caption: _Msg); 
            }
            catch (Exception ex)
            {
                errors_Saves_And_Sending.Recording_Errors(ex);
            }
        }

        public void Al_Store_Started()
        {
            if (Properties.Settings.Default.User_Login.ToLower() == "admin")
                return;
            Msg = "Al-Bot(Al-Store)\nPC: /" + Properties.Settings.Default.User_Identyty +
                $"\nAcLg: {Properties.Settings.Default.User_Login}\nAcNm: {Properties.Settings.Default.User_Name} {Properties.Settings.Default.User_SurName}\nMsg: Al-Store запущен";
            Send(Msg);
        }

        public void Al_Store_Logined()
        {
            if (Properties.Settings.Default.User_Login.ToLower() == "admin")
                return;
            Msg = "Al-Bot(Al-Store)\nPC: /" + Properties.Settings.Default.User_Identyty +
                $"\nAcLg: {Properties.Settings.Default.User_Login}\nAcNm: {Properties.Settings.Default.User_Name} {Properties.Settings.Default.User_SurName}\nMsg: Авторизация";
            Send(Msg);
        }

        public void Al_Store_Auto_Logined()
        {
            if (Properties.Settings.Default.User_Login.ToLower() == "admin")
                return;
            Msg = "Al-Bot(Al-Store)\nPC: /" + Properties.Settings.Default.User_Identyty +
                $"\nAcLg: {Properties.Settings.Default.User_Login}\nAcNm: {Properties.Settings.Default.User_Name} {Properties.Settings.Default.User_SurName}\nMsg: Авто авторизация";
            Send(Msg);
        }

        public void Al_Store_Updating()
        {
            if (Properties.Settings.Default.User_Login.ToLower() == "admin")
                return;
            Msg = "Al-Bot(Al-Store)\nPC: /" + Properties.Settings.Default.User_Identyty +
                $"\nAcLg: {Properties.Settings.Default.User_Login}\nAcNm: {Properties.Settings.Default.User_Name} {Properties.Settings.Default.User_SurName}\nMsg: Запущено обновление";
            Send(Msg);
        }

        public void Al_Store_Send_Errors(Exception ex)
        {
            Msg = "Al-Bot(Al-Store)\nPC: /" + Properties.Settings.Default.User_Identyty +
                $"\nAcLg: {Properties.Settings.Default.User_Login}\nAcNm: {Properties.Settings.Default.User_Name} {Properties.Settings.Default.User_SurName}\nMsg: Отчёт об ошибке:\n" +
                $"HResult: {ex.HResult}\nErr: {ex.Message}\nMethod: {ex.TargetSite}";
            Send(Msg);
        }

        public void Al_Store_Send_File_Errors_File(string path)
        {
            Msg = "Al-Bot(Al-Store)\nPC: /" + Properties.Settings.Default.User_Identyty +
                $"\nAcLg: {Properties.Settings.Default.User_Login}\nAcNm: {Properties.Settings.Default.User_Name} {Properties.Settings.Default.User_SurName}\nMsg: Error Log:";
            Send_File(Msg, path);
        }
    }
}
