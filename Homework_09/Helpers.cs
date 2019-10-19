using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;
using Homework_09.Model;

namespace Homework_09
{
    class Helpers
    {
        #region Private Methods

        /// <summary>
        /// Формирование инлайн клавиатуры
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        static InlineKeyboardMarkup Keyboard(string data)
        {
            if (data == "0")
            {
                var row = Repository.getInstance().Buttons.Where(x => x.ParentId == 0).OrderBy(x => x.Row).ThenBy(x => x.Column).GroupBy(x => x.Row).ToList();

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
            else
            {
                Int32.TryParse(data, out int id);

                var row = Repository.getInstance().Buttons.Where(x => x.ParentId == id).OrderBy(x => x.Row).ThenBy(x => x.Column).GroupBy(x => x.Row).ToList();

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

                var parentId = Repository.getInstance().Buttons.Where(x => x.Id == id).First().ParentId;

                BotButton backButton = new BotButton();

                //Проверка для возврата в основное меню
                if (parentId != 0)
                {
                    backButton = Repository.getInstance().Buttons.Where(x => x.Id == parentId).First();
                }
                else
                {
                    backButton = new BotButton { Id = 0, ParentId = 0, ButtonName = "Оснвоное меню" };
                }

                inlineKeyboard.Add(new List<InlineKeyboardButton>()
                {
                    InlineKeyboardButton.WithCallbackData($"<< Назад в {backButton.ButtonName}", backButton.Id.ToString())
                });

                return new InlineKeyboardMarkup(inlineKeyboard);
            }
        }

        /// <summary>
        /// Сохранение документа полученного ботом в сообщение
        /// </summary>
        /// <param name="fileId">Идентификатор файла</param>
        /// <param name="path"></param>
        //static async void DownloadAsync(string fileId, string path)
        //{
        //    var file = await bot.GetFileAsync(fileId);
        //    using (FileStream fs = new FileStream($"_{path}", FileMode.Create))
        //    {
        //        await bot.DownloadFileAsync(file.FilePath, fs);
        //    }
        //}

        #endregion

        #region Public Methods


        /// <summary>
        /// Ответ на нажатие по инлайн клавиатуре
        /// </summary>
        /// <param name="bot"></param>
        /// <param name="e"></param>
        public static void AnswerOnCallbackQerry(TelegramBotClient bot, UpdateEventArgs e)
        {
            string text = $"{DateTime.Now.ToLongTimeString()} | Type: {e.Update.Type.ToString()} | Data: {e.Update.CallbackQuery.Data}";
            Console.WriteLine(text);

            //bot.SendTextMessageAsync(e.Update.CallbackQuery.Message.Chat.Id, text, replyMarkup: Keyboard(e.Update.CallbackQuery.Data));

            bot.EditMessageTextAsync(e.Update.CallbackQuery.Message.Chat.Id, e.Update.CallbackQuery.Message.MessageId, text, replyMarkup: Keyboard(e.Update.CallbackQuery.Data));
        }

        /// <summary>
        /// Ответ на текстовое сообщение
        /// </summary>
        /// <param name="bot"></param>
        /// <param name="e"></param>
        public static void AnswerOnMessage(TelegramBotClient bot, UpdateEventArgs e)
        {
            string text = $"{DateTime.Now.ToLongTimeString()} | Type: {e.Update.Type.ToString()} | Text: {e.Update.Message.Text}";
            Console.WriteLine(text);

            switch (e.Update.Message.Type)
            {
                case MessageType.Unknown:
                    break;
                case MessageType.Text:
                    bot.SendTextMessageAsync(e.Update.Message.Chat.Id, text, replyMarkup: Keyboard("0"));
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
        }

        #endregion
    }
}
