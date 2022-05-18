using Alu_Prog_9.Classes;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Alu_Prog_9
{
    /// <summary>
    /// Логика взаимодействия для Logo_Window.xaml
    /// </summary>
    public partial class Logo_Window : Window
    {
        public Logo_Window()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Activate();
            Show_Logo();
        }
        private async Task Show_Logo()
        {
            for (Opacity = 0; Opacity <= 1;)
            {
                Opacity += 0.05;
                await Task.Delay(30);
            }

            for (Opacity = 1; Opacity >= 0.4;)
            {
                Opacity -= 0.05;
                await Task.Delay(30);
            }

            await Task.Delay(50);

            for (Opacity = 0.4; Opacity <= 1;)
            {
                Opacity += 0.05;
                await Task.Delay(30);
            }
            StaticVars.Logo_Animation = false;
            Check_Loaded();
        }

        private async void Check_Loaded()
        {
            await Task.Run(() => {
                while (true)
                {
                    if (!StaticVars.Loading_Data)
                    { break; }
                }
            });
            
            Hide_Logo();
        }

        private async Task Hide_Logo()
        {
            for (Opacity = 1; Opacity >= 0;)
            {
                Opacity -= 0.05;
                await Task.Delay(30);
            }
            
            Close();
        }

    }
}
