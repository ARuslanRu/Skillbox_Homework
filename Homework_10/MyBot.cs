using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Homework_10.Model;
using System.Collections.ObjectModel;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Homework_10
{
    class MyBot
    {
        private MainWindow w;
        private TelegramBotClient bot;
        private string token;

        public ObservableCollection<BotMessage> BotMessages { get; set; }
        public ObservableCollection<BotButton> BotButtons { get; set; }
        public MyBot(MainWindow w, string pathToken = "token")
        {
            token = System.IO.File.ReadAllText(pathToken);

            var json = System.IO.File.ReadAllText("buttons.json");
            BotButtons = JsonConvert.DeserializeObject<ObservableCollection<BotButton>>(json);
            BotMessages = new ObservableCollection<BotMessage>();
            this.w = w;
            this.bot = new TelegramBotClient(token);

            bot.OnUpdate += UpdateListener;
            bot.StartReceiving();
        }

        /// <summary>
        /// Сохранение текстовых сообщений в коллекцию
        /// </summary>
        /// <param name="msg"></param>
        private void MessageLogger(Message msg)
        {
            string text = $"{DateTime.Now.ToLongTimeString()}: {msg.Chat.FirstName} {msg.Chat.Id} {msg.Text}";
            Debug.WriteLine($"{text} TypeMessage: {msg.Type.ToString()}");

            if (msg.Text == null) return;

            var messageText = msg.Text;

            w.Dispatcher.Invoke(() =>
            {
                BotMessages.Add(
                new BotMessage(
                    DateTime.Now.ToLongTimeString(), messageText, msg.Chat.FirstName, msg.Chat.Id));
            });
        }

        /// <summary>
        /// Прослушивание изменений от бота
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateListener(object sender, UpdateEventArgs e)
        {
            switch (e.Update.Type)
            {
                case UpdateType.Unknown:
                    break;
                case UpdateType.Message:
                    ResponseOnMessage(e.Update);
                    break;
                case UpdateType.InlineQuery:
                    break;
                case UpdateType.ChosenInlineResult:
                    break;
                case UpdateType.CallbackQuery:
                    ResponseOnCallbackQerry(e.Update);
                    break;
                case UpdateType.EditedMessage:
                    break;
                case UpdateType.ChannelPost:
                    break;
                case UpdateType.EditedChannelPost:
                    break;
                case UpdateType.ShippingQuery:
                    break;
                case UpdateType.PreCheckoutQuery:
                    break;
                case UpdateType.Poll:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Ответ на сообщение
        /// </summary>
        /// <param name="e"></param>
        private void ResponseOnMessage(Update update)
        {
            string text = $"{DateTime.Now.ToLongTimeString()} | Type: {update.Type.ToString()} | Text: {update.Message.Text}";
            Debug.WriteLine(text);

            switch (update.Message.Type)
            {
                case MessageType.Unknown:
                    break;
                case MessageType.Text:
                    switch (update.Message.Text.Split(' ').First())
                    {
                        case "/start":
                            const string start = "/inline - получить инлайн клавиатуру";
                            bot.SendTextMessageAsync(
                                update.Message.Chat.Id,
                                start,
                                replyMarkup: new ReplyKeyboardRemove());
                            break;
                        case "/inline":
                            bot.SendTextMessageAsync(
                                update.Message.Chat.Id,
                                "Основное меню",
                                replyMarkup: StartKeyboard());
                            break;
                        default:
                            MessageLogger(update.Message);
                            break;
                    }
                    break;
                case MessageType.Photo:
                    bot.SendTextMessageAsync(update.Message.Chat.Id, text);

                    //TODO: Сохраняет и уменьшеное изображение и оригинал, надо как-то обрезать
                    var photos = update.Message.Photo;
                    foreach (var photo in photos)
                    {
                        DownloadAsync(photo.FileId, Guid.NewGuid().ToString());
                    }

                    break;
                case MessageType.Audio:
                    bot.SendTextMessageAsync(update.Message.Chat.Id, text);
                    break;
                case MessageType.Video:
                    bot.SendTextMessageAsync(update.Message.Chat.Id, text);
                    break;
                case MessageType.Voice:
                    bot.SendTextMessageAsync(update.Message.Chat.Id, text);
                    break;
                case MessageType.Document:
                    bot.SendTextMessageAsync(update.Message.Chat.Id, text);
                    DownloadAsync(update.Message.Document.FileId, update.Message.Document.FileName);

                    break;
                case MessageType.Sticker:
                    bot.SendTextMessageAsync(update.Message.Chat.Id, text);
                    break;
                case MessageType.Location:
                    break;
                case MessageType.Contact:
                    break;
                case MessageType.Venue:
                    break;
                case MessageType.Game:
                    break;
                case MessageType.VideoNote:
                    break;
                case MessageType.Invoice:
                    break;
                case MessageType.SuccessfulPayment:
                    break;
                case MessageType.WebsiteConnected:
                    break;
                case MessageType.ChatMembersAdded:
                    break;
                case MessageType.ChatMemberLeft:
                    break;
                case MessageType.ChatTitleChanged:
                    break;
                case MessageType.ChatPhotoChanged:
                    break;
                case MessageType.MessagePinned:
                    break;
                case MessageType.ChatPhotoDeleted:
                    break;
                case MessageType.GroupCreated:
                    break;
                case MessageType.SupergroupCreated:
                    break;
                case MessageType.ChannelCreated:
                    break;
                case MessageType.MigratedToSupergroup:
                    break;
                case MessageType.MigratedFromGroup:
                    break;
                //case MessageType.Animation:
                //    break;
                case MessageType.Poll:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Ответ на нажатие по кнопке инлайн клавиатуры
        /// </summary>
        /// <param name="e"></param>
        private void ResponseOnCallbackQerry(Update update)
        {
            string text = $"{DateTime.Now.ToLongTimeString()} | Type: {update.Type.ToString()} | Data: {update.CallbackQuery.Data}";
            Console.WriteLine(text);

            var callbackQuery = update.CallbackQuery;

            if (callbackQuery.Data == "0")
            {
                bot.EditMessageTextAsync(
                    callbackQuery.Message.Chat.Id,
                    callbackQuery.Message.MessageId,
                    "Основное меню",
                    replyMarkup: StartKeyboard());
            }
            else
            {
                Int32.TryParse(callbackQuery.Data, out int id);

                var row = BotButtons.Where(x => x.ParentId == id).OrderBy(x => x.Row).ThenBy(x => x.Column).GroupBy(x => x.Row).ToList();

                var inlineKeyboard = new List<List<InlineKeyboardButton>>();
                foreach (var item in row)
                {
                    var listRowButtons = new List<InlineKeyboardButton>();
                    foreach (var i in item)
                    {
                        listRowButtons.Add(InlineKeyboardButton.WithCallbackData($"{i.ButtonName}", i.Id.ToString()));
                    }
                    inlineKeyboard.Add(listRowButtons);
                }

                //Кнопка возврата в предыдущее меню
                //TODO: Сделать проверку parentId на NULL, необходимо для случаев когда кнопки были обновлены через админку, а в телеграмме осталось старое инлайн меню.
                //Пока что простой костыль
                int parentId = 0;
                try
                {
                    parentId = BotButtons.Where(x => x.Id == id).First().ParentId;
                }
                catch (Exception)
                {
                    return;
                }



                //TODO: Так же возможно нужно пересмотреть выдачу идшников для кнопок, например придумать какую-нибудь последовательность. Необходимо для того что бы если в админке изменили кнопки и не провалиться в непонятно какое меню.
                BotButton backButton;

                if (parentId != 0)
                {
                    backButton = BotButtons.Where(x => x.Id == parentId).First();
                }
                else
                {
                    backButton = new BotButton { Id = 0, ParentId = 0, ButtonName = "Оснвоное меню" };
                }

                inlineKeyboard.Add(new List<InlineKeyboardButton>()
                {
                    InlineKeyboardButton.WithCallbackData($"<< Назад в {backButton.ButtonName}", backButton.Id.ToString())
                });

                bot.EditMessageTextAsync(
                    callbackQuery.Message.Chat.Id,
                    callbackQuery.Message.MessageId,
                    BotButtons.Where(x => x.Id == id).First().ButtonName,
                    replyMarkup: new InlineKeyboardMarkup(inlineKeyboard));
            }
        }

        /// <summary>
        /// Стартовая клавиатура
        /// </summary>
        /// <returns>Возвращает стартовую клавиатуру</returns>
        private InlineKeyboardMarkup StartKeyboard()
        {
            //var row = buttons.Where(x => x.ParentId == 0).OrderBy(x => x.Row).ThenBy(x => x.Column).GroupBy(x => x.Row).ToList();
            var row = BotButtons.Where(x => x.ParentId == 0).OrderBy(x => x.Row).ThenBy(x => x.Column).GroupBy(x => x.Row).ToList();
            var inlineKeyboard = new List<List<InlineKeyboardButton>>();
            foreach (var item in row)
            {
                var listRowButtons = new List<InlineKeyboardButton>();
                foreach (var i in item)
                {
                    listRowButtons.Add(InlineKeyboardButton.WithCallbackData($"{i.ButtonName}", i.Id.ToString()));
                }
                inlineKeyboard.Add(listRowButtons);
            }

            return new InlineKeyboardMarkup(inlineKeyboard);
        }

        /// <summary>
        /// Сохранение документа полученного ботом в сообщение
        /// </summary>
        /// <param name="bot"></param>
        /// <param name="fileId">Идентификатор файла</param>
        /// <param name="path"></param>
        private async void DownloadAsync(string fileId, string path)
        {
            var file = await bot.GetFileAsync(fileId);
            using (FileStream fs = new FileStream($"_{path}", FileMode.Create))
            {
                Console.WriteLine(file.GetType().Name);
                await bot.DownloadFileAsync(file.FilePath, fs);
            }
        }

        /// <summary>
        /// Поучение свободного идентификатора
        /// </summary>
        /// <returns></returns>
        private int GettId()
        {
            if (BotButtons.Count != 0)
            {
                int[] number = BotButtons.Select(x => x.Id).ToArray();
                int[] missingNumbers = Enumerable.Range(number[0], number[number.Length - 1]).Except(number).ToArray();
                return missingNumbers.Length == 0 ? number.Max() + 1 : missingNumbers.FirstOrDefault();
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// Запуск бота
        /// </summary>
        public void Start()
        {
            //Проверяем доступность API (надо бы поправить, при недоступности валится в ошибку)
            if (bot.TestApiAsync().Result)
            {
                Console.WriteLine("API доступен.");
                bot.OnUpdate += UpdateListener;
                bot.StartReceiving();
                Console.WriteLine($"Token: {token}");
            }
            else
            {
                Console.WriteLine("API не доступен." +
                    "\nПроверьте интернет соединение." +
                    "\nИли возможно проблема с блокировкой.");
            }
        }

        /// <summary>
        /// Добавление кнопки
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="parentId"></param>
        /// <param name="buttonName"></param>
        /// <param name="content"></param>
        public void AddBotButton(int row, int column, int parentId, string buttonName, string content)
        {
            var id = GettId();

            BotButton botButton = new BotButton
            {
                Id = id,
                Row = row,
                Column = column,
                ParentId = parentId,
                ButtonName = buttonName,
                Content = content
            };

            BotButtons.Add(botButton);

            string json = JsonConvert.SerializeObject(BotButtons);
            System.IO.File.WriteAllText("buttons.json", json);
        }

        /// <summary>
        /// Удлание кнопки и всех дочерних кнопок
        /// </summary>
        /// <param name="botButton"></param>
        public void DeleteBotButton(BotButton botButton)
        {
            //Рекрсивно удаляем все вложенные кнопки

            var childBotButtons = BotButtons.Where(x => x.ParentId == botButton.Id).ToList();       
            foreach (var childButton in childBotButtons)
            {
                DeleteBotButton(childButton);
            }

            BotButtons.Remove(botButton);
            Debug.WriteLine($"Удалена кнопка с id: {botButton.Id} и именем: {botButton.ButtonName}");

            string json = JsonConvert.SerializeObject(BotButtons);
            System.IO.File.WriteAllText("buttons.json", json);
        }
    }
}
