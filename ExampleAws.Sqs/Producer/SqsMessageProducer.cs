using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace ExampleAws.Sqs;

public class SqsMessageProducer : ISqsMessageProducer
{
    private readonly AwsSqsConfiguration _awsSqsConfiguration;

    public SqsMessageProducer(AwsSqsConfiguration awsSqsConfiguration)
    {
        this._awsSqsConfiguration = awsSqsConfiguration;
    }

    public async Task Send(String message)
    {


        var creds = new BasicAWSCredentials(  this._awsSqsConfiguration.accessKey, this._awsSqsConfiguration.secret);

        var region = RegionEndpoint.GetBySystemName(this._awsSqsConfiguration.region);

        var sendMessageRequest = new SendMessageRequest(this._awsSqsConfiguration.queueUrl, message);

        if (this._awsSqsConfiguration.useFifo)
        {
            sendMessageRequest.MessageGroupId = this._awsSqsConfiguration.messageGroupId;
        }

        var sqsClient = new AmazonSQSClient(creds, region);

        await sqsClient.SendMessageAsync(sendMessageRequest);

    }

}