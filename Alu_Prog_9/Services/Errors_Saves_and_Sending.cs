using System;
using System.IO;

namespace Alu_Prog_9.Services
{
    internal class Errors_Saves_and_Sending
    {
        private readonly string path = Properties.Settings.Default.Path_Errors_Log + @"\Errors Log.txt";

        public void Recording_Errors(Exception ex)
        {
            Telegram_Bot_Send_Activity telegram_Bot_Send_Activity = new Telegram_Bot_Send_Activity();

            telegram_Bot_Send_Activity.Al_Store_Send_Errors(ex);
            string Msg = $"{DateTime.Today}\nHResult: {ex.HResult}\nErr: {ex.Message}\nMethod: {ex.TargetSite}\n\n";
            using (StreamWriter stream = new StreamWriter(path, true))
                stream.WriteLine(Msg);
        }

        public void Send_Recording_Errors()
        {
            Telegram_Bot_Send_Activity telegram_Bot_Send_Activity = new Telegram_Bot_Send_Activity();

            telegram_Bot_Send_Activity.Al_Store_Send_File_Errors_File(path);
        }
    }
}
