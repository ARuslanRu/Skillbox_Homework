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

namespace Homework_10
{
    class MyBot
    {
        private MainWindow w;
        private TelegramBotClient bot;
        public ObservableCollection<BotButton> botButtons;
        private string token;

        public MyBot(MainWindow w, string pathToken = "token")
        {
            this.botButtons = new ObservableCollection<BotButton>(Repository.getInstance().Buttons);
            this.w = w;
            token = System.IO.File.ReadAllText(pathToken);
            this.bot = new TelegramBotClient(token);

            bot.OnUpdate += UpdateListener;
            bot.StartReceiving();
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
            Console.WriteLine(text);

            switch (update.Message.Type)
            {
                case MessageType.Unknown:
                    break;
                case MessageType.Text:
                    switch (update.Message.Text.Split(' ').First())
                    {
                        case "/inline":
                            bot.SendTextMessageAsync(
                                update.Message.Chat.Id,
                                "Основное меню",
                                replyMarkup: StartKeyboard());
                            break;
                        default:
                            const string usage = "Помощь:" +
                                "\n/inline - получить инлайн клавиатуру";
                            bot.SendTextMessageAsync(
                                update.Message.Chat.Id,
                                usage,
                                replyMarkup: new ReplyKeyboardRemove());
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

                //var row = buttons.Where(x => x.ParentId == id).OrderBy(x => x.Row).ThenBy(x => x.Column).GroupBy(x => x.Row).ToList();
                var row = botButtons.Where(x => x.ParentId == id).OrderBy(x => x.Row).ThenBy(x => x.Column).GroupBy(x => x.Row).ToList();

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
                //var parentId = buttons.Where(x => x.Id == id).First().ParentId;
                var parentId = botButtons.Where(x => x.Id == id).First().ParentId;
                BotButton backButton;

                if (parentId != 0)
                {
                    //backButton = buttons.Where(x => x.Id == parentId).First();
                    backButton = botButtons.Where(x => x.Id == parentId).First();
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
                    //buttons.Where(x => x.Id == id).First().ButtonName,
                    botButtons.Where(x => x.Id == id).First().ButtonName,
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
            var row = botButtons.Where(x => x.ParentId == 0).OrderBy(x => x.Row).ThenBy(x => x.Column).GroupBy(x => x.Row).ToList();
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
    }
}
