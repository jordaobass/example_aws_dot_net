using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExampleAws.Sns;

public static class ServiceCollectionExtension
{
    
    public static void AddAwsSqsResolvers(this IServiceCollection serviceCollection, IConfiguration configuration)
    {

        var awsConf = new AwsSnsConfiguration()
        {
            messageGroupId = configuration["aws:sqs:messageGroupId"],
            region = configuration["aws:sqs:region"],
            secret = configuration["aws:sqs:secret"],
            accessKey = configuration["aws:sqs:accessKey"],
            queueUrl = configuration["aws:sqs:queueUrl"],
            useFifo = bool.Parse(configuration["aws:sqs:snsTopicArn"])
        };
        var sqsMessageProducer = new NotificationService(awsConf);
    
        serviceCollection.AddTransient<INotificationService, NotificationService>(x => sqsMessageProducer);        
         
        
    }
  
    
}