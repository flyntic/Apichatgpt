
using ConsoleApiChatGpt;
Console.WriteLine("Привет) Как Вас зовут?");

string? nameUser=Console.ReadLine();

if (!string.IsNullOrEmpty(nameUser))
{

    string? request;
    string? response;
    ApiChatGpt chat=new ApiChatGpt();
    do
    {
        Console.Write($"{nameUser}:");
        request = Console.ReadLine();
        if (string.IsNullOrEmpty(request)) break;

        response = string.Empty;
        Thread thread = new Thread(()=>{ response = chat.GetResult(request).Result; });
        thread.IsBackground= true;
        thread.Start();
        do
        {
            Console.Write($"ChatGPT:.  ");
            Thread.Sleep(500);
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write($"ChatGPT:.  ");
            Thread.Sleep(500);
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write($"ChatGPT:.. ");
            Thread.Sleep(500);
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write($"ChatGPT:...");
            Thread.Sleep(100);
            Console.SetCursorPosition(0, Console.CursorTop);
        } while (thread.IsAlive);

        if (string.IsNullOrEmpty(response))
            Console.WriteLine($"ChatGPT:{chat.Error}");
        else
            Console.WriteLine($"ChatGPT:{chat.GetResult(request).Result}");
    }
    while (true);
}

Console.WriteLine("Хорошего дня)");
Console.ReadLine();
return;
