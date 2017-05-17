using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NewBot
{
    class Program
    {
        static void Main(string[] args)
        {
            TestBot TestBot = new TestBot();
            TestBot.testApiAsync();
            Console.ReadLine();
        }
    }

    class TestBot
    {
        private string token = "378511170:AAHdK09xl1AVTdloW-wkwXT8_GtPjh5Lff4";
        static Telegram.Bot.Api Bot;

        public async void testApiAsync()
        {
            try
            {
                Bot = new Telegram.Bot.Api(token);
                var me = await Bot.GetMeAsync();
                Console.WriteLine("Hello," + me.FirstName);

                Thread newThread = new Thread(TestBot.ReceivedMessage);
                newThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hello, " + ex.Message);
            }
        }
        private static async void ReceivedMessage()
        {
            var lastMsgId = 0;
            while(true)
            {
                var msg = await Bot.GetUpdatesAsync();
                if (msg.Length > 0)
                {
                    var last = msg[msg.Length - 1];
                    if (lastMsgId != last.Id)
                    {
                        lastMsgId = last.Id;
                        Console.WriteLine(last.Message.Text);
                        if (last.Message.Text.Contains("Привет"))
                        {
                            Bot.SendTextMessageAsync(125957773, "Здравствуй");
                       
                        }
                        else if (last.Message.Text.Contains("Пока"))
                        {
                            Bot.SendTextMessageAsync(125957773, "Удачи");

                        }
                        else
                        {
                            Bot.SendTextMessageAsync(125957773, "Прости, но у нас нет столько времени");
                            Bot.SendPhotoAsync(125957773, "https://otvet.imgsmail.ru/download/u_42a37c20299e1b2d138bdb67785b92bc_800.png");
                        }
                    }
                }
                Thread.Sleep(200);
            }
        }
    }
}

       
