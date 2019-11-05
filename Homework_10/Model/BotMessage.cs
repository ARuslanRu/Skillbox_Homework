using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_10.Model
{
    class BotMessage
    {
        public string Time { get; set; }

        public long Id { get; set; }

        public string Msg { get; set; }

        public string FirstName { get; set; }

        public BotMessage(string time, string msg, string firstName, long id)
        {
            Time = time;
            Msg = msg;
            FirstName = firstName;
            Id = id;
        }
    }
}
