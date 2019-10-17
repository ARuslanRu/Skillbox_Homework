using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;

namespace Homework_09
{
    class Program
    {

        static TelegramBotClient bot;
        static void Main(string[] args)
        {
            #region Задание

            // Создать бота, позволяющего принимать разные типы файлов, 
            // *Научить бота отправлять выбранный файл в ответ
            // 
            // https://data.mos.ru/
            // https://apidata.mos.ru/
            // 
            // https://vk.com/dev
            // https://vk.com/dev/manuals
            //
            // https://dev.twitch.tv/
            // https://discordapp.com/developers/docs/intro
            // https://discordapp.com/developers/applications/
            // https://discordapp.com/verification


            //Плюс получение фото и аудио
            //

            #endregion

            #region Прокси
            //Подходящий прокси найти не удалось
            //https://hidemyna.me/ru/proxy-list/?maxtime=250#list

            //var proxy = new WebProxy()
            //{
            //    Address = new Uri($"http://109.86.229.189:8080"),
            //    UseDefaultCredentials = false
            //};

            //var httpClienHandler = new HttpClientHandler()
            //{
            //    Proxy = proxy
            //};

            //HttpClient hc = new HttpClient(httpClienHandler);
            #endregion

            #region Старт Бота

            string token = File.ReadAllText("token");

            bot = new TelegramBotClient(token);

            //при использовании прокси
            //bot = new TelegramBotClient(token, hc);

            if (bot.TestApiAsync().Result)
            {
                Console.WriteLine("Бот запущен.");
            }

            Console.WriteLine($"Token: {token}");

            //bot.OnMessage += MessageListener; // подписываемся на событие получения сообщения
            bot.OnUpdate += UpdateListener; // подписываемся на получение любых обновлений
            
            bot.StartReceiving();

            Console.ReadKey();
            #endregion
        }


        private static void UpdateListener(object sender, UpdateEventArgs e)
        {

            switch (e.Update.Type)
            {
                case UpdateType.Unknown:
                    break;
                case UpdateType.Message:
                    string text = $"OnUpdate: {DateTime.Now.ToLongTimeString()} | Type: {e.Update.Message.Type.ToString()}";
                    Console.WriteLine(text);

                    InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(new[]
                    {
                        new[] { InlineKeyboardButton.WithUrl("site", "https://google.com") }, //первая строка
                        new[] { InlineKeyboardButton.WithCallbackData("menu", "menu") } //вторая строка
                    });


                    switch (e.Update.Message.Type)
                    {
                        case MessageType.Unknown:
                            break;
                        case MessageType.Text:
                            //bot.SendTextMessageAsync(e.Update.Message.Chat.Id, text);
                            bot.SendTextMessageAsync(e.Update.Message.Chat.Id, text, replyMarkup: keyboard);
                            break;
                        case MessageType.Photo:
                            bot.SendTextMessageAsync(e.Update.Message.Chat.Id, text);
                            break;
                        case MessageType.Audio:
                            bot.SendTextMessageAsync(e.Update.Message.Chat.Id, text);
                            break;
                        case MessageType.Video:
                            bot.SendTextMessageAsync(e.Update.Message.Chat.Id, text);
                            break;
                        case MessageType.Voice:
                            bot.SendTextMessageAsync(e.Update.Message.Chat.Id, text);
                            break;
                        case MessageType.Document:
                            bot.SendTextMessageAsync(e.Update.Message.Chat.Id, text);
                            break;
                        case MessageType.Sticker:
                            bot.SendTextMessageAsync(e.Update.Message.Chat.Id, text);
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
                        case MessageType.Animation:
                            break;
                        case MessageType.Poll:
                            break;
                        default:
                            break;
                    }



                    break;
                case UpdateType.InlineQuery:
                    break;
                case UpdateType.ChosenInlineResult:
                    break;
                case UpdateType.CallbackQuery:
                    Helpers.AnswerCallbackQerry(bot, e);
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


        private static void MessageListener(object sender, MessageEventArgs e)
        {
            string text = $"OnMessage: {DateTime.Now.ToLongTimeString()}: {e.Message.Type.ToString()} {e.Message.Chat.FirstName} {e.Message.Chat.Id} {e.Message.Text}";

            Console.WriteLine(text);

            if (e.Message.Type == MessageType.Document)
            {
                Console.WriteLine(e.Message.Document.FileId);
                Console.WriteLine(e.Message.Document.FileName);
                Console.WriteLine(e.Message.Document.FileSize);

                DownloadAsync(e.Message.Document.FileId, e.Message.Document.FileName);
            }

            if (e.Message.Text != null)
            {
                //отправка ответного сообщения ботом
                bot.SendTextMessageAsync(e.Message.Chat.Id, text);
            }
        }

        /// <summary>
        /// Сохранение документа полученного ботом в сообщение
        /// </summary>
        /// <param name="fileId">Идентификатор файла</param>
        /// <param name="path"></param>
        static async void DownloadAsync(string fileId, string path)
        {
            var file = await bot.GetFileAsync(fileId);

            using (FileStream fs = new FileStream($"_{path}", FileMode.Create))
            {
                await bot.DownloadFileAsync(file.FilePath, fs);
            }
        }
    }
}
