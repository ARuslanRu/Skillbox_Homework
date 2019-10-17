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


        public static void AnswerCallbackQerry(TelegramBotClient bot, UpdateEventArgs e)
        {
            bot.SendTextMessageAsync(e.Update.CallbackQuery.Message.Chat.Id, "Заглушка");
        }
    }
}
