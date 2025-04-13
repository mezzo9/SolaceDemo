using System.Text;
using IoTShared;
using SolaceSystems.Solclient.Messaging;

namespace SolaceService;

public class TopicConsumer : IDisposable
{

    private readonly ISession _session;
    private readonly EventWaitHandle _waitEventWaitHandle = new AutoResetEvent(false);
    private ITopicConsumer? _consumer = null;
    private bool _disposedValue = false;
    public TopicConsumer()
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
            Password = "kvbjfldto53tu00mjdm898agei",
            ReconnectRetries = 2,
            SSLValidateCertificate = false,
            // to increase performance
            SendBlocking = false
        };

        var context = ContextFactory.Instance.CreateContext(new ContextProperties(), null);
        var session = context.CreateSession(sessionProps, HandleMessageEvent, null);
        ReturnCode returnCode = session.Connect();
        if (returnCode == ReturnCode.SOLCLIENT_OK)
        {
            Console.Out.WriteLine("Connected to Solace Cloud"); // connected to the Solace message router
        }
        else
        {
            Console.Out.WriteLine($"Error connecting to Solace!: {returnCode}");
        }

        return session;
    }

    /// <summary>
    /// This will consume events from a Queue.
    /// This is using the Open/Close pattern for S.O.L.I.D.
    /// Queue name and how the event is consumed will be provided by the caller
    /// </summary>
    /// <param name="consumer">instance of ICustomer</param>
    public void Consume(ITopicConsumer consumer)
    {
        // Provision the queue
        _consumer = consumer;
        _session.Subscribe(ContextFactory.Instance.CreateTopic(consumer.Topic), true);

        _waitEventWaitHandle.WaitOne();
    }

    /// <summary>
    /// This event handler is invoked by Solace Systems Messaging API when a message arrives
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    private void HandleMessageEvent(object? source, MessageEventArgs args)
    {
        // Received a message

        // uncomment for debugging
        // Console.WriteLine("Received message.");
        using IMessage message = args.Message;
        // Expecting the message content as a binary attachment
        var content = Encoding.ASCII.GetString(message.BinaryAttachment);

        // uncomment for debugging
        // Console.WriteLine("Message content: {0}", content);

        // This way we can Open for extension and close for modification/
        // Calling API can provide different ways of consuming events and has control over it 
        _consumer?.Consume(content);
        // finish the program
        _waitEventWaitHandle.Set();
    }



    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _session.Dispose();
            }

            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
    }
}