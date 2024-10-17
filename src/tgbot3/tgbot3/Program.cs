// See https://aka.ms/new-console-template for more information
using Microsoft.VisualBasic;
using System.Reflection.Metadata;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using static System.Runtime.InteropServices.JavaScript.JSType;

using var cts = new CancellationTokenSource();
var bot = new TelegramBotClient("7638692637:AAHZEXWSi8AAcD_PQV6GTA5VhTqi06bhBYs", cancellationToken: cts.Token);
var me = await bot.GetMeAsync();
bot.OnError += OnError;
bot.OnMessage += OnMessage;
bot.OnUpdate += OnUpdate;

Console.WriteLine($"@{me.Username} is running... Press Enter to terminate");
Console.ReadLine();
cts.Cancel();

async Task OnError(Exception exception, HandleErrorSource source)
{
    Console.WriteLine(exception);
}

async Task OnMessage(Message msg, UpdateType type)
{
    if (msg.Text == "/start")
    {
        await bot.SendTextMessageAsync(msg.Chat, "Пррривет! Хочешь, подскажу, как стать богатым? Не обманываю, честно!",
        replyMarkup: new InlineKeyboardMarkup(new[]
        {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("Привет! Что мне делать, чтобы стать богатым?", "Привет"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData("Ничего не получилось", "Помощь"),
            InlineKeyboardButton.WithCallbackData("У меня вырос базилик", "Картинка"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData("У меня все получилось!", "Стикер"),
            InlineKeyboardButton.WithCallbackData("А я точно так стану богатым?", "Видео"),
        }
        }));

        if (msg.Text == "Проверка")
        {
            await bot.SendTextMessageAsync(msg.Chat, "Проверка бота: работа корректна");
        }
    }
}

async Task OnUpdate(Update update)
{
    if (update.Type == UpdateType.CallbackQuery)
    {
        var callbackQuery = update.CallbackQuery;

        switch (callbackQuery.Data)
        {
            case "Привет":
                await bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Следуйте следующим инструкциям.\n1. Посадите любые семена какие только найдете в горшок.\n2. Проверяйте свой гороскоп ежедневно в течении трех дней.");
                break;

            case "Помощь":
                await bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "А точно три дня прошло-то?");
                break;

            case "Картинка":
                await bot.SendPhotoAsync(callbackQuery.Message.Chat.Id, "https://images.gastronom.ru/OjPWlF_4W_qScOR__RXCccdGED7FIIUydoc-5FyKAsI/pr:product-cover-image/g:ce/rs:auto:0:0:0/L2Ntcy9hbGwtaW1hZ2VzLzk5NGMwOTczLWQ4NmQtNGFkZi1hY2UzLWYwYjIyMWJiZWU2Zi5qcGVn.webp");
                break;

            case "Стикер":
                await bot.SendStickerAsync(callbackQuery.Message.Chat.Id, "https://telegrambots.github.io/book/docs/sticker-dali.webp");
                break;

            case "Видео":
                await bot.SendVideoAsync(callbackQuery.Message.Chat.Id, "https://i.gifer.com/FU8l.mp4");
                break;
        }

        await bot.AnswerCallbackQueryAsync(callbackQuery.Id);
    }
}