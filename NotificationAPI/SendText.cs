using IoTShared;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace NotificationAPI;

public class SendText
{
    /// <summary>
    /// Simulating a Text Message Alert
    /// </summary>
    /// <param name="text">content of text message</param>
    public void Send(string text)
    {
        Console.WriteLine("-------------  ALERT!!! -----------------");
        Console.WriteLine(text);
        Console.WriteLine("-----------------------------------------");
    }
}