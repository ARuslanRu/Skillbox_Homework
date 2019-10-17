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

namespace Homework_09
{
    class Helpers
    {
        static InlineKeyboardMarkup Keyboard(string id)
        {
            InlineKeyboardMarkup keyboard;

            if (id == "0")
            {
                keyboard = new InlineKeyboardMarkup(
                new[]{
                    InlineKeyboardButton.WithCallbackData("Фильмы", "1"),
                    InlineKeyboardButton.WithCallbackData("Мультфильмы", "2")
                });
                return keyboard;
            }

            //временно
            keyboard = new InlineKeyboardMarkup(
            new[]{
                InlineKeyboardButton.WithCallbackData("Фантастика", "3"),
                InlineKeyboardButton.WithCallbackData("Ужасы", "4"),
                InlineKeyboardButton.WithCallbackData("Триллер", "5"),
                InlineKeyboardButton.WithCallbackData("Фентези", "6")
            });

            return keyboard;
        }

        public static void AnswerOnCallbackQerry(TelegramBotClient bot, UpdateEventArgs e)
        {
            string text = $"{DateTime.Now.ToLongTimeString()} | Type: {e.Update.Type.ToString()} | Data: {e.Update.CallbackQuery.Data}";
            Console.WriteLine(text);

            //bot.SendTextMessageAsync(e.Update.CallbackQuery.Message.Chat.Id, text, replyMarkup: Keyboard(e.Update.CallbackQuery.Data));

            bot.EditMessageTextAsync(e.Update.CallbackQuery.Message.Chat.Id, e.Update.CallbackQuery.Message.MessageId, text, replyMarkup: Keyboard(e.Update.CallbackQuery.Data));
        }

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
    }
}
