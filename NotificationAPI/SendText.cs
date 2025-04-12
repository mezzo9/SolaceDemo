using IoTShared;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace NotificationAPI;

public class SendText
{
    public void Send(string text)
    {
        Console.WriteLine("-------------  ALERT!!! -----------------");
        Console.WriteLine(text);
        Console.WriteLine("-----------------------------------------");
    }
}