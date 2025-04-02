using System.Text;
using IoTShared;
using IoTShared.Devices;
using SolaceSystems.Solclient.Messaging;

namespace SolaceService;

public class QueueConsumer
{
    private readonly ISession _session;
    private IQueue? _queue;
    private IFlow? _flow;
    private readonly EventWaitHandle _waitEventWaitHandle = new AutoResetEvent(false);
    private IConsumer? _consumer = null;
    public QueueConsumer()
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
        var session = context.CreateSession(sessionProps, null, null);
        ReturnCode returnCode = session.Connect();
        if (returnCode == ReturnCode.SOLCLIENT_OK)
        {
            Console.Out.WriteLine("Connected to Solace Cloud"); // connected to the Solace message router
            
        }
        return session;
    }

    public void Consume(IConsumer consumer)
    {
        // Provision the queue
        string queueName = consumer.Queue;
        _consumer = consumer;
        Console.WriteLine("Attempting to provision the queue '{0}'...", queueName);

        // Set queue permissions to "consume" and access-type to "exclusive"
        EndpointProperties endpointProps = new EndpointProperties()
        {
            Permission = EndpointProperties.EndpointPermission.Consume,
            AccessType = EndpointProperties.EndpointAccessType.Exclusive
        };
        // Create the queue
        _queue = ContextFactory.Instance.CreateQueue(queueName);

        // Provision it, and do not fail if it already exists
        _session.Provision(_queue, endpointProps,
            ProvisionFlag.IgnoreErrorIfEndpointAlreadyExists | ProvisionFlag.WaitForConfirm, null);
        Console.WriteLine("Queue '{0}' has been created and provisioned.", queueName);

        // Create and start flow to the newly provisioned queue
        // NOTICE HandleMessageEvent as the message event handler 
        // and HandleFlowEvent as the flow event handler
        _flow = _session.CreateFlow(new FlowProperties()
            {
                AckMode = MessageAckMode.ClientAck
            },
            _queue, null, HandleMessageEvent, HandleFlowEvent);
        _flow.Start();
        Console.WriteLine("Waiting for a message in the queue '{0}'...", queueName);

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
        Console.WriteLine("Received message.");
        using IMessage message = args.Message;
        // Expecting the message content as a binary attachment
        var content = Encoding.ASCII.GetString(message.BinaryAttachment);
        Console.WriteLine("Message content: {0}", content);
            // TODO: Figure out the consumer
            _consumer?.Consume(content);
        // ACK the message
        _flow?.Ack(message.ADMessageId);
        // finish the program
        _waitEventWaitHandle.Set();
    }

    private void HandleFlowEvent(object? sender, FlowEventArgs args)
    {
        // Received a flow event
        Console.WriteLine("Received Flow Event '{0}' Type: '{1}' Text: '{2}'",
            args.Event,
            args.ResponseCode.ToString(),
            args.Info);
    }

}
