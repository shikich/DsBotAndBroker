using System;
using System.Threading.Tasks;

namespace DiscordBot
{
    class Program
    {
        public static string[] Main(string[] args)
        {
            var bot = new Bot();
            bot.RunAsync().GetAwaiter().GetResult();
            return null;
        }
    }
}
