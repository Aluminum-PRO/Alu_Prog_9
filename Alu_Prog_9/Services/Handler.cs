using Alu_Prog_9.Classes;
using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Alu_Prog_9.Services
{
    class Handler
    {
        string app_version;

        public void Get_Update_Information_Loaded(int id, string type, string name, string program_name, string version)
        {
            if (System.IO.File.Exists(Properties.Settings.Default.Full_Path + "\\" + type + "\\" + name + "\\" + name + ".exe"))
            {
                FileVersionInfo myFileVersionInfo_Store = FileVersionInfo.GetVersionInfo(Properties.Settings.Default.Full_Path + "\\" + type + "\\" + name + "\\" + name + ".exe");
                app_version = myFileVersionInfo_Store.FileVersion;
                if (Convert.ToInt32(app_version.Split('.')[0]) != 0)
                { app_version += ".Release"; }
                else if (Convert.ToInt32(app_version.Split('.')[1]) != 0)
                { app_version += ".Beta"; }
                else if (Convert.ToInt32(app_version.Split('.')[2]) != 0)
                { app_version += ".Alpha"; }
                else if (Convert.ToInt32(app_version.Split('.')[3]) != 0)
                { app_version += ".Pre-Alpha"; }

                if (app_version != version)
                {
                    StaticVars.Count_Update++;
                }
                //MessageBox.Show(id + "\n" + type + "\n" + name + "\n" + program_name + "\n" + version + "\n" + version_TPK_Ed + "\n" + app_version + "\n" + StaticVars.Count_Update);
            }
            
        }

        [System.Runtime.InteropServices.DllImport("Shell32.dll")]
        private static extern int SHChangeNotify(int eventId, int flags, IntPtr item1, IntPtr item2);

        public void Create_Shortcut(string type, string app_name, string directory_name, string short_description, string version, string hot_key)
        {
            if (System.IO.File.Exists("C:\\Users\\" + Properties.Settings.Default.User_Identyty + "\\Desktop\\" + app_name + ".lnk"))
            {
                System.IO.File.Delete("C:\\Users\\" + Properties.Settings.Default.User_Identyty + "\\Desktop\\" + app_name + ".lnk");
            }
            
            object shDesktop = (object)"Desktop";
            WshShell shell = new WshShell();
            string shortcutAddress = (string)shell.SpecialFolders.Item(ref shDesktop) + @"\" + app_name + ".lnk";
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
            FileInfo file = new System.IO.FileInfo(Properties.Settings.Default.Full_Path + "\\" + type + "\\" + directory_name + app_name + ".exe");
            double size = file.Length;
            size /= 1048576;
            shortcut.Description = "Описание файла: " + short_description + 
                "\nОрганизация: Aluminum-Company" +
                "\nВерсия файла: " + version +
                "\nДата создания: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm") +
                "\nРазмер: " + size.ToString("0.00") + " МБ";
            shortcut.Hotkey = hot_key;
            shortcut.TargetPath = Properties.Settings.Default.Full_Path + "\\" + type + "\\" + directory_name + app_name + ".exe";
            //shortcut.Arguments = "\"C:\\Program Files (x86)\\My Program\\Prog.accdr\"  /runtime";

            if (System.IO.File.Exists(Properties.Settings.Default.Full_Path + "\\" + type + "\\" + directory_name + app_name + ".exe"))
            {
                if (System.IO.File.Exists(Properties.Settings.Default.Full_Path + "\\" + type + "\\" + directory_name + "Иконки ярлыков\\" + app_name + ".ico"))
                {
                    shortcut.IconLocation = Properties.Settings.Default.Full_Path + "\\" + type + "\\" + directory_name + "Иконки ярлыков\\" + app_name + ".ico";
                }
                else
                {
                    MessageBox.Show("Ошибка");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Ошибка");
                return;
            }
            SHChangeNotify(0x8000000, 0x2000, IntPtr.Zero, IntPtr.Zero);

            shortcut.Save();
            { 
            //else if (Properties.Settings.Default.Create_Shortcut == "Off PC")
            //{

            //    if (System.IO.File.Exists("C:\\Users\\" + Properties.Settings.Default.User_Identyty + "\\Desktop\\Off PC.lnk"))
            //    {
            //        System.IO.File.Delete("C:\\Users\\" + Properties.Settings.Default.User_Identyty + "\\Desktop\\Off PC.lnk");
            //    }

            //    object shDesktop = (object)"Desktop";
            //    WshShell shell = new WshShell();
            //    string shortcutAddress = (string)shell.SpecialFolders.Item(ref shDesktop) + @"\" + "Off PC.lnk";
            //    IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
            //    System.IO.FileInfo file = new System.IO.FileInfo(Properties.Settings.Default.Path_Programs + "\\Off PC\\Off PC.exe");
            //    double size = file.Length;
            //    size /= 1048576;
            //    shortcut.Description = "Описание файла: Off PC - Умное выключение ПК\nОрганизация: Aluminum-Company\nВерсия файла: " + Properties.Settings.Default.Version_Off_PC + "\nДата создания: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm") + "\nРазмер: " + size.ToString("0.00") + " МБ";
            //    shortcut.Hotkey = "Ctrl+Shift+O";
            //    shortcut.TargetPath = Properties.Settings.Default.Path_Programs + "\\Off PC\\Off PC.exe";
            //    //shortcut.Arguments = "\"C:\\Program Files (x86)\\My Program\\Prog.accdr\"  /runtime";

            //    if (System.IO.File.Exists(Properties.Settings.Default.Path_Programs + "\\Off PC\\Off PC.exe"))
            //    {
            //        if (System.IO.File.Exists(Properties.Settings.Default.Path_Programs + "\\Off PC\\Иконки ярлыков\\Off PC.ico"))
            //        {
            //            shortcut.IconLocation = Properties.Settings.Default.Path_Programs + "\\Off PC\\Иконки ярлыков\\Off PC.ico";
            //        }
            //        else
            //        {
            //            Close();
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        Close();
            //        return;
            //    }
            //    SHChangeNotify(0x8000000, 0x2000, IntPtr.Zero, IntPtr.Zero);

            //    shortcut.Save();
            //    Close();
            //}
        }
        }
    }
}
