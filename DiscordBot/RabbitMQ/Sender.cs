using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using System.Threading.Tasks;
using System.Linq;
using System;

public class Sender
{
    public async Task<CommandContext> SendAsync(DiscordChannel channelD, string[] messArr)
    {     
        var messageEmbed = new DiscordEmbedBuilder
        {
            Title = "Game: " + messArr[1],
            Color = new DiscordColor(0xFFAD33),
            Description = messArr[3],
            //Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail
            //{
            //    Url="url for image" + messArr[5]
            //},
        };
        messageEmbed.AddField("Details: ", messArr[2]);
        messageEmbed.AddField("Price: ", messArr[4]);
        messageEmbed.Url = "https://theboosteam.com/";      
        Console.WriteLine($" Send message to {channelD.Name} - channel"); //check if it's need to
        await channelD.SendMessageAsync(embed: messageEmbed).ConfigureAwait(false);
        try //pega code for checking roles on discord server
        {   
            await channelD.SendMessageAsync(channelD.Guild.Roles.Values.Where(x => x.Name == messArr[0]).Select(z => z.Mention).First()).ConfigureAwait(false);
        }
        catch
        {
            Console.WriteLine("-This role does not exist");
        }
        return null;
    }
}