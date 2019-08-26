using System.Linq;

namespace Homework_Theme_03
{
    /// <summary>
    /// Класс логики игры компьютера
    /// </summary>
    public class Computer
    {
        /// <summary>
        /// Вычисляет число которым ходит компьютер
        /// </summary>
        /// <param name="gameNumber">Загаданное число</param>
        /// <param name="userTryNumbers">Доступные числа из набора</param>
        /// <returns>Возвращает число котрым ходит компьютер</returns>
        public static int Start(int gameNumber, int[] userTryNumbers)
        {
            int userTry = 1; //какой будет ход

            //если текущее загаданное в два раза меньше чем максимальное в наборе, то берем максимальное
            if (gameNumber > userTryNumbers.Max() * 2)
            {
                userTry = userTryNumbers.Max();
                return userTry;
            }

            //если текущее загаданное больше чем максимальное в наборе и меньше или равно максимальному умноженному на два
            if (gameNumber > userTryNumbers.Max() && gameNumber <= userTryNumbers.Max() * 2)
            {
                //перебираем все числа в наборе и выбираем такое максимальное которое не позволит сделать загаданное меньше или равным максимальному в наборе
                int index = userTryNumbers.Length - 1;
                while (true)
                {
                    if (gameNumber > userTryNumbers[index] * 2)
                    {
                        userTry = userTryNumbers[index];
                        break;
                    }
                    else
                    {
                        index--;
                    }

                    if (index == 0)
                    {
                        userTry = userTryNumbers[index];
                        break;
                    }
                }
            }

            //если текущее загаданное меньше или равно максимальному в наборе
            if (gameNumber <= userTryNumbers.Max())
            {
                //если текущее загаданное совпадает с числом из набора то берем его и побеждаем
                if (userTryNumbers.Contains(gameNumber))
                {
                    userTry = gameNumber;
                }
                // если нет то пытаемся подобрать такое которое позволит получить минимальное число пропущенное в наборе (например в наборе [1,2,3,5,7,11] это 4), но только которое можно использовать.
                else
                {
                    int number = 1;
                    for (int i = 0; i < userTryNumbers.Length; i++)
                    {
                        if (userTryNumbers[i] != number && userTryNumbers.Contains(gameNumber - number))
                        {
                            userTry = gameNumber - number;
                            break;
                        }
                        number++;
                    }
                }
            }
            return userTry;
        }
    }
}
