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

            #endregion

            #region Прокси
            //Подходящий прокси найти не удалось
            //https://hidemyna.me/ru/proxy-list/?maxtime=250#list

            var proxy = new WebProxy()
            {
                Address = new Uri($"http://109.86.229.189:8080"),
                UseDefaultCredentials = false
            };

            var httpClienHandler = new HttpClientHandler()
            {
                Proxy = proxy
            };

            HttpClient hc = new HttpClient(httpClienHandler);
            #endregion

            #region Старт Бота

            string token = File.ReadAllText("token");

            //при использовании прокси
            //bot = new TelegramBotClient(token, hc);

            bot = new TelegramBotClient(token);


            if (bot.TestApiAsync().Result)
            {
                Console.WriteLine("Бот запущен.");
            }

            Console.WriteLine($"Token: {token}");

            bot.OnMessage += MessageListener;
            bot.StartReceiving();
            
            Console.ReadKey();
            #endregion
        }

        private static void MessageListener(object sender, MessageEventArgs e)
        {
            string text = $"{DateTime.Now.ToLongTimeString()}: {e.Message.Type.ToString()} {e.Message.Chat.FirstName} {e.Message.Chat.Id} {e.Message.Text}";

            Console.WriteLine(text);
            if (e.Message.Type == MessageType.Document)
            {
                Console.WriteLine(e.Message.Document.FileId);
                Console.WriteLine(e.Message.Document.FileName);
                Console.WriteLine(e.Message.Document.FileSize);

                DownloadAsync(e.Message.Document.FileId, e.Message.Document.FileName);
            }
        }


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
