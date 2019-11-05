using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            buttonList.ItemsSource = client.BotButtons.Where(x => x.ParentId == 0).OrderBy(x => x.Row).ThenBy(x => x.Column).Distinct();
            messageList.ItemsSource = client.BotMessages;

            buttonList.MouseDoubleClick += BtnNext_DoubleClik;

            tbName.TextChanged += Tb_TextChanged;
            tbRow.TextChanged += Tb_TextChanged;
            tbColumn.TextChanged += Tb_TextChanged;

            tbRow.PreviewTextInput += NumbersValidation;
            tbColumn.PreviewTextInput += NumbersValidation;

            btnMsgSend.Click += BtnMsgSend_Click;
            btnBack.Click += BtnBack_Click;
            btnAdd.Click += BtnAddButton_Click;
            btnDelete.Click += BtnDeleteButton_Click;
        }

        /// <summary>
        /// Отправка сообщения в ответ на текстовое сообщение полученное через телеграм бота
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMsgSend_Click(object sender, RoutedEventArgs e)
        {
            var botMessage = messageList.SelectedItem as BotMessage;
            var botMessageChatId = botMessage.ChatId;
            var botMessageId = botMessage.Id;
            var message = tbMsgSend.Text;

            client.SendMessage(message, botMessageChatId, botMessageId);
        }

        /// <summary>
        /// При изменении содержимого TextBox менять цвет рамки на дефолтный
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            var sourse = e.Source as TextBox;
            var color = (Color)ColorConverter.ConvertFromString("#FFABADB3");
            sourse.BorderBrush = new SolidColorBrush(color);
        }

        /// <summary>
        /// Запрет ввода в TextBox всего кроме чисел 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumbersValidation(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        /// <summary>
        /// Провалиться в следующую кнопку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNext_DoubleClik(object sender, MouseButtonEventArgs e)
        {
            var button = buttonList.SelectedItem as BotButton;
            Debug.WriteLine($"Двойной клик: {button.Id}");

            selectedId = button.Id; //Id выбранной кнопки по которой откроется следующее меню. Необходимо для возврата назад.
            btnBack.IsEnabled = button.Id > 0; //Скрываем кнопку если находимся на самом верхнем уровне меню
            buttonList.ItemsSource = client.BotButtons.Where(x => x.ParentId == button.Id).OrderBy(x => x.Row).ThenBy(x => x.Column).Distinct();
        }

        /// <summary>
        /// Возврат в меню на уровень вверх
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            var buttonListItems = buttonList.ItemsSource as IEnumerable<BotButton>;

            var backId = client.BotButtons.Where(x => x.Id == selectedId).Select(x => x.ParentId).FirstOrDefault(); // получаем ParenId кнопки по нажатию на которую открыто текущее меню,                                                                                                                     // для того чтобы получить все кнопки верхнего уровня меню

            selectedId = backId; // Перезаписываем Id выбранной кнопки для того что бы вернуться еще на уровень выше

            btnBack.IsEnabled = backId > 0; //Скрываем кнопку если находимся на самом верхнем уровне меню
            buttonList.ItemsSource = client.BotButtons.Where(x => x.ParentId == backId).OrderBy(x => x.Row).ThenBy(x => x.Column).Distinct();
        }

        /// <summary>
        /// Валидация текстового поля и подсветка красной рамкой
        /// </summary>
        /// <param name="tb"></param>
        /// <param name="fieldName"></param>
        /// <returns>Возвращает сообщение для последующего вывода в MessageBox</returns>
        private string TextBoxValidation(TextBox tb, string fieldName)
        {
            if (string.IsNullOrEmpty(tb.Text))
            {
                tb.BorderBrush = Brushes.Red;
                return $"Поле \"{fieldName}\" не заполнено\n";
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Добавить кнопку для бота
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddButton_Click(object sender, RoutedEventArgs e)
        {
            string message = string.Empty;

            message += TextBoxValidation(tbName, "Название");
            message += TextBoxValidation(tbRow, "Строка");
            message += TextBoxValidation(tbColumn, "Колонка");

            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message);
                return;
            }

            var btnName = tbName.Text;
            var row = int.Parse(tbRow.Text);
            var column = int.Parse(tbColumn.Text);

            client.AddBotButton(row, column, selectedId, btnName, null);

            tbName.Text = string.Empty;
            tbRow.Text = string.Empty;
            tbColumn.Text = string.Empty;

            //обновление отображаемых кнопок
            buttonList.ItemsSource = client.BotButtons.Where(x => x.ParentId == selectedId).OrderBy(x => x.Row).ThenBy(x => x.Column).Distinct();
        }

        /// <summary>
        /// Удалить кнопку у бота
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedButton = buttonList.SelectedItem as BotButton;

            if (selectedButton == null)
            {
                //TODO: попробовать заменить на "если кнопка не выбрана то удаление не активно".
                MessageBox.Show("Выберите кнопку для удаления.");
            }

            //Подтверждение удаления
            if (MessageBox.Show("Удаление кнопки приведет к удалению всех вложенных кнопок.\nПродолжить удаление?", "Предупреждение!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                return;
            }

            client.DeleteBotButton(selectedButton);

            //обновление отображаемых кнопок
            buttonList.ItemsSource = client.BotButtons.Where(x => x.ParentId == selectedId).OrderBy(x => x.Row).ThenBy(x => x.Column).Distinct();
        }
    }
}
