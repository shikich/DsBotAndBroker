using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using DSharpPlus.CommandsNext;
using DiscordBot;
using DiscordBot.Commands;
using System.Collections.Generic;
using System.Linq;
using DSharpPlus;
using System.Threading;

StatusCommand sc = new StatusCommand();
List<CommandContext> ctxes = null;
DiscordClient dc = null;
string[] messArr = null;
                                                   //change path if it's need to
var factory = new ConnectionFactory() { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channelR = connection.CreateModel();

channelR.ExchangeDeclare(exchange: "pubsub", type: ExchangeType.Direct);
channelR.QueueBind(queue: "first_queue", exchange: "pubsub", routingKey: "");//check #65 line
channelR.QueuePurge("first_queue");

var consumer = new EventingBasicConsumer(channelR);
consumer.Received += async (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"FirstConsumer - Recieved new message: {message}\n");
    ctxes = StatusCommand._ctxes;
    AmqpEditor ae = new AmqpEditor(message);
    Sender s = new Sender();

    dc = Bot.Client;
    messArr = ae.GetArrayMess();
    foreach (var chats in dc.Guilds.ToArray().Select(x => x.Value.Channels.Values)) //list of text channels
    {   // remove invalid message and exit method
        if (messArr[0] != chats.Where(x => x.Name == messArr[0]).Select(x => x.Name).FirstOrDefault()) //check existing channel for messaging
        {
            channelR.BasicAck(ea.DeliveryTag, false);
            Console.WriteLine("-Message removed\n"); //if you want monitor losting messages
            return;
        }     
        foreach(var chat in chats)
        {
            if (chat.Name == messArr[0])
            { 
                Thread.Sleep(3000);// escape stacking same types of messages and blocking message by limmit of message/second
                await s.SendAsync(chat, messArr);// send message to one text channel
                channelR.BasicAck(ea.DeliveryTag, false);
                break;
            }
        }
    }
};
channelR.BasicConsume(queue: "first_queue", autoAck: false, consumer: consumer);
Console.WriteLine("Consuming");
DiscordBot.Program.Main(null);

//  work area:
//  exchange - pubsub
//  queue - first_queue - add manually at rabbitmq WebUI on host
//  routingKey -