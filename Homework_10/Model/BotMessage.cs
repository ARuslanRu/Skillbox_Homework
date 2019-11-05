using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_10.Model
{
    class BotMessage
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public long ChatId { get; set; }
        public string Msg { get; set; }
        public string FirstName { get; set; }

        public BotMessage(string time, string msg, string firstName, long chatId, int id)
        {
            Time = time;
            Msg = msg;
            FirstName = firstName;
            ChatId = chatId;
            Id = id;
        }
    }
}
