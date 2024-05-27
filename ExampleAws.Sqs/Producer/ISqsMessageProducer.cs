namespace ExampleAws.Sqs;

public interface ISqsMessageProducer
{
    Task Send(String message);
}