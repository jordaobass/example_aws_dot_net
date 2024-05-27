using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

namespace ExampleAws.Sns;

public class NotificationService : INotificationService
{
    private readonly IAmazonSimpleNotificationService _snsService;
    private readonly AwsSnsConfiguration _awsSnsConfiguration;
    private readonly string _topicArn;

    public NotificationService(AwsSnsConfiguration awsSnsConfiguration)
    {
        var creds = new BasicAWSCredentials(_awsSnsConfiguration.accessKey, _awsSnsConfiguration.secret);
        _awsSnsConfiguration = awsSnsConfiguration;
        _snsService = new AmazonSimpleNotificationServiceClient(creds);
        _topicArn = _awsSnsConfiguration.TopicArn;
    }

    public async Task RegisterSubscirption(string topic, string email)
    {
        await _snsService.SubscribeAsync(topic, "email", email);
    }

    public async Task RegisterSubscirption(string email)
    {
        await _snsService.SubscribeAsync(_topicArn, "email", email);
    }

    public async Task SendUploadNotification(string topic, string message)
    {
        var request = new PublishRequest
        {
            Message = message,
            TopicArn = topic,
        };
        await _snsService.PublishAsync(request);
    }

    public async Task SendUploadNotification(string message)
    {
        var request = new PublishRequest
        {
            Message = message,
            TopicArn = _topicArn,
        };
        await _snsService.PublishAsync(request);
    }
}