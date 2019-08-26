using System;

namespace Homework_01
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создание базы данных из 20 сотрудников
            Repository repository = new Repository(20);

            // Печать в консоль всех сотрудников
            repository.Print("База данных до преобразования");

            // Увольнение всех работников с именем "Агата"
            repository.DeleteWorkerByName("Агата");

            // Печать в консоль сотрудников, которые не попали под увольнение
            repository.Print("База данных после первого преобразования");

            // Увольнение всех работников с именем "Аделина"
            repository.DeleteWorkerByName("Аделина");

            // Печать в консоль сотрудников, которые не попали под увольнение
            repository.Print("База данных после второго преобразования");


            #region Домашнее задание

            // Уровень сложности: просто
            // Задание 1. Переделать программу так, чтобы до первой волны увольнени в отделе было не более 20 сотрудников

            // Уровень сложности: средняя сложность
            // * Задание 2. Создать отдел из 40 сотрудников и реализовать несколько увольнений, по результатам
            //              которых в отделе болжно остаться не более 30 работников

            // Уровень сложности: сложно
            // ** Задание 3. Создать отдел из 50 сотрудников и реализовать увольнение работников
            //               чья зарплата превышает 30000руб


            #endregion

            #region Задание 2
            // Создание базы данных из 40 сотрудников
            Repository repositoryTask2 = new Repository(40);

            // Печать в консоль всех сотрудников
            repositoryTask2.Print("Задание 2\n" +
                                    "База данных до преобразования");

            //Берем имя случайного сотрудника и удаляем всех с этим именем
            while (repositoryTask2.Workers.Count > 30)
            {   
                //получаем случайное имя
                var randomName = repositoryTask2.Workers[new Random().Next(repositoryTask2.Workers.Count)].FirstName;
                //увольняем по полученному имени
                repositoryTask2.DeleteWorkerByName(randomName);
                // Печать в консоль оставшихся сотрудников
                repositoryTask2.Print("База данных после увольнения.");
            }

            //Или можно еще вот так, но кажется так увольнять напрямую будет неправильно.
            //Вероятно в классе Repository свойство Workers необходимо сделать доступным только для чтения, но непойму как.
            Repository repositoryTask2_2 = new Repository(40);
            repositoryTask2_2.Print("Задание 2. Вариант 2.\n" +
                                    "База данных до преобразования");
            while (repositoryTask2_2.Workers.Count > 30)
            {
                repositoryTask2_2.Workers.RemoveAt(new Random().Next(repositoryTask2_2.Workers.Count));
            }
            repositoryTask2_2.Print("База данных после увольнений.");

            #endregion

            #region Задание 3
            // Создание базы данных из 40 сотрудников
            Repository repositoryTask3 = new Repository(50);

            // Печать в консоль всех сотрудников
            repositoryTask3.Print("Задание 3\n" +
                                    "База данных до преобразования");

            //Увольняем всех у кого зарплата больше 30000
            repositoryTask3.DeleteWorkerBySalary(30000);

            // Печать в консоль оставшихся сотрудников
            repositoryTask3.Print("База данных после увольнения работников\n" +
                                    "чья зарплата превышает 30000руб.");

            #endregion

            Console.ReadKey();

        }
    }
}
