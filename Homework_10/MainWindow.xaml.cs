using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
using Homework_10.Model;

namespace Homework_10
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //TODO: попробовать реализовать отображение кнопок как в телеграмме, с учетом расположения в строке и колонке.


        MyBot client;
        int selectedId;
        public MainWindow()
        {
            InitializeComponent();
            client = new MyBot(this);
            buttonList.ItemsSource = client.botButtons.Where(x => x.ParentId == 0).OrderBy(x => x.Row).ThenBy(x => x.Column).Distinct();
        }

        private void btnDoubleClik(object sender, MouseButtonEventArgs e)
        {
            var button = buttonList.SelectedItem as BotButton;
            Debug.WriteLine($"Двойной клик: {button.Id}");

            selectedId = button.Id; //Id выбранной кнопки по которой откроется следующее меню. Необходимо для возврата назад.

            //btnBack.Visibility = button.Id > 0 ? Visibility.Visible : Visibility.Collapsed; //Скрываем кнопку если находимся на самом верхнем уровне меню
            btnBack.IsEnabled = button.Id > 0; //Скрываем кнопку если находимся на самом верхнем уровне меню
            buttonList.ItemsSource = client.botButtons.Where(x => x.ParentId == button.Id).OrderBy(x => x.Row).ThenBy(x => x.Column).Distinct();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var buttonListItems = buttonList.ItemsSource as IEnumerable<BotButton>;

            var backId = client.botButtons.Where(x => x.Id == selectedId).Select(x => x.ParentId).FirstOrDefault(); // получаем ParenId кнопки по нажатию на которую открыто текущее меню,                                                                                                                     // для того чтобы получить все кнопки верхнего уровня меню

            selectedId = backId; // Перезаписываем Id выбранной кнопки для того что бы вернуться еще на уровень выше

            //btnBack.Visibility = backId > 0 ? Visibility.Visible : Visibility.Collapsed; //Скрываем кнопку если находимся на самом верхнем уровне меню
            btnBack.IsEnabled = backId > 0; //Скрываем кнопку если находимся на самом верхнем уровне меню
            buttonList.ItemsSource = client.botButtons.Where(x => x.ParentId == backId).OrderBy(x => x.Row).ThenBy(x => x.Column).Distinct();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            var btnName = BtnName.Text;
            var row = int.Parse(Row.Text);
            var column = int.Parse(Column.Text);

            client.AddBotButton(row, column, selectedId, btnName, null);



            //TODO: сделать нормальное добавление кнопки с выбором строки и колонки расположения в меню.
            //тест добавления кнопки
            //client.AddBotButton(1, 1, 0, "test", null);

            //обновление отображаемых кнопок
            buttonList.ItemsSource = client.botButtons.Where(x => x.ParentId == selectedId).OrderBy(x => x.Row).ThenBy(x => x.Column).Distinct();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedButton = buttonList.SelectedItem as BotButton;

            client.DeleteBotButton(selectedButton);

            //обновление отображаемых кнопок
            buttonList.ItemsSource = client.botButtons.Where(x => x.ParentId == selectedId).OrderBy(x => x.Row).ThenBy(x => x.Column).Distinct();
        }
    }
}
