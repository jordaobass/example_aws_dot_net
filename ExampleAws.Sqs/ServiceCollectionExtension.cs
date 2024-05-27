using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExampleAws.Sqs;

public static class ServiceCollectionExtension
{
    
    public static void AddAwsSqsResolvers(this IServiceCollection serviceCollection, IConfiguration configuration)
    {

        var awsConf = new AwsSqsConfiguration()
        {
            messageGroupId = configuration["aws:sqs:messageGroupId"],
            region = configuration["aws:sqs:region"],
            secret = configuration["aws:sqs:secret"],
            accessKey = configuration["aws:sqs:accessKey"],
            queueUrl = configuration["aws:sqs:queueUrl"],
            useFifo = bool.Parse(configuration["aws:sqs:useFifo"])
        };
        var sqsMessageProducer = new SqsMessageProducer(awsConf);
        var sqsMessageConsumer = new SqsMessageConsumer(awsConf);
        
        serviceCollection.AddTransient<ISqsMessageProducer, SqsMessageProducer>(x => sqsMessageProducer);        
        serviceCollection.AddTransient<ISqsMessageConsumer, SqsMessageConsumer>(x => sqsMessageConsumer);        
        
    }
  
    
}