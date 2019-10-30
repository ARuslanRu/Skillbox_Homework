using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_10.Model
{
    public class BotButton
    {
        public int Id { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int ParentId { get; set; }
        public string ButtonName { get; set; }
        public string Content { get; set; }
    }
}
