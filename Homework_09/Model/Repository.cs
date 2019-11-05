using System.Collections.Generic;
using System.Linq;

namespace Homework_09.Model
{
    class Repository
    {
        private static Repository instance;
        public static List<BotButton> Buttons { get; private set; }
        private Repository()
        {
            Buttons = new List<BotButton>();

            Buttons.Add(new BotButton { Id = 1, ParentId = 0, Row = 1, Column = 1, ButtonName = "Игры" });
            Buttons.Add(new BotButton { Id = 2, ParentId = 0, Row = 2, Column = 1, ButtonName = "Рецепты" });
            Buttons.Add(new BotButton { Id = 3, ParentId = 0, Row = 3, Column = 1, ButtonName = "Литература" });

            Buttons.Add(new BotButton { Id = 4, ParentId = 1, Row = 1, Column = 1, ButtonName = "Аркады" });
            Buttons.Add(new BotButton { Id = 5, ParentId = 1, Row = 1, Column = 2, ButtonName = "Стратегии" });
            Buttons.Add(new BotButton { Id = 6, ParentId = 1, Row = 2, Column = 1, ButtonName = "Симуляторы" });
            Buttons.Add(new BotButton { Id = 7, ParentId = 1, Row = 2, Column = 2, ButtonName = "РПГ" });
            Buttons.Add(new BotButton { Id = 8, ParentId = 1, Row = 2, Column = 3, ButtonName = "Экшн" });

            Buttons.Add(new BotButton { Id = 9, ParentId = 2, Row = 3, Column = 1, ButtonName = "Рецепт 1" });
            Buttons.Add(new BotButton { Id = 10, ParentId = 2, Row = 3, Column = 2, ButtonName = "Рецепт 2" });

            Buttons.Add(new BotButton { Id = 11, ParentId = 4, Row = 1, Column = 1, ButtonName = "Аркада 1" });
            Buttons.Add(new BotButton { Id = 12, ParentId = 4, Row = 1, Column = 2, ButtonName = "Аркада 2" });
            Buttons.Add(new BotButton { Id = 13, ParentId = 4, Row = 2, Column = 1, ButtonName = "Аркада 3" });

            Buttons.Add(new BotButton { Id = 14, ParentId = 5, Row = 1, Column = 1, ButtonName = "Стратегия 1" });
            Buttons.Add(new BotButton { Id = 15, ParentId = 5, Row = 1, Column = 2, ButtonName = "Стратегия 2" });
            Buttons.Add(new BotButton { Id = 16, ParentId = 5, Row = 2, Column = 1, ButtonName = "Стратегия 3" });

            Buttons.Add(new BotButton { Id = 17, ParentId = 3, Row = 1, Column = 1, ButtonName = "Фантастика" });
            Buttons.Add(new BotButton { Id = 18, ParentId = 3, Row = 2, Column = 1, ButtonName = "Комедия" });
            Buttons.Add(new BotButton { Id = 19, ParentId = 3, Row = 3, Column = 1, ButtonName = "Ужасы" });
            Buttons.Add(new BotButton { Id = 20, ParentId = 3, Row = 4, Column = 1, ButtonName = "Фентези" });
        }

        public static Repository getInstance()
        {
            if (instance == null)
                instance = new Repository();
            return instance;
        }

        public static void AddBotButton(int row, int column, int parentId, string buttonName, string content)
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

            Buttons.Add(botButton);
        }

        /// <summary>
        /// Поучение свободного идентификатора
        /// </summary>
        /// <returns></returns>
        private static int GettId()
        {
            if (Buttons.Count != 0)
            {
                int[] number = Buttons.Select(x => x.Id).ToArray();
                int[] missingNumbers = Enumerable.Range(number[0], number[number.Length - 1]).Except(number).ToArray();
                return missingNumbers.Length == 0 ? number.Max() + 1 : missingNumbers.FirstOrDefault();
            }
            else
            {
               return 1;
            }
        }
    }
}
