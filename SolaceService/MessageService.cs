using System.Text;
using SolaceSystems.Solclient.Messaging;

namespace SolaceService;

public class MessageService
{
    private readonly ISession _session;

    public MessageService()
    {
        _session = GetSession();
    }
    
    private ISession GetSession()
    {
        ContextFactoryProperties cfp = new ContextFactoryProperties()
        {
            SolClientLogLevel = SolLogLevel.Warning
        };
        cfp.LogToConsoleError();
        ContextFactory.Instance.Init(cfp);

        SessionProperties sessionProps = new SessionProperties()
        {
            Host = "tcps://mr-connection-5k08bz06mdg.messaging.solace.cloud:55443",
            VPNName = "solacedemo",
            UserName = "solace-cloud-client",
            // in production code, make sure password is not hard coded. use vault or KMS, or local encryption to retrieve the password 
            Password = "kvbjfldto53tu00mjdm898agei",
            ReconnectRetries = 2,
            SSLValidateCertificate = false,
            // to increase performance
            SendBlocking = false
        };

        var context = ContextFactory.Instance.CreateContext(new ContextProperties(), null);
        var session = context.CreateSession(sessionProps, null, null);
        ReturnCode returnCode = session.Connect();
        if (returnCode == ReturnCode.SOLCLIENT_OK)
        {
            Console.Out.WriteLine("Connected to Solace Cloud"); // connected to the Solace message router
            
        }
        return session;
    }
    
    public void PublishMessage(string msg, string topic)
    {
        // Create the message
        using IMessage message = ContextFactory.Instance.CreateMessage();
        message.Destination = ContextFactory.Instance.CreateTopic(topic);
        
        // Create the message content as a binary attachment
        message.BinaryAttachment = Encoding.ASCII.GetBytes(msg);

        // Publish the message to the topic on the Solace messaging router
        Console.WriteLine("Publishing message...");
        ReturnCode returnCode = _session.Send(message);
        if (returnCode == ReturnCode.SOLCLIENT_OK)
        {
            Console.WriteLine("Done.");
        }
        else
        {
            Console.WriteLine("Publishing failed, return code: {0}", returnCode);
        }
    }
}