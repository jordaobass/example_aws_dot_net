using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace ExampleAws.Sqs;

public class SqsMessageConsumer : ISqsMessageConsumer
{
    private AmazonSQSClient _sqsClient;
    private bool _isPolling;
    private int _delay;
    private string _queueUrl;
    private string _awsregion;
    private int _maxNumberOfMessages;
    private int _messageWaitTimeSeconds;
    private string _accessKey;
    private string _secret;
    private CancellationTokenSource _source;
    private CancellationToken _token;

    public SqsMessageConsumer(AwsSqsConfiguration awsSqsConfiguration)
    {
        _accessKey = awsSqsConfiguration.accessKey;
        _secret = awsSqsConfiguration.secret;
        _queueUrl = awsSqsConfiguration.queueUrl;
        _awsregion = awsSqsConfiguration.region;
        _messageWaitTimeSeconds = 20;
        _maxNumberOfMessages = 10;
        _delay = 0;

        var basicCredentials = new BasicAWSCredentials(_accessKey, _secret);
        var region = RegionEndpoint.GetBySystemName(_awsregion);

        _sqsClient = new AmazonSQSClient(basicCredentials, region);
        Console.CancelKeyPress += new ConsoleCancelEventHandler(CancelKeyPressHandler);
    }

    public async Task Listen()
    {
        _isPolling = true;

        int i = 0;
        try
        {
            _source = new CancellationTokenSource();
            _token = _source.Token;

            while (_isPolling)
            {
                i++;
                Console.Write(i + ": ");
                await FetchFromQueue();
                Thread.Sleep(_delay);
            }
        }
        catch (TaskCanceledException ex)
        {
            Console.WriteLine("Application Terminated: " + ex.Message);
        }
        finally
        {
            _source.Dispose();
        }
    }

    private async Task FetchFromQueue()
    {
        var receiveMessageRequest = new ReceiveMessageRequest();
        receiveMessageRequest.QueueUrl = _queueUrl;
        receiveMessageRequest.MaxNumberOfMessages = _maxNumberOfMessages;
        receiveMessageRequest.WaitTimeSeconds = _messageWaitTimeSeconds;
        var receiveMessageResponse =
            await _sqsClient.ReceiveMessageAsync(receiveMessageRequest, _token);

        if (receiveMessageResponse.Messages.Count != 0)
        {
            for (int i = 0; i < receiveMessageResponse.Messages.Count; i++)
            {
                string messageBody = receiveMessageResponse.Messages[i].Body;

                Console.WriteLine("Message Received: " + messageBody);

                await DeleteMessageAsync(receiveMessageResponse.Messages[i].ReceiptHandle);
            }
        }
        else
        {
            Console.WriteLine("No Messages to process");
        }
    }

    private async Task DeleteMessageAsync(string recieptHandle)
    {
        var deleteMessageRequest = new DeleteMessageRequest();
        deleteMessageRequest.QueueUrl = _queueUrl;
        deleteMessageRequest.ReceiptHandle = recieptHandle;

        var response = await _sqsClient.DeleteMessageAsync(deleteMessageRequest);
    }

    private void CancelKeyPressHandler(object sender, ConsoleCancelEventArgs args)
    {
        args.Cancel = true;
        _source.Cancel();
        _isPolling = false;
    }
}