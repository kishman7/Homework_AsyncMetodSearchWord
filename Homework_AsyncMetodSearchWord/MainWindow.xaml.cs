using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Homework_AsyncMetodSearchWord
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog(); //діалогове вікно
            if (ofd.ShowDialog() == true)
            {
                lbl3.Content = ofd.FileName; //виводимо шлях до файла
            }
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            SearchWordAsync(lbl3.Content.ToString(), textbox1.Text);
            lbl8.Content = lbl3.Content;
            //lbl6.Content = System.IO.Path.GetFileNameWithoutExtension((lbl3.Content as FileInfo).FullName.ToString());
            lbl6.Content = System.IO.Path.GetFileName(lbl3.Content.ToString());
        }

        static int SearchWord(string s, string word)
        {
            //var output = Regex.Match(s, @"'(.+?)'").Groups[1].Value;
            string a = s; // В цьому рядку шукаємо
            string b = word; //Это слово ищем
            char[] separators = { ',', ' ', ';', '.', '\\' }; //создаём массив символов, служащих разделителями слова
            string[] words = a.Split(separators, StringSplitOptions.RemoveEmptyEntries); // із отриманих символів отримуємо масив слів
            int result = 0; //сюда пишем результат
            foreach (string item in words)
            {
                if (b.Equals(item)) // порівнюємо слово з масиву з шукаючим словом
                {
                    result ++;
                }
            }
            return result;
        }

        async void SearchWordAsync(string s, string word) //асинхронний метод, який шукає слова та їх кількість в потоці, який працює паралельно з головним потоком
        {
            int res = await Task.Run(() => SearchWord(s, word));
            lbl10.Content = res.ToString();
        }
    }
}
