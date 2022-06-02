using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using System.Collections.Generic;
using DSharpPlus.Entities;

namespace DiscordBot.Commands
{
    public class StatusCommand : BaseCommandModule
    {
        public static List<CommandContext> _ctxes = new List<CommandContext>();

        [Command("ping")]
        [Description("Returns pong - check delay")]
        public async Task Ping(CommandContext ctx)
        {
            var pongEmbed = new DiscordEmbedBuilder
            {
                Title = "Pong ",
                Color = new DiscordColor(0x00CCCC),
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail 
                {
                    Url="https://media.tenor.com/images/33fa75ff5d56400c8aca9f728d8b1418/tenor.gif"
                }          
            };
            await ctx.Channel.SendMessageAsync(embed: pongEmbed).ConfigureAwait(false);
        }
    }
}
